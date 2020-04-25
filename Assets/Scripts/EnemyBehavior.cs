using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    //Links to holder and player's location
    private Holder holder;
    private Transform playerTransform;

    //Prefab to use when creating a bullet
    public Bullet bulletTemplate;

    //ID to differenciate the behaviors
    public int aiType;

    //Local variables proper to each enemy
    private float cooldown;
    private int health;

    //Sound objects
    private AudioSource aSource;
    public AudioClip clip;

    private void Start()
    {
        //Initialization of the variables and fetching the right objects
        health = 50;

        holder = GameObject.FindGameObjectWithTag("Holder").GetComponent<Holder>();
        playerTransform = holder.player.transform;
        cooldown = 1f;

        //Fetch the audio source on the game object
        aSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Vector2 of where to face at
        Vector2 faceTo = new Vector2(playerTransform.position.x - this.transform.position.x, playerTransform.position.y - this.transform.position.y);

        //Verifies that the player is in the same room, if not aborts the script step
        float dist = Vector2.Distance(this.transform.parent.position, playerTransform.position);
        if (dist >= 5) return;

        //Applies rotation to transform
        transform.up = faceTo;

        //Depending on the AI ID, applies the set behavior
        switch (aiType)
        {
            //For the triangle
            case 0:
                //When the cooldown reached back 0, shoots and sets the cooldown back to 1 second
                if (cooldown <= 0)
                {
                    cooldown = 1f;
                    Instantiate(bulletTemplate, Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(playerTransform.position.x, playerTransform.position.y), 0.35f), transform.rotation, holder.bulletParent);
                }
                cooldown -= Time.deltaTime;
                break;
            
            //For the square
            case 1:
                //moves the sprite towards the player
                Vector3 newPos = this.transform.position + transform.up * Time.deltaTime;
                this.transform.SetPositionAndRotation(newPos, this.transform.rotation);
                break;

            default:
                break;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Plays a sound on taking damage
        aSource.PlayOneShot(clip, 0.3f);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(20);
            Destroy(this.gameObject);
        }
    }

    //callback function to take damage and destroy object on death
    public void TakeDamage(int damage)
    {
        try
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(this.gameObject);
                holder.player.GetComponent<PlayerController>().addScore(10);
            }
        }
        catch
        {

        }

    }
}
