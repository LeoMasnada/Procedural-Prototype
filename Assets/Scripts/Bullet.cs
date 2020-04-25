using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //Link to body
    private Rigidbody2D body;

    //Speed of the bullet in pixels per frame
    public float mvntSpeed = 2f;
    
    //Links to holder and player's statistics for damage dealing
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
    //When player collides with an object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Copy of collided object's tag
        string tag = collision.gameObject.tag;
        
            
        //If the bullet hit the player, deals damage and despawns
        if(tag == "Player")
        {
            playerStats.TakeDamage(10);
            Destroy(this.gameObject);
        }
        //If the bullet hit an enemy, deals damage and despawns
        else if(tag == "Enemies")
        {
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(10);
            Destroy(this.gameObject);
        }
        //If the bullet hit an environment item, despawns
        if (collision.transform.parent && collision.transform.parent.tag == "Room")
        {
            Destroy(this.gameObject);
        }

        //Specifying all those cases mean bullets won't despawn if they touch another bullet or other cases not specified
    }
    
}
