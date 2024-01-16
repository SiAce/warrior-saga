using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioEventSO BGMEventSO;
    public AudioEventSO FXEventSO;

    public AudioSource BGMSource;
    public AudioSource FXSource;


    public void OnBGMEvent(AudioClip audioClip)
    {
        BGMSource.clip = audioClip;
        BGMSource.Play();
    }

    public void OnFXEvent(AudioClip audioClip)
    {
        FXSource.clip = audioClip;
        FXSource.Play();
    }

    private void OnEnable()
    {
        BGMEventSO.OnAudioEvent += OnBGMEvent;
        FXEventSO.OnAudioEvent += OnFXEvent;
    }

    private void OnDisable()
    {
        BGMEventSO.OnAudioEvent -= OnBGMEvent;
        FXEventSO.OnAudioEvent -= OnFXEvent;
    }
}
