using UnityEngine;

public class FruitSpawnerLevel3 : MonoBehaviour
{
    public GameObject fruitPrefab;
    public GameObject hamburguerPrefab;
    public RectTransform canvasRect;

    public int totalFruits = 20;
    public int totalHamburguers = 5;

    private int spawnedFruits = 0;
    private int spawnedHamburguers = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnItem), 1f, 1.5f);
    }

    void SpawnItem()
    {
        // parar quando tudo tiver sido criado
        if (spawnedFruits >= totalFruits && spawnedHamburguers >= totalHamburguers)
        {
            CancelInvoke(nameof(SpawnItem));
            return;
        }

        // 70% fruta, 30% hambúrguer
        bool spawnHamburguer = Random.value < 0.3f;

        if (spawnHamburguer && spawnedHamburguers < totalHamburguers)
        {
            Spawn(hamburguerPrefab);
            spawnedHamburguers++;
        }
        else if (spawnedFruits < totalFruits)
        {
            Spawn(fruitPrefab);
            spawnedFruits++;
        }
    }

    void Spawn(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, canvasRect);
        RectTransform rt = obj.GetComponent<RectTransform>();

        float x = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        rt.anchoredPosition = new Vector2(x, canvasRect.rect.height / 2 + 50);
    }
}

