using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class LimbController : MonoBehaviour
{
    public bool isEmpty = false;
    public COLOR color;
    public LIMB_PART type;
    public SpriteRenderer spriteRenderer;
    public int partIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetType(LIMB_PART type)
    {
        this.type = type;

    }

    [ContextMenu("SetPartDefault")]
    public void SetPartDefault()
    {

        switch (type)
        {
            //case LIMB_PART.NONE:
            default:
                spriteRenderer.sprite = null;
                break;
            case LIMB_PART.ARM:
            case LIMB_PART.LEG:
            case LIMB_PART.DETAIL:
                //RandomPart();
                SetPart(0);
                break;

        }

    }

    public void RandomPart(bool skipAnim = false)
    {
        type = (LIMB_PART)Random.Range(0,3);
        var pool = PartData.instance.GetLimb(color,type);
        var randResult = Random.Range(0, pool.Count);
        SetPart(randResult, skipAnim);
    }
    public void SetPart(LIMB_PART type,int index, COLOR color ,bool skipAnim = false)
    {
        SetType(type);
        this.color = color;
        SetPart(index, skipAnim);
    }
    public void SetPart(int index, bool skipAnim = false)
    {

        isEmpty = false;
        spriteRenderer.sprite = PartData.instance.GetLimb(color,type)[index];
        //random pos
        spriteRenderer.transform.localPosition = Random.insideUnitCircle * PartData.instance.partPosRandomDistance;

        partIndex = index;

        if (!skipAnim)
        {
            Squish();
        }

    }

    public void RemovePart()
    {
        isEmpty = true;
        spriteRenderer.sprite = null;
        spriteRenderer.flipX = false;
    }

    Vector3Tween squishTween;

    public void Squish()
    {

        if(squishTween != null)
        {
            squishTween.Stop(TweenStopBehavior.Complete);
        }
        spriteRenderer.transform.localScale = Vector3.one * 5f * 0.8f;
        System.Action<ITween<Vector3>> onUpdate = (t) =>
        {
            spriteRenderer.transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> onComplete = (t) =>
        {
            spriteRenderer.transform.localScale = t.CurrentValue;
        };

         squishTween = spriteRenderer.gameObject.Tween(null, Vector3.one * 0.8f * 5f, Vector3.one * 5f, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
    }

    public LimbDetail GetLimbDetail(){
        LimbDetail detail;
        detail.color = color;
        detail.type = type;
        detail.partIndex = partIndex;
        return detail;
    }
}

public struct LimbDetail{
    public COLOR color;
    public LIMB_PART type;
    public int partIndex;
}