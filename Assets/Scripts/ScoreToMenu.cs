using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreToMenu : MonoBehaviour {

    //Global variables
    public static int score;
    public static int highest;

    //Local variables
    public Text scoreText;
    public Text highestText;

    private void Start()
    {
        //If first boot, initialise values, if not, skip
        if (score == null)
            score = 0;
        if (highest == null)
            highest = 0;

        //If the text object has been passed and created fine, print scores
        if (scoreText != null)
        {
            scoreText.text += score;
        }
        if (highestText != null)
        {
            highestText.text += highest;
        }
    }
    //Setter function for the global variables
    public void setScores(int s, int h) { score = s; highest = h; }
}
