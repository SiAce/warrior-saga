using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FXBroadcast : MonoBehaviour
{
    public AudioEventSO audioEventSO;

    public void PlayFXAudio(AudioClip audioClip)
    {
        audioEventSO.PlayAudio(audioClip);
    }
}
