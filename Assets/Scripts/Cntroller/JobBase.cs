using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class JobBase : StatusControl {
	#region Properties
	private static float BUTTON_DISTANCE = 1;
	// HP
	public int _hp;
	// MP
	public int _mp;
	// 攻撃力
	public int _attack;
	//防御力
	public int _defence;
	//ターゲット
	public GameObject _target;
	//インスタンスを保存するコントローラ
	public Controller controller;
	// Skillを保存用配列
	public GameObject[] p_skillList;
	public Delegate[] p_funcList;
	//
	public GameObject p_object = null;
	//
	public Delegate skillUse;
	#endregion

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void OnTriggerStay(Collider other){
		switch(battelStatus){
		case BattelStatus.NOT_IN_BATTEL:
			GameObject other_go = other.gameObject;
			if(other_go.layer == LayerMask.NameToLayer("NPC")){
				if (_target == null) {
					if (CheckIsFront (other_go))
						_target = other_go;
				} else if (_target == other_go) {
					if (!CheckIsFront (other_go))
						_target = null;
				}
			}
			break;
		}
	}

	public void OnTriggerExit(Collider other){
		switch(battelStatus){
		case BattelStatus.NOT_IN_BATTEL:
			if (_target == other.gameObject)
				_target = null;
			break;
		}
	}
	#region Function
	/// <summary>
	/// モードを変更する
	/// </summary>
	/// <param name="newMode">New mode.</param>
	public void ChangeMode(Controller newMode){
		if (controller != newMode) {
			controller.Exit (this);
			controller = newMode;
			controller.Enter (this);
		} else
			Debug.LogError ("same");
	}
	/// <summary>
	/// Objectは前にいるかどうかをチェックする
	/// </summary>
	/// <returns><c>true</c>, 前にいるなら, <c>false</c> 違う場合.</returns>
	/// <param name="other">Object.</param>
	public bool CheckIsFront(GameObject other){
		bool isFront = false;
		// プレイヤーが現在向いている方向を保管
		Vector3 heading = this.transform.TransformDirection (Vector3.forward);
		// プレイヤーから見たObjectの方向を保管
		Vector3 to_other = other.transform.position - this.transform.position;
		heading.y = 0;
		to_other.y = 0;
		//正規化する
		heading.Normalize ();
		to_other.Normalize ();
		//内積を求める
		float dp = Vector3.Dot (heading, to_other);
		//内積が45度のコサイン値未満なら、falseを返す
		if (dp < Mathf.Cos (45)) {
			return isFront;
		}
		//内積が45度のコサイン以上なら、trueを返す
		isFront = true;	
		return isFront;
	}
	/// <summary>
	/// 攻撃
	/// </summary>
	virtual public void Attack(){
		Debug.Log ("Attack");
	}
	/// <summary>
	/// 防御
	/// </summary>
	virtual public void Defense(){
		Debug.Log ("Defense");
	}
	/// <summary>
	/// Jump
	/// </summary>
	virtual public void Jump(){
		Debug.Log ("Jump");
	}
	/// <summary>
	/// 異常状態を設定し、カウントする
	/// </summary>
	/// <param name="c_status">C status.</param>
	/// <param name="time">Time.</param>
	public void SetCondition (ConditionStatus c_status, float time){
		Set_c_Status (c_status);
		StartCoroutine(StatusCounter(c_status ,time));
	} 
	public void skillBtnGenerate(){
		float a = 360 / p_skillList.Length;
		Vector3 pos;
		for (int i = 0; i < p_skillList.Length; i++) {
			pos = new Vector3 (Mathf.Sin (Mathf.Deg2Rad * a * i) * BUTTON_DISTANCE , 
				Mathf.Cos (Mathf.Deg2Rad * a * i) * BUTTON_DISTANCE , 0);
			GameObject p_skillBtn = Instantiate (p_skillList [i]) as GameObject;
			p_skillBtn.transform.SetParent (this.transform);
			p_skillBtn.transform.localPosition = pos;
			p_skillBtn.GetComponentInChildren<SkillScript> ().skillMethod = p_funcList [i];
		}
	}
	public void skillBtnRemove(){
		GameObject[] btnList = GameObject.FindGameObjectsWithTag ("SkillButton");
		foreach (var obj in btnList) {
			Destroy (obj);
		}
	}
	#endregion
	#region Co-routine
	/// <summary>
	/// 状態の時間をカウント
	/// </summary>
	/// <param name="c_status">状態</param>
	/// <param name="time">効果時間</param>
	public IEnumerator StatusCounter(ConditionStatus c_status, float time){
		float startTime = Time.time;
		while (true) {
			if (CheckFlag (c_status)) {
				if ((Time.time - startTime) >= time) {
					Remove_c_status (c_status);
					yield break;
				}
				yield return new WaitForSeconds (COROUTINE_WAIT_TIME);
			} else
				yield break;
		}
	}
	/// <summary>
	/// どんな状態が立っているを検査する
	/// </summary>
	public IEnumerator CheckStatus(){
		while (true) {
			foreach (ConditionStatus status in Enum.GetValues(typeof(ConditionStatus))) {
				if (CheckFlag (status)) {
					Debug.Log (Enum.GetName (typeof(ConditionStatus), status));
				}
			}
			yield return new WaitForSeconds (COROUTINE_WAIT_TIME);
		}
	}
	#endregion
}
