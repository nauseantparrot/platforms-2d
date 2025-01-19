using UnityEngine;

public class PickableKeyBehaviour : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.GetComponent<PlayerBehaviour>().IncreaseKeysCounterByOne();
            Destroy(gameObject);
        }
    }
}
