using UnityEngine;
using System.Collections;

public class E_Attacker : EnemyBase {
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
        controller = A_Normal.Instance;
		e_pr = GameObject.Find("GameRoot").GetComponent<PlayerRoot>();
		// チームによるモードを変更する
		switch (GetModeNumber())
		{
		case 6:
			ChangeMode(A_A3.Instance);
			break;
		case 4:
			ChangeMode(A_A2D1.Instance);
			break;
		case 7:
			ChangeMode(A_A2M1.Instance);
			break;
		case 5:
			ChangeMode(A_D2A1.Instance);
			break;
		case 11:
			ChangeMode(A_M2A1.Instance);
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
    /// スラッシュ
    ///物理攻撃
	/// </summary>
	/// <param name="target">ターゲット</param>
	/// <param name="time">効果時間</param>
    override public void Skill1(GameObject target = null, float effectTime = 0)
	{

	}
	/// <summary>
    /// 物理強攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill2(GameObject target = null, float effectTime = 0)
	{

	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill3(GameObject target = null, float effectTime = 0)
	{

	}
	/// <summary>
    /// 回転切り
    /// 敵一体を中心に範囲攻撃
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill4(GameObject target = null, float effectTime = 0)
	{

	}
	#endregion
}