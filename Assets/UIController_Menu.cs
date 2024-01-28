using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;
using Unity.VisualScripting;
using TMPro;

public class UIController_Menu : MonoBehaviour
{
    public static UIController_Menu instance;

    public RectTransform title_group;
    public RectTransform startText_group;
    public RectTransform title_img;

    public TextMeshProUGUI start_text;
    public AudioClip titleSfx;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TitleTweenIn(1f);
    }

    float alphaElasped = 0;

    // Update is called once per frame
    void Update()
    {
        alphaElasped += Mathf.PI * 2 * Time.deltaTime / 2f;
        if (alphaElasped >= 1000) alphaElasped -= 1000;

        start_text.alpha = Mathf.Sin(alphaElasped).Remap(-1, 1, 0, 1);

    }


    FloatTween titleTween;

    [ContextMenu("TimebarTweenIn")]
    public void TitleTweenIn(float delay = 0f)
    {
        SoundManager.instance.playSFX(titleSfx);
        if (titleTween != null)
        {
            titleTween.Stop(TweenStopBehavior.Complete);
        }
        title_group.anchoredPosition = new Vector2(-1200, 0);
        startText_group.anchoredPosition = new Vector2(0, -200);

        System.Action<ITween<float>> onUpdate = (t) =>
        {
            title_group.anchoredPosition = new Vector2(t.CurrentValue, 0);
            startText_group.anchoredPosition = new Vector2(0, t.CurrentProgress.Remap(0, 1, -200, 0));
        };

        System.Action<ITween<float>> onComplete = (t) =>
        {
            title_group.anchoredPosition = new Vector2(t.CurrentValue, 0);
            startText_group.anchoredPosition = new Vector2(0, t.CurrentProgress.Remap(0, 1, -200, 0));
        };

        titleTween = title_group.gameObject.Tween(null, -1200, 0, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
        titleTween.Delay = delay;
    }
    [ContextMenu("TimebarTweenOut")]
    public void TitleTweenOut()
    {
        if (titleTween != null)
        {
            titleTween.Stop(TweenStopBehavior.Complete);
        }
        title_group.anchoredPosition = new Vector2(0, 0);
        startText_group.anchoredPosition = new Vector2(0, 0);

        System.Action<ITween<float>> onUpdate = (t) =>
        {
            title_group.anchoredPosition = new Vector2(t.CurrentValue, 0);
            startText_group.anchoredPosition = new Vector2(0, t.CurrentProgress.Remap(0, 1, 0, -200));
        };

        System.Action<ITween<float>> onComplete = (t) =>
        {
            title_group.anchoredPosition = new Vector2(t.CurrentValue, 0);
            startText_group.anchoredPosition = new Vector2(0, t.CurrentProgress.Remap(0, 1, 0, -200));
        };

        titleTween = title_group.gameObject.Tween(null, 0, -1200, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
    }

}
