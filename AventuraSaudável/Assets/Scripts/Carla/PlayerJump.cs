using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 8f;

    private Rigidbody2D rb;
    private bool estaNoChao = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Mantém o gato no mesmo sítio
        transform.position = new Vector3(
            -3f,
            transform.position.y,
            transform.position.z
        );

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.linearVelocity =
                new Vector2(rb.linearVelocity.x, jumpForce);

            estaNoChao = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
        }
    }
}