using UnityEngine;
using System.Collections;

/// <summary>
/// 巡回モード Singleton
/// </summary>
public class PatrolMode : Controller {
	// 巡回モードのインスタンス
	private static PatrolMode instance;
	/// <summary>
	/// 巡回モードのインスタンスを取得
	/// </summary>
	/// <value>巡回モードのインスタンス</value>
	public static PatrolMode Instance{
		get {
			if(instance == null)
				instance = new PatrolMode();
			return instance;
		}
	}
	override public void Enter(JobBase jb)
	{
		Debug.Log("PEnter");
	}
	override public void Excute(JobBase jb)
	{
		Debug.Log("PExcute");
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("PExit");
	}
}

/// <summary>
/// 追跡モード Singleton
/// </summary>
public class ChaseMode : Controller {
	// 追跡モードのインスタンス
	private static ChaseMode instance;
	/// <summary>
	/// 追跡モードのインスタンスを取得
	/// </summary>
	/// <value>追跡モードのインスタンス</value>
	public static ChaseMode Instance{
		get {
			if(instance == null)
				instance = new ChaseMode();
			return instance;
		}
	}
	override public void Enter(JobBase jb)
	{
		Debug.Log("PEnter");
	}
	override public void Excute(JobBase jb)
	{
		Debug.Log("PExcute");
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("PExit");
	}
}
