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

    private bool respondeu = false;

    void Start()
    {
       if (SceneManager.GetActiveScene().name == "1QuizzgrupoAlimentarCarla")
{
    pontos = 0;
    PlayerPrefs.DeleteAll();
}
        imagemBotao = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(Clicar);
    }

   void Clicar()
{
    string perguntaAtual = SceneManager.GetActiveScene().name;

    if (PlayerPrefs.GetInt(perguntaAtual, 0) == 1)
        return;

    PlayerPrefs.SetInt(perguntaAtual, 1);

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