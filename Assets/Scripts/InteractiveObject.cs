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
}
