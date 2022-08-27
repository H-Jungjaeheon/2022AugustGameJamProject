using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    BeforeGameStarts,
    Playing,
    GameOver,
    GameClear,
}

public class LogicManager : MonoSingleton<LogicManager>
{
    private int killEnemyCount;
    public int KillEnemyCount
    {
        get
        {
            return killEnemyCount;
        }
        set
        {
            killEnemyCount = value;
        }
    }

    private int nowGameState;

    public float gameTime;

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }

    public void ChangeNowGameState(int changeGameState) //enum타입에 맞는 변경할 상태 넣기
    {
        if (changeGameState <= (int)GameStates.GameClear)
        {
            nowGameState = changeGameState;
        }
    }

    private void StartTimer()
    {
        if (nowGameState == (int)GameStates.Playing)
        {
            gameTime += Time.deltaTime;
        }
        else 
        {
            gameTime = 0;
        }
    }

    public void GamePause(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }

    public void EnemyHit(float hitEventTime)
    {
        StartCoroutine(EnemyHitTimeScale(hitEventTime));
    }
    IEnumerator EnemyHitTimeScale(float hitEventTime)
    {
        float nowEventTime = 0;
        Time.timeScale = 0.3f;
        while (nowEventTime < hitEventTime)
        {
            if (nowEventTime > 0.3f)
            {
                Time.timeScale = nowEventTime * 2;
            }
            nowEventTime += Time.deltaTime;
            yield return null;
        }
        Time.timeScale = 1;
    }
}
