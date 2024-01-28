using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class itemspawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] collectiblePrefabs;
    public float minXOffset = -5f;
    public float maxXOffset = 5f;
    public float minZRotationOffset = -45f;
    public float maxZRotationOffset = 45f;
    public float minGravityScale = -.3f;
    public float maxGravityScale = -.1f;

    public float obstacleSpawnTick = 1.5f;
    public float collectibleSpawnTick = 3f;

    private void Start()
    {
        Invoke("SpawnObstacle", 0);
        Invoke("SpawnPart", 3);
    }

    public void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position, Quaternion.identity);
        obstacle.transform.position += new Vector3(Random.Range(minXOffset, maxXOffset), 0, 0);
        obstacle.transform.Rotate(0, 0, Random.Range(minZRotationOffset, maxZRotationOffset));
        Rigidbody2D rgbd2d = obstacle.GetComponent<Rigidbody2D>();
        rgbd2d.gravityScale = Random.Range(minGravityScale, maxGravityScale);
        rgbd2d.AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);

        Destroy(obstacle, 10);
        Invoke("SpawnObstacle", obstacleSpawnTick);
    }

    public void SpawnPart()
    {
        GameObject collectible = Instantiate(collectiblePrefabs[0], transform.position, Quaternion.identity);

        PartController part = collectible.GetComponent<PartController>();
        part.RandomPart();

        collectible.transform.position += new Vector3(Random.Range(minXOffset, maxXOffset), 0, 0);
        collectible.transform.Rotate(0, 0, Random.Range(minZRotationOffset, maxZRotationOffset));
        Rigidbody2D rgbd2d = collectible.GetComponent<Rigidbody2D>();
        rgbd2d.gravityScale = maxGravityScale;
        rgbd2d.AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);

        Destroy(collectible, 20);
        Invoke("SpawnLimb", collectibleSpawnTick);
    }
    public void SpawnLimb()
    {
        GameObject collectible = Instantiate(collectiblePrefabs[1], transform.position, Quaternion.identity);

        LimbController limb = collectible.GetComponent<LimbController>();
        limb.RandomColor();
        limb.RandomPart();

        collectible.transform.position += new Vector3(Random.Range(minXOffset, maxXOffset), 0, 0);
        collectible.transform.Rotate(0, 0, Random.Range(minZRotationOffset, maxZRotationOffset));
        Rigidbody2D rgbd2d = collectible.GetComponent<Rigidbody2D>();
        rgbd2d.gravityScale = maxGravityScale;
        rgbd2d.AddTorque(Random.Range(-5f, 5f), ForceMode2D.Impulse);

        Destroy(collectible, 20);
        Invoke("SpawnPart", collectibleSpawnTick);
    }
}
