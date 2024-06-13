using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        transform.Rotate(Speed * Time.deltaTime, 0, 0);
    }
}
