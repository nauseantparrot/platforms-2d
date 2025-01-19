using System.Collections;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour, IPlayerDetector
{
    [SerializeField] private GameObject statsBar;

    [Header("Movement elements")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float patrolSpeed;

    [Header("Attack elements")]
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private string playerTag = "Player";

    private int pointIndex = 0;
    private bool isPatrolling = true;
    private bool isAttacking = false;
    private bool isInRange = false;
    private GameObject playerGameObjectRef;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        animator.SetBool("atacando", false);
        while (isPatrolling)
        {
            Vector3 targetPosition = new Vector3(patrolPoints[pointIndex].position.x, transform.position.y, transform.position.z);
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
        animator.SetBool("atacando", true);
        while (isAttacking)
        {
            Vector3 targetPosition = new Vector3(playerGameObjectRef.transform.position.x, transform.position.y, transform.position.z);
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
            yield return null;
        }
    }

    IEnumerator GiveDamage()
    {
        while (isInRange)
        {
            playerGameObjectRef.GetComponent<HealthBehaviour>().TakeDamage(attackDamage);
            yield return new WaitForSeconds(0.6f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking && collision.CompareTag(playerTag))
        {
            isInRange = true;
            StartCoroutine(GiveDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAttacking && collision.CompareTag(playerTag))
        {
            isInRange = false;
        }
    }
}
