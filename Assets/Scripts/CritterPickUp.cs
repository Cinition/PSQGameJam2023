using UnityEngine;
using System.Collections.Generic;
public class CritterPickUp : MonoBehaviour
{
    GameObject[] critters;
    GameObject player;
    Transform critter;

    public float pickUpRange;

    private List<GameObject> heldCritters = new List<GameObject>();
    private int maxCrittersHeld = 5;

    void Start()
    {
        critters = GameObject.FindGameObjectsWithTag("Critter");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckCritters();
        }

        if (Input.GetKeyDown(KeyCode.Q) && heldCritters.Count > 0)
        {
            DropAll();
        }
    }

    private void PickUp(GameObject critterObject)
    {
        heldCritters.Add(critterObject);

        critter = critterObject.transform;
        critter.SetParent(player.transform);
        critter.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        critter.localScale = Vector3.one;

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
            Vector3 distanceToPlayer = player.transform.position - critterObject.transform.position;
            bool isCritterHeld = false;

            foreach (GameObject critter in heldCritters)
            {
                if (critter == critterObject)
                {
                    isCritterHeld = true;
                    break;
                }
            }

            if (!isCritterHeld && distanceToPlayer.magnitude <= pickUpRange && heldCritters.Count < maxCrittersHeld)
            {
                PickUp(critterObject);
            }
        }
    }
}
