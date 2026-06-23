using System.Collections;
using UnityEngine;

/// <summary>
/// FoodHole — Controla um buraco onde a comida aparece e desaparece.
///
/// SETUP POR BURACO:
///   1. GameObject vazio "FoodHole_X"  →  adiciona FoodHole.cs
///   2. Filho "FoodSprite"             →  SpriteRenderer + CircleCollider2D + FoodClickDetector.cs
///   3. (Opcional) Filho "HoleSprite"  →  SpriteRenderer decorativo do buraco/prato
///
/// IMPORTANTE:
///   • O CircleCollider2D no FoodSprite deve ter "Is Trigger" = FALSE
///   • A Main Camera precisa de ter o componente Physics2DRaycaster
///     (Add Component → Physics 2D Raycaster) para cliques 2D funcionarem
/// </summary>
public class FoodHole : MonoBehaviour
{
    [Header("Referências")]
    public SpriteRenderer foodSprite;   // arrasta o filho FoodSprite aqui

    [Header("Sprites — Comidas Boas")]
    public Sprite[] goodFoodSprites;

    [Header("Sprites — Comidas Más")]
    public Sprite[] badFoodSprites;

    [Header("Animação")]
    public float popUpSpeed = 8f;
    public float popDownSpeed = 12f;
    public float popUpHeight = 1f;     // unidades que a comida sobe

    [Header("Cores de aviso")]
    public Color goodTint = Color.white;
    public Color badTint = new Color(1f, 0.45f, 0.45f);

    // ── Estado ──────────────────────────────────
    public bool IsActive { get; private set; }
    public bool IsBadFood { get; private set; }   // público para FoodClickDetector
    private bool wasClicked;

    // ── Posições ────────────────────────────────
    private Vector3 hiddenPos;
    private Vector3 shownPos;

    // ── Coroutine ───────────────────────────────
    private Coroutine activeCoroutine;

    // ════════════════════════════════════════════
    //  UNITY
    // ════════════════════════════════════════════

    void Awake()
    {
        if (foodSprite == null)
            foodSprite = GetComponentInChildren<SpriteRenderer>();

        hiddenPos = transform.position;
        shownPos = hiddenPos + Vector3.up * popUpHeight;

        if (foodSprite) foodSprite.enabled = false;
        IsActive = false;
    }

    // ════════════════════════════════════════════
    //  API PÚBLICA
    // ════════════════════════════════════════════

    public void Show(bool bad, float showDuration)
    {
        if (IsActive) return;

        IsBadFood = bad;
        wasClicked = false;
        IsActive = true;

        Sprite[] pool = bad ? badFoodSprites : goodFoodSprites;
        if (foodSprite != null)
        {
            if (pool != null && pool.Length > 0)
                foodSprite.sprite = pool[Random.Range(0, pool.Length)];

            foodSprite.color = bad ? badTint : goodTint;
            foodSprite.enabled = true;
        }

        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(ShowRoutine(showDuration));
    }

    public void ForceHide()
    {
        if (activeCoroutine != null) { StopCoroutine(activeCoroutine); activeCoroutine = null; }
        Deactivate();
    }

    /// <summary>
    /// Chamado pelo FoodClickDetector quando o jogador clica nesta comida.
    /// </summary>
    public void OnClicked()
    {
        // Protecções
        if (!IsActive) { Debug.Log("[FoodHole] OnClicked ignorado — não está activo"); return; }
        if (wasClicked) { Debug.Log("[FoodHole] OnClicked ignorado — já foi clicado"); return; }
        if (!GameManager.Instance.gameRunning) { Debug.Log("[FoodHole] Jogo não está a correr"); return; }

        wasClicked = true;

        Debug.Log($"[FoodHole] Clicado! IsBadFood={IsBadFood}");

        if (IsBadFood)
            GameManager.Instance.OnBadFoodClicked(transform.position);
        else
            GameManager.Instance.OnGoodFoodClicked(transform.position);

        // Desce logo
        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(HideRoutine());
    }

    // ════════════════════════════════════════════
    //  COROUTINES
    // ════════════════════════════════════════════

    IEnumerator ShowRoutine(float duration)
    {
        yield return MoveTo(shownPos, popUpSpeed);

        float elapsed = 0f;
        while (elapsed < duration && !wasClicked)
        {
            elapsed += Time.deltaTime;
            // Balanceio subtil
            float wobble = Mathf.Sin(elapsed * 4f) * 0.025f;
            transform.position = shownPos + Vector3.right * wobble;
            yield return null;
        }

        if (!wasClicked)
        {
            GameManager.Instance.OnFoodExpired();
            transform.position = shownPos;
            yield return MoveTo(hiddenPos, popDownSpeed);
            Deactivate();
        }
    }

    IEnumerator HideRoutine()
    {
        yield return MoveTo(hiddenPos, popDownSpeed);
        Deactivate();
    }

    IEnumerator MoveTo(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.005f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }

    void Deactivate()
    {
        if (foodSprite) foodSprite.enabled = false;
        transform.position = hiddenPos;
        IsActive = false;
        wasClicked = false;
        activeCoroutine = null;
    }
}