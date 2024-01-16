using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BGM : MonoBehaviour
{
    public AudioClip BGMAudioClip;
    public AudioEventSO audioEventSO;

    private void OnEnable()
    {
        audioEventSO.PlayAudio(BGMAudioClip);
    }
}
