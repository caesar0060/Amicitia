using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class JobBase : StatusControl {
	#region Properties
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
	//Playerの移動速度
	public float moveSpeed;
	//インスタンスを保存するコントローラ
	public Controller controller;
	// Skillを保存用配列
	public delegate void funcDelegate();
	public static Dictionary<string, funcDelegate> skill_list = new Dictionary<string, funcDelegate> ();
	//
	public funcDelegate nowSkill;
	public funcDelegate previousSkill;
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
			Debug.Log ("same");
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
	#endregion
	#region Co-routine
	/// <summary>
	/// 状態の時間をカウント
	/// </summary>
	/// <returns>The counter.</returns>
	/// <param name="time">Time.</param>
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
