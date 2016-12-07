using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public enum JobType{
	NONE = 0,
	Attacker = 1,
	Defender = 2,
	Magician = 5,
	Leader,
}
[Serializable]
public class Skill{
	public string s_name;		//名前
    public float s_power;       //効果量
    public float s_pIncrease;   //強化後
	public float s_effectTime;	//効果時間
	public bool s_isRune;		//ルーンかどうか
	public float s_recast;		//リーキャストタイム
	public bool isRecast;		//リーキャスト中
	public float s_range;		//範囲
	public E_Delegate skillMethod;//スキルを保管する
}
[Serializable]
public class SkillCollect{
	public Skill[] skills;
}

public class EnemyBase : StatusControl {
	#region Properties
	// バトルが始めて最初にすべてのスキルをリーキャストする時間
	public static float BATTEL_START_RECAST_TIME = 5.0f;
	// プレイヤー攻撃のときの移動の必要な時間
	private static float ENEMY_MOVE_SPEED = 5.0f;
	// HP
	public int e_hp;
    // HP
    public int e_maxHP;
	// 攻撃力
	public int e_attack;
	//防御力
	public int e_defence;
    //エネミーのタイプ
    public JobType e_type;
	//インスタンスを保存するコントローラ
	public E_Controller controller = null;
	//前のインスタンスを保存するコントローラ
	public E_Controller preivousController = null;
	//ターゲット
    [HideInInspector] public GameObject e_target;
    //PlayerRootを保存する
	[HideInInspector] public PlayerRoot e_pr;
    // プレイヤーのリーダーを保管する
    [HideInInspector] public GameObject p_leader;
    // スキルを保管する
	[HideInInspector] public E_Delegate[] skillArray;
	[HideInInspector] public List<Skill> skillList;
	// 位置の最初値
	[HideInInspector] public Vector3 startPos;
	// Local位置の最初値
	[HideInInspector] public Vector3 startLocalPos;
	#endregion

	#region Function
	/// <summary>
	/// モードを変更する
	/// </summary>
	/// <param name="newMode">New mode.</param>
	public void ChangeMode(E_Controller newMode){
		if (controller != newMode) {
			controller.Exit (this);
			preivousController = controller;
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
	public void CreateSkillList(E_Delegate[] sa,string sd){
		SkillCollect sc = JsonUtility.FromJson<SkillCollect> (sd);
		for (int i = 0; i < sa.Length; i++) {
			sc.skills[i].skillMethod = sa [i];
			skillList.Add (sc.skills[i]);
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
	/// <summary>
	/// スキルを使う.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="skill">使うスキル.</param>
	public void SkillUse(GameObject target, Skill skill){
		ChangeMode (E_SkillMode.Instance);
		skill.skillMethod (target, skill.s_effectTime);
		skill.isRecast = true;
	}
	/// <summary>
	/// 全てのスキルのリーキャストをスタートする
	/// </summary>
	public void BattelStartRecast(){
		foreach (var skill in skillList) {
			StartCoroutine( SkillRecast (skill, BATTEL_START_RECAST_TIME));
		}
	}
	/// <summary>
	/// 死亡しているかどうかをチェック
	/// </summary>
	public void CheckDead(){
		if (e_hp <= 0) {
			Set_b_Status (BattelStatus.DEAD);
			this.GetComponent<CapsuleCollider> ().enabled = false;
			e_pr.enemyList.Remove (this.gameObject);
			Destroy (this.gameObject);
		}
	}
	/// <summary>
	/// 最初の位置に戻る
	/// </summary>
	public void ReturnPos(){
		//StartCoroutine(LerpMove(this.gameObject, this.transform.position, startPos, 2));
	}
	#endregion
    #region Skill
    virtual public void Skill1(GameObject target = null, float effectTime = 0)
    {

    }
    virtual public void Skill2(GameObject target = null, float effectTime = 0)
    {

    }
    virtual public void Skill3(GameObject target = null, float effectTime = 0)
    {

    }
    virtual public void Skill4(GameObject target = null, float effectTime = 0)
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
			Debug.Log (Time.time - startTime);
			if(Time.time - startTime >= time ){
				skill.isRecast = false;
				yield break;
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	/// <summary>
	/// 対象を等速で動かす,ターゲットがあれば攻撃する
	/// </summary>
	/// <returns>The move.</returns>
	/// <param name="obj">対象.</param>
	/// <param name="startPos">Start position.</param>
	/// <param name="endPos">End position.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="target">Target.</param>
	/// <param name="sc">Skill Script.</param>
	/// <param name="a_time">Animation time.</param>
	public IEnumerator LerpMove(GameObject obj, Vector3 startPos, Vector3 endPos, float speed =1, GameObject target =null, Skill skill = null, float a_time = 1){
		float timer = 0;
		obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", true);
		while (true) {
			if (target != null) {
				endPos = target.transform.position;
				endPos -= Vector3.Normalize (startLocalPos) * this.GetComponent<CapsuleCollider> ().radius;
			}
			float distance = Vector3.Distance (startPos, endPos);
			float time = distance / ENEMY_MOVE_SPEED;
			timer += Time.deltaTime * speed;
			float moveRate = timer / time;
			Vector3 dir = Vector3.RotateTowards (this.transform.forward,
				              target.transform.position - this.transform.position, 1, 0);
			this.transform.rotation = Quaternion.LookRotation (dir);
			if (moveRate  >= 1) {
				moveRate = 1;
				obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
				obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", false);
				if (target != null && skill!=null) {
					obj.GetComponentInChildren<Animator> ().SetTrigger ("isBom");
					StartCoroutine( Damage (target, skill, a_time));
				}
				yield break;
			}
			obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
			yield return new WaitForEndOfFrame ();
		}
	}
	/// <summary>
	/// Damage the specified target
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="skill">Skill.</param>
	/// <param name="time">攻撃のタイミング.</param>
	public IEnumerator Damage(GameObject target, Skill skill, float time){
		float timer = 0;
		while (true) {
			timer += Time.deltaTime;
			float counter = timer / time;
			if (counter >= 1) {
				float s_power = 1;	//精霊の力
				if (CheckFlag (ConditionStatus.POWER_UP))
					s_power = 1.5f;
				JobBase jb = target.GetComponent<JobBase> ();
				int damage = Math.Max ((int)((e_attack + skill.s_power) * s_power) - jb.p_defence, 0);
				jb.p_hp -= damage;
				Debug.Log (skill.s_recast);
				StartCoroutine (SkillRecast (skill, skill.s_recast));
				ChangeMode (preivousController);
				yield break;
			}
			yield return new WaitForEndOfFrame ();
		}
	}
	/// <summary>
	/// Loading this instance.
	/// </summary>
	public IEnumerator Loading(){
		while (true) {
			controller.Excute (this);
			yield return new WaitForSeconds (1.0f);
		}
	}
	#endregion
}
