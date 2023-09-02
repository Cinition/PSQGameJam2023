using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using Unity.Mathematics;

public class CritterMovement : MonoBehaviour
{
    public Transform map_min;
    public Transform map_max;
    public Transform player;
    public bool      has_desitination;
    public float     flee_radius;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent            = GetComponent< NavMeshAgent >();
        has_desitination = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( IsAtDestination() )
            has_desitination = false;
    }

    bool IsAtDestination()
    {
        float epsilon = 0.1f;

        if( ( transform.position.x - agent.destination.x ) <= epsilon )
            return true;

        if( ( transform.position.z - agent.destination.z ) <= epsilon )
            return true;

        return false;
    }

    public bool NeedsToFlee()
    {
        float length = math.length( player.position - transform.position );
        print( length );
        return length < flee_radius;
    }

    public void GenerateRandomLocation()
    {
        Vector3 random_location = new Vector3( 
            Random.Range( map_min.position.x, map_max.position.x ), 
            1.0f,
            Random.Range( map_min.position.z, map_max.position.z )
        );

        agent.destination = random_location;
        has_desitination  = true;
    }

    public void GetFleeingLocation()
    {
        Vector3 opposite_location = new Vector3( -player.position.x, player.position.y, -player.position.z );
        agent.destination         = opposite_location;
        has_desitination          = true;
    }
}
