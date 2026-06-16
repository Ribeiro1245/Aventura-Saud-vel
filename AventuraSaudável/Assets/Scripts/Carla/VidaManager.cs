using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaManager : MonoBehaviour
{
    public Image imagemVida;

    public Sprite vida0;
    public Sprite vida1;
    public Sprite vida2;
    public Sprite vida3;
    public Sprite vida4;
    public Sprite vida5;
    public Sprite vida6;

    public int vidaAtual = 0;

    void Start()
    {
        AtualizarVida();
    }

    public void GanharVida()
    {
        if (vidaAtual < 6)
        {
            vidaAtual++;
            AtualizarVida();

            if (vidaAtual == 6)
            {
                SceneManager.LoadScene("GanhouCarla");
            }
        }
    }

    public void PerderVida()
    {
        if (vidaAtual > 0)
        {
            vidaAtual--;
            AtualizarVida();

            if (vidaAtual == 0)
            {
                SceneManager.LoadScene("PerdeuCarla");
            }
        }
    }

    void AtualizarVida()
    {
        switch (vidaAtual)
        {
            case 0:
                imagemVida.sprite = vida0;
                break;

            case 1:
                imagemVida.sprite = vida1;
                break;

            case 2:
                imagemVida.sprite = vida2;
                break;

            case 3:
                imagemVida.sprite = vida3;
                break;

            case 4:
                imagemVida.sprite = vida4;
                break;

            case 5:
                imagemVida.sprite = vida5;
                break;

            case 6:
                imagemVida.sprite = vida6;
                break;
        }
    }
}