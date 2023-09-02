using UnityEngine;
using System.Collections.Generic;

public class CritterPickUp : MonoBehaviour
{
    GameObject[] critters;
    GameObject player;
    Transform critter;

    public float pickUpRange;
    public float dropForwardForce;
    public float dropUpwardForce;

    private List<GameObject> heldCritters = new List<GameObject>();
    public int maxCrittersHeld;

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

            Rigidbody rb = critterToDrop.GetComponent<Rigidbody>();

            rb.AddForce(player.transform.forward * dropForwardForce, ForceMode.Impulse);
            rb.AddForce(player.transform.up * dropUpwardForce, ForceMode.Impulse);

            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);
            rb.AddTorque(new Vector3(randomX, randomY, randomZ) * 10);

            rb.isKinematic = false;
            BoxCollider boxCollider = critterToDrop.GetComponent<BoxCollider>();
            boxCollider.isTrigger = false;
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
