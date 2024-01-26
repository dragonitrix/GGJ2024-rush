using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemspawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float minXOffset = -5f;
    public float maxXOffset = 5f;
    public float minZRotationOffset = -45f;
    public float maxZRotationOffset = 45f;
    public float minGravityScale = -.3f;
    public float maxGravityScale = -.1f;

    private void Start()
    {
        Invoke("SpawnObstacle", 0);
    }

    public void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position, Quaternion.identity);
        obstacle.transform.position += new Vector3(Random.Range(minXOffset, maxXOffset), 0, 0);
        obstacle.transform.Rotate(0, 0, Random.Range(minZRotationOffset, maxZRotationOffset));
        obstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(minGravityScale, maxGravityScale);
        Destroy(obstacle, 10);
        Invoke("SpawnObstacle", 3);
    }
}
