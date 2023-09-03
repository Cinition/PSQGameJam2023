using System.Collections.Generic;
using UnityEngine;

public class CritterPickUp : MonoBehaviour
{
    GameObject[] critters;
    GameObject player;
    Transform critter;
    TrashCan trashCan; 

    public float pickUpRange;
    private int maxCrittersHeld = 5; 
    private List<GameObject> heldCritters = new List<GameObject>();


    void Start()
    {
        critters = GameObject.FindGameObjectsWithTag("Critter");
        player = GameObject.FindGameObjectWithTag("Player");
        trashCan = FindObjectOfType<TrashCan>();
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
            if (distanceToTrashCan <= trashCan.trashRange)
            {
                DropAll();
            }
        }
    }

    private void PickUp(GameObject critterObject)
    {
        heldCritters.Add(critterObject);
        critter = critterObject.transform;
        critter.SetParent(player.transform);
        critter.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        Rigidbody rb = critter.GetComponent<Rigidbody>();
        BoxCollider boxCollider = critter.GetComponent<BoxCollider>();

        rb.isKinematic = true;
        boxCollider.isTrigger = true;
    }

    private void DropAll()
    {
        foreach (GameObject critterToDrop in heldCritters)
        {
            critterToDrop.transform.SetParent(null);
        }
        heldCritters.Clear();
    }
    private void CheckCritters()
    {
        foreach (GameObject critterObject in critters)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, critterObject.transform.position);
            bool isCritterHeld = false;

            foreach (GameObject critter in heldCritters)
            {
                if (critter == critterObject)
                {
                    isCritterHeld = true;
                    break;
                }
            }

            if (!isCritterHeld && distanceToPlayer <= pickUpRange && heldCritters.Count < maxCrittersHeld)
            {
                PickUp(critterObject);
            }
        }
    }
}
