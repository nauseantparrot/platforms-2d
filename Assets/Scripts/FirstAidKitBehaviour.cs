using UnityEngine;

public class FirstAidKitBehaviour : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.GetComponent<HealthBehaviour>().FillHealth();
            Destroy(gameObject);
        }
    }
}
