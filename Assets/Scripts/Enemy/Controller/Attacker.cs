using UnityEngine;
using System.Collections;

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
        Debug.Log("PExcute");
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
		Debug.Log("PExcute");
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
		Debug.Log("PExcute");
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
                            
        }
        else
            Debug.Log("TODO: 状態異常のモードに移動");
    }
    override public void Exit(EnemyBase eb = null)
    {
        Debug.Log("T_Normal_Exit");
    }
}
