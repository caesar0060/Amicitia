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
			
        }
        else
            Debug.Log("TODO: 状態異常のモードに移動");
    }
    override public void Exit(EnemyBase eb = null)
    {
        Debug.Log("L_Normal");
    }
}