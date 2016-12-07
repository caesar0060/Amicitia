﻿using UnityEngine;
using System.Collections;

public class MagicianScript: JobBase {
	#region Properties


	#endregion

	// ------------------------------------------------------------------------------------
	/*										Debug用
	void OnGUI() {
		GUI.Label (new Rect (120, 10, 200, 20), "Attacker: " + controller.ToString ());
		GUI.Label (new Rect (120, 50, 200, 20), "Attacker: " + conditionStatus.ToString());
	}
	*/

	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		p_funcList = new P_Delegate[]{ Skill1};
		Set_b_Status (BattelStatus.NORMAL);
		controller = ReadyMode.Instance;
		controller.Enter (this);
		skillBtnGenerate ();
		HideSkillBtn ();
	}

	// Update is called once per frame
	void Update () {
		//test-----
		controller.Excute (this);
		//test-----
	}
	#region Function
	/// <summary>
	/// ルーン
	/// 魔法攻撃
	/// </summary>
	/// <param name="sc">Skill Script.</param>
	/// <param name="target">Target.</param>
	/// <param name="effectTime">Effect time.</param>
	public void Skill1(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
		this.GetComponentInChildren<Animator> ().SetTrigger ("Attack");
		RotateToTarget (target);
		StartCoroutine (Damage (target, sc, 1.5f));
	}
	/// <summary>
	/// Skill2 the specified sc, target and effectTime.
	/// </summary>
	/// <param name="sc">Sc.</param>
	/// <param name="target">Target.</param>
	/// <param name="effectTime">Effect time.</param>
	public void Skill2(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
		//----
		//Animation
		//----
	}
	/// <summary>
	/// Skill3 the specified sc, target and effectTime.
	/// </summary>
	/// <param name="sc">Sc.</param>
	/// <param name="target">Target.</param>
	/// <param name="effectTime">Effect time.</param>
	public void Skill3(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
		Debug.Log ("Tank Skill1");
	}
	/// <summary>
	/// Skill4 the specified sc, target and effectTime.
	/// </summary>
	/// <param name="sc">Sc.</param>
	/// <param name="target">Target.</param>
	/// <param name="effectTime">Effect time.</param>
	public void Skill4(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
		//----
		//Animation
		//----
	}
	#endregion
}