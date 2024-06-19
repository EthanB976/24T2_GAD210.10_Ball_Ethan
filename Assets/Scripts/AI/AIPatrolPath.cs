using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolPath : AIBehaviour
{
    public PatrolPath patrolPath;
    [Range(0.1f, 1)]
    public float arriveDistance = 1;
    public float waitTime = 0.5f;
    [SerializeField] private bool isWaiting = false;
    [SerializeField] Vector2 currentPatrolTarget = Vector2.zero;
    bool isInitalized = false;

    private int currentIndex = -1;

    private void Awake()
    {
        if(patrolPath == null)
        {
            patrolPath = GetComponentInChildren<PatrolPath>();
        }
    }

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if(!isWaiting)
        {
            if (patrolPath.Length < 2)
            {
                return;
            }
            if (!isInitalized )
            {
                var currentPathPoint = patrolPath.GetClosetPathPoint(tank.transform.position);
                this.currentIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.Position;
                isInitalized = true;
            }
            if (Vector2.Distance(tank.transform.position, currentPatrolTarget) < arriveDistance)
            {
                isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }

            Vector2 directionToGo = currentPatrolTarget - (Vector2)tank.tankMover.transform.position;
            var dotProduct = Vector2.Dot(tank.tankMover.transform.up, directionToGo.normalized);

            if (dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(tank.tankMover.transform.up, directionToGo.normalized);
                int rotaionResult = crossProduct.z >= 0 ? -1 : 1;
                tank.HandleBodyMovement(new Vector2(rotaionResult, 1));
            }
            else
            {
                tank.HandleBodyMovement(Vector2.up);
            }
        }

        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(waitTime);
            var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
            currentPatrolTarget = nextPathPoint.Position;
            currentIndex = nextPathPoint.Index;
            isWaiting = false;
        }
    }
}
