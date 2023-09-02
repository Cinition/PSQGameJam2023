using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CritterMovement : MonoBehaviour
{

    public Transform go_to;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent< NavMeshAgent >();
        
    }

    // Update is called once per frame
    void Update()
    {
        if( this.transform.position == go_to.position )
            return;

        if( agent.destination != go_to.position )
        {
            print( "set destination" );
            agent.destination = go_to.position;
        }

    }
}
