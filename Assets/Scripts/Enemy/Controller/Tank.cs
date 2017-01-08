using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// T_D3 Singleton
/// </summary>
public class T_D3 : E_Controller
{
    // インスタンス
    private static T_D3 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static T_D3 Instance
    {
        get
        {
            if (instance == null)
                instance = new T_D3();
            return instance;
        }
    }
    override public void Enter(EnemyBase eb = null)
    {
        Debug.Log("PEnter");
    }
    override public void Excute(EnemyBase eb = null)
    {
        if (eb.CanTakeAction())
        {
            foreach (var target in eb.e_pr.enemyList)
            {
                EnemyBase t_eb = target.GetComponent<EnemyBase>();
                if (t_eb._type == JobType.Leader && t_eb.battelStatus != BattelStatus.DEAD)
                {
                    // Edit skill later
                    eb.SkillUse(target, eb.skillList[0]);
                    return;
                }
            }
        }
        else
        {
            Debug.Log("TODO: 状態異常のモードに移動");
            return;
        }
    }
    override public void Exit(EnemyBase eb = null)
    {
        Debug.Log("PExit");
    }
}

/// <summary>
/// T_A2D1 Singleton
/// </summary>
public class T_A2D1 : E_Controller
{
	// インスタンス
    private static T_A2D1 instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
    public static T_A2D1 Instance
    {
		get {
			if(instance == null)
                instance = new T_A2D1();
			return instance;
		}
	}
	override public void Enter(EnemyBase eb = null)
	{
		Debug.Log("PEnter");
	}
	override public void Excute(EnemyBase eb = null)
	{
		Debug.Log("PExcute");
	}
	override public void Exit(EnemyBase eb = null)
	{
		Debug.Log("PExit");
	}
}

/// <summary>
/// T_D2M1 Singleton
/// </summary>
public class T_D2M1 : E_Controller
{
	// インスタンス
    private static T_D2M1 instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
    public static T_D2M1 Instance
    {
		get {
			if(instance == null)
                instance = new T_D2M1();
			return instance;
		}
	}
	override public void Enter(EnemyBase eb = null)
	{
		Debug.Log("PEnter");
	}
	override public void Excute(EnemyBase eb = null)
	{
		Debug.Log("PExcute");
	}
	override public void Exit(EnemyBase eb = null)
	{
		Debug.Log("PExit");
	}
}

/// <summary>
/// T_D2A1 Singleton
/// </summary>
public class T_D2A1 : E_Controller
{
    // インスタンス
    private static T_D2A1 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static T_D2A1 Instance
    {
		get {
			if(instance == null)
                instance = new T_D2A1();
			return instance;
		}
	}
	override public void Enter(EnemyBase eb = null)
	{
		Debug.Log("PEnter");
	}
	override public void Excute(EnemyBase eb = null)
	{
		Debug.Log("PExcute");
	}
	override public void Exit(EnemyBase eb = null)
	{
		Debug.Log("PExit");
	}
}

/// <summary>
/// T_M2D1 Singleton
/// </summary>
public class T_M2D1 : E_Controller
{
    // インスタンス
    private static T_M2D1 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static T_M2D1 Instance
    {
        get
        {
            if (instance == null)
                instance = new T_M2D1();
            return instance;
        }
    }
    override public void Enter(EnemyBase eb = null)
    {
        Debug.Log("PEnter");
    }
    override public void Excute(EnemyBase eb = null)
    {
        Debug.Log("PExcute");
    }
    override public void Exit(EnemyBase eb = null)
    {
        Debug.Log("PExit");
    }
}

/// <summary>
/// T_Normal Singleton
/// </summary>
public class T_Normal : E_Controller
{
    // インスタンス
    private static T_Normal instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static T_Normal Instance
    {
        get
        {
            if (instance == null)
                instance = new T_Normal();
            return instance;
        }
    }
    override public void Enter(EnemyBase eb = null)
    {
        Debug.Log("T_Normal_Enter");
    }
    override public void Excute(EnemyBase eb = null)
    {
		if (eb.CanTakeAction())
        {
			if (!eb.CheckFlag (ConditionStatus.ALL_DAMAGE_DOWN)) {
				if (!eb.skillList [1].isRecast) {
                    // Edit skill later
                    eb.SkillUse(eb.gameObject, eb.skillList[1]);
                    return;
				}
			}

            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.enemyList)
            {
                EnemyBase t_eb = target.GetComponent<EnemyBase>();
                if ((t_eb._hp / t_eb._maxHP * 100 < 50) && t_eb.battelStatus != BattelStatus.DEAD)
                {
                    if (t_eb._type == JobType.Leader)
                    {
                        // Edit skill later
                        eb.SkillUse(target, eb.skillList[0]);
                        return;
                    }
                    targetCount.Add(target);
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[0].isRecast)
                {
                    // Edit skill later
                    eb.SkillUse(targetCount[ran], eb.skillList[0]);
                    return;
                }
            }

            foreach (var target in eb.e_pr.partyList)
            {
                int ran = Random.Range(0, eb.e_pr.partyList.Count);
                JobBase jb = eb.e_pr.partyList[ran].GetComponent<JobBase>();
                if (jb.p_target.layer == LayerMask.NameToLayer("Enemy"))
                {
                    // Edit skill later
                    eb.SkillUse(jb.p_target, eb.skillList[0]);
                    return;
                }
            }
        }
        else
            Debug.Log("TODO: 状態異常のモードに移動");
    }
    override public void Exit(EnemyBase eb = null)
    {
        Debug.Log("T_Normal_Exit");
    }
}