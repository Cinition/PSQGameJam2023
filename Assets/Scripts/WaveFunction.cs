using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

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
    List< Transform > spawners;

    public int       current_wave;
    public Transform player;

    private Vector3 last_spawner;
    private float   spawn_timer;

    // Update is called once per frame
    void Update()
    {
        spawn_timer += Time.deltaTime;
        if( spawn_timer > waves[ current_wave ].spawn_rate )
        {
            GetSpawner();
            spawn_timer = 0;
        }
    }

    void GetSpawner()
    {
        Vector3 spawn_location = new Vector3();
        foreach( Transform spawner in spawners )
        {
            if( math.length( spawner.position - last_spawner ) < 0.1f )
                continue;

            if( math.distance( spawner.position, player.position ) > math.distance( spawn_location, player.position ) )
            {
                spawn_location = spawner.position;
            }
        }
        last_spawner = spawn_location;
    }

    public void NextRound()
    {
        Wave wave = waves[ current_wave + 1 ];
    }
}
