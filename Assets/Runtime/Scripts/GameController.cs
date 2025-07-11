using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    // Score 
    public TextMeshProUGUI scoreText;
    private int score = 0;

    // Updates the score and UI 
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        ScoreView.Instance.VisualizeLikes(1);
    }
    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString();
        ScoreView.Instance.VisualizeLikes(addScore);
    }

    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    // Reload the current scene 
    public void ReloadLevel() {   SceneManager.LoadScene(SceneManager.GetActiveScene().name);}
}
