using UnityEngine;

public class FruitSpawnerUI : MonoBehaviour
{
    public GameObject fruitPrefab;
    public RectTransform canvasRect;

    public int totalFruits = 10;   // número total de frutas
    private int spawned = 0;       // contador interno

    void Start()
    {
        // 1 fruta a cada 2 segundos
        InvokeRepeating(nameof(SpawnFruit), 1f, 2f);
    }

    void SpawnFruit()
    {
        if (spawned >= totalFruits)
        {
            CancelInvoke(nameof(SpawnFruit)); // parar o spawner
            return;
        }

        GameObject f = Instantiate(fruitPrefab, canvasRect);
        RectTransform rt = f.GetComponent<RectTransform>();

        float x = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        rt.anchoredPosition = new Vector2(x, canvasRect.rect.height / 2 + 50);

        spawned++;
    }
}
