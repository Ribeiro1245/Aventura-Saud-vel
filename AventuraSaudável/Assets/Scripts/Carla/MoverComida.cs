using UnityEngine;

public class MoverComida : MonoBehaviour
{
    public float velocidade = 2.5f;

    void Update()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}