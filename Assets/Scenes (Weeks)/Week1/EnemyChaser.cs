using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : MonoBehaviour
{
    [Tooltip("Drag this object's NavMeshAgent here")]
    public NavMeshAgent chaserAgent;

    [Tooltip("Drag the player object here")]
    public Transform playerToChase;

    void Update()
    {

        if (chaserAgent != null && chaserAgent.isActiveAndEnabled && chaserAgent.isOnNavMesh)
        {
            if (playerToChase != null)
            {
                chaserAgent.SetDestination(playerToChase.position);
            }
        }
    }
}