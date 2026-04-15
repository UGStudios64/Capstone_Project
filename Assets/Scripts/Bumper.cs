using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private float bounceForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb == null) return;

        Vector2 direction = (collision.transform.position - transform.position).normalized;
        rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
    }
}