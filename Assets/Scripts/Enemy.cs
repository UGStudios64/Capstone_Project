using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [Header("// ATTACK -----------------------------------------------------------------------------------------")]
    [SerializeField] private int damage;
    [Space(5)]
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackUpForce;

    [Header("// MOVE -----------------------------------------------------------------------------------------")]
    [SerializeField] private float moveForce;
    [SerializeField] private float maxSpeed;
    private int direction = 1;
    private bool isMoving = true;

    [Header("// TIMING -----------------------------------------------------------------------------------------")]
    [SerializeField] private float moveTime;
    [SerializeField] private float stopTime;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(Move());
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                rb.AddForce(Vector2.right * direction * moveForce);
            }
            // ROTATION
            rb.AddTorque(-direction * moveForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"{other.gameObject.name} take {damage} damages");
            Life life = other.gameObject.GetComponent<Life>();
            life.TakeDamage(damage);

            KnockBack(other);
        }
    }



    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    IEnumerator Move()
    {
        while (true)
        {
            // MOVE
            isMoving = true;
            yield return new WaitForSeconds(moveTime);

            // STOP
            isMoving = false;
            rb.velocity = new Vector2(rb.velocity.x * 0.2f, rb.velocity.y);
            rb.angularVelocity *= 0.2f;

            yield return new WaitForSeconds(stopTime);

            // CHANGE DIRECTION
            direction *= -1;
        }
    }


    private void KnockBack(Collision2D other)
    {
        Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb != null)
        {
            // OPOSITE DIRECTION
            Vector2 knockbackDir = (other.transform.position - transform.position).normalized;

            // RESET VELOCITY
            playerRb.velocity = Vector2.zero;

            // APPLY KNOCKBACK BACK AND UP
            playerRb.AddForce(new Vector2(knockbackDir.x * knockbackForce, knockbackUpForce), ForceMode2D.Impulse);
        }
    }  
}