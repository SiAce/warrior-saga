using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Audio Event SO", menuName = "Scriptable Object/Audio Event")]
public class AudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnAudioEvent;

    public void PlayAudio(AudioClip audioClip)
    {
        OnAudioEvent?.Invoke(audioClip);
    }
}
