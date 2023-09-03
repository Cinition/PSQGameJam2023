using System.Collections.Generic;
using UnityEngine;

public class CritterPickUp : MonoBehaviour
{
    GameObject[] critters;
    GameObject   player;
    GameObject   trashCan;

    public float trashRange;
    public float pickUpRange;
    private int  maxCrittersHeld = 5;
    private      List<GameObject> heldCritters = new List<GameObject>();
    public       ScoreManager scoreManager;

    void Start()
    {
        critters = GameObject.FindGameObjectsWithTag("Critter");
        player = GameObject.FindGameObjectWithTag("Player");
        trashCan = GameObject.FindGameObjectWithTag("Trash Can");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckCritters();
        }

        if (Input.GetKeyDown(KeyCode.Q) && heldCritters.Count > 0)
        {
            float distanceToTrashCan = Vector3.Distance(player.transform.position, trashCan.transform.position);
            if (distanceToTrashCan <= trashRange)
            {
                DropAll();
            }
        }
    }

    private void PickUp(GameObject critterObject)
    {
        heldCritters.Add(critterObject);
        critterObject.SetActive(false);
    }

    private void DropAll()
    {
        scoreManager.AddScore(heldCritters.Count * 10);
        heldCritters.Clear();
    }

    private void CheckCritters()
    {
        foreach (GameObject critterObject in critters)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, critterObject.transform.position);

            if (distanceToPlayer <= pickUpRange && heldCritters.Count < maxCrittersHeld)
            {
                PickUp(critterObject);
            }
        }
    }
}
