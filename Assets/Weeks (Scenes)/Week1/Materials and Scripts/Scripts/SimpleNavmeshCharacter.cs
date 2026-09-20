using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem; // Required for the input system in your professor's code
using System.Collections;

public class SimpleNavmeshCharacter : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    private bool isTraversing = false;

    private void Update()
    {
        // 1. Point and Click Movement
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.point);
            }
        }

        // 2. Manual Link Traversal (Handles the Elevator)
        if (agent.isOnOffMeshLink && !isTraversing)
        {
            StartCoroutine(TraverseLink());
        }
    }

    private IEnumerator TraverseLink()
    {
        isTraversing = true;
        OffMeshLinkData data = agent.currentOffMeshLinkData;

        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos;
        float duration = 1.0f; // Takes 1 second to ride the elevator down
        float time = 0f;

        // Smoothly move the agent from the top of the link to the bottom
        while (time < 1f)
        {
            time += Time.deltaTime / duration;
            agent.transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
        }

        // Crucial: Tell the agent it finished crossing the link so it can continue walking
        agent.CompleteOffMeshLink();
        isTraversing = false;
    }
}