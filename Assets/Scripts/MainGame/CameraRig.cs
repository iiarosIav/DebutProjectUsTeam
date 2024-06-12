using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private float _speed;

    private Transform _transform;
    [SerializeField] private Transform _target;

    // [SerializeField] private PlayerMove PlayerComponent;

    [SerializeField] private float _koefDistance = 200f;

    [SerializeField] private float _maxDist = 12f;
    [SerializeField] private float _minDist = 6f;


    private void Awake()
    {
        _transform = transform;
        _transform.position = _objectToFollow.position;
    }

    private void FixedUpdate()
    {
        _objectToFollow.LookAt(_target.position);
        transform.LookAt(_target.position);
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1 && Vector3.Distance(_objectToFollow.transform.position, _target.transform.position) > _minDist)
        {
            _objectToFollow.position += transform.forward * Time.deltaTime * _koefDistance;
            // PlayerComponent.MaxDistanceCounter();
        }
        else if (mw < -0.1 && Vector3.Distance(_objectToFollow.transform.position, _target.transform.position) < _maxDist)
        {
            _objectToFollow.position -= transform.forward * Time.deltaTime * _koefDistance;
            // PlayerComponent.MaxDistanceCounter();
        }
        
        _transform.position = Vector3.Lerp(_transform.position, _objectToFollow.position, Time.fixedDeltaTime * _speed);
    }
}

