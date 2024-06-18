using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField] private float viewRadius = 11;
    [SerializeField] private float dectectionCheckDelay = 0.1f;
    [SerializeField] private Transform target = null;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask visibilityLayer;

    [field: SerializeField] public bool TargetVisible { get; private set; }

    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (Target != null)
        {
            TargetVisible = CheckTargetVisable();
        }
    }

    private bool CheckTargetVisable()
    {
        var result = Physics2D.Raycast(transform.position, target.position - transform.position, viewRadius, visibilityLayer);
        if (result.collider != null)
        {
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) !=0;
        }
        return false;
    }

    private void DetectTarget()
    {
        if (Target == null)
        {
            CheckIfPlayerInRange();
        }
        else if (Target != null)
        {
            DetectIfOutOfRange();
        }

    }

    private void DetectIfOutOfRange()
    {
        if (Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(transform.position, target.position) > viewRadius)
        {
            Target = null;
        }
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
        if (collision != null)
        {
            Target = collision.transform;
        }
        
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(dectectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
