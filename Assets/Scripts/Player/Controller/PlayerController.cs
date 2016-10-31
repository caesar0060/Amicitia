using UnityEngine;
using System.Collections;

/// <summary>
/// WalkMode Singleton
/// </summary>
public class WalkMode : Controller {
	// 移動モードのインスタンス
	private static WalkMode instance;
	/// <summary>
	/// 移動モードのインスタンスを取得
	/// </summary>
	/// <value>移動のインスタンス</value>
	public static WalkMode Instance{
		get {
			if(instance == null)
				instance = new WalkMode();
			return instance;
		}
	}
	override public void Enter(JobBase jb)
	{
		Debug.Log("WEnter");
	}
	override public void Excute(JobBase jb)
	{
		Debug.Log("WExcute");
		jb.Skill1 ();
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("WExit");
	}
}
/// <summary>
/// BattelMode Singleton
/// </summary>
public class BattelMode : Controller {
	// バトルモードのインスタンス
	private static BattelMode instance;
	/// <summary>
	/// バトルモードのインスタンスを取得
	/// </summary>
	/// <value>バトルモードのインスタンス</value>
	public static BattelMode Instance{
		get {
			if(instance == null)
				instance = new BattelMode();
			return instance;
		}
	}
	override public void Enter(JobBase jb)
	{
		Debug.Log("Enter");
	}
	override public void Excute(JobBase jb)
	{
		Debug.Log("Excute");
		jb.Attack ();
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("Exit");
	}
}

