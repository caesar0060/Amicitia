using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//引数および返り値のないデリゲート
public delegate void Delegate (GameObject target = null, float effectTime = 0, float recastTime = 0);
//対象数
public enum TargetNum{
	SELF = 0,
	ONE,
	MUTIPLE,
}
//対象数
public enum TargetType{
	PLAYER = 0,
	ENEMY,
}

public class SkillScript : MonoBehaviour {
	#region Properties
	public string s_name;				//名前
	public TargetNum t_num;				//対象数
	public TargetNum s_targetNum{		
		get{ return t_num; }
		set{ t_num = value;}
	}
	public TargetType t_type;			//対象は敵かどうか
	public TargetType s_targetype{		//対象は敵かどうか
		get{ return t_type; }
		set{ t_type = value;}
	}
	public float s_effectTime;			//効果時間
	public string s_description;		//詳細
	public bool s_isRune = false;		//ルーンかどうか
	public float s_recast = 0;			//リーキャストタイム
	public bool isRecast = false;		//リーキャスト中
	public float s_range = 0;			//範囲
	public Delegate skillMethod;		//スキルを保管する

	#endregion
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
