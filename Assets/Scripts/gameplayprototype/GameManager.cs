using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameplayTimer = 60;
    [HideInInspector]
    public float gameTimerCount = 0;
    public string[] endingCutscene;

    public PlayerFacialManager facialManager;
    bool isGameOver = false;

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
    }

    void GameOver()
    {
        if (isGameOver) return;
        Debug.Log("GameOver");
        UIController_Gameplay.instance.TimebarTweenOut();
        DataParser.instance.SetAllPartDetail(facialManager.GetAllPartDetail());
        CancelInvoke();
        facialManager.GetComponentInParent<CircleCollider2D>().enabled = false;
        RandomEnding(5);
        isGameOver = true;
    }

    void RandomEnding(float timer = 0)
    {
        StartCoroutine(RandomEndingEnum(timer));
    }

    IEnumerator RandomEndingEnum(float timer)
    {
        yield return new WaitForSeconds(timer);

        int endingCount = endingCutscene.Length;
        float chance = 100 / endingCount;
        float randomRoll = Random.Range(0f, 100f);
        for (int i = 0; i < endingCutscene.Length; i++)
        {
            if (randomRoll > i * chance && randomRoll < (i + 1) * chance)
            {
                SceneManager.LoadScene(endingCutscene[i]);
                break;
            }
        }
    }
}