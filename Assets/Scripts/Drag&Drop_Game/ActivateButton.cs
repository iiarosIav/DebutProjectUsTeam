using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private int _isActivate = -1;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _cube.transform.position) < 0.8f)
        {
            // StartCoroutine(GoToPos(0.2f, 0f));
            if (transform.localPosition.y >= 0.2f)
            {
                transform.position -= new Vector3(0, 0.2f, 0);
                _isActivate *= -1;
            }
        }
        else if (_isActivate == 1 && Vector3.Distance(transform.position, _cube.transform.position) >= 0.8f)
        {
            // StartCoroutine(GoToPos(0f, 0.2f));
            if (transform.localPosition.y <= 0f)
            {
                transform.position += new Vector3(0, 0.2f, 0);
                _isActivate *= -1;
            }
        }
    }

    private IEnumerator GoToPos(float firstPos, float secondPos)
    {
        for (float t = 0; t < 1f; t += (Time.deltaTime / 0.3f))
        {
            transform.localPosition = new Vector3(transform.position.x, Mathf.Lerp(firstPos, secondPos, t),
                transform.localPosition.z);
            yield return null;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, secondPos, transform.localPosition.z);
        _isActivate *= -1;
    }
}
