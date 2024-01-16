using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneLoadEventSO", menuName = "Scriptable Object/Scene/Load Event")]
public class SceneLoadEventSO: ScriptableObject
{
    public UnityAction<string, Vector3, SceneType, bool> SceneLoadEvent;

    public void RaiseSceneLoadEvent(string sceneToLoad, Vector3 spawnPoint, SceneType sceneType, bool shouldFade)
    {
        SceneLoadEvent?.Invoke(sceneToLoad, spawnPoint, sceneType, shouldFade);
    }

}
