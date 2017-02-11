using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LeaderScript : JobBase {
	#region Properties
	#endregion

	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		p_funcList = new P_Delegate[] { Skill4, Skill1, Skill2, Skill3 };
		Set_b_Status(BattelStatus.NORMAL);
		controller = ReadyMode.Instance;
		controller.Enter(this);
		skillBtnGenerate();
        HideKirenBtn();
		//---------		
		/*foreach (ConditionStatus status in Enum.GetValues(typeof(ConditionStatus))) {
			if (!CheckFlag (status))
				Debug.Log (Enum.GetName (typeof(ConditionStatus), status));
		}*/
	}

	// Update is called once per frame
	void Update () {
        CheckDead();
		controller.Excute (this);
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
        StartCoroutine(this.StatusMagic(_target, sc, 1f, "Prefabs/Magic/Power_up", 2f,ConditionStatus.POWER_UP));
	}
	/// <summary>
	/// 魔力の精霊
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill2(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
        StartCoroutine(this.StatusMagic(_target, sc, 1f, "Prefabs/Magic/Magic_up", 2f, ConditionStatus.MAGIC_UP));
	}
	/// <summary>
	/// 守りの精霊
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill3(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
        ConditionStatus[] status_array = {ConditionStatus.M_DEF_UP, ConditionStatus.P_DEF_UP};
        StartCoroutine(this.StatusMagic(_target, sc, 1f, "Prefabs/Magic/Defence_up", 2f, status_array));
	}
	/// <summary>
	/// 癒しの精霊
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill4(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
		StartCoroutine (MagicDamage (_target, sc, 1f,"Prefabs/Magic/Recorvery",2f));
	}
	#endregion
}
