using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    public float tempo = 15f;
    public TMP_Text textoTempo;

    private bool acabou = false;

    void Update()
    {
        if (acabou)
            return;

        tempo -= Time.deltaTime;

        textoTempo.text =
        Mathf.Ceil(tempo).ToString();

        if (tempo <= 0)
        {
            acabou = true;

            string nivelAtual =
            SceneManager.GetActiveScene().name;

            if (nivelAtual.Contains("1"))
            {
                SceneManager.LoadScene("1PerdeuCarla");
            }
            else if (nivelAtual.Contains("2"))
            {
                SceneManager.LoadScene("2PerdeuCarla");
            }
            else if (nivelAtual.Contains("3"))
            {
                SceneManager.LoadScene("3PerdeuCarla");
            }
        }
    }
}