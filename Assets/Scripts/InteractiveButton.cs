using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Transform _camera;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _camera = FindObjectOfType<Camera>().GetComponent<Transform>();
        Deactivate();
    }

    private void Update()
    {
        transform.LookAt(_camera);
    }

    [ContextMenu("Activate")] 
    public void Activate()
    {
        StartCoroutine(SetVisible(0.2f, 1));
    }
    
    public void Deactivate()
    {
        StartCoroutine(SetVisible(1, 0.2f));
    }
    
    private IEnumerator SetVisible(float a, float b)
    {
        var colorr = _renderer.color;
        for (float t = 0f; t < 1f; t += (Time.deltaTime / 0.25f))
        {
            colorr.a = Mathf.Lerp(a, b, t);
            _renderer.color = colorr;
            yield return null;
        }
        colorr.a = b;
        _renderer.color = colorr;
    }

    public void Action()
    {
        return;
    }
}
