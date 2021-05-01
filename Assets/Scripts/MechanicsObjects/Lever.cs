using System.Collections.Generic;
using System.Linq;
using Grid;
using UnityEngine;

public class Lever : MechanicBehaviour, ICounterListener
{
    [SerializeField] private CounterUI counterUI;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private List<GameObject> walls;

    private GridGenerator grid;
    private GameObject mesh;

    private bool isOn;

    private void Awake()
    {
        type = MechanicObjectType.Lever;
        
        grid = GameObject.Find(Names.Grid).GetComponent<GridGenerator>(); // TODO: Can this be a singleton?
        mesh = transform.Find(Names.Mesh).gameObject;
        counterUI.Listener = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
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

    /// <summary>
    /// Adds a wall around the lever
    /// </summary>
    /// <returns>True if adding a wall was successful else false</returns>
    private bool AddWall()
    {
        for (int i = 0; i < (int) CardinalDirection.Length; i++)
        {
            GameObject tile = grid.GetNeighbourTileOnGrid(mesh.transform.position, (CardinalDirection) i);
            if (tile != null) // Check if tile is on grid
            {
                if (!IsCollidingWithWall(tile))
                {
                    GameObject wall = Instantiate(wallPrefab, tile.transform.position, Quaternion.identity);
                    SelectedItemsTracker.Instance.AddSelectable(wall.GetComponent<Selectable>(), false);
                    walls.Add(wall);
                    return true;
                }
            }
        }

        return false;
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
        bool wasSuccessful = AddWall();

        if (!wasSuccessful)
        {
            counterUI.Counter--;
        }
    }

    public void OnMinusClick()
    {
        RemoveWall();
    }

    private bool IsCollidingWithWall(GameObject tile)
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask(Names.Wall);
        
        if (Physics.Raycast(tile.transform.position + Vector3.down, Vector3.up, out hit, 2f, mask))
        {
            if (hit.collider.name.Contains(Names.Wall))
            {
                return true;
            }
        }

        return false;
    }

    public override void OnLoad(SerializedMechanicObject smo)
    {
        // First portal
        if (smo.childIds != null && smo.childIds.Length > 0)
        {
            Wall[] walls = FindObjectsOfType<Wall>();
            foreach (Wall wall in walls)
            {
                if (smo.childIds.Contains(wall.ID))
                {
                    this.walls.Add(wall.gameObject);
                }
            }
        }

        counterUI.Counter = walls.Count;
        counterUI.UpdateAmount();
    }

    public override SerializedMechanicObject GetSerializedMechanicObject()
    {
        SerializedMechanicObject smo = base.GetSerializedMechanicObject();

        smo.childIds = walls.Select(go => go.GetComponent<MechanicBehaviour>().ID).ToArray();

        return smo;
    }

    public bool IsOn
    {
        get => isOn;
    }

    public override void OnEnterEditor()
    {
        //noop
    }

    public override void OnEnterPlayMode()
    {
        //noop
    }
}