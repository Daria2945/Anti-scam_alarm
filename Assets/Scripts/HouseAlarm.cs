using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SurveillanceCamera))]
public class HouseAlarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeSteep = 0.4f;

    private SurveillanceCamera _camera;
    private AudioSource _audioSource;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _currentVolume;

    private Coroutine _coroutine;

    private void Awake()
    {
        _camera = GetComponent<SurveillanceCamera>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _currentVolume = _minVolume;
    }

    private void OnEnable()
    {
        _camera.IsRobberInHouse += StartChangeVolume;
    }

    private void OnDisable()
    {
        _camera.IsRobberInHouse -= StartChangeVolume;
    }

    private void StartChangeVolume(bool isRobberInHouse)
    {
        if (isRobberInHouse)
            _coroutine = StartCoroutine(ChangeVolume(_volumeChangeSteep));
        else
            _coroutine = StartCoroutine(ChangeVolume(-_volumeChangeSteep));
    }

    private void ChangeCurrentVolume(float volumeChangeSteep)
    {
        _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, volumeChangeSteep * Time.deltaTime);
        _audioSource.volume = _currentVolume;
    }

    private IEnumerator ChangeVolume(float volumeChangeStep)
    {
        while (true)
        {
            ChangeCurrentVolume(volumeChangeStep);

            if (_currentVolume >= _maxVolume)
                StopCoroutine(_coroutine);

            if (_currentVolume <= _minVolume)
                StopCoroutine(_coroutine);

            yield return null;
        }
    }
}