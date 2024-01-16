using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public string menuSceneToLoad;
    public Vector3 menuspawnPoint;

    public SceneLoadEventSO sceneLoadEventSO;

    public GameObject player;

    public AsyncOperationHandle currentSceneOperationHandle;
    public bool hasCurrentScene;

    private void Awake()
    {
        hasCurrentScene = false;
    }

    private void OnEnable()
    {
        sceneLoadEventSO.SceneLoadEvent += OnSceneLoad;
    }

    private void OnDisable()
    {
        sceneLoadEventSO.SceneLoadEvent -= OnSceneLoad;
    }

    private void Start()
    {
        sceneLoadEventSO.RaiseSceneLoadEvent(menuSceneToLoad, menuspawnPoint, SceneType.Menu, false);
    }

    private void OnSceneLoad(string sceneToLoad, Vector3 spawnPoint, SceneType sceneType, bool shouldFade)
    {
        StartCoroutine(SceneLoadCoroutine(sceneToLoad, spawnPoint, sceneType, shouldFade));
    }

    private IEnumerator SceneLoadCoroutine(string sceneToLoad, Vector3 spawnPoint, SceneType sceneType, bool shouldFade)
    {
        player.SetActive(false);
        if (hasCurrentScene)
        {
            yield return Addressables.UnloadSceneAsync(currentSceneOperationHandle);
        }
        var loadSceneOperationHandle = Addressables.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        yield return loadSceneOperationHandle;
        currentSceneOperationHandle = loadSceneOperationHandle;
        hasCurrentScene = true;
        player.transform.position = spawnPoint;
        player.SetActive(sceneType == SceneType.Map);
    }
}
