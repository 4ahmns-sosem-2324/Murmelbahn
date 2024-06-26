using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTileMap;
    [SerializeField] private TileBase whiteTile;

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;

    private PlaceableObject objectToPlade;

    [SerializeField] InputActionAsset inputs;
    InputActionMap map;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
        
        map = inputs.FindActionMap("Debug");
        map.FindAction("Obj1").performed += InitializeWithObject;
        map.FindAction("Obj2").performed += InitializeWithObject;
        map.FindAction("Obj3").performed += InitializeWithObject;
        map.FindAction("Obj4").performed += InitializeWithObject;

        map.FindAction("Obj2").performed += Test;
        //map.FindAction("Return").performed += objectToPlade.Rotate;
        
    }

    private void Update()
    {
        
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("aaaaaa");
            InitializeWithObject(prefab1);
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            InitializeWithObject(prefab2);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            InitializeWithObject(prefab3);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            InitializeWithObject(prefab4);
        }
        */
        if (!objectToPlade)
        {
            return; 
        }
        
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            objectToPlade.Rotate();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanBePlaced(objectToPlade))
            {
                objectToPlade.Place();
                Vector3Int start = gridLayout.WorldToCell(objectToPlade.GetStartPosition());
                TakeArea(start, objectToPlade.size); 
            }
            else
            {
                Destroy(objectToPlade.gameObject);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(objectToPlade.gameObject); 
        }*/
    }


    public void Test(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("AAAAAAAA");
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point; 
        }
        else
        {
            return Vector3.zero; 
        }
    }

    public Vector3 SnapCoordinatesToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position; 
    }
    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0; 

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++; 
        }
        return array; 
    }

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinatesToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlade = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();

        Debug.Log("aaaaaaaaah");
    }

    public void InitializeWithObject(InputAction.CallbackContext callbackContext)
    {
        Vector3 position = SnapCoordinatesToGrid(Vector3.zero);

        GameObject prefab = null;

        if (callbackContext.action == map.FindAction("Obj1"))
        {
            prefab = prefab1;
        }
        else if (callbackContext.action.name == "Obj2")
        {
            prefab = prefab2;
        }
        else if (callbackContext.action == map.FindAction("Obj3"))
        {
            prefab = prefab3;
        }
        else if (callbackContext.action == map.FindAction("Obj4"))
        {
            prefab = prefab4;
        }
        else
        {
            Debug.LogWarning("No Valid Input detected");
        }

        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab, position, Quaternion.identity);
            objectToPlade = obj.GetComponent<PlaceableObject>();
            obj.AddComponent<ObjectDrag>();
        }

    }
    
    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlade.GetStartPosition());
        area.size = placeableObject.size;

        TileBase[] baseArray = GetTilesBlock(area, mainTileMap);

        foreach (var b in baseArray)
        {
            if (b == whiteTile)
            {
                return false;  
            }
        }

        return true; 
    }

    public void TakeArea(Vector3Int start, Vector3Int size) 
    {
        mainTileMap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y); 
    }

    private void OnDestroy()
    {
        map.FindAction("Obj1").performed -= InitializeWithObject;
        map.FindAction("Obj2").performed -= InitializeWithObject;
        map.FindAction("Obj3").performed -= InitializeWithObject;
        map.FindAction("Obj4").performed -= InitializeWithObject;

        //map.FindAction("Return").performed -= objectToPlade.Rotate;
    }
}
