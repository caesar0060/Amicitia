using UnityEngine;
using System.Collections;

public class PartyRoot : MonoBehaviour {
	// バトルが始めて最初にすべてのスキルをリーキャストする時間
	public static float BATTEL_START_RECAST_TIME = 5.0f;
	//
	public static float DISTANCE = 2.0f;
	//
	public static Vector3[]  posArray= new Vector3[] {new Vector3(0,0,-1),
		new Vector3(0,0,1), new Vector3(1,0,0), new Vector3(-1,0,0)
	};
	//
	public Transform enemyRoot;
	// Use this for initialization
	void Start () {
		ReadyBattel();
	}
	
	// Update is called once per frame
	void Update () {
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
			player.GetComponent<JobBase> ().Set_b_Status (BattelStatus.NORMAL);
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
