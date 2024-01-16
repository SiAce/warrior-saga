using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAudio : MonoBehaviour
{
    public UnityEvent<AudioClip> OnPlayerAttack;

    public void PlayerAttackAudio(AudioClip attackAudioClip)
    {
        OnPlayerAttack?.Invoke(attackAudioClip);
    }
}
