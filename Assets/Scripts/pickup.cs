using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour {


    private AudioSource aSource;
    public AudioClip shoot;


    private void Start()
    {
        //Fetch the audio source on the game object
        aSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Plays a sound on shoot action
        

        if (other.gameObject.tag == "Player")
        {
            aSource.PlayOneShot(shoot, 0.5f);
            other.gameObject.GetComponent<PlayerController>().pickUpHP(50);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
