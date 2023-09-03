using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

using Random = UnityEngine.Random;

public class WaveFunction : MonoBehaviour
{
    [Serializable]
    public struct Wave
    {
        public float spawn_rate;
        public int   critter_count;
    }

    [SerializeField]
    List< Wave > waves;

    [SerializeField]
    List< GameObject > critters;

    [SerializeField]
    List< Transform > spawners;

    public int       current_wave;
    public Transform player;

    private Transform current_spawn;
    private Vector3   last_spawner_pos;
    private float     spawn_timer;
    private int       spawned_critters;

    // Update is called once per frame
    void Update()
    {
        if( spawned_critters >= waves[ current_wave ].critter_count )
            return;

        spawn_timer += Time.deltaTime;
        if( spawn_timer > waves[ current_wave ].spawn_rate )
        {
            GetSpawner();
            Instantiate( critters[ Random.Range( 0, critters.Count - 1 ) ], current_spawn );
            spawn_timer = 0;
        }
    }

    void GetSpawner()
    {
        Vector3 spawn_location = new Vector3();
        foreach( Transform spawner in spawners )
        {
            if( math.length( spawner.position - last_spawner_pos ) < 0.1f )
                continue;

            if( math.distance( spawner.position, player.position ) > math.distance( spawn_location, player.position ) )
                spawn_location = spawner.position;
        }

        last_spawner_pos = spawn_location;
    }

    public void NextRound()
    {
        current_wave++;
        spawn_timer      = 0.0f;
        spawned_critters = 0;
    }
}
