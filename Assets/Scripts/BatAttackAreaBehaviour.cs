using UnityEngine;

public class BatAttackAreaBehaviour : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int attackDamage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.GetComponent<HealthBehaviour>().TakeDamage(attackDamage);
        }
    }
}
