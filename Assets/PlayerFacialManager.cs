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

    [Header("Transforms")]
    public Transform bodyGroup;
    public Transform subPartGroup;
    public Transform limbGroup;

    [Header("Reference")]
    public SpriteRenderer sr_body;

    public List<PartController> subParts = new List<PartController>();

    public List<LimbController> limbs = new List<LimbController>();

    [Header("Main Limb")]
    public LimbController limb_LeftArm;
    public LimbController limb_RightArm;
    public LimbController limb_LeftLeg;
    public LimbController limb_RightLeg;
    public LimbController limb_LeftEar;
    public LimbController limb_RightEar;

    public List<LimbController> subLimbs = new List<LimbController>();

    [Header("Settings")]
    public float ejectForce = 100f;

    public int max_subPart = 10;

    //[ContextMenu("TestAdd")]
    //public void TesttAdd()
    //{
    //    AddPart(FACIAL_PART.LEFT_EYE, 0);
    //}
    //[ContextMenu("TestAdd Limb")]
    //public void TesttAddLimb()
    //{
    //    AddPart(LIMB_PART.ARM, 0);
    //}

    public void RemovePart(bool anim = true)
    {
        var result = Random.Range(0, 2);
        if (result == 0)
        {
            RemoveFacial();
        }
        else
        {
            RemoveLimb();
        }

        if (anim)
        {
            BodyFlash();
            Squish();
        }

    }

    public void RemoveFacial()
    {

        if (subParts.Count > 0)
        {
            // remove sub first
            var sub = subParts[subParts.Count - 1];
            subParts.Remove(sub);

            EjectPart(sub.GetPartDetail());

            sub.Kill();
        }
        else
        {
            // remove main facial by order

            if (!part_Mouth.isEmpty)
            {
                part_Mouth.RemovePart();
                EjectPart(part_Mouth.GetPartDetail());
                return;
            }
            if (!part_RightEye.isEmpty)
            {
                part_RightEye.RemovePart();
                EjectPart(part_RightEye.GetPartDetail());
                return;
            }
            if (!part_LeftEye.isEmpty)
            {
                part_LeftEye.RemovePart();
                EjectPart(part_LeftEye.GetPartDetail());
                return;
            }
        }

    }

    public void RemoveLimb()
    {
        bool noSubLimb = true;
        foreach (var subLimb in subLimbs)
        {
            if (!subLimb.isEmpty)
            {
                subLimb.RemovePart();
                EjectPart(subLimb.GetLimbDetail());
                noSubLimb = false;
                break;
            }
        }
        if (!noSubLimb) return;

        if (!limb_LeftArm.isEmpty)
        {
            limb_LeftArm.RemovePart();
            EjectPart(limb_LeftArm.GetLimbDetail());
            return;
        }
        if (!limb_RightArm.isEmpty)
        {
            limb_RightArm.RemovePart();
            EjectPart(limb_RightArm.GetLimbDetail());
            return;
        }
        if (!limb_LeftLeg.isEmpty)
        {
            limb_LeftLeg.RemovePart();
            EjectPart(limb_LeftLeg.GetLimbDetail());
            return;
        }
        if (!limb_RightLeg.isEmpty)
        {
            limb_RightLeg.RemovePart();
            EjectPart(limb_RightLeg.GetLimbDetail());
            return;
        }
        if (!limb_LeftEar.isEmpty)
        {
            limb_LeftEar.RemovePart();
            EjectPart(limb_LeftEar.GetLimbDetail());
            return;
        }
        if (!limb_RightEar.isEmpty)
        {
            limb_RightEar.RemovePart();
            EjectPart(limb_RightEar.GetLimbDetail());
            return;
        }

    }

    public void EjectPart(PartDetail detail)
    {
        EjectPart(detail.type, detail.partIndex);
    }
    public void EjectPart(FACIAL_PART facial, int index)
    {
        var clone = Instantiate(PartData.instance.ejected_part_prefab);
        var controller = clone.GetComponent<PartController>();
        controller.SetType(facial);
        controller.SetPart(index);
        clone.transform.position = transform.position;
        var rb = clone.GetComponent<Rigidbody2D>();
        Fling(rb);
    }

    public void EjectPart(LimbDetail detail)
    {
        EjectPart(detail.type, detail.partIndex);
    }
    public void EjectPart(LIMB_PART limb, int index)
    {
        var clone = Instantiate(PartData.instance.ejected_limb_prefab);
        var controller = clone.GetComponent<LimbController>();
        controller.SetType(limb);
        controller.SetPart(index);
        clone.transform.position = transform.position;
        var rb = clone.GetComponent<Rigidbody2D>();
        Fling(rb);
    }

    public void Fling(Rigidbody2D rb_part)
    {
        var dir = Random.insideUnitCircle.normalized;
        var force = ejectForce;
        rb_part.AddForce(dir * force, ForceMode2D.Impulse);

        Destroy(rb_part.gameObject, 5f);
    }

    public void AddPart(PartDetail detail)
    {
        AddPart(detail.type, detail.partIndex);
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
                    AddSubPart(type, index);
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

    public void AddPart(LimbDetail detail)
    {
        AddPart(detail.type, detail.partIndex, detail.color);
    }

    public void AddPart(LIMB_PART type, int index, COLOR color)
    {

        switch (type)
        {
            case LIMB_PART.ARM:
                if (limb_LeftArm.isEmpty)
                {
                    limb_LeftArm.SetPart(index);
                    return;
                }
                if (limb_RightArm.isEmpty)
                {
                    limb_RightArm.SetPart(index);
                    return;
                }
                break;
            case LIMB_PART.LEG:
                if (limb_LeftLeg.isEmpty)
                {
                    limb_LeftLeg.SetPart(index);
                    return;
                }
                if (limb_RightLeg.isEmpty)
                {
                    limb_RightLeg.SetPart(index);
                    return;
                }
                break;
            case LIMB_PART.DETAIL:
                if (limb_LeftEar.isEmpty)
                {
                    limb_LeftEar.SetPart(index);
                    return;
                }
                if (limb_RightEar.isEmpty)
                {
                    limb_RightEar.SetPart(index);
                    return;
                }
                break;
        }

        var subLimbs_shuffle = ShuffleList(subLimbs);


        foreach (var subLimb in subLimbs_shuffle)
        {
            if (subLimb.isEmpty)
            {
                subLimb.SetPart(type, index, color);
                subLimb.spriteRenderer.flipX = (Random.Range(0, 2) == 1) ? true : false;
                return;
            }
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

    FloatTween bodyFlashTween;

    [ContextMenu("BodyFlash")]
    public void BodyFlash()
    {

        var duration = 1f;

        if (bodyFlashTween != null)
        {
            bodyFlashTween.Stop(TweenStopBehavior.Complete);
        }

        sr_body.material.shader = PartData.instance.shaderSpritesDefault;

        System.Action<ITween<float>> onUpdate = (t) =>
        {
            var tick = Mathf.Round(t.CurrentValue);

            if (tick % 10 >= 5)
            {
                sr_body.material.shader = PartData.instance.shaderGUItext;
            }
            else
            {
                sr_body.material.shader = PartData.instance.shaderSpritesDefault;
            }
        };

        System.Action<ITween<float>> onComplete = (t) =>
        {
            sr_body.material.shader = PartData.instance.shaderSpritesDefault;
        };

        bodyFlashTween = sr_body.gameObject.Tween(
            null,
            0f,
            duration * 60f,
            duration,
            TweenScaleFunctions.Linear,
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

    float elapsed = 0f;
    float body_elapsed = 0f;

    // Update is called once per frame
    void Update()
    {

        elapsed += Time.deltaTime;
        if (elapsed >= 10000f)
        {
            elapsed -= 10000f;
        }
        body_elapsed += Time.deltaTime * 3f;
        if (body_elapsed >= 10000f)
        {
            body_elapsed -= 10000f;
        }

        var xPos = transform.position.x;
        var zPos = transform.position.z;

        var horizontal_movement = new Vector2(xPos, zPos).magnitude * 1.5f;

        var distance = 0.5f;

        var l_Yoffset = Mathf.Sin(horizontal_movement) * distance;
        var r_Yoffset = Mathf.Sin(horizontal_movement + Mathf.PI) * distance;

        limb_LeftLeg.spriteRenderer.transform.localPosition = new Vector3(0, l_Yoffset, 0);
        limb_RightLeg.spriteRenderer.transform.localPosition = new Vector3(0, r_Yoffset, 0);

        var body_Yoffset = Mathf.Sin(body_elapsed) * 0.05f;
        bodyGroup.transform.localPosition = new Vector3(0, body_Yoffset, 0);

    }

    List<T> ShuffleList<T>(List<T> list)
    {
        List<T> shuffledList = new List<T>(list); // Create a new list with the same elements

        int n = shuffledList.Count;
        System.Random random = new System.Random();

        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = shuffledList[k];
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }

        return shuffledList;
    }

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
    ARM,
    LEG,
    DETAIL
}
