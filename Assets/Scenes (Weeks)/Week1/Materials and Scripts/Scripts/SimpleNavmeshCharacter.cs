using UnityEngine;
using UnityEngine.AI;

public class SimpleNavmeshCharacter : MonoBehaviour
{
    [Tooltip("Drag NavMeshAgent component")]
    public NavMeshAgent Agent;

    [Tooltip("Shape Marker")]
    public Transform Destination;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (Destination != null)
                {
                    Destination.position = hit.point;
                }
                Agent.SetDestination(hit.point);
            }
        }
    }
}