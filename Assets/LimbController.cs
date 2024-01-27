using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class LimbController : MonoBehaviour
{
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
        spriteRenderer.sprite = null;
    }

    public void RandomPart(bool skipAnim = false)
    {
        var pool = PartData.instance.GetLimb(color,type);
        var randResult = Random.Range(0, pool.Count);
        SetPart(randResult, skipAnim);

    }
    public void SetPart(int index, bool skipAnim = false)
    {
        spriteRenderer.sprite = PartData.instance.GetLimb(color,type)[index];

        //random pos
        spriteRenderer.transform.localPosition = Random.insideUnitCircle * PartData.instance.partPosRandomDistance;

        partIndex = index;

        if (!skipAnim)
        {
            Squish();
        }

    }

    Vector3Tween squishTween;

    public void Squish()
    {

        if(squishTween != null)
        {
            squishTween.Stop(TweenStopBehavior.Complete);
        }
        spriteRenderer.transform.localScale = Vector3.one * 0.8f;
        System.Action<ITween<Vector3>> onUpdate = (t) =>
        {
            spriteRenderer.transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> onComplete = (t) =>
        {
            spriteRenderer.transform.localScale = t.CurrentValue;
        };

         squishTween = spriteRenderer.gameObject.Tween(null, Vector3.one * 0.8f, Vector3.one, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
    }
}