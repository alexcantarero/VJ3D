using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    private int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("Record cargado: " + highScore);
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
        Debug.Log("Puntos sumados: " + points + ", nuevo puntaje: " + score);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save(); // Opcional, para asegurarte que se guarde inmediatamente
            Debug.Log("Nuevo récord: " + highScore);
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
