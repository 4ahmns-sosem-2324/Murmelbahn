using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

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

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
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

        if (!objectToPlade)
        {
            return; 
        }

        if (Input.GetKeyDown(KeyCode.Return))
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
        }
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
}
