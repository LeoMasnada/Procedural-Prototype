using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody2D body;
    public float mvntSpeed = 2f;
    
    private Holder holder;
    private PlayerController playerStats;

    private void Start()
    {

        //Fetching of the rigidbody object for easier access
        body = this.gameObject.GetComponent<Rigidbody2D>();

        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update ()
    {
        //Movement directed towards the front of the sprite
        body.velocity = transform.up*mvntSpeed;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        
            
        if(tag == "Player")
        {
            playerStats.TakeDamage(10);
            Destroy(this.gameObject);
        }
        else if(tag == "Enemies")
        {
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(10);
            Destroy(this.gameObject);
        }
        if (collision.transform.parent && collision.transform.parent.tag == "Room")
        {
            Destroy(this.gameObject);
        }
    }
    
}
