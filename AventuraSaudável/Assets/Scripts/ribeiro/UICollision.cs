using UnityEngine;

public class UICollision : MonoBehaviour
{
    public RectTransform basket;
    public GameManagerUI gm;

    void Update()
    {
        foreach (FruitUI fruit in Object.FindObjectsByType<FruitUI>(FindObjectsSortMode.None))
        {
            RectTransform f = fruit.GetComponent<RectTransform>();

            if (RectOverlaps(basket, f))
            {
                gm.AddScore(1);
                Destroy(fruit.gameObject);
            }
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(a, b.position);
    }
}
