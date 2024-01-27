using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DigitalRuby.Tween;
using UnityEngine;

public class PlayerFacialManager : MonoBehaviour
{
    [Header("Type")]
    public COLOR mainColor = COLOR.RED;

    [Header("Main Part")]
    public PartController part_LeftEye;
    public PartController part_RightEye;
    public PartController part_Mouth;

    [Header("Reference")]
    public SpriteRenderer sr_body;
    public Transform subPartGroup;

    public List<PartController> subParts = new List<PartController>();

    public List<LimbController> limbs = new List<LimbController>();

    [Header("Main Limb")]
    public LimbController limb_LeftArm;
    public LimbController limb_RightArm;
    public LimbController limb_LeftLeg;
    public LimbController limb_RightLeg;
    public LimbController limb_LeftEar;
    public LimbController limb_RightEar;
    public Transform limbGroup;

    public int max_subPart = 10;

    [ContextMenu("TestAdd")]
    public void TesttAdd()
    {
        AddPart(FACIAL_PART.LEFT_EYE, 0);
    }

    public void AddPart(FACIAL_PART type, int index)
    {
        // check if facial is empty

        switch (type)
        {
            case FACIAL_PART.LEFT_EYE:
            case FACIAL_PART.RIGHT_EYE:
                if (part_LeftEye.isEmpty)
                {
                    part_LeftEye.SetPart(index);
                }
                else if (part_RightEye.isEmpty)
                {
                    part_RightEye.SetPart(index);
                }
                else
                {
                    AddSubPart(type,index);
                }
                break;
            case FACIAL_PART.MOUTH:
                if (part_Mouth.isEmpty)
                {
                    part_Mouth.SetPart(index);
                }
                else
                {
                    AddSubPart(type, index);
                }
                break;
        }
    }

    public void AddSubPart()
    {
        AddSubPart((FACIAL_PART)Random.Range(0, 3));
    }

    public void AddSubPart(FACIAL_PART type, int index = -1)
    {
        if (subParts.Count >= max_subPart)
            return;

        var pos = NewPartPosition();

        var clone = Instantiate(PartData.instance.part_prefab, subPartGroup);
        clone.transform.position = pos;
        var partController = clone.GetComponent<PartController>();

        partController.SetType(type);
        if (index == -1) partController.RandomPart();
        else partController.SetPart(index);

        subParts.Add(partController);

        Squish();
    }

    public Vector3 NewPartPosition()
    {
        var pos = transform.position;

        for (int i = 0; i < 1000; i++)
        {
            pos =
                transform.position
                + (Vector3)(Random.insideUnitCircle * PartData.instance.newPartRandomDistance);
            var result = CheckInsideOtherPart(pos, 0.3f);
            if (!result)
            {
                //Debug.Log("found");
                break;
            }
        }

        //do
        //{
        //    pos = transform.position + (Vector3)(Random.insideUnitCircle * PartData.instance.newPartRandomDistance);
        //} while (CheckInsideOtherPart(pos, 1f));

        return pos;
    }

    public bool CheckInsideOtherPart(Vector2 pos, float radius)
    {
        var colliders = Physics2D.OverlapCircleAll(pos, radius).ToList<Collider2D>();
        //Debug.Log("colliders count:" + colliders.Count);
        var colliders_filtered = new List<Collider2D>();
        foreach (var collider in colliders)
        {
            //Debug.Log(collider.name);
            if (collider.CompareTag("Part"))
                colliders_filtered.Add(collider);
        }

        //Debug.Log("filtered count: "+colliders_filtered.Count);

        if (colliders_filtered.Count > 0)
            return true;
        else
            return false;
    }

    public void SetBodyType(int index)
    {
        sr_body.sprite = PartData.instance.GetBobySprites(mainColor)[index];

        Squish();
    }

    public void RandomBodyType()
    {
        var pool = PartData.instance.GetBobySprites(mainColor);
        SetBodyType(Random.Range(0, pool.Count));
    }

    public void RandomPart(int facial)
    {
        RandomPart((FACIAL_PART)facial);
    }

