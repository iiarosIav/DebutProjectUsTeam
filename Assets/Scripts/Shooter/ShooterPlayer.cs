using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShooterPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    // [SerializeField] private float _jumpSpeed = 6f;
    [SerializeField] private Transform _playerModel;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _mouseSensevity = 7f;
    
    [SerializeField] private int _winCount;
    // [SerializeField] private int _sceneIndex;
    
    [SerializeField] private GameObject _mainGame;
    [SerializeField] private GameObject _miniGame;
    [SerializeField] private GameObject _miniGamePrefab;
    [SerializeField] private Transform _miniGamePosition;
    
    [SerializeField] private GameObject _winWindow;
    
    [SerializeField] private Gun _gun;

    private Rigidbody _rigidbody;

    private float _xAngle;
    public bool IsWin;
    
    public int TargetCoounter;

    public TextMeshProUGUI TarText;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (IsWin) return;
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        float horizontaInput = Input.GetAxis("Horizontal");
        float verticalnput = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(horizontaInput, 0, verticalnput);
        Vector3 worldVelocity = _playerModel.TransformVector(inputVector) * _speed;

        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     _rigidbody.velocity = new Vector3(worldVelocity.x * 2, _rigidbody.velocity.y, worldVelocity.z * 2);
        // }
        // else
        // {
        // _rigidbody.velocity = new Vector3(worldVelocity.x, _rigidbody.velocity.y, worldVelocity.z);
        // }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _playerModel.localEulerAngles += new Vector3(0f, mouseX * _mouseSensevity, 0f);
        _playerModel.localEulerAngles = new Vector3(0f, Mathf.Clamp(_playerModel.localEulerAngles.y, 90, 250), 0f);

        _xAngle -= mouseY * _mouseSensevity;
        _xAngle = Mathf.Clamp(_xAngle, -80, 80);

        _cameraTransform.localEulerAngles = new Vector3(_xAngle, 0f, 0f);

        // if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        // {
        //     _rigidbody.velocity += Vector3.up * _jumpSpeed;
        // }
    }

    public void TargetCheck()
    {
        TarText.text = (_winCount - TargetCoounter).ToString();
        if (TargetCoounter >= _winCount)
        {
            IsWin = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _winWindow.SetActive(true);
        }
    }
    
    public void Restart()
    {
        IsWin = false;
        _gun.Restart();
        TargetCoounter = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        TarText.text = _winCount.ToString();
        var miniGamePosition = Instantiate(_miniGamePrefab, _miniGamePosition.position, _miniGamePosition.rotation).transform;
        miniGamePosition.SetParent(_miniGame.transform);
        Destroy(_miniGamePosition.gameObject);
        _miniGamePosition = miniGamePosition;
    }

    public void LoadMainGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _mainGame.SetActive(true);
        GameManager.Instance.Action();
        _miniGame.SetActive(false);
    }

    // private void OnCollisionStay(Collision collision)
    // {
    //     if (Vector3.Angle(collision.contacts[0].normal, Vector3.up) < 45f)
    //     {
    //         _grounded = true;
    //     }
    // }
    //
    // private void OnCollisionExit(Collision collision)
    // {
    //     _grounded = false;
    // }
}