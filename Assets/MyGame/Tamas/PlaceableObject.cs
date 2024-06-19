using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool placed { get; private set; }
    public Vector3Int size { get; private set; }
    Vector3[] vertices; 

    void GetColliderVerticesPositionsLocal()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        vertices = new Vector3[4];
        vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) *0.5f;
        vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }

    void CallculateSizeInCells()
    {
        Vector3Int[] Vertices = new Vector3Int[vertices.Length];

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            Vertices[i] = BuildingSystem.current.gridLayout.WorldToCell(worldPos);
        }

        size = new Vector3Int(Math.Abs((Vertices[0] - Vertices[1]).x), Math.Abs((Vertices[0] - Vertices[3]).y), 1);
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(vertices[0]);
    }

    void Start()
    {
        GetColliderVerticesPositionsLocal();
        CallculateSizeInCells();
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        size = new Vector3Int(size.y, size.x, 1);

        Vector3[] Vertices = new Vector3[vertices.Length];
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i] = vertices[(i+1)%vertices.Length];
        }
        vertices = Vertices; 
    }

    public virtual void Place()
    {
        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(drag);

        placed = true; 
    }
}
