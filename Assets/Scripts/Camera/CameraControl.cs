using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CameraControl : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;

    public void CameraShake()
    {
        impulseSource.GenerateImpulse();
    }
}
