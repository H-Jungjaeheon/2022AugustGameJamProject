using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] prefabArray;
    [SerializeField]
    private int objectSpqwnCount = 10;
    private Vector3 ScreenCenter;
    public float time;
    public float spawnTime = 5;
    public bool isGameOver = false;
    private void Update()
    {
        time += Time.deltaTime;
        Debug.Log(time);
    }

    private void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        StartCoroutine(this.CreateMonster());
    }
    IEnumerator CreateMonster()
    {
        //���� ���� �ñ��� ���� ����
        while (!isGameOver)
        {
            //���� ������ ���� ���� ����
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
            int index = Random.Range(0, prefabArray.Length);
            float x = Random.Range(-7.5f, 7.5f);
            float y = 0f;
            Vector3 position = new Vector3(x, y, 0);

            if (monsterCount < objectSpqwnCount)
            {
                //������ ���� �ֱ� �ð���ŭ ���
                yield return new WaitForSeconds(spawnTime);

                //�ұ�Ģ���� ��ġ ����
                int idx = Random.Range(1, Camera.main.pixelWidth);
                //������ ���� ����
                GameObject clone = Instantiate(prefabArray[index], position, Quaternion.identity);
                clone.tag = "Enemy";
                clone.AddComponent <EnemyMove> ();
                clone.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            else
            {
                yield return null;
            }
        }
    }
}