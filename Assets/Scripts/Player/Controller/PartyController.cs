using UnityEngine;
using System.Collections;

/// <summary>
/// AI用のWorldMode Singleton
/// </summary>
public class WorldMode : Controller {
	// バトルモードのインスタンス
	private static WorldMode instance;
	/// <summary>
	/// WorldModeのインスタンスを取得
	/// </summary>
	/// <value>WorldModeのインスタンス</value>
	public static WorldMode Instance{
		get {
			if(instance == null)
				instance = new WorldMode();
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
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("Exit");
	}
}
