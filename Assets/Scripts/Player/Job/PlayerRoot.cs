﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerRoot : SingletonMonoBehaviour<PlayerRoot>
{
    #region Properties
	public static float BUTTON_RETURN_TIME = 0.5f;
    //インスタンスを保存するコントローラ
	public RootController controller;
	// JobBaseを保管する
	public JobBase p_jb;
	// 敵を保管する配列
	[HideInInspector] public List<GameObject> enemyList = new List<GameObject> ();
	// パーティーメンバーを保管する配列
	public List<GameObject>partyList = new List<GameObject> ();
	// ボタンの移動用タイマー
	private float timer =0;
	// ターゲットのレイヤー
	public Dictionary<TargetType,Dictionary<TargetNum, int>> s_targetLayer;
	// 
	[HideInInspector] public GameObject btn = null;
	//
	[HideInInspector] public SkillScript s_script;
    #endregion

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		//-----test
		controller = WalkMode.Instance;
		controller.Enter (this);
		//---------
		CreateDictionary();
	}
	// ------------------------------------------------------------------------------------
	//										Debug用
	void OnGUI() {
		if (GUI.Button (new Rect (10, 10, 100, 20), "Battel Mode"))
			ChangeMode (BattelStart.Instance);

		if (GUI.Button(new Rect(10, 50, 100, 20), "Walk Mode"))
			ChangeMode (WalkMode.Instance);

	}
	//--------------------------------------------------------------------------------------
	// Update is called once per frame
	void Update () {
			controller.Excute(this);
	}
	#region Function
	/// <summary>
	/// モードを変更する
	/// </summary>
	/// <param name="newMode">New mode.</param>
	public void ChangeMode(RootController newMode){
		if (controller != newMode) {
			controller.Exit (this);
			controller = newMode;
			controller.Enter (this);
		} else
			Debug.LogError ("same");
	}
	/// <summary>
	/// 対象を等速で動かす.
	/// </summary>
	/// <param name="obj">対象.</param>
	/// <param name="timeRate">Time rate.</param>
	public IEnumerator LerpMove(GameObject obj,Vector3 startPos, Vector3 endPos, float time, float speed =1){
		while (true) {
			timer += Time.deltaTime * speed;
			float moveRate = timer / BUTTON_RETURN_TIME;
			if (moveRate  >= 1) {
				moveRate = 1;
				obj.transform.position = Vector3.Lerp (endPos, startPos, moveRate);
				timer = 0;
				yield break;
			}
			obj.transform.position = Vector3.Lerp (endPos, startPos, moveRate);
			yield return new WaitForEndOfFrame ();
		}
	}
	/// <summary>
	/// Creates the dictionary.
	/// </summary>
	public void CreateDictionary(){
		Dictionary<TargetNum , int> playerList = new Dictionary<TargetNum, int> () {
			{TargetNum.ONE, LayerMask.NameToLayer("Player")},
			{TargetNum.MUTIPLE, LayerMask.NameToLayer("Player")}
		};
		Dictionary<TargetNum , int> enemyList = new Dictionary<TargetNum, int> () {
			{TargetNum.ONE, LayerMask.NameToLayer("Enemy")},
			{TargetNum.MUTIPLE, LayerMask.NameToLayer("Enemy")}
		};
		s_targetLayer = new Dictionary<TargetType, Dictionary<TargetNum, int>> () {
			{ TargetType.PLAYER, playerList },
			{ TargetType.ENEMY, enemyList }
		};
	}
	/// <summary>
	/// プレイヤーを生成する
	/// </summary>
	public void CreatePlayer(){
		GameObject player = Instantiate (partyList [0], Vector3.zero, this.transform.rotation) as GameObject;
		player.transform.parent = this.transform;
		player.name = "Player";
		player.transform.localPosition = Vector3.zero;
		p_jb = player.GetComponent<JobBase> ();
	}
	/// <summary>
	/// 子供を削除する
	/// </summary>
	public void DestroyChild(string name){
		Destroy (this.transform.FindChild (name).gameObject);
	}
	#endregion
}
