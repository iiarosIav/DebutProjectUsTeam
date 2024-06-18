using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;
    [SerializeField] private Transform _playerModel;
    // [SerializeField] private float _mouseSencetivity = 1f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _grabDistance;
    private InteractiveObject[] _interactiveObjects;
    public int Level = 1;
    
    private float _maxDistance;

    private bool _grounded;
    private bool _groundedForIO;
    private GameObject _interactiveObject;
    private Transform _interactiveObjectParent;
    private bool _canRotate = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _interactiveObjects = FindObjectsOfType<InteractiveObject>();
        _maxDistance = (_objectToFollow.position - _cameraTransform.position).magnitude;
    }

    private void Update()
    {
        float speed = _speed;
        if (_canRotate == false)
        {
            speed /= 2;
        }
        else
        {
            CheckDistance();
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 worldVelocity = _camera.TransformVector(inputVector) * speed;

        if (inputVector != Vector3.zero && _canRotate)
        {
            _playerModel.rotation = Quaternion.LookRotation(worldVelocity);
            _playerModel.localEulerAngles = new Vector3(0f, _playerModel.localEulerAngles.y, 0f);
        }
        
        _rigidbody.velocity = new Vector3(worldVelocity.x, _rigidbody.velocity.y, worldVelocity.z);

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _rigidbody.velocity += Vector3.up * _jumpSpeed;
        }

        if (Input.GetKeyDown(KeyCode.E) && _interactiveObject != null)
        {
            if (Vector3.Angle(_playerModel.forward, (_interactiveObject.transform.position - transform.position)) < 60)
            {
                if (_interactiveObject != null && _canRotate && _groundedForIO)
                {
                    _interactiveObjectParent = _interactiveObject.transform.parent;
                    _interactiveObject.transform.parent = _playerModel.transform;
                    _canRotate = false;
                }
                else if (_interactiveObject != null && !_canRotate && _groundedForIO)
                {
                    _interactiveObject.transform.parent = _interactiveObjectParent;
                    _canRotate = true;
                }
            }
        }

        CameraObstacleReact();
    }

    public void ClearStates()
    {
        _canRotate = true;
        _interactiveObject.GetComponent<InteractiveObject>().Deactivate();
        _interactiveObject = null;
    }

    private void CheckDistance()
    {
        Transform closetObject = FindClosest(_interactiveObjects);
        if (closetObject == null) return;
        if (Vector3.Distance(closetObject.transform.position, transform.position) < _grabDistance && _canRotate)
        {
            if (_interactiveObject != null)
            {
                _interactiveObject.GetComponent<InteractiveObject>().Deactivate();
            }
            _interactiveObject = closetObject.gameObject;
            _interactiveObject.GetComponent<InteractiveObject>().Activate();
            
        }
        else if (Vector3.Distance(closetObject.transform.position, transform.position) > _grabDistance && _interactiveObject != null)
        {
            _interactiveObject.GetComponent<InteractiveObject>().Deactivate();
            _interactiveObject = null;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Vector3.Angle(collision.contacts[0].normal, Vector3.up) < 40f)
        {
            _grounded = true;
            if (!collision.gameObject.GetComponent<InteractiveObject>())
            {
                _groundedForIO = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
        _groundedForIO = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<LevelTrigger>() is var levelTrigger)
        {
            Level = levelTrigger.Level;
            GameManager.Instance.LoadLocations();
        }
    }

    private void CameraObstacleReact() // Ставить коллайдер стен на 0.01 больше Невидимые стены помещать на слой "Player"
    {
        RaycastHit hit;
        LayerMask layerMask = LayerMask.NameToLayer("Player");
        float distance = Vector3.Distance(_objectToFollow.position, _playerModel.position);
        if (Physics.Raycast(_cameraTransform.position, _objectToFollow.position - _cameraTransform.position, out hit,
                _maxDistance, layerMask))
        {
            _objectToFollow.position = hit.point;
            _camera.position = _objectToFollow.position;
        }
        else if (distance < _maxDistance && !Physics.Raycast(_objectToFollow.position,-_objectToFollow.forward, .1f, layerMask))
        {
            _objectToFollow.position -= _objectToFollow.forward * .05f;
        }
    }

    public void MaxDistanceCounter(float distance)
    {
        _maxDistance = distance;
    }

    private Transform FindClosest(InteractiveObject[] gameObjects)
    {
        Vector3 selfPos = transform.position;

        Transform closest = null;
        float closestDist = float.MaxValue;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Transform targ = gameObjects[i].transform;
            Vector3 enemyPos = targ.position;
            Vector3 sub = enemyPos - selfPos;
            float distToEnemy = sub.sqrMagnitude;
            if (distToEnemy < closestDist)
            {
                closestDist = distToEnemy;
                closest = targ;
            }
        }

        return closest;
    }
}
