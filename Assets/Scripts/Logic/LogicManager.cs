using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Playing,
    GameOver,
    GameClear
}

public class LogicManager : MonoSingle<LogicManager>
{
    public GameObject playerObj = null;
    public Camera subCamera = null;
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

    public void GameOver()
    {
        nowGameState = GameStates.GameOver;
        if(UIMgr.Inst != null)
        {
            UIMgr.Inst.OnGameOverPopup("생존 시간 ( " + getParseTime(gameTime) + " )");
        }
        GamePause(true);
    }

    public void ReStart()
    {
        nowGameState = GameStates.Playing;
        SceneTransition obj = gameObject.AddComponent<SceneTransition>();
        obj.scene = "GameScene";
        obj.PerformTransition();
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
        StartCoroutine(CameraZoom(hitEventTime));
        
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
    IEnumerator CameraZoom(float hitEventTime)
    {
        subCamera.gameObject.SetActive(true);
        float nowEventTime = 0;
        while (nowEventTime < hitEventTime/2.0f)
        {
            nowEventTime += Time.deltaTime;
            subCamera.fieldOfView = Mathf.Lerp(subCamera.fieldOfView, 40.0f, nowEventTime / (hitEventTime / 2.0f));
            yield return null;
        }

        //60 - > 40
        nowEventTime = 0;
        while (nowEventTime < hitEventTime / 2.0f)
        {
            nowEventTime += Time.deltaTime;
            subCamera.fieldOfView = Mathf.Lerp(subCamera.fieldOfView, 60.0f, nowEventTime / (hitEventTime / 2.0f));
            yield return null;
        }
        // 40 -> 60

        yield return null;
        subCamera.gameObject.SetActive(false);

    }

}
