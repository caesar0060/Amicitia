using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartyRoot : MonoBehaviour {
	// バトルが始めて最初にすべてのスキルをリーキャストする時間
	public static float BATTEL_START_RECAST_TIME = 5.0f;
	// メンバーとの距離
	public static float DISTANCE = 1.2f;
	// 配置の最初値
	public static Vector3[]  posArray= new Vector3[] {new Vector3(0,0,-1),
		new Vector3(0,0,1), new Vector3(1,0,0), new Vector3(-1,0,0)
	};
	//　敵の配置ポイント
	public Transform enemyRoot;
	//　攻撃の順番
	[HideInInspector] public List<GameObject> attackList = new List<GameObject> ();
	//
	[HideInInspector] public bool canAttack = true;
	// Use this for initialization
	void Start () {
		ReadyBattel();
	}
	
	// Update is called once per frame
	void Update () {
		if (attackList.Count > 0 && canAttack) {
			try{
				switch (attackList[0].layer) {
				case 8:
					JobBase jb = attackList [0].GetComponent<JobBase> ();
					jb.skillUsing.skillMethod (jb.skillUsing, jb.p_target, jb.skillUsing.s_effectTime);
					break;
				case 12:
					EnemyBase eb = attackList[0].GetComponent<EnemyBase> ();
					eb.skillUsing.skillMethod (eb.e_target, eb.skillUsing.s_effectTime);
					break;
				}
				canAttack = false;
			}
			catch(MissingReferenceException){
				return;
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
		}
		for (int i = 0; i < PlayerRoot.Instance.e_prefabList.Count; i++) {
			GameObject enemy = Instantiate (PlayerRoot.Instance.e_prefabList[i], Vector3.zero, this.transform.rotation) as GameObject;
			PlayerRoot.Instance.enemyList.Add (enemy);
			enemy.transform.parent = enemyRoot;
			enemy.transform.localPosition = posArray[i] * DISTANCE;
			enemy.transform.rotation = Quaternion.Euler (0, 180, 0);
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
