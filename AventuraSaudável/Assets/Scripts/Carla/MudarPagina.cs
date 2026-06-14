using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarPagina : MonoBehaviour
{
    public void IrParaPagina(string nomePagina)
    {
        SceneManager.LoadScene(nomePagina);
    }
}