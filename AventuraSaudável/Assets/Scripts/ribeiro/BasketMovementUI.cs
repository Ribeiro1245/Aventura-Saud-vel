using UnityEngine;

public class BasketMovementUI : MonoBehaviour
{
    public float speed = 600f;
    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rt.anchoredPosition += new Vector2(move * speed * Time.deltaTime, 0);
    }
}
