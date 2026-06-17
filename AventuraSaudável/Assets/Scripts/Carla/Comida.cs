using UnityEngine;

public class Comida : MonoBehaviour
{
    public bool comidaBoa = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Temporizador temporizador =
            FindFirstObjectByType<Temporizador>();

            if (temporizador != null &&
                temporizador.tempo <= 0)
            {
                return;
            }

            VidaManager vida =
            FindFirstObjectByType<VidaManager>();

            if (comidaBoa)
            {
                vida.GanharVida();
            }
            else
            {
                vida.PerderVida();
            }

            Destroy(gameObject);
        }
    }
}