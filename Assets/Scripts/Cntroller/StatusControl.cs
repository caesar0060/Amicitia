using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public enum JobType
{
    NONE = 0,
    Attacker = 1,
    Defender = 2,
    Magician = 5,
    Leader,
}
// 戦闘用ステータス
public enum BattelStatus
{
	NOT_IN_BATTEL = 0,
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
	SLEEP = 0x002,		//眠り
	SLOW = 0x004,		//スロウ
	PULL = 0x008,		//敵を引き寄せる
	POWER_UP = 0x010,	//力アップ
	MAGIC_UP = 0x020,	//魔力アップ
	P_DEF_UP = 0x040,	//物理防御アップ
	M_DEF_UP = 0x080,	//魔法防御アップ
	NO_DAMAGE = 0x100,	//無敵
	ALL_DAMAGE_DOWN = 0x200,//全てダメージカット
    TAKE_OVER = 0x400,	//肩代わり
    POSION = 0x800,	    //毒
}

public class StatusControl : MonoBehaviour{
	#region Static_Properties
	public static float COROUTINE_WAIT_TIME = 1.0f;
    // HP
    public int _hp;
    // HP
    public int _maxHP;
    // 攻撃力
    public int _attack;
    //防御力
    public int _defence;
    //エネミーのタイプ
    public JobType _type;
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
    /// HPを変更する
    /// </summary>
    /// <param name="point">変化値</param>
    public void Set_HP(int point)
    {
        _hp -= point;
		
        // slider
		//this.GetComponentInChildren<Slider>().value = -_hp;
    }
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
		conditionStatus |= newStatus;
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
	/// <summary>
	/// 行動できるかどうか
	/// </summary>
	/// <returns><c>true</c>行動できる<c>false</c>行動できない</returns>
	public bool CanTakeAction(){
		return !(CheckFlag (ConditionStatus.PALSY) || CheckFlag (ConditionStatus.SLEEP)|| battelStatus == BattelStatus.DEAD);
	}
    /// <summary>
    /// スキルによるの防御を計算する
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
	public float GetDamageRate(GameObject target){
		switch (target.GetComponent<StatusControl> ().conditionStatus) {
		case ConditionStatus.NO_DAMAGE:
			return 0;
		case ConditionStatus.ALL_DAMAGE_DOWN:
			return 0.8f;
		}
		return 1;
	}
	#endregion
}
