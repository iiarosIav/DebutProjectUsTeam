using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;
    [SerializeField] private Transform _playerModel;
    [SerializeField] private float _mouseSencetivity = 1f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _jumpSpeed;
    
    private float _xAngle;

    private bool _grounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    private void Update()
    {
        float speed = _speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 worldVelocity = _cameraTransform.TransformVector(inputVector) * speed;

        if (worldVelocity != Vector3.zero)
        {
            _playerModel.rotation = Quaternion.LookRotation(inputVector);
            _playerModel.localEulerAngles += new Vector3(0f, _cameraTransform.localEulerAngles.y, 0f);
        }
        
        _rigidbody.velocity = new Vector3(worldVelocity.x, _rigidbody.velocity.y, worldVelocity.z);

        float mouseX = Input.GetAxis("Mouse X"); 
        float mouseY = Input.GetAxis("Mouse Y");

        // _playerModel.localEulerAngles += new Vector3(0f, mouseX * _mouseSencetivity, 0f);
        
        _xAngle -= mouseY * _mouseSencetivity;
        _xAngle = Mathf.Clamp(_xAngle, -80, 55);

        _cameraTransform.localEulerAngles = new Vector3(_xAngle, _cameraTransform.localEulerAngles.y + mouseX * _mouseSencetivity, 0f);;

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _rigidbody.velocity += Vector3.up * _jumpSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Vector3.Angle(collision.contacts[0].normal, Vector3.up) < 40f)
        {
            _grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }
}
