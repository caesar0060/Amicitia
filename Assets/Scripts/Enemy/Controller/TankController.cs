using UnityEngine;
using System.Collections;

/// <summary>
/// Defender1 Singleton
/// </summary>
public class Defender1 : e_Controller {
	// Tank Normal Modeのインスタンス
	private static Defender1 instance;
	/// <summary>
	/// Tank Normal Modeのインスタンスを取得
	/// </summary>
	/// <value>Tank Normal Modeのインスタンス</value>
	public static Defender1 Instance{
		get {
			if(instance == null)
				instance = new Defender1();
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
/// Defender2 Singleton
/// </summary>
public class Defender2 : e_Controller {
	// Tank Normal Modeのインスタンス
	private static Defender2 instance;
	/// <summary>
	/// Tank Normal Modeのインスタンスを取得
	/// </summary>
	/// <value>Tank Normal Modeのインスタンス</value>
	public static Defender2 Instance{
		get {
			if(instance == null)
				instance = new Defender1();
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
/// Defender3 Singleton
/// </summary>
public class Defender3 : e_Controller {
	// Tank Normal Modeのインスタンス
	private static Defender3 instance;
	/// <summary>
	/// Tank Normal Modeのインスタンスを取得
	/// </summary>
	/// <value>Tank Normal Modeのインスタンス</value>
	public static Defender3 Instance{
		get {
			if(instance == null)
				instance = new Defender1();
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
