using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMng : MonoBehaviour
{
    GameObject[] enemy;
    GameObject[] bullet;

    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public ObjectMng objectMng;
    GameObject[] targetPool;
    // Start is called before the first frame update
    private void Awake()
    {
        enemy = new GameObject[20];
        bullet = new GameObject[100];

        Generate();
    }
    void Generate()
    {
        //enemy
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i] = Instantiate(enemyPrefab);
            enemy[i].SetActive(false);
        }
        //bullet
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i] = Instantiate(bulletPrefab);
            bullet[i].SetActive(false);
        }
    }
    public GameObject MakeObject(string type)
    {
        switch (type)
        {
            case "Enemy":
                targetPool = enemy;
                break;
            case "Bullet":
                targetPool = bullet;
                break;
        }
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
