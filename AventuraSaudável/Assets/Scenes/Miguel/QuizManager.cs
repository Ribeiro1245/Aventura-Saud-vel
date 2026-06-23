using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// SCENE DO QUIZ — os textos/perguntas já estão feitos na scene.
/// Só precisas de indicar o índice da resposta correcta para cada pergunta.
/// </summary>
public class QuizManager : MonoBehaviour
{
    [Header("Botões de Resposta (ordem A, B, C, D)")]
    public Button[] botoesResposta;

    [Header("Resposta Correcta por Pergunta")]
    [Tooltip("Coloca aqui o índice do botão correcto para cada pergunta (0=A, 1=B, 2=C, 3=D)")]
    public int[] respostasCorrectas;   // tamanho = nº de perguntas

    [Header("Feedback Visual")]
    public Color corCorreta = new Color(0.2f, 0.8f, 0.2f);
    public Color corErrada = new Color(0.9f, 0.2f, 0.2f);
    public Color corNormal = new Color(0.9f, 0.9f, 0.9f);
    public float tempFeedback = 1.0f;

    [Header("Navegação")]
    [Tooltip("Nome exacto da scene de resultados no Build Settings")]
    public string nomeSceneResultados = "Resultados";

    private int indicePerguntaAtual = 0;
    private int totalAcertos = 0;
    private bool aAguardar = false;

    // -------------------------------------------------------
    void Start()
    {
        for (int i = 0; i < botoesResposta.Length; i++)
        {
            int idx = i;
            botoesResposta[i].onClick.AddListener(() => ResponderPergunta(idx));
        }

        AtivarBotoes();
    }

    // -------------------------------------------------------
    void AtivarBotoes()
    {
        for (int i = 0; i < botoesResposta.Length; i++)
        {
            botoesResposta[i].interactable = true;
            SetCorBotao(i, corNormal);
        }
    }

    // -------------------------------------------------------
    void ResponderPergunta(int indiceEscolhido)
    {
        if (aAguardar) return;

        int correcto = respostasCorrectas[indicePerguntaAtual];
        bool acertou = (indiceEscolhido == correcto);
        if (acertou) totalAcertos++;

        for (int i = 0; i < botoesResposta.Length; i++)
        {
            botoesResposta[i].interactable = false;
            if (i == correcto) SetCorBotao(i, corCorreta);
            else if (i == indiceEscolhido) SetCorBotao(i, corErrada);
        }

        StartCoroutine(AvancarDepoisDeFeedback());
    }

    IEnumerator AvancarDepoisDeFeedback()
    {
        aAguardar = true;
        yield return new WaitForSeconds(tempFeedback);
        aAguardar = false;

        indicePerguntaAtual++;

        if (indicePerguntaAtual >= respostasCorrectas.Length)
            IrParaResultados();
        else
            AtivarBotoes();
    }

    // -------------------------------------------------------
    void IrParaResultados()
    {
        QuizDados.acertos = totalAcertos;
        QuizDados.totalPerguntas = respostasCorrectas.Length;

        SceneManager.LoadScene(nomeSceneResultados);
    }

    // -------------------------------------------------------
    void SetCorBotao(int i, Color cor)
    {
        ColorBlock cb = botoesResposta[i].colors;
        cb.normalColor = cor;
        cb.highlightedColor = cor;
        cb.pressedColor = cor;
        cb.selectedColor = cor;
        botoesResposta[i].colors = cb;
    }
}