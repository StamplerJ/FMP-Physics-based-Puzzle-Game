using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, 90f, 0f));
    }
}
