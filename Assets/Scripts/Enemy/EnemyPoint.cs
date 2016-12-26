using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EnemyPoint : SingletonMonoBehaviour<EnemyPoint> {
    //
    private static float ENEMY_GENERATE_TIME = 30;
    public bool isEnemyDead = false;
    public bool isLoading = false;
    private GameObject enemy = null;
    //生成するenemyのprefab
    public GameObject[] prefabs;
    //バトルにでるenemy、入れていないとランダムで決める
    public List<GameObject> battelEnemyList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        enemy = Instantiate(prefabs[0]) as GameObject;
        enemy.transform.parent = this.transform;
        enemy.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (enemy == null && !isEnemyDead)
        {
            isEnemyDead = true;
        }
	}
    IEnumerator GenerateEnemy()
    {
        float time = Time.time;
        while (true)
        {
            if (SceneManager.GetActiveScene().name == "NormalScene")
            {
                if ((Time.time - time) / ENEMY_GENERATE_TIME >= 1)
                {
                    enemy = Instantiate(prefabs[0]) as GameObject;
                    enemy.transform.parent = this.transform;
                    enemy.transform.localPosition = Vector3.zero;
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
                this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (this.transform.childCount >= 1)
            this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
