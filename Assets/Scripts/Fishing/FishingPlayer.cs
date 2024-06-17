using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FishingPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;
    [SerializeField] private Transform _fishingRodModel;
    [SerializeField] private Transform _fishEndPosition;
    [SerializeField] private Transform _fishingLine;
    [SerializeField] private Transform _fishSpawn;
    [SerializeField] private Fish _fishPrefab;
    [SerializeField] private Fish _fish;
    private int _fishingVector;
    [SerializeField] private float _fishingProgress;
    private float _fishingTarget;
    [SerializeField] private float _fishingSpeed = 1f;


    [SerializeField] private float _startRot = 1f;
    [SerializeField] private float _endRot = 1f;
    
    [SerializeField] private int _winCount = 10;
    [SerializeField] private TMP_Text _winCountText;

    [SerializeField] private Slider _slider;

    private Coroutine _fishigCoroutine;
    
    [SerializeField] private GameObject _mainGame;
    [SerializeField] private GameObject _miniGame;
    
    [SerializeField] private GameObject _winWindow;

    private void Start()
    {
        _winCountText.text = Convert.ToString(_winCount);
        _slider.value = 0;
        _slider.transform.LookAt(FindObjectOfType<Camera>().transform.position);
    }

    private void Update()
    {
        if (_fish == null)
        {
            _fishingLine.localScale = new Vector3(_fishingLine.localScale.x, _fishingLine.localScale.y, 0.05f);
        }

        if (_fishigCoroutine == null && Input.GetKeyDown(KeyCode.E))
        {
            _fishigCoroutine = StartCoroutine(StartingProcess(_startRot, _endRot));
        }
    }

    private IEnumerator StartingProcess(float startRot, float endRot)
    {
        for (float t = 0; t < 1f; t += (Time.deltaTime / 0.25f))
        {
            _fishingRodModel.localEulerAngles = new Vector3(_fishingRodModel.localEulerAngles.x,
                _fishingRodModel.localEulerAngles.y, Mathf.Lerp(startRot, endRot, t));
            yield return null;
        }

        _fishingRodModel.localEulerAngles = new Vector3(_fishingRodModel.localEulerAngles.x,
            _fishingRodModel.localEulerAngles.y, endRot);

        _fishSpawn = FindObjectOfType<FishSpawn>().transform;
        _fish = Instantiate(_fishPrefab, _fishSpawn.position, Quaternion.identity);

        _fishingLine.LookAt(_fish.transform.position);

        for (float t = 0; t < 1f; t += (Time.deltaTime / 0.25f))
        {
            _fishingLine.LookAt(_fish.transform.position);
            _fishingLine.localScale = new Vector3(_fishingLine.localScale.x, _fishingLine.localScale.y,
                Mathf.Lerp(0.05f, Vector3.Distance(_fishingLine.position, _fish.transform.position), t));
            _fishingRodModel.localEulerAngles = new Vector3(_fishingRodModel.localEulerAngles.x,
                _fishingRodModel.localEulerAngles.y, Mathf.Lerp(endRot, startRot, t));
            yield return null;
        }

        _fishingRodModel.localEulerAngles = new Vector3(_fishingRodModel.localEulerAngles.x,
            _fishingRodModel.localEulerAngles.y, startRot);
        
        _fishingTarget = _fish.FishingTarget;
        _slider.maxValue = _fishingTarget;
        _fishigCoroutine = StartCoroutine(FishingProcess());
    }

    private IEnumerator FishingProcess()
    {
        int lastRot = 0;
        float rotSpeed = 0.5f;
        while (_fishingProgress < _fishingTarget)
        {
            _slider.value = _fishingProgress;
            _fishingLine.LookAt(_fish.transform.position);
            int fishingVec = _fish.FishingVector;
            if (Input.GetKey(KeyCode.A))
            {
                _playerModel.localEulerAngles = new Vector3(0,
                    Mathf.Clamp(_playerModel.localEulerAngles.y - 30 * (Time.deltaTime / rotSpeed), 60, 120), 0);
                _fishingVector = 1;
                lastRot = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _fishingVector = -1;
                lastRot = -1;
                _playerModel.localEulerAngles = new Vector3(0,
                    Mathf.Clamp(_playerModel.localEulerAngles.y + 30 * (Time.deltaTime / rotSpeed), 60, 120), 0);
            }

            if (_fishingVector < 0 && Input.GetKeyUp(KeyCode.D))
            {
                _fishingVector = 0;
            }
            else if (_fishingVector > 0 && Input.GetKeyUp(KeyCode.A))
            {
                _fishingVector = 0;
            }

            if (_fishingVector == 0 && lastRot == 1)
            {
                _playerModel.localEulerAngles = new Vector3(0,
                    Mathf.Clamp(_playerModel.localEulerAngles.y + 30 * (Time.deltaTime / rotSpeed), 60, 90), 0);
            }
            else if (_fishingVector == 0 && lastRot == -1)
            {
                _playerModel.localEulerAngles = new Vector3(0,
                    Mathf.Clamp(_playerModel.localEulerAngles.y - 30 * (Time.deltaTime / rotSpeed), 90, 120), 0);
            }

            if (_fishingVector == fishingVec && fishingVec != 0 && _fish.CanBeFished)
            {
                _fishingProgress += _fishingSpeed * Time.deltaTime;
            }
            else if (_fishingVector != fishingVec && _fish.CanBeFished)
            {
                _fishingProgress -= _fishingSpeed * Time.deltaTime / 0.5f;
                _fishingProgress = Mathf.Clamp(_fishingProgress, 0, 10000);
            }

            _fishingLine.localScale = new Vector3(_fishingLine.localScale.x, _fishingLine.localScale.y,
                Vector3.Distance(_fishingLine.position, _fish.transform.position));

            yield return null;
        }

        _fish.Catch(_fishEndPosition.position);

        while (_playerModel.localEulerAngles.y != 90)
        {
            if (lastRot == 1)
            {
                _playerModel.localEulerAngles = new Vector3(0,
                    Mathf.Clamp(_playerModel.localEulerAngles.y - 30 * (Time.deltaTime / rotSpeed), 90, 120), 0);
            }
            else if (lastRot == -1)
            {
                _playerModel.localEulerAngles = new Vector3(0,
                    Mathf.Clamp(_playerModel.localEulerAngles.y + 30 * (Time.deltaTime / rotSpeed), 60, 90), 0);
            }
        }

        _fish = null;
        _fishingProgress = 0;
        _fishingVector = 0;
        _slider.value = 0;
        _fishigCoroutine = null;
    }

    public void CheckFish()
    {
        _winCount--;
        _winCountText.text = Convert.ToString(_winCount);
        if (_winCount <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _winWindow.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    public void LoadMainGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _mainGame.SetActive(true);
        GameManager.Instance.Action();
        _miniGame.SetActive(false);
    }
}