using System;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    private bool _isRobberInHouse = false;

    public event Action<bool> IsRobberInHouse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Robber>())
        {
            _isRobberInHouse = true;
            IsRobberInHouse?.Invoke(_isRobberInHouse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Robber>())
        {
            _isRobberInHouse = false;
            IsRobberInHouse?.Invoke(_isRobberInHouse);
        }
    }
}