using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour, IComparable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CompareTo(object o)
    {
        if(o is Location location) return name.CompareTo(location.gameObject.name);
        else throw new ArgumentException("Некорректное значение параметра");
    }
}
