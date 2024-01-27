using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;


    [Header("Gameplay Settings")]

    public float LevelDuration = 60f;

    public GAME_STATE gameState;

    public void SetState(GAME_STATE targetState)
    {
        OnExitState();
        gameState = targetState;
        OnEnterState();
    }

    public void OnExitState()
    {
        switch (gameState)
        {
            case GAME_STATE.START:
                break;
            case GAME_STATE.GAMEPLAY:
                break;
            case GAME_STATE.END:
                break;
        }
    }
    public void OnEnterState()
    {
        switch (gameState)
        {
            case GAME_STATE.START:
                break;
            case GAME_STATE.GAMEPLAY:
                break;
            case GAME_STATE.END:
                break;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}

public enum GAME_STATE
{
    START,
    GAMEPLAY,
    END
}
