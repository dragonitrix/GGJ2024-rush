using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartData : MonoBehaviour
{
    public static PartData instance;

    [Header("Prefabs")]
    public GameObject part_prefab;
    public GameObject limb_prefab;

    public GameObject ejected_part_prefab;
    public GameObject ejected_limb_prefab;

    [Header("Sprite")]
    public List<Sprite> eye_sprites = new List<Sprite>();
    public List<Sprite> month_sprites = new List<Sprite>();

    [Header("Body")]
    public BodyPart blue;
    public BodyPart brown;
    public BodyPart green;
    public BodyPart red;
    public BodyPart white;
    public BodyPart yellow;

    [Header("Settings")]
    public float newPartRandomDistance = 1f;
    public float partPosRandomDistance = 0.1f;

    [Header("Shader")]

	public Shader shaderGUItext;
	public Shader shaderSpritesDefault;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        shaderGUItext = Shader.Find("GUI/Text Shader");
		shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used

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

    public List<Sprite> GetBobySprites(COLOR color)
    {
        return GetBodyFromColor(color).bodies;
    }

    public BodyPart GetBodyFromColor(COLOR color)
    {
        switch (color)
        {
            case COLOR.BLUE:
                return blue;
            case COLOR.BROWN:
                return brown;
            case COLOR.GREEN:
                return green;
            case COLOR.RED:
                return red;
            case COLOR.WHITE:
                return white;
            case COLOR.YELLOW:
                return yellow;
            default:
                return white;
        }
    }

    public List<Sprite> GetLimb(COLOR color, LIMB_PART type)
    {
        var body = GetBodyFromColor(color);

        switch (type)
        {
            case LIMB_PART.ARM:
                return body.arms;
            case LIMB_PART.LEG:
                return body.legs;
            case LIMB_PART.DETAIL:
                return body.details;
            default:
                return null;
        }
    }

    //public List<Sprite> GetLimbSprites()
    //{
    //
    //}
}

[Serializable]
public class BodyPart
{
    public List<Sprite> arms = new List<Sprite>();
    public List<Sprite> bodies = new List<Sprite>();
    public List<Sprite> details = new List<Sprite>();
    public List<Sprite> legs = new List<Sprite>();
}
