using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudParticle : MonoBehaviour
{
    SpriteRenderer sr;

    public float min_far_speed = 5f;
    public float max_far_speed = 5f;
    public float min_near_speed = 5f;
    public float max_near_speed = 5f;

    float speed = 5f;
    public float min_x_offset = -5f;
    public float max_x_offset = 5f;
    public float startY = -20;
    public float maxY = 50f;

    public List<Sprite> cloudSprites = new List<Sprite>();

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void InitFarCloud()
    {
        sr.sprite = cloudSprites[Random.Range(0, cloudSprites.Count)];
        sr.sortingOrder = Random.Range(-80, -70);
        speed = Random.Range(min_far_speed, max_far_speed);

        transform.position = new Vector3(
            Random.Range(min_x_offset, max_x_offset),//
            startY,
            transform.position.z);

        transform.localScale = Vector3.one * Random.Range(0.5f,1f);

    }

    public void InitNearCloud()
    {
        sr.sprite = cloudSprites[Random.Range(0, cloudSprites.Count)];
        sr.sortingOrder = Random.Range(50, 60);
        speed = Random.Range(min_near_speed, max_near_speed);

        sr.color = new Color(255,255,255,Random.Range(0.5f,0.7f));


        transform.position = new Vector3(
            Random.Range(min_x_offset, max_x_offset),//
            startY,
            transform.position.z);

        transform.localScale = Vector3.one * Random.Range(3f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        if (transform.position.y >= maxY)
        {
            Destroy(gameObject);
        }
    }
}
