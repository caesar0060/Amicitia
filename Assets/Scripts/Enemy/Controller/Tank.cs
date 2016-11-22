using UnityEngine;
using System.Collections;

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
        Debug.Log("PExcute");
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
					eb.skillList [1].skillMethod ();
					eb.SkillRecast (eb.skillList [1], eb.skillList [1].s_recast);
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