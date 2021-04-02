using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<GameObject> walls;

    private bool isOn;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            print("Trigger walls");
            TriggerWalls();
        }
    }

    private void TriggerWalls()
    {
        isOn = !isOn;

        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }
    }

    public bool IsOn
    {
        get => isOn;
    }
}
