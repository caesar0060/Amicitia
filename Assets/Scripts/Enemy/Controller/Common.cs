using UnityEngine;
using System.Collections;

/// <summary>
/// E_SkillMode Singleton
/// </summary>
public class E_SkillMode : E_Controller
{
	// インスタンス
	private static E_SkillMode instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
	public static E_SkillMode Instance
	{
		get
		{
			if (instance == null)
				instance = new E_SkillMode();
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
/// E_StatusMode Singleton
/// </summary>
public class E_StatusMode : E_Controller
{
	// インスタンス
	private static E_StatusMode instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
	public static E_StatusMode Instance
	{
		get
		{
			if (instance == null)
				instance = new E_StatusMode();
			return instance;
		}
	}
	override public void Enter(EnemyBase eb = null)
	{
		
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
/// E_FieldMode Singleton
/// </summary>
public class E_FieldMode : E_Controller
{
	// インスタンス
	private static E_FieldMode instance;
	/// <summary>
	/// インスタンスを取得
	/// </summary>
	/// <value>インスタンス</value>
	public static E_FieldMode Instance
	{
		get
		{
			if (instance == null)
				instance = new E_FieldMode();
			return instance;
		}
	}
	override public void Enter(EnemyBase eb = null)
	{
		eb.BattelStartRecast();
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
