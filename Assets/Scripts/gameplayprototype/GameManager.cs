using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TransitionsPlus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameplayTimer = 60;
    [HideInInspector]
    public float gameTimerCount = 0;
    public string[] endingCutscene;

    public PlayerFacialManager facialManager;
    public bool isGameOver = false;
    public AudioClip gameplayBGM;
    public TransitionAnimator fadeout;
    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    private void Start()
    {
        GameStart();
    }
    private void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        gameTimerCount += Time.deltaTime;

        if (gameTimerCount > gameplayTimer || (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S)))
        {
            GameOver();
        }
    }
    void GameStart()
    {
        isGameOver = false;
        UIController_Gameplay.instance.TimebarTweenIn(1f);
        SoundManager.instance.playBGM(gameplayBGM);
    }

    void GameOver()
    {
        if (isGameOver) return;
        Debug.Log("GameOver");
        UIController_Gameplay.instance.TimebarTweenOut();
        DataParser.instance.SetAllPartDetail(facialManager.GetAllPartDetail());
        CancelInvoke();
        RandomEnding(5);
        isGameOver = true;
    }

    void RandomEnding(float timer = 0)
    {
        StartCoroutine(RandomEndingEnum(timer));
    }

    IEnumerator RandomEndingEnum(float timer)
    {
        yield return new WaitForSeconds(timer - fadeout.profile.duration);
        fadeout.gameObject.SetActive(true);
        yield return new WaitForSeconds(fadeout.profile.duration);
        //int endingCount = endingCutscene.Length;
        //float chance = 100 / endingCount;
        //float randomRoll = Random.Range(0f, 100f);
        //for (int i = 0; i < endingCutscene.Length; i++)
        //{
        //    if (randomRoll > i * chance && randomRoll < (i + 1) * chance)
        //    {
        //        SceneManager.LoadScene(endingCutscene[i]);
        //        break;
        //    }
        //}
        SoundManager.instance.stopBGM();
        SceneManager.LoadScene(endingCutscene[DataParser.instance.lastEndingIndex]);
        DataParser.instance.lastEndingIndex = DataParser.instance.lastEndingIndex == 0 ? 1 : 0;
    }
}