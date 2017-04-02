using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EnemyPoint : MonoBehaviour
{
    // 死んだモンスター、また生成する時間
    private static float ENEMY_GENERATE_TIME = 30;
    //
    public static float MOVE_DISTANCE = 2.0f;
    public bool isEnemyDead = false;
    public bool isLoading = false;
    public GameObject enemy = null;
    //生成するenemyのprefab
    public GameObject[] prefabs;
    //バトルにでるenemy、入れていないとランダムで決める
    public List<GameObject> battelEnemyList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        enemy = Instantiate(prefabs[0]) as GameObject;
        enemy.transform.parent = this.transform;
        enemy.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	}
    /// <summary>
    /// モンスター生成のコールチン
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateEnemy()
    {
        float time = Time.time;
        while (true)
        {
            if (SceneManager.GetActiveScene().name == "NormalScene")
            {
                if ((Time.time - time) / ENEMY_GENERATE_TIME >= 1)
                {
                    CreateEnemy();
                    isLoading = false;
                    isEnemyDead = false;
                    yield break;
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    public void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            if (!isLoading && isEnemyDead)
            {
                isLoading = true;
                StartCoroutine("GenerateEnemy");
            }
            else
            {
                if (this.transform.childCount == 0)
                {
                    CreateEnemy();
                }
            }
        }
        else if (this.transform.childCount >= 1)
            Destroy(this.transform.GetChild(0).gameObject);
    }
    /// <summary>
    /// モンスター生成する
    /// </summary>
    public void CreateEnemy()
    {
        enemy = Instantiate(prefabs[0]) as GameObject;
        enemy.transform.parent = this.transform;
        enemy.transform.localPosition = Vector3.zero;
    }
}
