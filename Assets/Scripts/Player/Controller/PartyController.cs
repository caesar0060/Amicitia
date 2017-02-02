using UnityEngine;
using System.Collections;

/// <summary>
/// WorldMode Singleton
/// </summary>
public class ReadyMode : Controller {
	// バトルモードのインスタンス
	private static ReadyMode instance;
	/// <summary>
	/// WorldModeのインスタンスを取得
	/// </summary>
	/// <value>WorldModeのインスタンス</value>
	public static ReadyMode Instance{
		get {
			if(instance == null)
				instance = new ReadyMode();
			return instance;
		}
	}
	override public void Enter(JobBase jb)
	{
		Debug.Log("Enter");
	}
	override public void Excute(JobBase jb)
	{
		if (!jb.CanTakeAction())
			jb.ChangeMode (ConditionMode.Instance);
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
		if (!jb.CanTakeAction())
			jb.ChangeMode (ConditionMode.Instance);
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("Exit");
	}
}
/// <summary>
/// ConditionMode Singleton
/// </summary>
public class ConditionMode : Controller {
	// バトルモードのインスタンス
	private static ConditionMode instance;
	/// <summary>
	/// WorldModeのインスタンスを取得
	/// </summary>
	/// <value>WorldModeのインスタンス</value>
	public static ConditionMode Instance{
		get {
			if(instance == null)
				instance = new ConditionMode();
			return instance;
		}
	}
	override public void Enter(JobBase jb)
	{
		Debug.Log("Enter");
	}
	override public void Excute(JobBase jb)
	{
		if (jb.CanTakeAction())
			jb.ChangeMode (ReadyMode.Instance);
	}
	override public void Exit(JobBase jb)
	{
		Debug.Log("Exit");
	}
}
/// <summary>
/// SkillMode Singleton
/// </summary>
public class t_SkillMode : Controller
{
    // バトルモードのインスタンス
    private static t_SkillMode instance;
    /// <summary>
    /// WorldModeのインスタンスを取得
    /// </summary>
    /// <value>WorldModeのインスタンス</value>
    public static t_SkillMode Instance
    {
        get
        {
            if (instance == null)
                instance = new t_SkillMode();
            return instance;
        }
    }
    override public void Enter(JobBase jb)
    {
        Debug.Log("Enter");
    }
    override public void Excute(JobBase jb)
    {
        if (!jb.CanTakeAction())
            jb.ChangeMode(ConditionMode.Instance);
    }
    override public void Exit(JobBase jb)
    {
        TutorialRoot.Instance.counter++;
    }
}
