using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGates : LinkedObject
{
    private float _rotY;
    [SerializeField] private DoorGates _secondDoorGates;

    public override void Action()
    {
        if (_miniGame != null)
        {
            _miniGame.SetActive(true);
            _miniGame = null;
            GameManager.Instance.LinObject = this;
            _mainGame.SetActive(false);
            return;
        }
        if (_secondDoorGates != null)
        {
            _secondDoorGates.Action();
        }
        _rotY = transform.localEulerAngles.y;
        StartCoroutine(Opening());
    }

    private IEnumerator Opening()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.Lerp(_rotY, _openedPosition.localEulerAngles.y, t),
                transform.localEulerAngles.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, _openedPosition.position.y, transform.position.z);
    }
}
