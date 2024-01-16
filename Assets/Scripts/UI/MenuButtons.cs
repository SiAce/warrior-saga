using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour
{
    public SceneLoadEventSO sceneLoadEventSO;
    public string firstSceneToLoad;
    public Vector3 firstspawnPoint;

    public GameObject newGameButton;

    public void StartNewGame()
    {
        sceneLoadEventSO.RaiseSceneLoadEvent(firstSceneToLoad, firstspawnPoint, SceneType.Map, true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }
}
