using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class PartyRoot : MonoBehaviour {
	// バトルが始めて最初にすべてのスキルをリーキャストする時間
	private static float BATTEL_START_RECAST_TIME = 5.0f;
	// メンバーとの距離
	private static float DISTANCE = 1f;
	// HPゲージのX位置
	private static float[] UI_POS = new float[] {-525, -325, -125, 75};
    private static float[] E_UI_POS = new float[] { 360, 310, 260, 210 };
	// 配置の最初値
	public static Vector3[]  posArray= new Vector3[] {new Vector3(0,0,-2.25f),
		new Vector3(-2f,0,-0.5f), new Vector3(0,0,0), new Vector3(2f,0,-0.5f)
	};
	//　敵の配置ポイント
	public Transform enemyRoot;
	//　攻撃の順番
	[HideInInspector] public List<GameObject> attackList = new List<GameObject> ();
	//	攻撃可能かどうか
	[HideInInspector] public bool canAttack = true;
	//	敵のプレハブリスト
	private List<GameObject> EnemyPrefabList = new List<GameObject>();
	// Use this for initialization
	void Start () {
		ReadyBattel();
	}
	
	// Update is called once per frame
	void Update () {
		if (attackList.Count > 0 && canAttack) {
			try{
                StatusControl sc = attackList[0].GetComponent<StatusControl>();
                if (sc.battelStatus != BattelStatus.DEAD)
                {
                    switch (attackList[0].layer)
                    {
                        case 8:
                            JobBase jb = attackList[0].GetComponent<JobBase>();
                            jb.skillUsing.skillMethod(jb.skillUsing, jb._target, jb.skillUsing.s_effectTime);
                            break;
                        case 12:
                            EnemyBase eb = attackList[0].GetComponent<EnemyBase>();
                            eb.skillUsing.skillMethod(eb._target, eb.skillUsing.s_effectTime);
                            break;
                    }
                    canAttack = false;
                }
                else
                    ReadyNextAttack();
			}
			catch(MissingReferenceException){
                ReadyNextAttack();
			}
		}
	}
	/// <summary>
	/// 次の攻撃を行えるようにする
	/// </summary>
	public void ReadyNextAttack(){
		try{
			attackList.Remove (attackList [0]);
		}
		catch(MissingReferenceException){
		}
		canAttack = true;
	}
	/// <summary>
	/// プレイヤーやエネミーたちを作成、最初のリーキャスト
	/// </summary>
	public void ReadyBattel()
	{
		for (int i = 0; i < PlayerRoot.Instance.p_prefabList.Count; i++) {
			GameObject player = Instantiate (PlayerRoot.Instance.p_prefabList[i], Vector3.zero, this.transform.rotation) as GameObject;
			PlayerRoot.Instance.partyList.Add (player);
			player.transform.parent = this.transform;
			player.transform.localPosition = posArray[i] * DISTANCE;
			StartCoroutine ("BattelStartRecast", player);
			player.GetComponentInChildren<Animator> ().SetTrigger ("Battel");
			JobBase jb = player.GetComponent<JobBase> ();
			jb.Set_b_Status (BattelStatus.NORMAL);
			GameObject hp_ui = Instantiate(jb.UI_hp_prefab) as GameObject;
			hp_ui.transform.SetParent(GameObject.FindGameObjectWithTag("HP_UI").transform);
			hp_ui.transform.localRotation = Quaternion.Euler(Vector3.zero);
			hp_ui.transform.localPosition = new Vector3(UI_POS[i], -330, 0);
			hp_ui.transform.localScale = new Vector3(1.5f, 0.3f, 0);
			jb.UI_hp = hp_ui.GetComponentInChildren<Slider>();
			jb.UI_hp.maxValue = jb._maxHP;
			jb.UI_hp.value = jb._hp;
		}
        //if (PlayerRoot.Instance.battelEnemyList.Count == 0)
        if (PlayerRoot.Instance.battelEnemyList.Count > 0)
        {
            EnemyPrefabList = PlayerRoot.Instance.battelEnemyList;
            PlayerRoot.Instance.battelEnemyList = new List<GameObject>();

        }
        else
        {
            string ed = GetEnemyData("Enemy_Set.json");
            GetEnemyPerfabs(ed);
        }
        for (int i = 0; i < EnemyPrefabList.Count; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefabList[i], Vector3.zero, this.transform.rotation) as GameObject;
            PlayerRoot.Instance.enemyList.Add(enemy);
            enemy.transform.parent = enemyRoot;
            enemy.transform.localPosition = posArray[i] * DISTANCE;
			EnemyBase eb = enemy.GetComponent<EnemyBase>();
			GameObject hp_ui = Instantiate(eb.UI_hp_prefab) as GameObject;
			hp_ui.transform.SetParent(GameObject.FindGameObjectWithTag("HP_UI").transform);
			hp_ui.transform.localRotation = Quaternion.Euler(Vector3.zero);
            hp_ui.transform.localPosition = new Vector3(550, E_UI_POS[i], 0);
			hp_ui.transform.localScale = new Vector3(1.5f, 0.3f, 0);
			eb.UI_hp = hp_ui.GetComponentInChildren<Slider>();
			eb.UI_hp.maxValue = eb._maxHP;
			eb.UI_hp.value = eb._hp;
        }
		enemyRoot.rotation = Quaternion.Euler(0, 180, 0);
	}

	/// <summary>
	/// スキルのデータを読み込む
	/// </summary>
	/// <returns>Json date.</returns>
	/// <param name="fileName">File name.</param>
	public string GetEnemyData(string fileName){
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
		//json fileを読み込む
		string JsonString = File.ReadAllText (filePath);
		return JsonString;
	}
    /// <summary>
    /// Ge tEnemy Perfab list(バグってる、直す必要がある)
    /// </summary>
    /// <param name="ed">Enemy Date</param>
	public void GetEnemyPerfabs(string ed)
	{
		PlayerRoot pr = this.GetComponent<PlayerRoot>();
		SetCollect sc = JsonUtility.FromJson<SetCollect>(ed);
		float p = 0;
		foreach (var set in sc.sets)
		{
			p += set.probability;
		}
		float setNum = Random.Range(0,p);
		p = 0;
		foreach (var set in sc.sets)
		{
			p += set.probability;
			if (setNum <= p)
			{
				foreach (var name in set.enemy_set)
				{
					switch (name)
					{
						case "A":
							EnemyPrefabList.Add(pr.e_prefabList[1]);
							break;
						case "D":
							EnemyPrefabList.Add(pr.e_prefabList[2]);
							break;
						case "M":
							EnemyPrefabList.Add(pr.e_prefabList[3]);
							break;
						case "S":
							EnemyPrefabList.Add(pr.e_prefabList[0]);
							break;
					}
				}
			}
		}
	}
	/// <summary>
	/// 全てのスキルのリーキャストをスタートする
	/// </summary>
	/// <param name="player">Player.</param>
	IEnumerator BattelStartRecast(GameObject player){
		JobBase jb =  player.GetComponent<JobBase>();
		while (true) {
			GameObject c = player.transform.GetChild (0).gameObject;
			if (c.transform.childCount != 0) {
				for (int i = 0; i < c.transform.childCount; i++) {
					jb.StartCoroutine(jb.SkillRecast (c.transform.GetChild (i).gameObject, BATTEL_START_RECAST_TIME));
				}
				yield break;
			}
			yield return new WaitForEndOfFrame ();
		}
	}
}
