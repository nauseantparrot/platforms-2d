using UnityEngine;

public class BoxTargetBehaviour : MonoBehaviour
{
    [SerializeField] private string boxTag = "Box";
    [SerializeField] private GameObject platformObject;
    [SerializeField] private GameObject[] lightObjects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(boxTag))
        {
            platformObject.GetComponent<PlatformBehaviour>().StartMovement();
            foreach (GameObject light in lightObjects)
            {
                light.SetActive(true);
            }
        }
    }
}
