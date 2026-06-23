using UnityEngine;

/// <summary>
/// FoodClickDetector — Anexa ao GameObject filho que tem o Collider2D da comida.
/// Detecta cliques de rato E toques em mobile.
/// 
/// SETUP:
/// 1. No filho "FoodSprite" (ou onde está o Collider2D):
///    - Adiciona BoxCollider2D (ou CircleCollider2D)
///    - Adiciona este script
///    - Garante que a Camera tem Physics2DRaycaster para UI click events
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class FoodClickDetector : MonoBehaviour
{
    private FoodHole parentHole;

    void Awake()
    {
        // Procura o FoodHole no pai (ou no próprio)
        parentHole = GetComponentInParent<FoodHole>();
        if (parentHole == null)
            Debug.LogWarning("[FoodClickDetector] Não encontrou FoodHole no pai de " + gameObject.name);
    }

    void OnMouseDown()
    {
        if (parentHole != null)
            parentHole.OnClicked();
    }
}