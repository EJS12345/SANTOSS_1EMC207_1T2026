using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Tooltip("Drag an empty GameObject here")]
    [SerializeField] private Transform targetWaypoint;

    [SerializeField] private float speed = 3f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToTarget = true;

    private void Start()
    {

        startPosition = transform.position;

        if (targetWaypoint != null)
        {
            targetPosition = targetWaypoint.position;
        }
        else
        {
            targetPosition = startPosition;
            Debug.LogWarning("Missing Target Waypoint on " + gameObject.name);
        }
    }

    private void Update()
    {
        
        Vector3 currentDestination = movingToTarget ? targetPosition : startPosition;

        transform.position = Vector3.MoveTowards(transform.position, currentDestination, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentDestination) < 0.01f)
        {
            movingToTarget = !movingToTarget;
        }
    }
}