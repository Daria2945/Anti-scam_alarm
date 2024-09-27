using UnityEngine;

[RequireComponent(typeof(HouseAlarm))]
public class SurveillanceCamera : MonoBehaviour
{
    private HouseAlarm _almor;

    private void Start()
    {
        _almor = GetComponent<HouseAlarm>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Robber>())
        {
            _almor.SoundAlarm();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Robber>())
        {
            _almor.SilenceAlarm();
        }
    }
}