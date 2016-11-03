using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// 戦闘用ステータス
public enum BattelStatus
{
	NONE =-1,
	NOT_IN_BATTEL,
	TARGET_CHOOSE,
	NORMAL,			//通常
	DEAD,			//死亡
	APRAXIA,		//行動不能
	PATROL,			//巡回
	CHASE,			//追跡
	PROTECT,		//保護
	SUPPORT,		//支援
	NUM_OF_TYPE,
}
// condition
public enum StatusCondition
{
	PALSY,			//麻痺
	SLEEP,			//睡眠
	SLOW,			//遅鈍
	GRAVITATE,		//引き寄せられる
	POWER_UP,		//力アップ
	MAGIC_UP,		//魔力アップ
	P_DEF_UP,		//物理防御アップ
	M_DEF_UP,		//魔法防御アップ
	NO_DAMAGE,		//無敵
	ALL_DAMAGE_DOWN,//全てダメージダウン
	M_DAMAGE_DOWN,	//魔法ダメージダウン
	P_DAMAGE_DOWN,	//物理ダメージダウン
	NUM_OF_TYPE,
}

public class JobBase : MonoBehaviour {
	#region Properties
	// HP
	public int _hp;
	// MP
	public int _mp;
	// 攻撃力
	public int _attack;
	//防御力
	public int _defence;
	//ターゲット
	public GameObject _target;
	// 戦闘用ステータス
	private BattelStatus b_status;
	public BattelStatus battelStatus{
		get{ return b_status;}
		set{ b_status = value; }
	}
	// condition
	private StatusCondition c_status;
	public StatusCondition statusCondition{
		get{ return c_status;}
		set{ c_status = value; }
	}
	//Playerの移動速度
	public float moveSpeed;
	//インスタンスを保存するコントローラ
	public Controller controller;
	// Skillを保存用配列
	public delegate void funcDelegate();
	public static Dictionary<string, funcDelegate> skill_list = new Dictionary<string, funcDelegate> ();
	//
	public funcDelegate skillUse;
	#endregion

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}
	public void OnTriggerStay(Collider other){
		switch(battelStatus){
		case BattelStatus.NOT_IN_BATTEL:
			GameObject other_go = other.gameObject;
			if(other_go.layer == LayerMask.NameToLayer("NPC")){
				if (_target == null) {
					if (CheckIsFront (other_go))
						_target = other_go;
				} else if (_target == other_go) {
					if (!CheckIsFront (other_go))
						_target = null;
				}
			}
			break;
		}
	}

	public void OnTriggerExit(Collider other){
		switch(battelStatus){
		case BattelStatus.NOT_IN_BATTEL:
			if (_target == other.gameObject)
				_target = null;
			break;
		}
	}
	#region Function
	/// <summary>
	/// モードを変更する
	/// </summary>
	/// <param name="newMode">New mode.</param>
	public void ChangeMode(Controller newMode){
		if (controller != newMode) {
			controller.Exit (this);
			controller = newMode;
			controller.Enter (this);
		} else
			Debug.Log ("same");
	}
	/// <summary>
	/// Objectは前にいるかどうかをチェックする
	/// </summary>
	/// <returns><c>true</c>, 前にいるなら, <c>false</c> 違う場合.</returns>
	/// <param name="other">Object.</param>
	public bool CheckIsFront(GameObject other){
		bool isFront = false;
		// プレイヤーが現在向いている方向を保管
		Vector3 heading = this.transform.TransformDirection (Vector3.forward);
		// プレイヤーから見たObjectの方向を保管
		Vector3 to_other = other.transform.position - this.transform.position;
		heading.y = 0;
		to_other.y = 0;
		//正規化する
		heading.Normalize ();
		to_other.Normalize ();
		//内積を求める
		float dp = Vector3.Dot (heading, to_other);
		//内積が45度のコサイン値未満なら、falseを返す
		if (dp < Mathf.Cos (45)) {
			return isFront;
		}
		//内積が45度のコサイン以上なら、trueを返す
		isFront = true;	
		return isFront;
	}
	/// <summary>
	/// 攻撃
	/// </summary>
	virtual public void Attack(){
		Debug.Log ("Attack");
	}
	/// <summary>
	/// 防御
	/// </summary>
	virtual public void Defense(){
		Debug.Log ("Defense");
	}
	/// <summary>
	/// Jump
	/// </summary>
	virtual public void Jump(){
		Debug.Log ("Jump");
	}
	#endregion
}
