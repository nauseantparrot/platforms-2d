using System.Collections;
using UnityEngine;

public class WizardBehaviour : MonoBehaviour, IPlayerDetector
{
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject statsBar;

    private Animator animator;
    private GameObject playerGameObjectRef;
    private bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DetectPlayer(GameObject playerGameObject)
    {
        playerGameObjectRef = playerGameObject;
        isAttacking = true;
        StartCoroutine(FacePlayerPosition());
        StartCoroutine(Attack());
    }

    public void LostPlayer()
    {
        isAttacking = false;
    }

    IEnumerator Attack()
    {
        while (isAttacking)
        {
            animator.SetTrigger("attack");
            yield return new WaitForSeconds(timeBetweenAttack);
        }
    }

    IEnumerator FacePlayerPosition()
    {
        while (isAttacking)
        {
            if (transform.position.x > playerGameObjectRef.transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                statsBar.transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
                statsBar.transform.eulerAngles = Vector3.zero;
            }
            yield return null;
        }
    }

    private void ThrowFireball()
    {
        Instantiate(fireball, spawnPosition.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.GetComponent<HealthBehaviour>().TakeDamage(attackDamage);
        }
    }
}
