using UnityEngine;
using System.Collections;

public class E_Leader : EnemyBase {
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
    /// 力の精霊
    /// 力上昇
	/// </summary>
	/// <param name="target">ターゲット</param>
	/// <param name="time">効果時間</param>
    override public void Skill1(GameObject target = null, float effectTime = 0)
	{
		Set_c_Status(ConditionStatus.POWER_UP);
        StatusCounter(ConditionStatus.POWER_UP, effectTime);

	}
	/// <summary>
    /// 守りの精霊
    /// 物理・魔法防御上昇
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill2(GameObject target = null, float effectTime = 0)
	{
        Set_c_Status(ConditionStatus.P_DEF_UP);
        StatusCounter(ConditionStatus.P_DEF_UP, effectTime);
        Set_c_Status(ConditionStatus.M_DEF_UP);
        StatusCounter(ConditionStatus.M_DEF_UP, effectTime);     
	}
	/// <summary>
    /// 魔力の精霊
    /// 魔力上昇
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill3(GameObject target = null, float effectTime = 0)
	{
        Set_c_Status(ConditionStatus.MAGIC_UP);
        StatusCounter(ConditionStatus.MAGIC_UP, effectTime);
	}
	/// <summary>
    /// 癒しの精霊
    /// 回復/強ダメージ
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill4(GameObject target = null, float effectTime = 0)
	{

	}
	#endregion
}