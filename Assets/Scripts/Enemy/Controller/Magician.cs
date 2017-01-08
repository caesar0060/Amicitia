using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// M_M3 Singleton
/// </summary>
public class M_M3 : E_Controller
{
    // インスタンス
	private static M_M3 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
	public static M_M3 Instance
    {
        get
        {
            if (instance == null)
				instance = new M_M3();
            return instance;
        }
    }
    override public void Enter(EnemyBase eb = null)
    {
        Debug.Log("PEnter");
    }
    override public void Excute(EnemyBase eb = null)
    {
        if (eb.CanTakeAction()) {
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
                if (!eb.skillList[1].isRecast)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.SkillUse(targetCount[ran], eb.skillList[0]);
                        return;
                    }
                }
                else
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.SkillUse(targetCount[ran], eb.skillList[0]);
                        return;
                    }
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
                    if (!eb.skillList[1].isRecast)
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(targetCount[ran], eb.skillList[0]);
                            return;
                        }
                    }
                    else
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(targetCount[ran], eb.skillList[0]);
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
/// M_M2D1 Singleton
/// </summary>
public class M_M2D1 : E_Controller
{
	// インスタンス
	private static M_M2D1 instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
	public static M_M2D1 Instance
    {
		get {
			if(instance == null)
				instance = new M_M2D1();
			return instance;
		}
	}
	override public void Enter(EnemyBase eb = null)
	{
		Debug.Log("PEnter");
	}
	override public void Excute(EnemyBase eb = null)
	{
        if (eb.CanTakeAction ()) {
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
                JobBase jb = targetCount[ran].GetComponent<JobBase>();
                if (!jb.CheckFlag(ConditionStatus.SLOW))
                {
                    if (!eb.skillList[2].isRecast)
                    {
                        eb.SkillUse(targetCount[ran], eb.skillList[2]);
                        return;
                    }
                }
                else
                {
                    if (!eb.skillList[1].isRecast)
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(targetCount[ran], eb.skillList[0]);
                            return;
                        }
                    }
                    else
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(targetCount[ran], eb.skillList[0]);
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
/// M_D2M1 Singleton
/// </summary>
public class M_D2M1 : E_Controller
{
	// インスタンス
	private static M_D2M1 instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
	public static M_D2M1 Instance
    {
		get {
			if(instance == null)
				instance = new M_D2M1();
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
            }
            else
            {
            }
        }
	}
	override public void Exit(EnemyBase eb = null)
	{
		Debug.Log("PExit");
	}
}

/// <summary>
/// M_M2A1 Singleton
/// </summary>
public class M_M2A1 : E_Controller
{
    // インスタンス
	private static M_M2A1 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
	public static M_M2A1 Instance
    {
		get {
			if(instance == null)
				instance = new M_M2A1();
			return instance;
		}
	}
	override public void Enter(EnemyBase eb = null)
	{
		Debug.Log("PEnter");
	}
	override public void Excute(EnemyBase eb = null)
	{
        if (eb.CanTakeAction ()) {
            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if ((jb._type == JobType.Attacker || jb._type == JobType.Magician) && jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!jb.CheckFlag(ConditionStatus.SLOW))
                    {
                        targetCount.Add(target);
                    }
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                JobBase jb = targetCount[ran].GetComponent<JobBase>();
                if (!jb.CheckFlag(ConditionStatus.SLOW))
                {
                    if (!eb.skillList[2].isRecast)
                    {
                        eb.SkillUse(targetCount[ran], eb.skillList[2]);
                        return;
                    }
                }
                else
                {
                    if (!eb.skillList[1].isRecast)
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(targetCount[ran], eb.skillList[0]);
                            return;
                        }
                    }
                    else
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(targetCount[ran], eb.skillList[0]);
                            return;
                        }
                    }
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
                    if (!eb.skillList[1].isRecast)
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(targetCount[ran], eb.skillList[0]);
                            return;
                        }
                    }
                    else
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(targetCount[ran], eb.skillList[0]);
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
/// M_A2M1 Singleton
/// </summary>
public class M_A2M1 : E_Controller
{
    // インスタンス
	private static M_A2M1 instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
	public static M_A2M1 Instance
    {
        get
        {
            if (instance == null)
				instance = new M_A2M1();
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
                if (jb._hp / jb._maxHP * 100 < 30 && jb._hp / jb._maxHP * 100 > 0)
                {
                    if (!eb.skillList[1].isRecast)
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(target, eb.skillList[0]);
                            return;
                        }
                    }
                    else
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(target, eb.skillList[0]);
                            return;
                        }
                    }
                }
            }
            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if ((jb._type == JobType.Attacker || jb._type == JobType.Magician) && jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!jb.CheckFlag(ConditionStatus.SLOW))
                    {
                        targetCount.Add(target);
                    }
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[2].isRecast)
                {
                    eb.SkillUse(targetCount[ran], eb.skillList[2]);
                    return;
                }
            }
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
                if (!eb.skillList[1].isRecast)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.SkillUse(targetCount[ran], eb.skillList[0]);
                        return;
                    }
                }
                else
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.SkillUse(targetCount[ran], eb.skillList[0]);
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
/// M_Normal Singleton
/// </summary>
public class M_Normal : E_Controller
{
    // インスタンス
	private static M_Normal instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
	public static M_Normal Instance
    {
        get
        {
            if (instance == null)
				instance = new M_Normal();
            return instance;
        }
    }
    override public void Enter(EnemyBase eb = null)
    {
        Debug.Log("T_Normal_Enter");
    }
    override public void Excute(EnemyBase eb = null)
    {
		if (eb.CanTakeAction ()) {
			foreach (var target in eb.e_pr.partyList) {
				JobBase jb = target.GetComponent<JobBase> ();
                if (jb._hp / jb._maxHP * 100 < 30 && jb._hp / jb._maxHP * 100 > 0)
                {
					if (!eb.skillList [1].isRecast) {
						if (!eb.skillList [0].isRecast) {
							eb.SkillUse (target, eb.skillList [0]);
							return;
						}
					}
                    else
                    {
                        if (!eb.skillList[0].isRecast)
                        {
                            eb.SkillUse(target, eb.skillList[0]);
                            return;
                        }
                    }
				}
			}
            List<GameObject> targetCount = new List<GameObject>();
            foreach (var target in eb.e_pr.partyList)
            {
                JobBase jb = target.GetComponent<JobBase>();
                if ((jb._type == JobType.Attacker || jb._type == JobType.Magician) && jb.battelStatus != BattelStatus.DEAD)
                {
                    if (!jb.CheckFlag(ConditionStatus.SLOW))
                    {
                        targetCount.Add(target);
                    }
                }
            }
            if (targetCount.Count > 0)
            {
                int ran = Random.Range(0, targetCount.Count);
                if (!eb.skillList[2].isRecast)
                {
                    eb.SkillUse(targetCount[ran], eb.skillList[2]);
                    return;
                }
            }
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
                if (!eb.skillList[1].isRecast)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.SkillUse(targetCount[ran], eb.skillList[0]);
                        return;
                    }
                }
                else
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.SkillUse(targetCount[ran], eb.skillList[0]);
                        return;
                    }
                }
            }
		} else {
			Debug.Log ("TODO: 状態異常のモードに移動");
			return;
		}
    }
    override public void Exit(EnemyBase eb = null)
    {
        Debug.Log("T_Normal_Exit");
    }
}
