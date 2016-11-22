using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType{
	NONE = 0,
	Attacker = 1,
	Defender = 2,
	Magician = 5,
}

public class Skill{
	public string s_name;		//名前
	public float s_effectTime;	//効果時間
	public bool s_isRune;		//ルーンかどうか
	public float s_recast;		//リーキャストタイム
	public bool isRecast;		//リーキャスト中
	public float s_range;		//範囲
	public Delegate skillMethod;//スキルを保管する
}

public class EnemyBase : StatusControl {
	#region Properties
	// HP
	public int e_hp;
	// MP
	public int e_mp;
	// 攻撃力
	public int e_attack;
	//防御力
	public int e_defence;
    //エネミーのタイプ
    public EnemyType e_type;
	//インスタンスを保存するコントローラ
	public E_Controller controller = null;
	//ターゲット
    [HideInInspector] public GameObject e_target;
    //PlayerRootを保存する
    public PlayerRoot e_pr;
    // プレイヤーのリーダーを保管する
    [HideInInspector] public GameObject p_leader;
    // スキルを保管する
	[HideInInspector] public Delegate[] skillArray;
	[HideInInspector] public List<Skill> skillList;
	#endregion

	#region Function
	/// <summary>
	/// モードを変更する
	/// </summary>
	/// <param name="newMode">New mode.</param>
	public void ChangeMode(E_Controller newMode){
		if (controller != newMode) {
			controller.Exit (this);
			controller = newMode;
			controller.Enter (this);
		} else
			Debug.LogError ("same");
	}
    /// <summary>
    /// チームの構成によるモードを決める
    /// </summary>
    /// <returns>result</returns>
    public int GetModeNumber()
    {
        int result = 0;
        foreach (var enemy in e_pr.enemyList)
        {
			result += (int)enemy.GetComponent<EnemyBase>().e_type;
        }
        return result;
    }
	/// <summary>
	/// Skill listを作る.
	/// </summary>
	/// <param name="sa">Skill Array.</param>
	/// <param name="sd">Skill Date.</param>
	public void CreateSkillList(Delegate[] sa,string sd){
		for (int i = 0; i < sa.Length; i++) {
			Skill skill = JsonUtility.FromJson<Skill> (sd);
			skill.skillMethod = sa [i];
			skillList.Add (skill);
		}
	}
	/// <summary>
	/// スキルのデータを読み込む
	/// </summary>
	/// <returns>Json date.</returns>
	/// <param name="fileName">File name.</param>
	public string GetSKillDate(string fileName){
		//json fileを読み込む
		string JsonString = File.ReadAllText (Application.dataPath + "/Resources/Enemy_Skill/" + fileName);
		return JsonString;
	}
	#endregion
    #region Skill
    virtual public void Skill1(GameObject target = null, float time = 0)
    {

    }
    virtual public void Skill2(GameObject target = null, float time = 0)
    {

    }
    virtual public void Skill3(GameObject target = null, float time = 0)
    {

    }
    virtual public void Skill4(GameObject target = null, float time = 0)
    {

    }
    #endregion
    #region Co-routine
    /// <summary>
	/// 状態の時間をカウント
	/// </summary>
	/// <param name="c_status">状態</param>
	/// <param name="time">効果時間</param>
	public IEnumerator StatusCounter(ConditionStatus c_status, float time){
		float startTime = Time.time;
		while (true) {
			if (CheckFlag (c_status)) {
				if ((Time.time - startTime) >= time) {
					Remove_c_status (c_status);
					yield break;
				}
				yield return new WaitForSeconds (COROUTINE_WAIT_TIME);
			} else
				yield break;
		}
	}
	/// <summary>
	/// どんな状態が立っているを検査する
	/// </summary>
	public IEnumerator CheckStatus(){
		while (true) {
			foreach (ConditionStatus status in Enum.GetValues(typeof(ConditionStatus))) {
				if (CheckFlag (status)) {
					Debug.Log (Enum.GetName (typeof(ConditionStatus), status));
				}
			}
			yield return new WaitForSeconds (COROUTINE_WAIT_TIME);
		}
	}
	/// <summary>
	/// スキルのリーキャストタイム
	/// </summary>
	/// <param name="skill">Skill.</param>
	/// <param name="time">リーキャストタイム.</param>
	public IEnumerator SkillRecast(Skill skill, float time){
		skill.isRecast = true;
		float startTime = Time.time;
		while(true){
			if(Time.time - startTime >= time ){
				skill.isRecast = false;
				yield break;
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
	#endregion
}
