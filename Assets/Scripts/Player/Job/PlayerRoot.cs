﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerRoot : SingletonMonoBehaviour<PlayerRoot>
{
    #region Properties

    //シーンの転移の時、フェイドアウト・インの時間
    public static float SCENE_FADE_TIME = 2f;
    //ボタンが元の場所に戻るまでの時間
	public static float BUTTON_RETURN_TIME = 0.5f;
    //インスタンスを保存する
	public RootController controller;
	// JobBaseを保管する
	public JobBase p_jb;
	// 敵を保管する配列
	//[HideInInspector] 
	public List<GameObject> e_prefabList = new List<GameObject> ();
	// プレイヤーのプレハブを保管する配列
	public List<GameObject> p_prefabList = new List<GameObject> ();
	// パーティーメンバーを保管する配列
	[HideInInspector] public List<GameObject> partyList = new List<GameObject>();
	// 敵を保管する配列
	[HideInInspector] public List<GameObject> enemyList = new List<GameObject>();
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
	}
	// ------------------------------------------------------------------------------------
	//										Debug用
	void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 100, 20), "Battel Mode"))
        {
			this.GetComponent<FadeManager>().LoadLevel("BattelScene", 2, BattelStart.Instance);
        }

        if (GUI.Button(new Rect(10, 50, 100, 20), "Walk Mode"))
        {
            this.GetComponent<FadeManager>().LoadLevel("NormalScene", 2, WalkMode.Instance);
        
        }
		GUI.Label (new Rect (10, 90, 200, 20), "Root: " + controller.ToString ());

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
	public IEnumerator LerpMove(GameObject obj,Vector3 startPos, Vector3 endPos, float speed =1){
		// ボタンの移動用タイマー
		float timer =0;
		while (true) {
			timer += Time.deltaTime * speed;
			float moveRate = timer / BUTTON_RETURN_TIME;
			if (moveRate  >= 1) {
				moveRate = 1;
				obj.transform.localPosition = Vector3.Lerp (startPos, endPos, moveRate);
				yield break;
			}
			obj.transform.localPosition = Vector3.Lerp (startPos, endPos, moveRate);
			yield return new WaitForEndOfFrame ();
		}
	}
	/// <summary>
    /// 子供を生成する
	/// </summary>
	public void CreateChild(string name, GameObject obj){
		GameObject child = Instantiate (obj, Vector3.zero, this.transform.rotation) as GameObject;
        child.transform.parent = this.transform;
        child.name = name;
        child.transform.localPosition = Vector3.zero;
        p_jb = child.GetComponent<JobBase>();
	}
	/// <summary>
	/// 子供を削除する
	/// </summary>
	public void DestroyChild(string name){
		Destroy (this.transform.FindChild (name).gameObject);
	}

	#endregion
}
