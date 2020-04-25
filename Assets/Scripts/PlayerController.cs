using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
    //Movement related objects
    public float mvntSpeed = 2f;
    private Rigidbody2D body;

    //Orientation related objects
    private Vector3 mousePos;
    private Vector2 transform2d;

    //Actions related objects
    public GameObject bullet;
    private Holder holder;
    private Transform bulletParent;
    private AudioSource aSource;
    public AudioClip shoot;

    private static int health;
    public static int highestScore;
    public static int score;

    public static int level = 1;

	// Use this for initialization
	void Start () {
        //Fetch rigidbody item to save accessing time later
        body = gameObject.GetComponent<Rigidbody2D>();

        //Fetch the audio source on the game object
        aSource = gameObject.GetComponent<AudioSource>();

        //Gets the holder script and the object we're interested into that is inside of it
        holder = GameObject.FindGameObjectWithTag("Holder").GetComponent<Holder>();
        bulletParent = holder.bulletParent;
        

        health = 100;

        if (score == null)
            score = 0;

        if (highestScore ==null)
            highestScore = score;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(level);
        //Stores the 2D coordinates into a Vector2 object for simpler usage
        transform2d = new Vector2(this.transform.position.x, this.transform.position.y);

        if (Input.GetKey(KeyCode.LeftShift))
            mvntSpeed = 4;
        else
            mvntSpeed = 2;
        
        //Moves Player according to acceleration from the inputs
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*mvntSpeed, Input.GetAxis("Vertical")*mvntSpeed);

        //Vector3 of mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 of where to face at
        Vector2 faceTo = new Vector2(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);

        //Applies rotation to transform
        transform.up = faceTo;
        
        if (Input.GetMouseButtonDown(1))
        {
            //Creates a bullet turned in the same direction as the player at a set radius of distance
            // 0.35 = radius of the player + radius of the bullet sprite + margin
            Instantiate(bullet, Vector2.MoveTowards(transform2d, mousePos, 0.35f) , transform.rotation, bulletParent);

            //Plays a sound on shoot action
            aSource.PlayOneShot(shoot, 0.3f);
            
        }

        this.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, (float)health / 100);

        holder.scoreText.text = "Score: " + score;
        if (score > highestScore)
            highestScore = score;

        if (score >= 200 * level)
        {
            level++;
            holder.levelUpText.GetComponent<AlwaysFade>().LevelUp();
        }

        holder.scores.setScores(score, highestScore);

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            score -= 10;
            SceneManager.LoadScene(1);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //If player enters new room
        if (other.tag == "Room")
        { 
            //Camera snaps to new room
            Camera.main.gameObject.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -10);
        }
        
    }

    public void TakeDamage(int damage)
    {
        
        health -= damage;
        Debug.Log("Health left: " + health);
        if (health <= 0)
        {
            GetComponent<PlayerController>().enabled = false;
            score -= 50 * level;
            SceneManager.LoadScene(1);
        }
    }

    public int getLevel() { return level; }

    public void addScore(int val) { score += val; }

    public void pickUpHP(int val) { health += val; if (health > 100) health = 100; }
}
