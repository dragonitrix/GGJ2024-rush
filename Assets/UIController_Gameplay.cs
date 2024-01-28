using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class UIController_Gameplay : MonoBehaviour
{

    public RectTransform timebar_group;
    public RectTransform timebar_pointer;

    public static UIController_Gameplay instance;

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


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null)
        {
            var Ypos = GameManager.instance.gameTimerCount.Remap(0, GameManager.instance.gameplayTimer, -50, -1030);
            timebar_pointer.anchoredPosition = new Vector2(
                timebar_pointer.anchoredPosition.x,
                Ypos
            );
        }
    }
    FloatTween timebarTween;

    [ContextMenu("TimebarTweenIn")]
    public void TimebarTweenIn(float delay = 0f)
    {
        if (timebarTween != null)
        {
            timebarTween.Stop(TweenStopBehavior.Complete);
        }
        timebar_group.anchoredPosition = new Vector2(200, 0);

        System.Action<ITween<float>> onUpdate = (t) =>
        {
            timebar_group.anchoredPosition = new Vector2(t.CurrentValue, 0);
        };

        System.Action<ITween<float>> onComplete = (t) =>
        {
            timebar_group.anchoredPosition = new Vector2(t.CurrentValue, 0);
        };

        timebarTween = timebar_group.gameObject.Tween(null, 200, 0, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
        timebarTween.Delay = delay;
    }
    [ContextMenu("TimebarTweenOut")]
    public void TimebarTweenOut()
    {
        if (timebarTween != null)
        {
            timebarTween.Stop(TweenStopBehavior.Complete);
        }
        timebar_group.anchoredPosition = new Vector2(0, 0);

        System.Action<ITween<float>> onUpdate = (t) =>
        {
            timebar_group.anchoredPosition = new Vector2(t.CurrentValue, 0);
        };

        System.Action<ITween<float>> onComplete = (t) =>
        {
            timebar_group.anchoredPosition = new Vector2(t.CurrentValue, 0);
        };

        timebarTween = timebar_group.gameObject.Tween(null, 0, 200, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
    }
}
