using System;
using Grid;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 3;
    [SerializeField] private int depth = 10;
    
    private int xOff, zOff;
    
    [SerializeField] private float tileSize = 1f;
    
    private GameObject[,,] tiles;

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject floorTile;
    [SerializeField] private Transform nodes;

    private void Start()
    {
        floor.transform.localScale = new Vector3(width * tileSize, 0.01f, depth * tileSize);

        xOff = width / 2;
        zOff = depth / 2;

        tiles = new GameObject[width, height, depth];
        
        ResetNodes();
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector3[,,] points = new Vector3[width, height, depth];
        
        for (int i = 0, y = 0; y < height; y++)
        {
            for (int z = -depth / 2; z < depth / 2; z++) 
            {
                for (int x = -width / 2; x < width / 2; x++, i++) 
                {
                    Vector3 offset = transform.position;
        
                    int xIndex = x + xOff;
                    int zIndex = z + zOff;
                    
                    points[xIndex, y, zIndex] = new Vector3(
                        x * tileSize + tileSize / 2f + offset.x, 
                        y * tileSize + offset.y, 
                        z * tileSize + tileSize / 2f + offset.z);
                    
                    if (y == 0)
                    {
                        tiles[xIndex, y, zIndex] = Instantiate(floorTile, points[xIndex, y, zIndex], Quaternion.identity, nodes);
                    }
                }
            }
        }
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        float xCount = Mathf.Floor(position.x / tileSize);
        float yCount = Mathf.Floor(position.y / tileSize);
        float zCount = Mathf.Floor(position.z / tileSize);

        Vector3 result = new Vector3(
            xCount * tileSize + tileSize / 2f,
            yCount * tileSize + tileSize / 2f,
            zCount * tileSize + tileSize / 2f
        );

        result += transform.position;

        return result;
    }

    public GameObject GetNeighbourTileOnGrid(Vector3 origin, CardinalDirection direction)
    {
        GameObject tile = null;

        int x = 0;
        int z = 0;
        
        if(direction == CardinalDirection.East ||
           direction == CardinalDirection.NorthEast ||
           direction == CardinalDirection.SouthEast)
        {
            x = 1;
        }
        else if(direction == CardinalDirection.West ||
           direction == CardinalDirection.NorthWest ||
           direction == CardinalDirection.SouthWest)
        {
            x = -1;
        }
        
        if(direction == CardinalDirection.North ||
           direction == CardinalDirection.NorthEast ||
           direction == CardinalDirection.NorthWest)
        {
            z = 1;
        }
        else if(direction == CardinalDirection.South ||
                direction == CardinalDirection.SouthEast ||
                direction == CardinalDirection.SouthWest)
        {
            z = -1;
        }

        Vector3 tilePosition = origin + new Vector3(x * tileSize, 0, z * tileSize);
        if (IsOnGrid(tilePosition))
        {
            Vector3Int indices = PositionToIndicies(tilePosition);
            tile = tiles[indices.x, indices.y, indices.z];
        }
        
        return tile;
    }

    public GameObject GetNextNeighbourTileOnGrid(Vector3 origin)
    {
        for (int i = 0; i < (int) CardinalDirection.Length; i++)
        {
            GameObject tile = GetNeighbourTileOnGrid(origin, (CardinalDirection) i);
            if (tile != null)
            {
                return tile;
            }
        }
        
        return null;
    }
    
    public bool IsOnGrid(Vector3 position)
    {
        Vector3Int indices = PositionToIndicies(position);

        try
        {
            GameObject tile = tiles[indices.x, indices.y, indices.z];
            return tile != null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void ResetNodes()
    {
        for (int i = 0; i < nodes.childCount; i++)
        {
            Destroy(nodes.GetChild(i).gameObject);
        }
    }

    private Vector3Int PositionToIndicies(Vector3 position)
    {
        position -= transform.position;
    
        int xCount = Mathf.FloorToInt(position.x / tileSize) + xOff;
        int yCount = Mathf.FloorToInt(position.y / tileSize);
        int zCount = Mathf.FloorToInt(position.z / tileSize) + zOff;

        return new Vector3Int(xCount, yCount, zCount);
    }

    private void OnDrawGizmos ()
    {
        if (tiles == null || tiles.Length <= 0)
            return;
        
        Gizmos.color = Color.black;
        foreach (GameObject tile in tiles)
        {
            if (tile != null)
                Gizmos.DrawSphere(tile.transform.position, 0.1f);
        }
    }
}
