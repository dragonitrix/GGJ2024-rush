using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TransitionsPlus;

public class titleGameController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            UIController_Menu.instance.TitleTweenOut();

            animator.SetBool("isStart", true);
        }
    }

    public void goToGameplayScene()
    {
        var fade = TransitionAnimator.Start(
        TransitionType.SeaWaves, // transition type
        color: Color.white,
        duration: 1f, // transition duration in seconds
        sceneNameToLoad: "sc_gameplayprototype"
        );
        //SceneManager.LoadScene("sc_gameplayprototype");
    }
}
