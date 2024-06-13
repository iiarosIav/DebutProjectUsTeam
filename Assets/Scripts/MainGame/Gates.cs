using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : LinkedObject
{
    [SerializeField] private Transform _openedPosition;
    [SerializeField] private GameObject _mainGame;
    [SerializeField] private GameObject _miniGame;

    private float _posY;

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