using UnityEngine;
using System.Collections;

public class Portal : InteractableController
{
    protected override void InteractEffect()
    {
        PortalSO portalSO = (PortalSO)interactableSO;
        portalSO.sceneLoadEventSO.RaiseSceneLoadEvent(
            portalSO.sceneToLoad,
            portalSO.spawnPoint,
            portalSO.sceneType,
            portalSO.shouldFade
        );
    }
}

