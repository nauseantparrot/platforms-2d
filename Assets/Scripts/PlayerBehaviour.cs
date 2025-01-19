using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keysCounter;

    [Header("Movement elements")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 10;

    [Header("Jump elements")]
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float jumpCircleRadius;
    [SerializeField] private LayerMask jumpSupportLayer;

    [Header("Attack elements")]
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackCircleRadius;
    [SerializeField] private LayerMask attackLayer;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private int pickedKeys = 0;

    public int PickedKeys { get => pickedKeys; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rigidBody.linearVelocityX = horizontalInput * speed;

        animator.SetBool("isRunning", horizontalInput != 0);

        if (horizontalInput > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Collider2D collider = Physics2D.OverlapCircle(feetPosition.position, jumpCircleRadius, jumpSupportLayer);
            if (collider != null) {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger("jump");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("attack");
        }
    }

    public void IncreaseKeysCounterByOne()
    {
        pickedKeys++;
        keysCounter.text = "x " + pickedKeys;
    }

    private void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackCircleRadius, attackLayer);
        foreach (Collider2D collider in colliders)
        {
            if (!collider.CompareTag("Player"))
            {
                collider.GetComponent<HealthBehaviour>().TakeDamage(attackDamage);
            }
        }
    }
}
