using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private InteractiveButton _button;
    
    public void Activate()
    {
        _button.Activate();
    }
    
    public void Deactivate()
    {
        _button.Deactivate();
    }

    public void Kinematic()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void UnKinematic()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
