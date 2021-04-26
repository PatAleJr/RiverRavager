using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score scoreScript;

    private void Awake()
    {
        if (scoreScript != null && scoreScript != this)
        {
            Destroy(scoreScript);
        }

        scoreScript = this;
    }

    public int score;
    public float score_f;
    public int highScore;
    public float scorePerTime = 10f;

    public int digitsToDisplay = 6;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI scoreText_go;  //In game over screen
    public TextMeshProUGUI hiscoreText_go;  //In game over screen

    public bool playing = false;

    public void startPlaying()
    {
        playing = true;
        score = 0;
        score_f = 0.0f;
        scoreText.text = score.ToString();
    }

    public void gameOver()
    {
        playing = false;
        updateStats();
    }

    public void reset()
    {
        score = 0;
        score_f = 0.0f;
        scoreText.text = getNumberWithZeros(score);
    }

    void Update()
    {
        if (playing)
            incrementScore(scorePerTime * Time.deltaTime);
    }

    public void incrementScore(float toIncrement)
    {
        score_f += toIncrement;
        score = Mathf.FloorToInt(score_f);
        
        scoreText.text = getNumberWithZeros(score);
    }

    void updateStats()
    {
        if (score > highScore)
        {
            highScore = score;
        }

        scoreText_go.text = "Score: " + getNumberWithZeros(score);
        hiscoreText_go.text = "High Score: " + getNumberWithZeros(highScore);
    }

    public string getNumberWithZeros(int n)
    {
        int digits = (int)Mathf.Log10(n) + 1;
        int zerosToDisplay = digitsToDisplay - digits;

        string s = "";
        for (int i = 0; i < zerosToDisplay; i++)
            s += "0";
        s += n.ToString();

        return s;
    }
}
