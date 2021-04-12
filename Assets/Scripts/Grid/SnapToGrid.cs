using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    [SerializeField] private bool isMovable = true;
    [SerializeField] private float velocity = 50f;
    [SerializeField] private Vector3 offset;

    private GridGenerator grid;

    private Vector3 snapPosition;
    private float traveledTime;

    private bool isSelected = false;

    private void Awake()
    {
        grid = GameObject.Find(Names.Grid).GetComponent<GridGenerator>();

        snapPosition = transform.position;
    }

    private void Update()
    {
        if (!isMovable)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.Equals(this.gameObject))
                {
                    isSelected = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
            transform.position = snapPosition;
        }

        if (isSelected)
        {
            UpdatePosition();
        }
    }

    void LateUpdate()
    {
        if (isSelected && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag.Equals(Tags.Stackable))
                {
                    SnapToPosition(hit.point, hit.collider.gameObject);   
                }
            }
        }
    }

    private void SnapToPosition(Vector3 position, GameObject hitObject)
    {
        Vector3 targetPos = grid.GetNearestPointOnGrid(position);
        // targetPos += new Vector3(0f, transform.localScale.y / 2f, 0f);
        snapPosition = targetPos + offset;

        if (!hitObject.name.Equals(Names.Floor) && hitObject.gameObject != this.gameObject)
        {
            snapPosition += new Vector3(0f, hitObject.transform.localScale.y, 0f);
        }
        
        traveledTime = 0f;
    }

    private void UpdatePosition()
    {
        traveledTime += Time.deltaTime * velocity;
        transform.position = Vector3.Lerp(transform.position, snapPosition, traveledTime);
    }

    public Vector3 GetCenterPosition()
    {
        return transform.position - offset;
    }

    public bool IsSelected
    {
        get => isSelected;
        set => isSelected = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GetCenterPosition(), 0.5f);
    }
}
