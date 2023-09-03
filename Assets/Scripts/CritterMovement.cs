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
    public float     tired_speed;
    public float     wandering_speed;
    public float     fleeing_speed;

    private NavMeshAgent agent;
    private float        fleeing_timer;

    // Start is called before the first frame update
    void Start()
    {
        agent            = GetComponent< NavMeshAgent >();
        has_desitination = false;
        agent.speed      = wandering_speed;
    }

    // Update is called once per frame
    void Update()
    {
        if( IsAtDestination() )
            has_desitination = false;

        if( NeedsToFlee() )
        {
            fleeing_timer += Time.deltaTime;
            float timer = 1 - math.pow( 2, -10 );
            agent.speed = math.lerp( fleeing_speed, tired_speed, math.clamp( timer, 0.0f, 1.0f ) );
        }
    }

    bool IsAtDestination()
    {
        float epsilon = 1.0f;

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
        Vector3 direction     = math.normalize( transform.position - player.position );
        Vector3 flee_position = transform.position + ( direction * 2 );
        agent.destination         = flee_position;
        has_desitination          = true;
    }
}