using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HouseAlarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeSteep = 0.4f;

    private AudioSource _audioSource;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _currentVolume;

    private bool _isRobberInHouse = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _currentVolume = _minVolume;
    }

    private void Update()
    {
        if (_isRobberInHouse)
        {
            ChangeVolume(_volumeChangeSteep);
        }
        
        if(_isRobberInHouse == false && _currentVolume > _minVolume)
        {
            ChangeVolume(-_volumeChangeSteep);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Robber>())
            _isRobberInHouse = true;
    }

    private void OnTriggerExit(Collider other)
    {
            _isRobberInHouse = false;
    }

    private void ChangeVolume(float volumeChangeSteep)
    {
        _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, volumeChangeSteep * Time.deltaTime);
        _audioSource.volume = _currentVolume;
    }
}