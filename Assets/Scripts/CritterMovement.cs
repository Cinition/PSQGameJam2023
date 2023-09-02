using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class CritterMovement : MonoBehaviour
{
    public Transform map_min;
    public Transform map_max;
    public Vector3   player_position;
    public bool      has_desitination;

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
        Vector3 opposite_location = new Vector3( -player_position.x, player_position.y, -player_position.z );
        agent.destination         = opposite_location;
        has_desitination          = true;
    }
}
