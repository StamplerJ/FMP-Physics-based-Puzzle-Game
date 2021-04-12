using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MechanicalObjectName", menuName = "ScriptableObjects/ListItem", order = 1)]
public class ListItemScriptableObject : ScriptableObject
{
    public MechanicObjectType type;
    public Sprite image;
    public GameObject prefab;
}
