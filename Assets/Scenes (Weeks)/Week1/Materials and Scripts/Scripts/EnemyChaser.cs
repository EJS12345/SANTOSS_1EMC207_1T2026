using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : MonoBehaviour
{
    [Tooltip("Drag this object's NavMeshAgent here")]
    public NavMeshAgent ChaserAgent;

    [Tooltip("Drag the player object here")]
    public Transform PlayerToChase;

    private void Update()
    {
        if (ChaserAgent != null && ChaserAgent.isActiveAndEnabled && ChaserAgent.isOnNavMesh)
        {
            if (PlayerToChase != null)
            {
                ChaserAgent.SetDestination(PlayerToChase.position);
            }
        }
    }
}