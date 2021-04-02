using UnityEngine;

[ExecuteInEditMode]
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 3;
    [SerializeField] private int depth = 10;

    [SerializeField] private float tileSize = 1f;
    
    private Vector3[] points;

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject floorTile;
    [SerializeField] private Transform nodes;

    private void Start()
    {
        floor.transform.localScale = new Vector3(width * tileSize, 0.01f, depth * tileSize);
        
        GenerateGrid();
    }

    private void GenerateGrid()
    {        
        points = new Vector3[width * height * depth];

        for (int i = 0, y = 0; y < height; y++)
        {
            for (int z = -depth / 2; z < depth / 2; z++) 
            {
                for (int x = -width / 2; x < width / 2; x++, i++) 
                {
                    Vector3 offset = transform.position;
                    
                    points[i] = new Vector3(
                        x * tileSize + tileSize / 2f + offset.x, 
                        y * tileSize + offset.y, 
                        z * tileSize + tileSize / 2f + offset.z);
                    
                    if (y == 0)
                    {
                        Instantiate(floorTile, points[i], Quaternion.identity, nodes);   
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

    private void OnDrawGizmos ()
    {
        if (points == null || points.Length <= 0)
            return;
        
        Gizmos.color = Color.black;
        foreach (Vector3 point in points)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
