using UnityEngine;

public class RocketEngine : MechanicBehaviour
{
    public static readonly float MaxFuel = 10f;

    [SerializeField] private float speed = 50f;
    [SerializeField] private GameObject engine;
    [SerializeField] private GameObject front;
    [SerializeField] private float fuelConsumption = 2f;

    private new Rigidbody rigidbody;
    
    private float fuel;
    private float deltaPos;

    private int stoppedTicks;
    private bool isStopped;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if(LevelTracker.Instance.IsLevelFinished)
            return;
        
        if (Input.GetKey(KeyCode.Space))
        {
            Accelerate();
        }
        
        UpdateVelocityStatus();
        
        // Show defeat when the rocket can't move anymore
        if (fuel <= 0f && isStopped)
        {
            MenuDefeat.Instance.ShowMenu();
        }

        // Freeze rotation manually
        transform.eulerAngles = new Vector3(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public void Accelerate()
    {
        if (fuel > 0)
        {
            print(GetDirection());
            rigidbody.AddForce(GetDirection() * speed);
            fuel -= fuelConsumption * Time.deltaTime;
        }
        else
        {
            fuel = 0f;
        }
    }

    private void UpdateVelocityStatus()
    {
        if (rigidbody.velocity.magnitude <= 0.1f)
        {
            stoppedTicks++;
        }
        else
        {
            stoppedTicks = 0;
        }
        
        isStopped = stoppedTicks > 5;
    }
    
    public Vector3 GetDirection()
    {
        return (front.transform.position - engine.transform.position).normalized;
    }

    public override void OnLoad(SerializedMechanicObject smo)
    {
        //noop
    }

    public override void OnEnterEditor()
    {
        if (FuelBar.Instance != null)
        {
            FuelBar.Instance.Hide();
        }
    }

    public override void OnEnterPlayMode()
    {
        fuel = MaxFuel;
        
        if (FuelBar.Instance != null)
        {
            FuelBar.Instance.Setup(this);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(front.transform.position, front.transform.position + GetDirection());
    }

    public float Fuel
    {
        get => fuel;
        set => fuel = value;
    }
}
