using UnityEngine;
using System.Collections;

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
        Debug.Log("PExcute");
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
		Debug.Log("PExcute");
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
		Debug.Log("PExcute");
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
		Debug.Log("PExcute");
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
        Debug.Log("PExcute");
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
		if (eb.CanTakeAction())
        {
                            
        }
        else
            Debug.Log("TODO: 状態異常のモードに移動");
    }
    override public void Exit(EnemyBase eb = null)
    {
        Debug.Log("T_Normal_Exit");
    }
}
