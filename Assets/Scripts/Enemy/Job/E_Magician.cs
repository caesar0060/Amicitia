﻿using UnityEngine;
using System.Collections;

public class E_Magician : EnemyBase {
	#region Properties
	#endregion
	// ------------------------------------------------------------------------------------
	//*										Debug用
	void OnGUI() {
		GUI.Label(new Rect(300, 10, 100, 20), controller.ToString());
		GUI.Label(new Rect(300, 50, 100, 20), "skill1"+skillList[0].isRecast.ToString());
		GUI.Label(new Rect(300, 90, 100, 20), "skill3"+skillList[2].isRecast.ToString());
	}
	//*/
	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		startLocalPos = this.transform.localPosition;
		// Skillの配列に登録--------
		skillArray = new E_Delegate[]{Skill1,Skill2,Skill3,Skill4};
		string skillDate = GetSKillDate ("Magician_Skill.json");
		CreateSkillList (skillArray, skillDate);
		//---------------------------
		Set_b_Status(BattelStatus.NORMAL);
		controller = E_BattelMode.Instance;
		controller.Enter(this);
		e_pr = GameObject.Find("GameRoot").GetComponent<PlayerRoot>();
		// チームによるモードを変更する
		switch (GetModeNumber())
		{
		case 15:
			ChangeMode(M_M3.Instance);
			break;
		case 12:
			ChangeMode(M_M2D1.Instance);
			break;
		case 11:
			ChangeMode(M_M2A1.Instance);
			break;
		case 7:
			ChangeMode(M_A2M1.Instance);
			break;
		case 9:
			ChangeMode(M_D2M1.Instance);
			break;  
		default:
			ChangeMode(M_Normal.Instance);
			break;
		}
		StartCoroutine (Loading(5.0f));
	}
	// Update is called once per frame
	void Update () {
		CheckDead ();
	}
	#region Skill
	/// <summary>
    /// ルーン
    /// 魔法攻撃　
	/// </summary>
	/// <param name="target">ターゲット</param>
	/// <param name="time">効果時間</param>
    override public void Skill1(GameObject target = null, float effectTime = 0)
	{
		StartCoroutine( LerpMove (this.gameObject, this.transform.position, 
			target.transform.position, 1, target, skillList [0], "isBom"));
	}
	/// <summary>
    /// エオロー
    /// 魔法攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill2(GameObject target = null, float effectTime = 0)
	{
		Debug.Log ("Skill2");
	}
	/// <summary>
    /// ハーガル
    /// スロウ状態にする
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill3(GameObject target = null, float effectTime = 0)
	{
		JobBase jb = target.GetComponent<JobBase> ();
		this.GetComponentInChildren<Animator> ().SetTrigger ("isBom");
		StartCoroutine (Damage (target, skillList [2], 1));
        jb.Set_c_Status(ConditionStatus.SLOW);
		jb.StartCoroutine( jb.StatusCounter(ConditionStatus.SLOW, effectTime));
	}
	/// <summary>
    /// 範囲魔法攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill4(GameObject target = null, float effectTime = 0)
	{
		Debug.Log ("Skill4");
	}
	#endregion
}