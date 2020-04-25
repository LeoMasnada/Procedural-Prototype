using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateRoom : MonoBehaviour {

    //Distance between player and center of the room
    private float dist;

    //Holder object
    private Holder holder;

    //Accessor to player's position
    private Transform playerTransform;

    //Flag to avoid respawn
    private bool hasSpawned;

    //Level of difficulty
    private int level;

	// Use this for initialization
	void Start () {
        this.dist = 10;
        this.hasSpawned = false;

        holder = GameObject.FindGameObjectWithTag("Holder").GetComponent<Holder>();
        playerTransform = holder.player.transform;
    }
	
	// Update is called once per frame
	void Update() {
        //Calculates distance between player and room's center
        this.dist = Vector2.Distance(this.transform.position, playerTransform.position);

        //Updates the level value to player's current level
        level = holder.player.GetComponent<PlayerController>().getLevel();

        //If the player enters a room and it hasn't already spawned enemies
        if (dist < 5 && !hasSpawned)
        {
            float x, y;
            //For each level passed
            for(int i = 0; i < level; i++)
            {
                //Selects a random location in the room and spawns a triangle enemy
                x = y = 0;
                x = Random.Range(this.transform.position.x - 4, this.transform.position.x + 4);
                y = Random.Range(this.transform.position.y - 4, this.transform.position.y + 4);
                Instantiate(holder.turretEnemy,new Vector3(x,y),Quaternion.identity, this.transform);

                //Selects a new random location and spawns a square enemy
                x = y = 0;
                x = Random.Range(this.transform.position.x - 4, this.transform.position.x + 4);
                y = Random.Range(this.transform.position.y - 4, this.transform.position.y + 4);
                Instantiate(holder.followEnemy, new Vector3(x, y), Quaternion.identity, this.transform);
                hasSpawned = true;


            }

            //3 out of 10 chances to spawn a star in the level
            int pickup= Random.Range(0, 10);
            Debug.Log(pickup);
            if (pickup >= 7)
            {
                x = Random.Range(this.transform.position.x - 4, this.transform.position.x + 4);
                y = Random.Range(this.transform.position.y - 4, this.transform.position.y + 4);
                Instantiate(holder.pickup, new Vector3(x, y), Quaternion.identity, this.transform);
            }

        }
	}
}
