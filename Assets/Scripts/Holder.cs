using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Holder: MonoBehaviour {

    

    //Rooms
    public GameObject[] T;
    public GameObject[] R;
    public GameObject[] B;
    public GameObject[] L;

    public GameObject closedRoom;

    //Objects to attach new generated assets to in the hierarchy
    public Transform roomParent;
    public Transform bulletParent;

    //Useful links
    public GameObject player;
    public GameObject pickup;
    public GameObject turretEnemy;
    public GameObject followEnemy;

    public Text scoreText;
    public Text levelUpText;

    public ScoreToMenu scores;

}
