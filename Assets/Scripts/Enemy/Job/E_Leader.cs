using UnityEngine;
using System.Collections;

public class E_Leader : EnemyBase {
	#region Properties
	private int hit_count;		//撃たれた回数
	[HideInInspector]
	public GameObject t_myTarget;
	#endregion
    // ------------------------------------------------------------------------------------										Debug用
    void OnGUI()
    {
        /*
        GUI.Label(new Rect(300, 10, 100, 20), controller.ToString());
        GUI.Label(new Rect(300, 50, 100, 20), "skill1"+skillList[0].isRecast.ToString());
        GUI.Label(new Rect(300, 90, 100, 20), "skill4"+skillList[3].isRecast.ToString());
         */
    }

	// Use this for initialization
    void Start()
    {
        startPos = this.transform.position;
        startLocalPos = this.transform.localPosition;
        // Skillの配列に登録--------
        skillArray = new E_Delegate[] { Skill1, Skill2, Skill3, Skill4 };
        string skillDate = GetSKillDate("Patron_Skill.json");
        CreateSkillList(skillArray, skillDate);
        //---------------------------
        Set_b_Status(BattelStatus.NOT_IN_BATTEL);
        controller = E_FieldMode.Instance;
        controller.Enter(this);
        e_pr = GameObject.Find("GameRoot").GetComponent<PlayerRoot>();
        if (GameObject.FindGameObjectWithTag("PartyRoot"))
        {
            Set_b_Status(BattelStatus.NORMAL);
            this.GetComponent<SphereCollider>().enabled = false;
            // チームによるモードを変更する
            switch (GetModeNumber())
            {
                default:
                    ChangeMode(L_Normal.Instance);
                    break;
            }
            coroutine = StartCoroutine(Loading(5.0f));
        }
        else
            coroutine = StartCoroutine(Loading(1.0f));
    }
    // Update is called once per frame
    void Update()
    {
        CheckDead();
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
        this.GetComponentInChildren<Animator>().SetTrigger("Attack");
        StartCoroutine(this.StatusMagic(_target, skillList[0], 1f, "Prefabs/Magic/Power_up", 2f, ConditionStatus.POWER_UP));
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
        this.GetComponentInChildren<Animator>().SetTrigger("Attack");
        StartCoroutine(this.StatusMagic(_target, skillList[2], 1f, "Prefabs/Magic/Magic_up", 2f, ConditionStatus.POWER_UP));
	}
	/// <summary>
    /// 癒しの精霊
    /// 回復/強ダメージ
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
    override public void Skill4(GameObject target = null, float effectTime = 0)
	{
        this.GetComponentInChildren<Animator>().SetTrigger("Attack");
        StartCoroutine(this.StatusMagic(_target, skillList[3], 1f, "Prefabs/Magic/Recorvery", 2f, ConditionStatus.POWER_UP));
	}
	#endregion
}