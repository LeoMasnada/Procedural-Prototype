using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateRoom : MonoBehaviour {

    private float dist;
    private Holder holder;
    private Transform playerTransform;
    private bool hasSpawned;
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
        this.dist = Vector2.Distance(this.transform.position, playerTransform.position);

        level = holder.player.GetComponent<PlayerController>().getLevel();


        if (dist < 5 && !hasSpawned)
        {
            float x, y;
            for(int i = 0; i < level; i++)
            {
                
                x = y = 0;
                x = Random.Range(this.transform.position.x - 4, this.transform.position.x + 4);
                y = Random.Range(this.transform.position.y - 4, this.transform.position.y + 4);
                Instantiate(holder.turretEnemy,new Vector3(x,y),Quaternion.identity, this.transform);


                x = y = 0;
                x = Random.Range(this.transform.position.x - 4, this.transform.position.x + 4);
                y = Random.Range(this.transform.position.y - 4, this.transform.position.y + 4);
                Instantiate(holder.followEnemy, new Vector3(x, y), Quaternion.identity, this.transform);
                hasSpawned = true;


            }

            int pickup= Random.Range(0, 10);
            Debug.Log(pickup);
            if (pickup >= 9)
            {
                x = Random.Range(this.transform.position.x - 4, this.transform.position.x + 4);
                y = Random.Range(this.transform.position.y - 4, this.transform.position.y + 4);
                Instantiate(holder.pickup, new Vector3(x, y), Quaternion.identity, this.transform);
            }

        }
	}
}
