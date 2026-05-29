using UnityEngine;

public class UICollision : MonoBehaviour
{
    public RectTransform basket;
    public GameManagerUI gm;

    void Update()
    {
        // Apanhar frutas
        foreach (FruitUI fruit in Object.FindObjectsByType<FruitUI>(FindObjectsSortMode.None))
        {
            RectTransform f = fruit.GetComponent<RectTransform>();

            if (fruit.CompareTag("Fruit") && RectOverlaps(basket, f))
            {
                gm.AddScore(1);
                Destroy(fruit.gameObject);
            }
        }

        // Apanhar hambúrgueres
        foreach (HamburguerUI hamb in Object.FindObjectsByType<HamburguerUI>(FindObjectsSortMode.None))
        {
            RectTransform h = hamb.GetComponent<RectTransform>();

            if (hamb.CompareTag("Hamburguer") && RectOverlaps(basket, h))
            {
                gm.LosePoints(2);
                Destroy(hamb.gameObject);
            }
        }
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
{
    Vector3[] aCorners = new Vector3[4];
    Vector3[] bCorners = new Vector3[4];

    a.GetWorldCorners(aCorners);
    b.GetWorldCorners(bCorners);

    Rect aRect = new Rect(aCorners[0], aCorners[2] - aCorners[0]);
    Rect bRect = new Rect(bCorners[0], bCorners[2] - bCorners[0]);

    return aRect.Overlaps(bRect);
}

}
