using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// SCENE DE RESULTADOS — lê os dados do QuizDados e mostra o resultado final.
/// Attach este script a um GameObject na scene "Resultados".
/// </summary>
public class ResultadosManager : MonoBehaviour
{
    [Header("UI - Resultados")]
    public TextMeshProUGUI textoAcertos;        // "Acertaste X de Y perguntas!"
    public TextMeshProUGUI textoClassificacao;  // "Excelente!", etc.
    public TextMeshProUGUI textoPercentagem;    // "80%"

    [Header("Botões")]
    public Button botaoJogarNovamente;
    public Button botaoMenuPrincipal;

    [Header("Navegação")]
    [Tooltip("Nome exacto da scene do quiz no Build Settings")]
    public string nomeSceneQuiz = "Quiz";
    [Tooltip("Nome exacto da scene do menu principal (opcional)")]
    public string nomeSceneMenu = "Menu";

    // -------------------------------------------------------
    void Start()
    {
        MostrarResultado();

        botaoJogarNovamente.onClick.AddListener(() =>
            SceneManager.LoadScene(nomeSceneQuiz));

        if (botaoMenuPrincipal != null)
            botaoMenuPrincipal.onClick.AddListener(() =>
                SceneManager.LoadScene(nomeSceneMenu));
    }

    // -------------------------------------------------------
    void MostrarResultado()
    {
        int acertos = QuizDados.acertos;
        int total = QuizDados.totalPerguntas;

        // Protecção caso a scene seja carregada directamente no editor
        if (total == 0)
        {
            textoAcertos.text = "Sem dados de quiz.";
            textoClassificacao.text = "";
            textoPercentagem.text = "";
            return;
        }

        float pct = (float)acertos / total * 100f;

        textoAcertos.text = $"Parabéns!\nAcertaste: {acertos} de {total}";
        textoPercentagem.text = $"{Mathf.RoundToInt(pct)}%";

        string classificacao;
        if (pct == 100) classificacao = "🏆 Perfeito!";
        else if (pct >= 80) classificacao = "⭐ Excelente!";
        else if (pct >= 60) classificacao = "👍 Bom trabalho!";
        else if (pct >= 40) classificacao = "📚 Podes melhorar!";
        else classificacao = "😅 Tenta outra vez!";

        textoClassificacao.text = classificacao;
    }
}
