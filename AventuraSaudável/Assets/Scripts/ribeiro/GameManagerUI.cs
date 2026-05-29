using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerUI : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;

    public int totalFruits = 10;   // número total de frutas que vão cair
    public int caughtFruits = 0;   // frutas apanhadas

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        caughtFruits++;
        UpdateUI();

        if (caughtFruits >= totalFruits)
            SceneManager.LoadScene("Game Win");
    }


    public void LoseLife()
    {
         Debug.Log("LoseLife FOI CHAMADO!");
        lives--;
        UpdateUI();

        if (lives <= 0)
            SceneManager.LoadScene("Game Over");
    }

    public void LosePoints(int amount)
{
    score -= amount;
    if (score < 0) score = 0;
    UpdateUI();
}
    void UpdateUI()
    {
        scoreText.text = "Pontos: " + score;
        livesText.text = "Vidas: " + lives;
    }
}
