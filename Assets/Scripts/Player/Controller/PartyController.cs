using UnityEngine;
using System.Collections;

/// <summary>
/// WorldMode Singleton
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

/// <summary>
/// SkillMode Singleton
/// </summary>
public class SkillMode : Controller {
	// バトルモードのインスタンス
	private static SkillMode instance;
	/// <summary>
	/// WorldModeのインスタンスを取得
	/// </summary>
	/// <value>WorldModeのインスタンス</value>
	public static SkillMode Instance{
		get {
			if(instance == null)
				instance = new SkillMode();
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

/// <summary>
/// RecastMode Singleton
/// </summary>
public class RecastMode : Controller {
	// バトルモードのインスタンス
	private static RecastMode instance;
	/// <summary>
	/// WorldModeのインスタンスを取得
	/// </summary>
	/// <value>WorldModeのインスタンス</value>
	public static RecastMode Instance{
		get {
			if(instance == null)
				instance = new RecastMode();
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
