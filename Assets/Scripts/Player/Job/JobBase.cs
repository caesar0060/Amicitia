using UnityEngine;
using System.Collections;

public class JobBase : MonoBehaviour {
	#region Properties
	// HP
	public int p_hp;
	// MP
	public int p_mp;
	// 攻撃力
	public int p_attack;
	//防御力
	public int p_defence;
	//Status
	public enum STATUS
	{
		NONE =-1,
		NORMAL,
		DEAD,
	}
	private STATUS _status;
	// 戦闘用ステータス
	public STATUS BattelStatus{
		get{ return _status;}
		private set{ _status = value; }
	}
	//Playerの移動速度
	private float moveSpeed;
	/// <summary>
	/// Playerの移動速度を取得、セットする
	/// </summary>
	/// <value>Playerの移動速度</value>
	public float MoveSpeed {
		get{ return moveSpeed; }
		private set{ moveSpeed = value; }
	}
	public PlayerOrAI playerControl;
	#endregion

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}
	#region Function
	/// <summary>
	/// 攻撃
	/// </summary>
	void Attack(){
	}
	/// <summary>
	/// 防御
	/// </summary>
	void Defense(){
	}
	/// <summary>
	/// Jump
	/// </summary>
	void Jump(){
	}
	#endregion
}
