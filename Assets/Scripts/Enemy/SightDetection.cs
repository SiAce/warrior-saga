using UnityEngine;

public class SightDetection : MonoBehaviour
{
    [Header("Dectection Parameters")]
    public Vector2 sightOriginOffset;
    public Vector2 sightBoxSize;
    public float sightDistance;
    public LayerMask layerToDetect;


    //[Header("Dectection Results")]


    public RaycastHit2D InSight(Vector3 faceDir) => Physics2D.BoxCast((Vector2)transform.position + sightOriginOffset, sightBoxSize, 0, faceDir, sightDistance, layerToDetect);

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(
            transform.position + (Vector3)sightOriginOffset + sightDistance / 2 * new Vector3(-transform.localScale.x, 0, 0),
            new Vector3(sightDistance + sightBoxSize.x, 2 * sightBoxSize.y, 0)
            );
    }
}