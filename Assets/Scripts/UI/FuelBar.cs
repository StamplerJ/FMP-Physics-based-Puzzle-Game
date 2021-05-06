using UnityEngine;
using UnityEngine.UI;

public class FuelBar : Singleton<FuelBar>
{
    [SerializeField] private GameObject holder;

    private Slider slider;
    private RocketEngine rocketEngine;

    private bool isPressedDown;

    public override void Awake()
    {
        base.Awake();
        slider = holder.GetComponentInChildren<Slider>();
    }

    public void Setup(RocketEngine rocketEngine)
    {
        this.rocketEngine = rocketEngine;
        slider.value = RocketEngine.MaxFuel;
        holder.SetActive(true);
    }

    public void Hide()
    {
        holder.SetActive(false);
    }

    public void OnPointerDown()
    {
        isPressedDown = true;
    }

    public void OnPointerUp()
    {
        isPressedDown = false;
    }
    
    private void FixedUpdate()
    {
        if (rocketEngine != null)
        {
            slider.value = rocketEngine.Fuel;

            if (isPressedDown && !Input.GetKey(KeyCode.Space))
            {
                rocketEngine.Accelerate();
            }
        }
    }
}