    public void RandomPart(FACIAL_PART facial)
    {
        switch (facial)
        {
            case FACIAL_PART.LEFT_EYE:
                part_LeftEye.RandomPart();
                break;
            case FACIAL_PART.RIGHT_EYE:
                part_RightEye.RandomPart();
                break;
            case FACIAL_PART.MOUTH:
                part_Mouth.RandomPart();
                break;
        }
        Squish();
    }

    public void SetPart(FACIAL_PART facial, int index)
    {
        switch (facial)
        {
            case FACIAL_PART.LEFT_EYE:
                part_LeftEye.SetPart(index);
                break;
            case FACIAL_PART.RIGHT_EYE:
                part_RightEye.SetPart(index);
                break;
            case FACIAL_PART.MOUTH:
                part_Mouth.SetPart(index);
                break;
        }
        Squish();
    }

    Vector3Tween coreTween;

    public void Squish()
    {
        if (coreTween != null)
        {
            coreTween.Stop(TweenStopBehavior.Complete);
        }

        sr_body.transform.localScale = Vector3.one * 0.8f;
        System.Action<ITween<Vector3>> onUpdate = (t) =>
        {
            sr_body.transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> onComplete = (t) =>
        {
            sr_body.transform.localScale = t.CurrentValue;
        };

        coreTween = sr_body.gameObject.Tween(
            null,
            Vector3.one * 0.8f,
            Vector3.one,
            1f,
            TweenScaleFunctions.EaseOutElastic,
            onUpdate,
            onComplete
        );
    }

    // Start is called before the first frame update
    void Start() { }

    //[ContextMenu("SetLimbPos")]
    //public void SetLimbPos()
    //{
    //    var distance = 0.7f;
    //    var division = limbs.Count;
    //    var delta = Mathf.PI * 2 / division;
    //
    //    for (int i = 0; i < division; i++)
    //    {
    //
    //        var clone = limbs[i].gameObject;
    //
    //        var pos = new Vector2(
    //            distance * Mathf.Cos(delta * i),
    //            distance * Mathf.Sin(delta * i)
    //            );
    //
    //        clone.transform.localPosition = (Vector3)pos;
    //        clone.transform.Rotate(0f, 0f, delta * i * Mathf.Rad2Deg + 90);
    //    }
    //
    //
    //    limb_LeftArm.transform.rotation = Quaternion.Euler(Vector3.zero);
    //    limb_RightArm.transform.rotation = Quaternion.Euler(Vector3.zero);
    //    limb_LeftLeg.transform.rotation = Quaternion.Euler(Vector3.zero);
    //    limb_RightLeg.transform.rotation = Quaternion.Euler(Vector3.zero);
    //    limb_LeftEar.transform.rotation = Quaternion.Euler(Vector3.zero);
    //    limb_RightEar.transform.rotation = Quaternion.Euler(Vector3.zero);
    //
    //    limb_LeftArm.spriteRenderer.flipX = true;
    //    limb_LeftLeg.spriteRenderer.flipX = true;
    //    limb_LeftEar.spriteRenderer.flipX = true;
    //
    //    foreach(var limb in limbs)
    //    {
    //        limb.SetPartDefault();
    //    }
    //
    //}

    //public GameObject pivot;
    //
    //[ContextMenu("SpawnLimb")]
    //public void SpawnLimb()
    //{
    //    var distance = 0.7f;
    //    var division = 12f;
    //    var delta = Mathf.PI * 2 / division;
    //
    //    for (int i = 0; i < division; i++)
    //    {
    //        var pos = new Vector2(
    //            distance * Mathf.Cos(delta * i),
    //            distance * Mathf.Sin(delta * i)
    //            );
    //        var clone = Instantiate(pivot, limbGroup);
    //        clone.transform.localPosition = (Vector3)pos;
    //
    //        clone.transform.Rotate(0f, 0f, delta * i * Mathf.Rad2Deg + 90);
    //
    //        var limb = clone.GetComponent<LimbController>();
    //
    //        limbs.Add(limb);
    //
    //    }
    //}

    // Update is called once per frame
    void Update() { }
}

public enum COLOR
{
    BLUE,
    BROWN,
    GREEN,
    RED,
    WHITE,
    YELLOW
}

public enum FACIAL_PART
{
    LEFT_EYE,
    RIGHT_EYE,
    MOUTH
}

public enum LIMB_PART
{
    NONE,
    ARM,
    LEG,
    DETAIL
}
