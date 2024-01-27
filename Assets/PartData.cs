using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartData : MonoBehaviour
{

    public static PartData instance;


    [Header("Prefabs")]
    public GameObject part_prefab;

    [Header("Sprite")]
    public List<Sprite> eye_sprites = new List<Sprite>();
    public List<Sprite> month_sprites = new List<Sprite>();

    [Header("Settings")]
    public float newPartRandomDistance = 1f;
    public float partPosRandomDistance = 0.1f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public List<Sprite> GetPartSprites(FACIAL_PART type)
    {
        switch (type)
        {
            case FACIAL_PART.LEFT_EYE:
            case FACIAL_PART.RIGHT_EYE:
                return eye_sprites;
            case FACIAL_PART.MOUTH:
                return month_sprites;
            default:
                return null;
        }
    }

}
