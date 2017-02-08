using UnityEngine;
using System.Collections;


/// <summary>
/// L_Normal Singleton
/// </summary>
public class L_Normal : E_Controller
{
    // インスタンス
    private static L_Normal instance;
    /// <summary>
    /// インスタンスを取得
    /// </summary>
    /// <value>インスタンス</value>
    public static L_Normal Instance
    {
        get
        {
            if (instance == null)
                instance = new L_Normal();
            return instance;
        }
    }
    override public void Enter(EnemyBase eb = null)
    {
        Debug.Log("L_Normal");
    }
    override public void Excute(EnemyBase eb = null)
    {
		if (eb.CanTakeAction())
        {
            if (!eb.skillList[3].isRecast)
            {
                foreach (var target in eb.e_pr.enemyList)
                {
                    EnemyBase t_eb = target.GetComponent<EnemyBase>();
                    if (t_eb._hp / t_eb._maxHP * 100 < 50 && t_eb.battelStatus != BattelStatus.DEAD)
                    {
                        eb.StartCoroutine(eb.SkillUse(target, eb.skillList[3]));
                        return;
                    }
                    if (t_eb._type == JobType.Leader && t_eb._hp / t_eb._maxHP * 100 < 50 && t_eb.battelStatus != BattelStatus.DEAD)
                    {
                        eb.StartCoroutine(eb.SkillUse(target, eb.skillList[3]));
                        return;
                    }
                }
            }
            foreach (var target in eb.e_pr.enemyList)
            {
                EnemyBase t_eb = target.GetComponent<EnemyBase>();
                if ( t_eb._type == JobType.Magician && t_eb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[2].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(target, eb.skillList[2]));
                        return;
                    }
                }
                if (t_eb._type == JobType.Attacker && t_eb.battelStatus != BattelStatus.DEAD)
                {
                    if (!eb.skillList[0].isRecast)
                    {
                        eb.StartCoroutine(eb.SkillUse(target, eb.skillList[0]));
                        return;
                    }
                }
            }
            if (!eb.skillList[3].isRecast)
            {
                GameObject obj = null;
                foreach (var target in eb.e_pr.partyList)
                {
                    JobBase jb = target.GetComponent<JobBase>();
                    if (jb._type == JobType.Leader && jb.battelStatus != BattelStatus.DEAD)
                    {
                        if(obj == null)
                            obj = target; 
                    }
                    if (jb._hp / jb._maxHP * 100 < 30 && jb.battelStatus != BattelStatus.DEAD)
                    {
                        obj = target;
                        break;
                    }
                }
                eb.StartCoroutine(eb.SkillUse(obj, eb.skillList[3]));
                return;
            }
        }
        else
            Debug.Log("TODO: 状態異常のモードに移動");
    }
    override public void Exit(EnemyBase eb = null)
    {
        Debug.Log("L_Normal");
    }
}