using System.Collections;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float lifespan = 1;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private string playerTag = "Player";

    private Animator animator;
    private bool hasExploded = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(ScheduleExplode());
    }

    private void Update()
    {
        if (!hasExploded)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void Explode()
    {
        hasExploded = true;
        animator.SetTrigger("explotar");
    }

    private IEnumerator ScheduleExplode()
    {
        yield return new WaitForSeconds(lifespan);
        if (!hasExploded)
        {
            Explode();
        }
    }

    private void FinishLifespan()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasExploded && collision.CompareTag(playerTag))
        {
            collision.GetComponent<HealthBehaviour>().TakeDamage(attackDamage);
            Explode();
        }
    }
}
