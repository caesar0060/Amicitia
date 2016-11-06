using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//引数および返り値のないデリゲート
public delegate void Delegate ();
//対象数
public enum TargetNum{
	SELF =0,
	ONE,
	MUTIPLE,
}

public class SkillScript : MonoBehaviour {
	
	public string s_name;				//名前
	public bool isEnemyTarget = false;	//対象は敵かどうか
	private TargetNum t_num;			//対象数
	public TargetNum s_targetNum{		
		get{ return t_num; }
		set{ t_num = value;}
	}
	public float s_effectTime;			//効果時間
	public string s_description;		//詳細
	public bool s_isRune = false;		//ルーンかどうか
	public Delegate skillMethod;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
