using UnityEngine;
using System;
using System.Collections;

// 戦闘用ステータス
public enum BattelStatus
{
	NOT_IN_BATTEL = 0,
	TARGET_CHOOSE,
	NORMAL,			//通常
	DEAD,			//死亡
	PATROL,			//巡回
	CHASE,			//追跡
	PROTECT,		//保護
	SUPPORT,		//支援
	NUM_OF_TYPE,
}
// condition
public enum ConditionStatus
{
	NORMAL =0,			//通常
	PALSY = 0x001,		//麻痺
	SLEEP = 0x002,		//睡眠
	SLOW = 0x004,		//遅鈍
	GRAVITATE = 0x008,	//引き寄せられる
	POWER_UP = 0x010,	//力アップ
	MAGIC_UP = 0x020,	//魔力アップ
	P_DEF_UP = 0x040,	//物理防御アップ
	M_DEF_UP = 0x080,	//魔法防御アップ
	NO_DAMAGE = 0x100,	//無敵
	ALL_DAMAGE_DOWN = 0x200,//全てダメージダウン
	M_DAMAGE_DOWN = 0x400,	//魔法ダメージダウン
	P_DAMAGE_DOWN = 0x800,	//物理ダメージダウン
}

public class StatusControl : MonoBehaviour{
	#region Static_Properties
	public static float COROUTINE_WAIT_TIME = 1.0f;
	#endregion
	// 戦闘用ステータス
	private BattelStatus b_status;
	public BattelStatus battelStatus{
		get{ return b_status;}
		private set{ b_status = value; }
	}
	// condition
	private ConditionStatus c_status;
	public ConditionStatus conditionStatus{
		get{ return c_status;}
		private set{ c_status = value; }
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	#region fuction
	/// <summary>
	/// バトル状態を設定する
	/// </summary>
	/// <param name="newStatus">New status.</param>
	public void Set_b_Status(BattelStatus newStatus){
		if (battelStatus != newStatus)
			battelStatus = newStatus;
	}
	/// <summary>
	/// 異常状態を設定する
	/// </summary>
	/// <param name="newStatus">New status.</param>
	public void Set_c_Status(ConditionStatus newStatus){
		if (CheckFlag (newStatus))
			return;
		conditionStatus = conditionStatus | newStatus;
	}
	/// <summary>
	/// 異常状態を解除する
	/// </summary>
	/// <param name="status">Status.</param>
	public void Remove_c_status(ConditionStatus status){
		if (CheckFlag (status))
			conditionStatus &= ~status;
	}
	/// <summary>
	/// 状態を確認する
	/// </summary>
	/// <param name="newStatus">New status.</param>
	public bool CheckFlag(ConditionStatus newStatus){
		return ((conditionStatus & newStatus) == newStatus);
	}
	#endregion
}
