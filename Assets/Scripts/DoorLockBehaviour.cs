using UnityEngine;

public class DoorLockBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject doorObject;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int requiredKeys = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && collision.GetComponent<PlayerBehaviour>().PickedKeys >= requiredKeys)
        {
            Destroy(doorObject);
            Destroy(gameObject);
        }
    }
}
