using UnityEngine;

public class HamburguerUI : MonoBehaviour
{
    public float speed = 300f;
    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        rt.anchoredPosition += Vector2.down * speed * Time.deltaTime;

        if (rt.anchoredPosition.y < -600f)
            Destroy(gameObject);
    }
}
