using UnityEngine;

public class Comida : MonoBehaviour
{
    public bool comidaBoa = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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