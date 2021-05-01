/// <summary>
/// This class is just there so that the ramp can be saved
/// </summary>
public class Wall : MechanicBehaviour
{
    private void Awake()
    {
        type = MechanicObjectType.Wall;
    }

    public override void OnLoad(SerializedMechanicObject smo)
    {
        //noop
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