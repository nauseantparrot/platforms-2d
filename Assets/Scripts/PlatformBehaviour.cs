using System.Collections;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3;

    private bool isMoving = false;

    public void StartMovement()
    {
        isMoving = true;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (isMoving)
        {
            while (transform.position.y != pointA.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            while (transform.position.y != pointB.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
