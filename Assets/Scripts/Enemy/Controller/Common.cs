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
        eb.isMove = false;
		eb.BattelStartRecast();
	}
	override public void Excute(EnemyBase eb = null)
	{
        if (!eb.isMove)
            move(eb);
	}
	override public void Exit(EnemyBase eb = null)
	{
		Debug.Log("PExit");
	}
    private void move(EnemyBase eb)
    {
        eb.isMove = true;
        float range = 2;
        float x = Random.Range(range * -1, range + 1);
        float z = Random.Range(range * -1, range + 1);
        Vector3 pos = new Vector3(x, 0, z);
        pos += eb.transform.parent.position;
        eb.StartCoroutine(eb.LerpMove(eb.gameObject, eb.transform.position, pos));
    }
}
