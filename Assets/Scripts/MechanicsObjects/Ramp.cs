using System;

/// <summary>
/// This class is just there so that the ramp can be saved
/// </summary>
public class Ramp : MechanicBehaviour
{
    private void Awake()
    {
        type = MechanicObjectType.Ramp;
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