using System.Collections.Generic;
using UnityEngine;

public class CritterPickUp : MonoBehaviour
{
    GameObject[] critters;
    GameObject   player;
    GameObject   trashCan;

    public float trashRange;
    public float pickUpRange;
    private      List<GameObject> heldCritters = new List<GameObject>();
    public       ScoreManager scoreManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trashCan = GameObject.FindGameObjectWithTag("Trash Can");
    }

    void Update()
    {
        critters = GameObject.FindGameObjectsWithTag("Critter");
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
        Destroy(critterObject);
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

            if (!heldCritters.Contains(critterObject) && distanceToPlayer <= pickUpRange)
            {
                PickUp(critterObject);
            }
        }
    }
}
