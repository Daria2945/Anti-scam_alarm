using UnityEngine;

public class RobberMover : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform[] _wayponints;

    private int _currentWaypoint;

    private void Awake()
    {
        _currentWaypoint = 0;
        transform.position = _wayponints[_currentWaypoint].position;
    }

    private void Update()
    {
        if(transform.position == _wayponints[_currentWaypoint].position)
            _currentWaypoint = ++_currentWaypoint % _wayponints.Length;

        transform.position = Vector3.MoveTowards(transform.position, _wayponints[_currentWaypoint].position, _speed * Time.deltaTime);
        transform.LookAt(_wayponints[_currentWaypoint].position);
    }
}