using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HouseAlarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeSteep = 0.4f;

    private AudioSource _audioSource;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _currentVolume;

    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
        _currentVolume = _minVolume;
    }

    public void SoundAlarm()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void SilenceAlarm()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(_minVolume));
    }

    private void ChangeCurrentVolume(float finalVolume)
    {
        _currentVolume = Mathf.MoveTowards(_currentVolume, finalVolume, _volumeChangeSteep * Time.deltaTime);
        _audioSource.volume = _currentVolume;
    }

    private IEnumerator ChangeVolume(float finalVolume)
    {
        while (_currentVolume != finalVolume)
        {
            ChangeCurrentVolume(finalVolume);

            yield return null;
        }
    }
}