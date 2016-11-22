using UnityEngine;
using System.Collections;

public class E_TankScript : EnemyBase {
	#region Properties
	private int hit_count;		//撃たれた回数
	[HideInInspector]
	public GameObject t_myTarget;
	#endregion
	// Use this for initialization
	void Start () {
		// Skillの配列に登録--------
		skillArray = new Delegate[]{Skill1,Skill2,Skill3,Skill4};
		string skillDate = GetSKillDate ("Tank_Skill.json");
		CreateSkillList (skillArray, skillDate);
		//---------------------------
		Set_b_Status(BattelStatus.NORMAL);
		controller = T_Normal.Instance;
		e_pr = GameObject.Find("GameRoot").GetComponent<PlayerRoot>();
		// チームによるモードを変更する
		switch (GetModeNumber())
		{
		case 6:
			ChangeMode(T_D3.Instance);
			break;
		case 4:
			ChangeMode(T_A2D1.Instance);
			break;
		case 9:
			ChangeMode(T_D2M1.Instance);
			break;
		case 5:
			ChangeMode(T_D2A1.Instance);
			break;
		case 12:
			ChangeMode(T_M2D1.Instance);
			break;
		default:
			controller.Enter (this);
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		controller.Excute (this);
	}
	#region Skill
	/// <summary>
	/// カウンター
	/// 敵視を集め、一定時間攻撃受ける
	/// カウンター：受けた回数×１０自身の攻撃に上乗せする　
	/// </summary>
	/// <param name="target">ターゲット</param>
	/// <param name="time">効果時間</param>
	override public void Skill1(GameObject target = null, float time = 0)
	{
		Set_c_Status(ConditionStatus.PULL);
		StatusCounter(ConditionStatus.PULL, time);
	}
	/// <summary>
	/// イース
	/// 全攻撃２０％カット
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	override public void Skill2(GameObject target = null, float time = 0)
	{
		Set_c_Status(ConditionStatus.ALL_DAMAGE_DOWN);
		StatusCounter(ConditionStatus.ALL_DAMAGE_DOWN, time);
	}
	/// <summary>
	/// 対象の攻撃を肩代わりする
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	override public void Skill3(GameObject target = null, float time = 0)
	{
		Set_c_Status(ConditionStatus.TAKE_OVER);
		StatusCounter(ConditionStatus.TAKE_OVER, time);
	}
	/// <summary>
	/// 魔法、物理攻撃１０％カット
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	override public void Skill4(GameObject target = null, float time = 0)
	{
		if (target != null)
		{
			JobBase jb = target.GetComponent<JobBase>();
			jb.Set_c_Status(ConditionStatus.ALL_DAMAGE_DOWN);
		}
	}
	#endregion
}