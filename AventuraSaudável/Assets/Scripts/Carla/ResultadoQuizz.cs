using TMPro;
using UnityEngine;

public class ResultadoQuiz : MonoBehaviour
{
    public TMP_Text textoResultado;

    void Start()
    {
        textoResultado.text =  QuizBotao.pontos + " de 3";
    }
}