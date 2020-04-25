using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlwaysFade : MonoBehaviour {

    private Color color;
    private Text text;

	// Use this for initialization
	void Start ()
    {
        text = this.GetComponent<Text>();
        color = text.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (color.a > 0)
            color.a -= 0.005f;
        text.color = new Color(color.r, color.g, color.b, color.a);
	}

    public void LevelUp()
    {
        color.a = 1;
    }
}
