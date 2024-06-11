using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform objectToFollow;
    public float speed;

    private Transform _transform;
    public Transform Player;


    public PlayerMove PlayerComponent;

    public float KoefDistance;

    [SerializeField] private float _maxDist;
    [SerializeField] private float _minDist;


    private void Awake()
    {
        _transform = transform;
        _transform.position = objectToFollow.position;
    }

    private void FixedUpdate()
    {
        objectToFollow.LookAt(Player.position);
        transform.LookAt(Player.position);
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1 && Vector3.Distance(objectToFollow.transform.position, Player.transform.position) > _minDist)
        {
            objectToFollow.position += transform.forward * Time.deltaTime * KoefDistance;
            PlayerComponent.MaxDistanceCounter();
        }
        else if (mw < -0.1 && Vector3.Distance(objectToFollow.transform.position, Player.transform.position) < _maxDist)
        {
            objectToFollow.position -= transform.forward * Time.deltaTime * KoefDistance;
            PlayerComponent.MaxDistanceCounter();
        }
        
        _transform.position = Vector3.Lerp(_transform.position, objectToFollow.position, Time.fixedDeltaTime * speed);
    }
}

