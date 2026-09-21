using UnityEngine;
using System.Collections;

public class AutoDoor : MonoBehaviour
{
    [Tooltip("Drag the physical door object here")]
    [SerializeField] private Transform door;

    [SerializeField] private float interval = 3f;
    [SerializeField] private float moveDuration = 0.5f;

    [Tooltip("Drag the Opened Position Transform here")]
    [SerializeField] private Transform openedState;

    [Tooltip("Drag the ClosedPosition Transform here")]
    [SerializeField] private Transform closedState;

    [SerializeField] private GameObject target;
    [SerializeField] private bool enableRaycastingCheck;

    private bool isOpen = false;

    private void Start()
    {
        StartCoroutine(ToggleDoorRoutine());
    }

    private IEnumerator ToggleDoorRoutine()
    {
        while (true)
        {
            // Interval
            yield return new WaitForSeconds(interval);

            // Toggle state
            isOpen = !isOpen;
            Vector3 targetPos = isOpen ? openedState.position : closedState.position;

            float time = 0;
            Vector3 startPos = door.position;

            // Move Duration
            while (time < 1f)
            {
                time += Time.deltaTime / moveDuration;
                door.position = Vector3.Lerp(startPos, targetPos, time);
                yield return null;
            }
        }
    }
}