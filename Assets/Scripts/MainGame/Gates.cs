using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : LinkedObject
{
    private float _posY;

    public override void Action()
    {
        base.Action();
        _posY = transform.position.y;
        StartCoroutine(Opening());
    }

    private IEnumerator Opening()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(_posY, _openedPosition.position.y, t),
                transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, _openedPosition.position.y, transform.position.z);
    }
}