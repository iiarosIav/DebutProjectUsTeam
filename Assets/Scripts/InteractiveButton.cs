using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    int a = -1;


    private void Start()
    {
        Activate();
    }

    [ContextMenu("Activate")] 
    public void Activate()
    {
        StartCoroutine(SetVisible(a));
        
    }
    public IEnumerator SetVisible(int vis)
    {
        for (float t = 0f; t < 0.25f; t += Time.deltaTime)
        {
            var colorr = _renderer.color;
            colorr.a += vis * Time.deltaTime * 4f;
            colorr.a = Mathf.Clamp(colorr.a, 0.2f, 1);
            _renderer.color = colorr;
            yield return null;
        }
        a *= -1;
    }

    public void Action()
    {
        return;
    }
}
