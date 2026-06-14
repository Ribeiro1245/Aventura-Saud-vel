using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuizBotao : MonoBehaviour
{
    public bool respostaCorreta;
    public string proximaCena;

    private Image imagemBotao;

    public static int pontos = 0;

    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "1QuizzgrupoAlimentarCarla")
    {
        pontos = 0;
    }

        imagemBotao = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(Clicar);
    }

    void Clicar()
    {
        if (respostaCorreta)
        {
            imagemBotao.color = Color.green;
            pontos++;
        }
        else
        {
            imagemBotao.color = Color.red;
        }

        StartCoroutine(IrParaProximaCena());
    }

    IEnumerator IrParaProximaCena()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(proximaCena);
    }
}