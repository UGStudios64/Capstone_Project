using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

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
        StartCoroutine(PatrolLoop());
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



    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    IEnumerator PatrolLoop()
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
}