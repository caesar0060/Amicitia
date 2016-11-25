using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TankScript : JobBase {
	#region Properties
	private int hit_count;		//撃たれた回数
	#endregion

	// Use this for initialization
	void Start () {
		p_funcList = new Delegate[]{ Skill1, Skill2, Skill3, Skill4 };
		Set_b_Status (BattelStatus.NORMAL);
		//-----test
		controller = WorldMode.Instance;
		controller.Enter (this);
		//---------		
		/*foreach (ConditionStatus status in Enum.GetValues(typeof(ConditionStatus))) {
			if (!CheckFlag (status))
				Debug.Log (Enum.GetName (typeof(ConditionStatus), status));
		}*/
		skillBtnGenerate ();
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
	/// カウンター
    /// 敵視を集め、一定時間攻撃受ける
    /// カウンター：受けた回数×１０自身の攻撃に上乗せする　
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    public void Skill1(GameObject target = null, float effectTime = 0, float recastTime = 0)
    {
		Set_c_Status(ConditionStatus.PULL);
        StatusCounter(ConditionStatus.PULL, effectTime);
		Debug.Log ("Tank");
	}
	/// <summary>
	/// イース
    /// 全攻撃２０％カット
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    public void Skill2(GameObject target = null, float effectTime = 0, float recastTime = 0)
    {
		Set_c_Status(ConditionStatus.ALL_DAMAGE_DOWN);
		StatusCounter (ConditionStatus.ALL_DAMAGE_DOWN, effectTime);
	}
	/// <summary>
    /// 対象の攻撃を肩代わりする
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    public void Skill3(GameObject target = null, float effectTime = 0, float recastTime = 0)
    {
	}
	/// <summary>
	/// 魔法、物理攻撃１０％カット
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    public void Skill4(GameObject target = null, float effectTime = 0, float recastTime = 0)
    {
		JobBase jb = target.GetComponent<JobBase> ();
		jb.Set_c_Status (ConditionStatus.ALL_DAMAGE_DOWN);
        StatusCounter(ConditionStatus.ALL_DAMAGE_DOWN, effectTime);
	}
	#endregion
}
