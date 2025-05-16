using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
        Debug.Log("Puntos sumados: " + points + ", nuevo puntaje: " + score);
    }
}
