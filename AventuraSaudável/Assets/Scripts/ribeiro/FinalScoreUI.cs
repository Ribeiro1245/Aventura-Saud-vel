using UnityEngine;
using TMPro;

public class FinalScoreUI : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "Pontuação Final: " + score;
    }
}
