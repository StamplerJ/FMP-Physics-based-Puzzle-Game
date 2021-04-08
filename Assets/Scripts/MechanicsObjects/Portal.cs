using Grid;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool createOtherPortal;

    private GridGenerator grid;
    private GameObject mesh;
    
    private Portal otherPortal;
    private GlowingTextureRotation glowingTextureRotation;

    private bool canTeleport = true;

    private void Awake()
    {
        grid = GameObject.Find(Names.Grid).GetComponent<GridGenerator>(); // TODO: Can this be a singleton?
        mesh = transform.Find(Names.Mesh).gameObject;
        
        glowingTextureRotation = GetComponent<GlowingTextureRotation>();
    }

    private void Start()
    {
        if (createOtherPortal)
        {
            SetupPortals();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            if (canTeleport)
            {
                otherPortal.CanTeleport = false;
                
                // Change position, but keep y-value
                Transform rocket = other.transform;
                
                Vector3 newPosition = otherPortal.transform.position;
                newPosition.y = rocket.position.y;
                
                rocket.position = newPosition;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            canTeleport = true;
        }
    }
    
    private void SetupPortals()
    {
        createOtherPortal = false;
        
        glowingTextureRotation.GlowColor = Colors.Red;

        otherPortal = CreatePortal();
        otherPortal.OtherPortal = this;
        otherPortal.GetComponent<GlowingTextureRotation>().GlowColor = Colors.Blue;
    }
    
    private Portal CreatePortal()
    {
        for (int i = 0; i < (int) CardinalDirection.Length; i++)
        {
            GameObject tile = grid.GetNeighbourTileOnGrid(mesh.transform.position, (CardinalDirection) i);
            if (tile != null) // Check if tile is on grid
            {
                Vector3 position = tile.transform.position;
                position.y = transform.position.y;
                
                GameObject portal = Instantiate(gameObject, position, Quaternion.identity);

                // This fixes multiple outline materials
                Material temp = portal.transform.Find(Names.Mesh).GetComponent<MeshRenderer>().materials[0];
                portal.transform.Find(Names.Mesh).GetComponent<MeshRenderer>().materials = new []{temp};
                
                SelectedItemsTracker.Instance.AddSelectable(portal.GetComponent<Selectable>());
                
                return portal.GetComponent<Portal>();
            }
        }

        return null;
    }
    
    public Portal OtherPortal
    {
        get => otherPortal;
        set => otherPortal = value;
    }

    public bool CreateOtherPortal
    {
        get => createOtherPortal;
        set => createOtherPortal = value;
    }

    public bool CanTeleport
    {
        get => canTeleport;
        set => canTeleport = value;
    }
}
