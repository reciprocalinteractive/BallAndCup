using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GUIText accelerationText;
    public GUIText scoreText;
    public GUIText highScoreText;
    public Accelerometer accelerometer;
    public float updateInterval = 0.1f;

    public Transform thrownObject;

    public int score;

    private int height;
    private int maxHeight;
    private int highScore;

    private void Awake()
    {
        maxHeight = 0;
        score = 0;

        if (!PlayerPrefs.HasKey("highScore"))
        {
            highScore = 0;
        }
        else
        {
            highScore = PlayerPrefs.GetInt("highScore");
        }

        highScoreText.text = "High Score: " + highScore;
        UpdateScore(score);
        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        accelerationText.text =
            "Acceleration.x = " + accelerometer.FilteredX +
            "\nAcceleration.y = " + accelerometer.FilteredY +
            "\nAcceleration.z = " + accelerometer.FilteredZ;

        yield return new WaitForSeconds(updateInterval);
    }

    void UpdateScore(int newScore)
    {
        score += newScore;
        scoreText.text = "Score: " + score;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void SetScore()
    {
        height = Mathf.RoundToInt(thrownObject.position.y) + 5;
        if (height > maxHeight)
        {
            maxHeight = height;
        }

        UpdateScore(maxHeight);

        Debug.Log("Object Position: " + thrownObject.position.y);
        Debug.Log("Height: " + height);
        Debug.Log("Max Height: " + maxHeight);

    }
}
