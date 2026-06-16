using UnityEngine;
using UnityEngine.SceneManagement;

public class JogarNovamente : MonoBehaviour
{
    public void Recomeçar()
    {
        Debug.Log("Nivel guardado: " + GestorNivel.ultimoNivel);

        SceneManager.LoadScene(GestorNivel.ultimoNivel);
    }
}