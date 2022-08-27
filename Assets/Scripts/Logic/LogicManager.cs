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

public class LogicManager : MonoSingle<LogicManager>
{
    public GameObject playerObj = null;
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
            if(UIMgr.Inst !=null)
            {
                UIMgr.Inst.killCountText.text ="(" + killEnemyCount.ToString()+ ")";
            }

        }
    }

    public GameStates nowGameState;

    [HideInInspector]
    public float gameTime;

    private void Start()
    {
        nowGameState = GameStates.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }

    public void ChangeNowGameState(GameStates changeGameState) //enum타입에 맞는 변경할 상태 넣기
    {
            nowGameState = changeGameState;
        //if (changeGameState <= GameStates.GameClear)
        //{
        //}
    }

    private void StartTimer()
    {
        if (nowGameState == GameStates.Playing)
        {
            gameTime += Time.deltaTime;

            if(UIMgr.Inst != null)
                UIMgr.Inst.survivalTimeText.text ="생존 시간 ( "  + getParseTime(gameTime) + " )";
        }
        else 
        {
            gameTime = 0;
        }
    }
    public string getParseTime(float time)
    {
        string t = System.TimeSpan.FromSeconds(time).ToString("m\\:ss");
        string[] tokens = t.Split(':');
        return tokens[0] + ":" + tokens[1];
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
