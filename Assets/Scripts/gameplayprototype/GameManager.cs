using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameplayTimer = 60;
    private float gameTimerCount = 0;
    public SceneAsset[] endingCutscene;

    public PlayerFacialManager facialManager;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    private void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        gameTimerCount += Time.fixedDeltaTime;

        if (gameTimerCount > gameplayTimer || (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S)))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        facialManager.GetAllPartDetail();

        RandomEnding();
    }

    void RandomEnding()
    {
        int endingCount = endingCutscene.Length;
        float chance = 100 / endingCount;
        float randomRoll = Random.Range(0f, 100f);
        for (int i = 0; i < endingCutscene.Length; i++)
        {
            if (randomRoll > i * chance && randomRoll < (i + 1) * chance)
            {
                SceneManager.LoadScene(endingCutscene[i].name);
                return;
            }
        }
    }
}
