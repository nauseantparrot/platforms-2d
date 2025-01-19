using System.Collections;
using UnityEngine;

public class BatBehaviour : MonoBehaviour, IPlayerDetector
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private Transform backoffPoint;
    [SerializeField] private GameObject statsBar;

    private int pointIndex = 0;
    private bool isPatrolling = true;
    private bool isAttacking = false;
    private GameObject playerGameObjectRef;
    private Vector3 cachedBackoffPosition;

    private void Start()
    {
        StartCoroutine(Patrol());
    }

    public void DetectPlayer(GameObject playerGameObject)
    {
        playerGameObjectRef = playerGameObject;
        isPatrolling = false;
        isAttacking = true;
        StartCoroutine(Attack());
    }

    public void LostPlayer()
    {
        isPatrolling = true;
        isAttacking = false;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (isPatrolling)
        {
            Vector3 targetPosition = patrolPoints[pointIndex].position;
            if (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);
                if (targetPosition.x > transform.position.x)
                {
                    transform.eulerAngles = Vector3.zero;
                    statsBar.transform.eulerAngles = Vector3.zero;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    statsBar.transform.eulerAngles = Vector3.zero;
                }
            }
            else
            {
                pointIndex++;
                if (pointIndex == patrolPoints.Length)
                {
                    pointIndex = 0;
                }                
            }
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        Vector3 targetPosition = playerGameObjectRef.transform.position;
        while (isAttacking && transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * 1.5f * Time.deltaTime);
            if (targetPosition.x > transform.position.x)
            {
                transform.eulerAngles = Vector3.zero;
                statsBar.transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                statsBar.transform.eulerAngles = Vector3.zero;
            }
            yield return null;
        }
        if (isAttacking)
        {
            gameObject.GetComponent<Animator>().SetTrigger("atacar");
        }
    }

    IEnumerator MoveToBackoffPosition()
    {
        while (isAttacking && transform.position != cachedBackoffPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, cachedBackoffPosition, patrolSpeed * Time.deltaTime);
            yield return null;
        }
        if (isAttacking)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Attack());
        }
    }

    private void FinishAttack()
    {
        cachedBackoffPosition = backoffPoint.position;
        StartCoroutine(MoveToBackoffPosition());
    }
}
