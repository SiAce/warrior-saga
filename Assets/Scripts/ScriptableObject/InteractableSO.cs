using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable SO", menuName = "Scriptable Object/Interactable/Base")]
public class InteractableSO : ScriptableObject
{
    public Sprite beforeInteractionSprite;
    public Sprite afterInteractionSprite;

    public AudioClip interactionFXAudioClip;

    public RuntimeAnimatorController interactionPromptAnimatorController;
}
