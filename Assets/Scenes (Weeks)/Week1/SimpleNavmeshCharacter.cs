using UnityEngine;
using UnityEngine.AI;

public class SimpleNavmeshCharacter : MonoBehaviour
{
    [Tooltip("Drag NavMeshAgent component")]
    public NavMeshAgent agent;

    [Tooltip("Shape Marker")]
    public Transform destination;

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (destination != null)
                {
                    destination.position = hit.point;
                }

                agent.SetDestination(hit.point);
            }
        }
    }
}