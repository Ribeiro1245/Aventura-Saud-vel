using UnityEngine;

public class SpawnerComida : MonoBehaviour
{
    public GameObject[] comidas;

    public float tempoSpawn = 1f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnComida), 1f, tempoSpawn);
    }

    void SpawnComida()
    {
        VidaManager vida =
            FindFirstObjectByType<VidaManager>();

        if (vida.vidaAtual >= 6)
        {
            CancelInvoke();
            return;
        }

        Vector3 posicao = new Vector3(
            12f,
            -2.8f,
            0
        );

        int index = Random.Range(0, comidas.Length);

        Instantiate(comidas[index], posicao, Quaternion.identity);
    }
}