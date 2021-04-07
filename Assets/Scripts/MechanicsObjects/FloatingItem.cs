using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] private float height = 0.5f;
    [SerializeField] private float rotationSpeed = 0.5f;

    private float startY, endY;
    private int deltaDirection = 1;
    private float deltaMovement = 0f;

    private void Start()
    {
        startY = transform.position.y + 0.5f;
        endY = startY + height;
    }

    void Update()
    {
        deltaMovement += Time.deltaTime * deltaDirection;

        float newY = Mathf.Lerp(startY, endY, deltaMovement); // TODO: Implement smooth tweening
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (deltaMovement >= 1f || deltaMovement <= 0f)
        {
            deltaDirection = -deltaDirection;
            deltaMovement = deltaDirection < 0 ? 1f : 0f;
        }
        
        transform.Rotate(0f, rotationSpeed, 0f, Space.World);
    }
}
