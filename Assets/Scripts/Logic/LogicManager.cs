using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoSingle<LogicManager>
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

    public float gameTime;

    public bool isGameStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
    }
    private void StartTimer()
    {
        if (isGameStart)
        {
            gameTime += Time.deltaTime;
        }
    }
}
