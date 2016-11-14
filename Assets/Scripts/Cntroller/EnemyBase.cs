using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType{
	NONE = 0,
	Attacker = 1,
	Defender = 2,
	Magician = 5,
}

public class EnemyBase : StatusControl {
	#region Properties
	// HP
	public int _hp;
	// MP
	public int _mp;
	// 攻撃力
	public int _attack;
	//防御力
	public int _defence;
	//インスタンスを保存するコントローラ
	public e_Controller controller;
	//ターゲット
	public GameObject _target;
	#endregion
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region Function
	/// <summary>
	/// モードを変更する
	/// </summary>
	/// <param name="newMode">New mode.</param>
	public void ChangeMode(e_Controller newMode){
		if (controller != newMode) {
			controller.Exit (this);
			controller = newMode;
			controller.Enter (this);
		} else
			Debug.LogError ("same");
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
