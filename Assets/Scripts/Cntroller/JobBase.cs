using UnityEngine;
using System.Collections;

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
	public enum BattelStatus
	{
		NONE =-1,
		NORMAL,		//通常
		DEAD,		//死亡
		APRAXIA,	//行動不能
		PATROL,		//巡回
		CHASE,		//追跡
		PROTECT,	//保護
		SUPPORT,	//支援
		NUM_OF_TYPE,
	}
	private BattelStatus b_status;
	public BattelStatus battelStatus{
		get{ return b_status;}
		set{ b_status = value; }
	}
	// condition
	public enum StatusCondition
	{
		POSION,		//中毒
		NUM_OF_TYPE,
	}
	private StatusCondition c_status;
	public StatusCondition statusCondition{
		get{ return c_status;}
		set{ c_status = value; }
	}
	//Playerの移動速度
	public float moveSpeed;
	//インスタンスを保存するコントローラ
	public Controller controller;
	#endregion

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}
	#region Function
	/// <summary>
	/// モードを変更する
	/// </summary>
	/// <param name="newMode">New mode.</param>
	virtual public void ChangeMode(Controller newMode){
		controller.Exit (this);
		controller = newMode;
		controller.Enter (this);
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
	virtual public void Skill1(){
		Debug.Log ("Skill1");
	}
	virtual public void Skill2(){
		Debug.Log ("Skill2");
	}
	virtual public void Skill3(){
		Debug.Log ("Skill3");
	}
	virtual public void Skill4(){
		Debug.Log ("Skill4");
	}
	#endregion
}
