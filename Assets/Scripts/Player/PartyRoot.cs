using UnityEngine;
using System.Collections;

public class PartyRoot : MonoBehaviour {
	// バトルが始めて最初にすべてのスキルをリーキャストする時間
	public static float BATTEL_START_RECAST_TIME = 5.0f;
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
		foreach (var obj in PlayerRoot.Instance.p_prefabList)
		{
			GameObject player = Instantiate(obj, Vector3.zero, this.transform.rotation) as GameObject;
			player.transform.parent = this.transform;
			player.transform.localPosition = Vector3.zero;
			StartCoroutine ("BattelStartRecast", player);
			player.GetComponentInChildren<Animator> ().SetTrigger ("Battel");
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
