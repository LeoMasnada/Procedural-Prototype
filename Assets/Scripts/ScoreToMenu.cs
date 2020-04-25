using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreToMenu : MonoBehaviour {

    public static int score;
    public static int highest;

    public Text scoreText;
    public Text highestText;

    private void Start()
    {
        if (score == null)
            score = 0;
        if (highest == null)
            highest = 0;

        if (scoreText != null)
        {
            scoreText.text += score;
        }
        if (highestText != null)
        {
            highestText.text += highest;
        }
    }

    public void setScores(int s, int h) { score = s; highest = h; }
}
