using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(float direction)
    {
        rb.linearVelocity = new Vector2(direction * speed, 0f);

        if (direction < 0)
        {
            transform.localScale = new Vector3(-1f, 0.5f, 1f);
        }

        Destroy(gameObject, 0.5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage();
            }

            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}