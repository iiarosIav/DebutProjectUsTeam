using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedObject : MonoBehaviour
{
    [SerializeField] protected Transform _openedPosition;
    [SerializeField] protected GameObject _mainGame;
    [SerializeField] protected GameObject _miniGame;
    
    public virtual void Action()
    {
        // проигрываем анимации
        // совершаем действия
        if (_miniGame != null)
        {
            _miniGame.SetActive(true);
            _miniGame = null;
            GameManager.Instance.LinObject = this;
            _mainGame.SetActive(false);
            return;
        }
    }
}
