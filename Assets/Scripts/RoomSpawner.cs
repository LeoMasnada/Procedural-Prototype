using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    public int openingDrirection;
    /**
     * 1: need bottom connection
     * 2: need left connection
     * 3: need top connection
     * 4: need right connection
     */
    
    //Holder made in the editor that owns all the possible rooms sorted by exit available in 4 arrays
    private Holder holder;
    public Transform parent;

    //Local variable for the ID of the room to spawn
    private int rand;
    
    //Boolean flag to see if a spawner has already generated a room or not
    public bool hasSpawnedFlag = false;

    //Time before the spawner self destructs to save some ressources after the generation phase is done
    private float waitToSelfDestruct = 2.0f;

    private void Start()
    {
        //If the spawner has spawned at the start point, sets life time to 0 for immediate destruction
        if (transform.position.x == 0 && transform.position.y == 0)
            waitToSelfDestruct = 0f;

        //Set of the self destruction
        Destroy(gameObject, waitToSelfDestruct);

        //Grabs the script that holds all the rooms sorted by available doors
        holder = GameObject.FindGameObjectWithTag("Holder").GetComponent<Holder>();
        parent = holder.roomParent;

        //Calls in the spawn function with a delay of a 40th of the time to self destructs
        Invoke("Spawn", waitToSelfDestruct/40);
    }

    private void Spawn()
    {

        if (!hasSpawnedFlag) {
            //Checks what direction is the current spawner is at and generates a compatible room according to it
            switch (openingDrirection)
            {
                case 1:
                    //need bottom
                    rand = Random.Range(0, holder.B.Length);
                    Instantiate(holder.B[rand], transform.position, holder.B[rand].transform.rotation,parent);
                    break;
                case 2:
                    //need left
                    rand = Random.Range(0, holder.L.Length);
                    Instantiate(holder.L[rand], transform.position, holder.L[rand].transform.rotation, parent);
                    break;
                case 3:
                    //need top
                    rand = Random.Range(0, holder.T.Length);
                    Instantiate(holder.T[rand], transform.position, holder.T[rand].transform.rotation, parent);
                    break;
                case 4:
                    //need right
                    rand = Random.Range(0, holder.R.Length);
                    Instantiate(holder.R[rand], transform.position, holder.R[rand].transform.rotation, parent);
                    break;

                default:
                    break;
            }
            //Raising of the flag to notify that a room was generated
            hasSpawnedFlag = true;
        }
    }

    //If two spawners are colliding at the same spot, deals with colliding by denying both of them and forcing a full room to be generated
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If the spawnpoint detects another one spawning on top of him
        try
        {
            if (other.CompareTag("SpawnPoint"))
            {
                //If the detected spawnpoint didn't do anything yet and the current either
                if (!other.GetComponent<RoomSpawner>().hasSpawnedFlag && !hasSpawnedFlag)
                {
                    //Creates full room and destroys itself
                    Instantiate(holder.closedRoom, transform.position, holder.closedRoom.transform.rotation, parent);
                    Destroy(gameObject);
                }
                //Notifies that a room has been created at this location
                hasSpawnedFlag = true;
            }
        }
        catch
        {
            //Catch an error if needed
        }
    }
}
