using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LaderScript : JobBase {
	#region Properties
	#endregion

	// Use this for initialization
	void Start () {
		p_funcList = new P_Delegate[]{ Skill1, Skill2, Skill3, Skill4 };
		skillBtnGenerate ();
		Set_b_Status (BattelStatus.NORMAL);
		//-----test
		controller = ReadyMode.Instance;
		controller.Enter (this);
		//---------		
		/*foreach (ConditionStatus status in Enum.GetValues(typeof(ConditionStatus))) {
			if (!CheckFlag (status))
				Debug.Log (Enum.GetName (typeof(ConditionStatus), status));
		}*/
		HideSkillBtn ();
	}

	// Update is called once per frame
	void Update () {
		//test-----
		controller.Excute (this);
		//test-----
	}

	#region Skill
	/// <summary>
	/// 力の精霊
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill1(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
		JobBase jb = target.GetComponent<JobBase>();
		jb.Set_c_Status(ConditionStatus.POWER_UP);
		jb.StartCoroutine(jb.StatusCounter(ConditionStatus.POWER_UP, effectTime));
	}
	/// <summary>
	/// 魔力の精霊
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill2(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
		JobBase jb = target.GetComponent<JobBase>();
		jb.Set_c_Status(ConditionStatus.MAGIC_UP);
		jb.StartCoroutine(jb.StatusCounter(ConditionStatus.MAGIC_UP, effectTime));
	}
	/// <summary>
	/// 守りの精霊
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill3(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
		JobBase jb = target.GetComponent<JobBase>();
		jb.Set_c_Status(ConditionStatus.M_DEF_UP);
		jb.Set_c_Status(ConditionStatus.P_DEF_UP);
		jb.StartCoroutine(jb.StatusCounter(ConditionStatus.M_DEF_UP, effectTime));
		jb.StartCoroutine(jb.StatusCounter(ConditionStatus.P_DEF_UP, effectTime));
	}
	/// <summary>
	/// 癒しの精霊
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill4(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
		switch (target.layer)
		{
			
		}
	}
	#endregion
}
