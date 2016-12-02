using UnityEngine;
using System.Collections;

public class AttackerScript : JobBase {
	#region Properties


	#endregion

	// Use this for initialization
	void Start () {
		p_funcList = new P_Delegate[]{ Skill1, Skill1, Skill1, Skill1, Skill1, Skill1 };
		Set_b_Status (BattelStatus.NORMAL);
		//-----test
		controller = WorldMode.Instance;
		controller.Enter (this);
		//---------
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
	/// スラッシュ
	/// 物理攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="effectTime">Effect time.</param>
	public void Skill1(SkillScript sc, GameObject target = null, float effectTime = 0)
    {
		float s_power = 1;	//精霊の力
		EnemyBase eb = target.GetComponent<EnemyBase>();
		if (CheckFlag (ConditionStatus.POWER_UP))
			s_power = 1.5f;
		eb.e_hp -= (int)((p_attack + sc.s_power) * s_power) - eb.e_defence;
		//----
		//Animation
		//----
	}
	/// <summary>
	/// 物理強攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="effectTime">Effect time.</param>
	public void Skill2(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
		float s_power = 1;	//精霊の力
		EnemyBase eb = target.GetComponent<EnemyBase>();
		if (CheckFlag (ConditionStatus.POWER_UP))
			s_power = 1.5f;
		eb.e_hp -= (int)((p_attack + sc.s_power) * s_power) - eb.e_defence;
		//----
		//Animation
		//----
	}
	/// <summary>
	/// 敵一体を中心に範囲攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="effectTime">Effect time.</param>
	public void Skill3(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
		Debug.Log ("Tank Skill1");
	}
	/// <summary>
	/// 回転切り
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="effectTime">Effect time.</param>
	public void Skill4(SkillScript sc, GameObject target = null, float effectTime = 0)
	{
		float s_power = 1;	//精霊の力
		EnemyBase eb = target.GetComponent<EnemyBase>();
		if (CheckFlag (ConditionStatus.POWER_UP))
			s_power = 1.5f;
		eb.e_hp -= (int)((p_attack + sc.s_power) * s_power) - eb.e_defence;
		//----
		//Animation
		//----
	}
	#endregion
}
