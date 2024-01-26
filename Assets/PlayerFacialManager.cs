using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class PlayerFacialManager : MonoBehaviour
{

    [Header("SpritesList")]

    //public List<Sprite> leftEye_sprites = new List<Sprite>();
    //public List<Sprite> rightEye_sprites = new List<Sprite>();
    public List<Sprite> eye_sprites = new List<Sprite>();
    public List<Sprite> mouth_sprites = new List<Sprite>();

    [Header("Reference")]
    public SpriteRenderer sr_LeftEye;
    public SpriteRenderer sr_RightEye;
    public SpriteRenderer sr_Mouth;

    public GameObject obj_Core;

    public void RandomPart(int facial)
    {
        RandomPart((FACIAL)facial);

    }

    public void RandomPart(FACIAL facial)
    {

        List<Sprite> pool = new List<Sprite>();

        switch (facial)
        {
            case FACIAL.LEFT_EYE:
            case FACIAL.RIGHT_EYE:
                pool = eye_sprites;
                break;
            case FACIAL.MOUTH:
                pool = mouth_sprites;
                break;
        }


        SwapPart(facial, Random.Range(0, pool.Count));

    }

    public void SwapPart(FACIAL facial, int index)
    {
        switch (facial)
        {
            case FACIAL.LEFT_EYE:
                sr_LeftEye.sprite = eye_sprites[index];
                break;
            case FACIAL.RIGHT_EYE:
                sr_RightEye.sprite = eye_sprites[index];
                break;
            case FACIAL.MOUTH:
                sr_Mouth.sprite = mouth_sprites[index];
                break;
        }
        Squish();
    }

    public void Squish()
    {

        System.Action<ITween<Vector3>> onUpdate = (t) =>
        {
            obj_Core.transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> onComplete = (t) =>
        {
            obj_Core.transform.localScale = t.CurrentValue;
        };

        obj_Core.Tween("SquishTween", Vector3.one * 0.8f, Vector3.one, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum FACIAL
    {
        LEFT_EYE,
        RIGHT_EYE,
        MOUTH
    }
}
