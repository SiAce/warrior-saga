using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIManager : MonoBehaviour
{
    public PlayerStatusBar playerStatusBar;
    public SceneLoadEventSO sceneLoadEventSO;

    private void Awake()
    {
        playerStatusBar.ResetStatus();
    }

    private void OnEnable()
    {
        sceneLoadEventSO.SceneLoadEvent += OnSceneLoad;
    }

    private void OnDisable()
    {
        sceneLoadEventSO.SceneLoadEvent -= OnSceneLoad;
    }

    private void OnSceneLoad(string sceneToLoad, Vector3 spawnPoint, SceneType sceneType, bool shouldFade)
    {
        playerStatusBar.gameObject.SetActive(sceneType == SceneType.Map);
    }

    public void OnHealthChange(float percent)
    {
        playerStatusBar.OnHealthChange(percent);
    }
}
