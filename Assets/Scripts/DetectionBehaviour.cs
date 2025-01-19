using UnityEngine;

public class DetectionBehaviour : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeReference] private GameObject interestedObject;

    private IPlayerDetector interestedDetector;

    private void Awake()
    {
        interestedDetector = interestedObject.GetComponent<IPlayerDetector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && interestedObject != null)
        {
            interestedDetector.DetectPlayer(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && interestedObject != null)
        {
            interestedDetector.LostPlayer();
        }
    }
}
