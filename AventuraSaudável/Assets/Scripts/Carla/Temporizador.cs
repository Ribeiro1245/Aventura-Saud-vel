using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    public float tempo = 15f;
    public TMP_Text textoTempo;

    void Start()
    {
        GestorNivel.ultimoNivel =
        SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        tempo -= Time.deltaTime;

        textoTempo.text =
        Mathf.Ceil(tempo).ToString();

        if (tempo <= 0)
        {
            SceneManager.LoadScene("PerdeuCarla");
        }
    }
}