using UnityEngine;
using System.Collections;

public class E_Magician : EnemyBase {
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
		controller = M_Normal.Instance;
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
    /// ルーン
    /// 魔法攻撃　
	/// </summary>
	/// <param name="target">ターゲット</param>
	/// <param name="time">効果時間</param>
    override public void Skill1(GameObject target = null, float effectTime = 0)
	{

	}
	/// <summary>
    /// エオロー
    /// 魔法攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill2(GameObject target = null, float effectTime = 0)
	{

	}
	/// <summary>
    /// ハーガル
    /// スロウ状態にする
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill3(GameObject target = null, float effectTime = 0)
	{
        target.GetComponent<EnemyBase>().Set_c_Status(ConditionStatus.SLOW);
        target.GetComponent<EnemyBase>().StatusCounter(ConditionStatus.SLOW, effectTime);
	}
	/// <summary>
    /// 範囲魔法攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill4(GameObject target = null, float effectTime = 0)
	{

	}
	#endregion
}