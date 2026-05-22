using UnityEngine;
using TMPro;

public class GameManagerUI : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
