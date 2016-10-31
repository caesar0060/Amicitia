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
	override public void Enter()
	{
		Debug.Log("WEnter");
	}
	override public void Excute()
	{
		Debug.Log("WExcute");
	}
	override public void Exit()
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
	override public void Enter()
	{
		Debug.Log("Enter");
	}
	override public void Excute()
	{
		Debug.Log("Excute");
	}
	override public void Exit()
	{
		Debug.Log("Exit");
	}
}

