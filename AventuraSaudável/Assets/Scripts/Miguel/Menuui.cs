using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// MenuUI — Anexa ao Canvas do menu principal.
/// Gera os botões de dificuldade automaticamente ou usa os que existem na cena.
/// 
/// SETUP MANUAL (recomendado para aprender):
///   Cria 3 Buttons com texto "Fácil", "Médio", "Difícil"
///   No OnClick de cada botão chama GameManager.Instance.StartGame(0/1/2)
/// 
/// SETUP AUTOMÁTICO:
///   Anexa este script ao MenuCanvas e deixa ele criar os botões.
/// </summary>
public class MenuUI : MonoBehaviour
{
    [Header("Botões (opcional — se já existirem na cena)")]
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    [Header("Título")]
    public TextMeshProUGUI titleText;

    void Start()
    {
        if (titleText)
            titleText.text = "🍎 Apanha as Comidas! 🍔";

        // Liga os botões se foram atribuídos no Inspector
        if (easyButton) easyButton.onClick.AddListener(() => GameManager.Instance.StartGame(0));
        if (mediumButton) mediumButton.onClick.AddListener(() => GameManager.Instance.StartGame(1));
        if (hardButton) hardButton.onClick.AddListener(() => GameManager.Instance.StartGame(2));
    }
}