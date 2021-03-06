﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TankScript : JobBase {
	#region Properties
	private int hit_count;		//撃たれた回数
	#endregion

	// Use this for initialization
	void Start () {
		startPos = this.transform.position;
		p_funcList = new P_Delegate[] { Skill2, Skill4 };
		Set_b_Status(BattelStatus.NORMAL);
		controller = ReadyMode.Instance;
		controller.Enter(this);
		skillBtnGenerate();
		HideSkillBtn();
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
	/// カウンター
    /// 敵視を集め、一定時間攻撃受ける
    /// カウンター：受けた回数×１０自身の攻撃に上乗せする　
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill1(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
        StartCoroutine(MagicDamage(this.gameObject, sc, 1f, "Prefabs/Magic/Counter", 2f));
        /// 敵配列からすべての敵のターゲットを自分に変える　持続的？
		List<GameObject> enemyList = PlayerRoot.Instance.enemyList;
		foreach (GameObject enemy in enemyList)
		{
			EnemyBase eb = enemy.GetComponent<EnemyBase>();
            try{
			    if(eb._target.layer == LayerMask.NameToLayer("Player"))
			    {
				    eb._target = this.gameObject;
			    }
            }
            catch (UnassignedReferenceException)
            {

            }
		}
	}
	/// <summary>
	/// イース
    /// 全攻撃２０％カット
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill2(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
        StartCoroutine(StatusMagic(target, sc, 1f, "Prefabs/Magic/SingleSheild", 2f, ConditionStatus.ALL_DAMAGE_DOWN_20));
	}
	/// <summary>
    /// 対象の攻撃を肩代わりする
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill3(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		this.GetComponentInChildren<Animator>().SetTrigger("Attack");
	}
	/// <summary>
	/// 魔法、物理攻撃１０％カット
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill4(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
        this.GetComponentInChildren<Animator>().SetTrigger("Attack2");
        StartCoroutine(StatusMagic(target, sc, 1f, "Prefabs/Magic/Counter", 2f, ConditionStatus.ALL_DAMAGE_DOWN));
	}
	#endregion
}
