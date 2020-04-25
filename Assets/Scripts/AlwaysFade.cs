using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlwaysFade : MonoBehaviour {

    //Original color of the text object
    private Color color;

    //Text object notifying level up
    private Text text;

	// Use this for initialization
	void Start ()
    {
        //Initialization of the variables
        text = this.GetComponent<Text>();
        color = text.color;
	}
	
	// Update is called once per frame
	void Update () {
        //Forces alpha of the material to decrease until reaching 0
        if (color.a > 0)
            color.a -= 0.005f;
        text.color = new Color(color.r, color.g, color.b, color.a);
	}

    //Accessor to make the text visible again
    public void LevelUp()
    {
        color.a = 1;
    }
}
