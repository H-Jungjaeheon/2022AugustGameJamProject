using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject[] prefabArray;
    [SerializeField]
    List<GameObject> enemys = new List<GameObject>();
    [SerializeField]
    public int objectSpqwnCount = 2;
    private Vector3 ScreenCenter;
    public float time;
    public GameObject player;
    public float spawnTime = 5;
    public bool isGameOver = false;
    public ObjectPool enemyPool = null;
    public ObjectMng objectMng = null;
    private void Update()
    {
        time += Time.deltaTime;
    }

    private void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        StartCoroutine(this.CreateMonster());
    }
    IEnumerator CreateMonster()
    {
        ////���� ���� �ñ��� ���� ����
        //while (!isGameOver)
        //{
        //    //���� ������ ���� ���� ����
        //    int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
        //    int index = Random.Range(0, prefabArray.Length);
        //    float x = Random.Range(-7.5f, 7.5f);
        //    float y = -4f;
        //    Vector3 position = new Vector3(x, y, 0);
        //    if (monsterCount < objectSpqwnCount)
        //    {
        //        //������ ���� �ֱ� �ð���ŭ ���
        //        yield return new WaitForSeconds(spawnTime);
        //        //�ұ�Ģ���� ��ġ ����
        //        int idx = Random.Range(1, Camera.main.pixelWidth);
        //        //������ ���� ����
        //        GameObject clone = Instantiate(prefabArray[index], position, Quaternion.identity);
        //        clone.tag = "Enemy";
        //        clone.AddComponent <EnemyMove> ();
        //        clone.AddComponent<CircleCollider2D>();
        //        clone.GetComponent<Rigidbody2D>().gravityScale = 0;
        //        EnemyMove enemyLogic = clone.GetComponent<EnemyMove>();
        //        //enemyLogic.player = player;
        //    }
        //    else
        //    {
        //        yield return null;
        //    }
        //}


        //���� ���� �ñ��� ���� ����
        while (!isGameOver)
        {
            //���� ������ ���� ���� ����
            int monsterCount = 0;
            //int index = Random.Range(0, prefabArray.Length);
            if (15 < time && time < 60)
            {
                spawnTime = 5;
                monsterCount = 2;
            }
            else if (60 < time && time < 90)
            {
                spawnTime = 5;
                monsterCount = 3;
            }
            else if (90 < time && time < 120)
            {
                spawnTime = 3;
                monsterCount = 3;
            }
            else if (120 < time && time < 150)
            {
                spawnTime = 3;
                monsterCount = 5;
            }
            else if (150 < time && time < 180)
            {
                spawnTime = 2;
                monsterCount = 5;
            }
            else if(time > 180)
            {
                spawnTime = 1;
                monsterCount = 5;
            }

            if (monsterCount < objectSpqwnCount)
            {
                //������ ���� �ֱ� �ð���ŭ ���
                yield return new WaitForSeconds(spawnTime);

                //�ұ�Ģ���� ��ġ ����
                int idx = Random.Range(1, Camera.main.pixelWidth);
                float x = Random.Range(Camera.main.pixelWidth/2, idx);
                float y = -4f;
                Vector3 position = new Vector3(x, y, 0);

                //������ ���� ����
                GameObject clone = enemyPool.GetObject();
            
                clone.transform.position = position;
                clone.transform.rotation = Quaternion.identity;
                clone.tag = "Enemy";
                //clone.AddComponent<Enemy>();
                //Enemy enemyLogic = clone.GetComponent<Enemy>();
                //enemyLogic.player = player;
                monsterCount++;
            }
            else
            {
                yield return null;
            }
        }

    }
}
