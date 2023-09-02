using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.AI;

public class CritterStateMachine : MonoBehaviour
{

    private enum CritterStates
    {
        kSpawn,
        kWandering,
        kFleeing
    }

    private CritterMovement critter_movement;
    private CritterStates   current_state;
    private float           on_state_timer;
    private bool            has_desitination;

    // Start is called before the first frame update
    void Start()
    {
        critter_movement = GetComponent< CritterMovement >();
        current_state    = CritterStates.kSpawn;
        has_desitination = false;
    }

    // Update is called once per frame
    void Update()
    {
        on_state_timer += Time.deltaTime;

        switch( current_state )
        {
            case CritterStates.kSpawn:     SpawnState();     break;
            case CritterStates.kWandering: WanderingState(); break;
            case CritterStates.kFleeing:   FleeingState();   break;
        }
    }

    void SpawnState()
    {
        // Do some spawn vfx
        // Do some spawn animation

        if( on_state_timer < 2.0f )
            return;

        current_state = CritterStates.kWandering;
        on_state_timer = 0.0f;
    }

    void WanderingState()
    {
        if( critter_movement.has_desitination )
            return;

        if( on_state_timer < 3.0f )
            return;

        on_state_timer = 0.0f;
        critter_movement.GenerateRandomLocation();
        has_desitination = true;

    }

    void FleeingState()
    {


    }
}
