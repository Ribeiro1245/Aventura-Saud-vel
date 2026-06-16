using UnityEngine;

public class MoverFundo : MonoBehaviour
{
    public float velocidade = 2f;

    public float limiteEsquerda = -20f;
    public float novaPosicaoX = 20f;

    void Update()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

        if (transform.position.x < limiteEsquerda)
        {
            transform.position =
                new Vector3(
                    novaPosicaoX,
                    transform.position.y,
                    transform.position.z
                );
        }
    }
}