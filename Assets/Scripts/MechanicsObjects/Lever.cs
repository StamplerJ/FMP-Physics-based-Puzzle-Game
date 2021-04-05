using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class Lever : MonoBehaviour, ICounterListener
{
    [SerializeField] private CounterUI counterUI;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private List<GameObject> walls;

    private GridGenerator grid;
    private GameObject mesh;

    private bool isOn;
    
    private void Awake()
    {
        grid = GameObject.Find(Names.Grid).GetComponent<GridGenerator>(); // TODO: Can this be a singleton?
        mesh = transform.Find(Names.Mesh).gameObject;
        counterUI.Listener = this;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            print("Trigger walls");
            TriggerWalls();
        }
    }

    private void TriggerWalls()
    {
        isOn = !isOn;

        foreach (GameObject wall in walls)
        {
            wall.SetActive(isOn);
        }
    }

    private void AddWall()
    {
        GameObject tile = grid.GetTileOnGrid(mesh.transform.position, CardinalDirection.SouthEast);

        if (tile != null)
        {
            GameObject wall = Instantiate(wallPrefab, tile.transform.position, Quaternion.identity);
            walls.Add(wall);
        }
    }

    private void RemoveWall()
    {
        if (walls.Count > 0)
        {
            Destroy(walls[walls.Count - 1]);
            walls.RemoveAt(walls.Count - 1);
        }
    }
    
    public void OnPlusClick()
    {
        AddWall();
    }

    public void OnMinusClick()
    {
        RemoveWall();
    }
    
    public bool IsOn
    {
        get => isOn;
    }
}
