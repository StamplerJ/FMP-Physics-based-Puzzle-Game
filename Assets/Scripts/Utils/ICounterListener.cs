/// <summary>
/// This is used to hookup a class to the CounterUI gameobject
/// </summary>
public interface ICounterListener
{
    void OnPlusClick();
    void OnMinusClick();
}