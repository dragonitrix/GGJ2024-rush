using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public GameObject cloud_prefab;

    public Transform cloud_group;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnFarCloud",0);
        Invoke("SpawnNearCloud",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFarCloud(){
        var clone = Instantiate(cloud_prefab,cloud_group);
        var cloud = clone.GetComponent<CloudParticle>();
        cloud.InitFarCloud();

        Invoke("SpawnFarCloud",Random.Range(5f,7f));

    }
    public void SpawnNearCloud(){
        var clone = Instantiate(cloud_prefab,cloud_group);
        var cloud = clone.GetComponent<CloudParticle>();
        cloud.InitNearCloud();

        Invoke("SpawnNearCloud",Random.Range(7f,10f));

    }

}
