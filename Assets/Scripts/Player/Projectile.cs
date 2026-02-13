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
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Destroy(gameObject, 5f);
    }
}
