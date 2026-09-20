using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System.Collections;

public class SimpleNavmeshCharacter : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [Tooltip("Jump In Seconds")]
    [SerializeField] private float jumpDuration = 0.5f;

    [Tooltip("Arc Jump")]
    [SerializeField] private float jumpHeight = 1.5f;

    private bool isTraversing = false;

    private void Start()
    {

        if (agent != null)
        {
            agent.autoTraverseOffMeshLink = false;
        }
    }

    private void Update()
    {

        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame && agent.isOnNavMesh)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.point);
            }
        }

        if (agent.isOnNavMesh && agent.isOnOffMeshLink && !isTraversing)
        {
            StartCoroutine(TraverseLinkCoroutine());
        }
    }

    private IEnumerator TraverseLinkCoroutine()
    {
        isTraversing = true;
        OffMeshLinkData data = agent.currentOffMeshLinkData;

        float distance = Vector3.Distance(data.startPos, data.endPos);

        if (distance > 5f)
        {

            agent.transform.position = data.endPos;
        }
        else
        {

            float time = 0f;
            Vector3 startPos = agent.transform.position;

            while (time < 1f)
            {
                time += Time.deltaTime / jumpDuration;

                float yOffset = jumpHeight * 4.0f * (time - time * time);

                agent.transform.position = Vector3.Lerp(startPos, data.endPos, time) + new Vector3(0, yOffset, 0);

                yield return null;
            }
        }

        agent.CompleteOffMeshLink();
        isTraversing = false;
    }
}