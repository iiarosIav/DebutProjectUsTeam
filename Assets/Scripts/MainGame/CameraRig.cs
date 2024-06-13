using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private float _speedFar;
    [SerializeField] private float _speedClose;

    private Transform _transform;
    [SerializeField] private Transform _target;

    [SerializeField] private PlayerMove _playerComponent;

    [SerializeField] private float _koefDistance = 200f;

    [SerializeField] private float _maxDist = 12f;
    [SerializeField] private float _minDist = 6f;


    private void Awake()
    {
        _transform = transform;
        _transform.position = _objectToFollow.position;
        _objectToFollow.LookAt(_target.position);
        transform.LookAt(_target.position);
    }

    private void FixedUpdate()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1 && Vector3.Distance(_objectToFollow.transform.position, _target.transform.position) > _minDist)
        {
            _objectToFollow.position += transform.forward * Time.deltaTime * _koefDistance;
            _playerComponent.MaxDistanceCounter(Vector3.Distance(_objectToFollow.position, _target.transform.position));
        }
        else if (mw < -0.1 && Vector3.Distance(_objectToFollow.transform.position, _target.transform.position) < _maxDist)
        {
            _objectToFollow.position -= transform.forward * Time.deltaTime * _koefDistance;
            _playerComponent.MaxDistanceCounter(Vector3.Distance(_objectToFollow.position, _target.transform.position));
        }

        var speed = _speedFar;
        if (Vector3.Distance(_objectToFollow.transform.position, _target.transform.position) <= 8)
        {
            speed = _speedClose;
        }
        _transform.position = Vector3.Lerp(_transform.position, _objectToFollow.position, Time.fixedDeltaTime * speed);
    }
}

