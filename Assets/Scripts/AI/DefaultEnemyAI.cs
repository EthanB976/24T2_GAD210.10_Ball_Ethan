using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour
{

    [SerializeField] private AIBehaviour shootBehaviour, patrolBehaviour;

    [SerializeField] private TankController tank;
    [SerializeField] private AIDetector detector;

    private void Awake()
    {
        detector = GetComponent<AIDetector>();
        tank = GetComponent<TankController>();
    }

    private void Update()
    {
        if (detector.TargetVisible)
        {
            shootBehaviour.PerformAction(tank, detector);
        }
        else
        {
            patrolBehaviour.PerformAction(tank, detector);
        }
    }
}
