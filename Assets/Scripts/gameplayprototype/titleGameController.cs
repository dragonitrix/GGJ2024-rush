using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TransitionsPlus;

public class titleGameController : MonoBehaviour
{
    Animator animator;
    public AudioClip footSFX; 
    public AudioClip jumpSFX;
    public AudioClip loopBGM;
    private void Start()
    {
        animator = GetComponent<Animator>();
        SoundManager.instance.playBGM(loopBGM);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            UIController_Menu.instance.TitleTweenOut();

            animator.SetBool("isStart", true);
        }
    }

    public void playFootSfx()
    {
        SoundManager.instance.playSFX(footSFX);
    }
    public void playJumpSfx()
    {
        SoundManager.instance.playSFX(jumpSFX);
    }

    public void goToGameplayScene()
    {
        SoundManager.instance.stopBGM();
        var fade = TransitionAnimator.Start(
        TransitionType.SeaWaves, // transition type
        color: Color.white,
        duration: 1f, // transition duration in seconds
        sceneNameToLoad: "sc_gameplayprototype"
        );
        //SceneManager.LoadScene("sc_gameplayprototype");
    }


}
