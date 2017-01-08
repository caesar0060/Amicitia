using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A_A3 Singleton
/// </summary>
public class A_A3 : E_Controller
{
    // インスタンス
    private static A_A3 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static A_A3 Instance
    {
        get
        {
            if (instance == null)
                instance = new A_A3();
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
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if (jb._type == JobType.Leader && jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(target, eb.skillList[0]));
                        return;
                    }
                }
            }

            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if (jb.battelStatus != BattelStatus.DEAD)
                {
                    targetCount.Add(target);
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[0].isRecast)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                        return;
                    }
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
/// A_A2D1 Singleton
/// </summary>
public class A_A2D1 : E_Controller
{
    #region Properties
    private EnemyBase atk_eb;
    #endregion
    // インスタンス
    private static A_A2D1 instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
    public static A_A2D1 Instance
    {
		get {
			if(instance == null)
                instance = new A_A2D1();
			return instance;
		}
	}
	override public void Enter(EnemyBase eb = null)
	{
        foreach (var enemy in eb.e_pr.enemyList)
        {
            EnemyBase enemy_eb = enemy.GetComponent<EnemyBase>();
            if (enemy_eb._type == JobType.Attacker)
                atk_eb = enemy_eb;
        }
	}
	override public void Excute(EnemyBase eb = null)
	{
        if (eb.CanTakeAction())
        {
            if (atk_eb.e_target != null)
            {
                JobBase jb = atk_eb.e_target.GetComponent<JobBase>();
                if (atk_eb.e_target.layer == LayerMask.NameToLayer("Player") &&
                        jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(atk_eb.e_target, eb.skillList[0]));
                        return;
                    }
                }
            }
            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if (jb.battelStatus != BattelStatus.DEAD)
                {
                    targetCount.Add(target);
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[0].isRecast)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                        return;
                    }
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
/// A_D2A1 Singleton
/// </summary>
public class A_D2A1 : E_Controller
{
	// インスタンス
    private static A_D2A1 instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
    public static A_D2A1 Instance
    {
		get {
			if(instance == null)
                instance = new A_D2A1();
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
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if ((jb._hp / jb._maxHP * 100 < 50 && jb._type == JobType.Leader) &&
                    jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(target, eb.skillList[0]));
                        return;
                    }
                }
            }

            EnemyBase leader_eb = eb.e_pr.enemyList[0].GetComponent<EnemyBase>();
            if (leader_eb.e_target != null)
            {
                JobBase jb = leader_eb.e_target.GetComponent<JobBase>();
                if (leader_eb.e_target.layer == LayerMask.NameToLayer("Player") &&
                    jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(leader_eb.e_target, eb.skillList[0]));
                        return;
                    }
                }
            }

            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if ((jb._type == JobType.Attacker || jb._type == JobType.Magician) && jb.battelStatus != BattelStatus.DEAD)
                {
                    targetCount.Add(target);
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[0].isRecast)
                {
                    eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                    return;
                }
            }
            else
            {
                foreach (var target in eb.e_pr.partyList)
                {
                    JobBase jb = target.GetComponent<JobBase>();
                    if (jb.battelStatus != BattelStatus.DEAD)
                    {
                        targetCount.Add(target);
                    }
                }
                if (targetCount.Count > 0)
                {
                    int ran = Random.Range(0, targetCount.Count);
                    if (!eb.skillList[0].isRecast)
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                            return;
                        }
                    }
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
/// A_M2A1 Singleton
/// </summary>
public class A_M2A1 : E_Controller
{
    // インスタンス
    private static A_M2A1 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static A_M2A1 Instance
    {
		get {
			if(instance == null)
                instance = new A_M2A1();
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
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if ((jb._hp / jb._maxHP * 100 < 50 && jb._type == JobType.Leader) &&
                    jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(target, eb.skillList[0]));
                        return;
                    }
                }
            }

            EnemyBase leader_eb = eb.e_pr.enemyList[0].GetComponent<EnemyBase>();
            if (leader_eb.e_target != null)
            {
                JobBase jb = leader_eb.e_target.GetComponent<JobBase>();
                if (leader_eb.e_target.layer == LayerMask.NameToLayer("Player") &&
                    jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(leader_eb.e_target, eb.skillList[0]));
                        return;
                    }
                }
            }

            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if ((jb._type == JobType.Attacker || jb._type == JobType.Magician) && jb.battelStatus != BattelStatus.DEAD)
                {
                    targetCount.Add(target);
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[0].isRecast)
                {
                    eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                    return;
                }
            }
            else
            {
                foreach (var target in eb.e_pr.partyList)
                {
                    JobBase jb = target.GetComponent<JobBase>();
                    if (jb.battelStatus != BattelStatus.DEAD)
                    {
                        targetCount.Add(target);
                    }
                }
                if (targetCount.Count > 0)
                {
                    int ran = Random.Range(0, targetCount.Count);
                    if (!eb.skillList[0].isRecast)
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                            return;
                        }
                    }
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
/// A_A2M1 Singleton
/// </summary>
public class A_A2M1 : E_Controller
{
    #region Properties
    private EnemyBase atk_eb;
    #endregion
    // インスタンス
    private static A_A2M1 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static A_A2M1 Instance
    {
        get
        {
            if (instance == null)
                instance = new A_A2M1();
            return instance;
        }
    }
    override public void Enter(EnemyBase eb = null)
    {
        foreach (var enemy in eb.e_pr.enemyList)
        {
            EnemyBase enemy_eb = enemy.GetComponent<EnemyBase>();
            if (enemy_eb._type == JobType.Attacker)
                atk_eb = enemy_eb;
        }
    }
    override public void Excute(EnemyBase eb = null)
    {
        if (eb.CanTakeAction())
        {
            if (atk_eb.e_target != null)
            {
                JobBase jb = atk_eb.e_target.GetComponent<JobBase>();
                if (atk_eb.e_target.layer == LayerMask.NameToLayer("Player") &&
                        jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(atk_eb.e_target, eb.skillList[0]));
                        return;
                    }
                }
            }
            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if (jb.battelStatus != BattelStatus.DEAD)
                {
                    targetCount.Add(target);
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[0].isRecast)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                        return;
                    }
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
/// A_Normal Singleton
/// </summary>
public class A_Normal : E_Controller
{
    // インスタンス
    private static A_Normal instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static A_Normal Instance
    {
        get
        {
            if (instance == null)
                instance = new A_Normal();
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
			foreach (var target in eb.e_pr.partyList)
			{
				JobBase jb = target.GetComponent<JobBase>();
				if((jb._hp/jb._maxHP * 100 < 50 && jb._type == JobType.Leader) &&
                    jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(target, eb.skillList[0]));
                        return;
                    }
				}
			}

            EnemyBase leader_eb = eb.e_pr.enemyList[0].GetComponent<EnemyBase>();
            if(leader_eb.e_target != null)
            {
                JobBase jb = leader_eb.e_target.GetComponent<JobBase>();
                if(leader_eb.e_target.layer == LayerMask.NameToLayer("Player") &&
                    jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(leader_eb.e_target, eb.skillList[0]));
                        return;
                    }
                }
            }

            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if ((jb._type == JobType.Attacker || jb._type == JobType.Magician) && jb.battelStatus != BattelStatus.DEAD)
                {
                        targetCount.Add(target);
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[0].isRecast)
                {
                    eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                    return;
                }
            }
            else
            {
                foreach (var target in eb.e_pr.partyList)
                {
                    JobBase jb = target.GetComponent<JobBase>();
                    if (jb.battelStatus != BattelStatus.DEAD)
                    {
                        targetCount.Add(target);
                    }
                }
                if (targetCount.Count > 0)
                {
                    int ran = Random.Range(0, targetCount.Count);
                    if (!eb.skillList[0].isRecast)
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.StartCoroutine(eb.SkillUse(targetCount[ran], eb.skillList[0]));
                            return;
                        }
                    }
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
        Debug.Log("T_Normal_Exit");
    }
}
