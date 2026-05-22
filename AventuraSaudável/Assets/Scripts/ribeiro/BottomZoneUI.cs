using UnityEngine;

public class BottomZoneUI : MonoBehaviour
{
    public GameManagerUI gm;
    public RectTransform canvasRect;

    void Update()
    {
        // percorre todas as frutas ativas
        foreach (var fruit in GameObject.FindGameObjectsWithTag("Fruit"))
        {
            RectTransform rt = fruit.GetComponent<RectTransform>();
            if (rt == null) continue;

            // ignorar frutas que ainda estão a nascer (acima do topo do canvas)
            if (rt.anchoredPosition.y > canvasRect.rect.height / 2 + 10f)
                continue;

            // fruta perdida: passou abaixo do fundo do canvas
            if (rt.anchoredPosition.y < -canvasRect.rect.height / 2 - 10f)
            {
                Debug.Log("Fruta perdida!");
                gm.LoseLife();
                Destroy(fruit);
            }
        }
    }
}
