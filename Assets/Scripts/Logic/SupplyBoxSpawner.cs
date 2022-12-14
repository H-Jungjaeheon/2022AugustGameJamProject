using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBoxSpawner : MonoSingle<SupplyBoxSpawner>
{
    [SerializeField]
    private ObjectPool SupplyBox;

    public int MaxSupplyBoxCount;

    private int nowSupplyBoxCount;
    public int NowSupplyBoxCount
    {
        get { return nowSupplyBoxCount; }
        set 
        {
            if (value < MaxSupplyBoxCount)
            {
                SpawnSupplyBox();
            }
            nowSupplyBoxCount = value;
        }
    }

    private float DeleteCount;

    BoxCollider2D boxCollider;
    Vector2 spawnVector;

    void Start()
    {
        StartSetting();
    }

    void Update()
    {
        DeleteSupplyBoxCount();
    }

    private void DeleteSupplyBoxCount()
    {
        if (MaxSupplyBoxCount > 1)
        {
            DeleteCount += Time.deltaTime;
            if (DeleteCount >= 60)
            {
                DeleteCount = 0;
                MaxSupplyBoxCount--;
            }
        }
    }

    private void StartSetting()
    {
        NowSupplyBoxCount = MaxSupplyBoxCount;
        boxCollider = GetComponent<BoxCollider2D>();
        spawnVector = boxCollider.size;
    }
    private void SpawnSupplyBox()
    {
        float posX = transform.position.x + Random.Range(-spawnVector.x / 2f, spawnVector.x / 2f);
        float posY = transform.position.y + Random.Range(-spawnVector.y / 2f, spawnVector.y / 2f);
        Vector2 spawnRandPos = new Vector2(posX, posY);
        GameObject obj = SupplyBox.GetObject();
        obj.transform.position = spawnRandPos;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.parent = null;
        nowSupplyBoxCount++;
    }
}
