using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    private Holder holder;
    private Transform playerTransform;

    public Bullet bulletTemplate;

    public int aiType;

    private float cooldown;

    private int health;


    private AudioSource aSource;
    public AudioClip clip;

    private void Start()
    {
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

        float dist = Vector2.Distance(this.transform.parent.position, playerTransform.position);
        if (dist >= 5) return;

        //Applies rotation to transform
        transform.up = faceTo;

        switch (aiType)
        {
            case 0:
                if (cooldown <= 0)
                {
                    cooldown = 1f;
                    Instantiate(bulletTemplate, Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(playerTransform.position.x, playerTransform.position.y), 0.35f), transform.rotation, holder.bulletParent);
                }
                cooldown -= Time.deltaTime;
                break;

            case 1:
                Vector3 newPos = this.transform.position + transform.up * Time.deltaTime;
                this.transform.SetPositionAndRotation(newPos, this.transform.rotation);
                break;

            default:
                break;
        }
        

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Plays a sound on shoot action
        aSource.PlayOneShot(clip, 0.3f);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(20);
            Destroy(this.gameObject);
        }
    }

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
