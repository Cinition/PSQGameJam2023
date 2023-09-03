using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Transform map_min;
    public Transform map_max;

    private Vector3   spawner_pos;
    private float     spawn_timer = 0.0f;
    private int       spawned_critters = 0;
    public List< GameObject > alive_critter;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject item in alive_critter)
        {
            if( !item )
                alive_critter.Remove( item );
        }

        print( spawn_timer );
        if( spawn_timer > 10.0 )
            NextRound();

        if( alive_critter.Count >= 22 || current_wave >= waves.Count )
            SceneManager.LoadScene( 2 );

        spawn_timer += Time.deltaTime;

        if( spawned_critters >= waves[ current_wave ].critter_count )
            return;

        if( spawn_timer > waves[ current_wave ].spawn_rate )
        {
            GetSpawner();
            GameObject random_critter = critters[Random.Range(0, critters.Count)];
            GameObject critter = Instantiate(random_critter, spawner_pos, Quaternion.identity);
            critter.tag = "Critter";
            critter.GetComponent< CritterMovement >().map_min = map_min;
            critter.GetComponent< CritterMovement >().map_max = map_max;
            critter.GetComponent< CritterMovement >().player  = player;
            spawned_critters++;
            alive_critter.Add( critter );
            spawn_timer = 0;
        }
    }

    void GetSpawner()
    {
        Vector3 spawn_location = new Vector3();
        foreach( Transform spawner in spawners )
        {
            if( math.length( spawner.position - spawner_pos ) < 0.1f )
                continue;

            if( math.distance( spawner.position, player.position ) > math.distance( spawn_location, player.position ) )
                spawn_location = spawner.position;
        }

        spawner_pos = spawn_location;
    }

    public void NextRound()
    {
        current_wave++;
        spawn_timer      = 0.0f;
        spawned_critters = 0;
    }
}
