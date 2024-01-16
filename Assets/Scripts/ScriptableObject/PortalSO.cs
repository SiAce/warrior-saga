using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Portal SO", menuName = "Scriptable Object/Interactable/Portal")]
public class PortalSO : InteractableSO
{
    public string sceneToLoad;
    public Vector3 spawnPoint;
    public SceneType sceneType = SceneType.Map;
    public bool shouldFade = true;
    public SceneLoadEventSO sceneLoadEventSO;
}