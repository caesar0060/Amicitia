using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

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
	//使うスキルを登録する
	[HideInInspector] public Skill skillUsing;
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
			result += (int)enemy.GetComponent<EnemyBase>()._type;
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
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Enemy_Skill/" + fileName);
		//json fileを読み込む
		string JsonString = File.ReadAllText (filePath);
		return JsonString;
	}
	/// <summary>
	/// スキルを使う.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="skill">使うスキル.</param>
	public void SkillUse(GameObject target, Skill skill){
		ChangeMode (E_SkillMode.Instance);
		e_target = target; skillUsing = skill;
		GameObject.FindGameObjectWithTag ("PartyRoot").GetComponent<PartyRoot> ().attackList.Add (this.gameObject);
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
		if (_hp <= 0) {
			Set_b_Status (BattelStatus.DEAD);
			this.GetComponent<CapsuleCollider> ().enabled = false;
			e_pr.enemyList.Remove (this.gameObject);
			//Destroy (this.gameObject);
		}
	}
	/// <summary>
	/// Rotates to target.
	/// </summary>
	/// <param name="target">Target.</param>
	public void RotateToTarget(GameObject target){
		Vector3 dir = target.transform.position - this.transform.position;
		this.transform.rotation = Quaternion.LookRotation (dir);
	}
	/// <summary>
	/// 最初の位置に戻る
	/// </summary>
	public void ReturnPos(){
		StartCoroutine(LerpMove(this.gameObject, this.transform.position, startPos, 2));
		this.transform.rotation = Quaternion.Euler (Vector3.zero);
		GameObject.FindGameObjectWithTag ("PartyRoot").GetComponent<PartyRoot> ().ReadyNextAttack();
		ChangeMode (preivousController);
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
	public IEnumerator LerpMove(GameObject obj, Vector3 startPos, Vector3 endPos, float speed = 1, GameObject target =null, Skill skill = null, string a_name = "", float a_time = 1){
		float timer = 0;
		obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", true);
		while (true) {
			try{
				if (target != null) {
					Vector3 dir = target.transform.position - this.transform.position;
					RotateToTarget(target);
					endPos = target.transform.position - Vector3.Normalize(dir) * this.GetComponent<CapsuleCollider>().radius;
				}
				float distance = Vector3.Distance (startPos, endPos);
				float time = distance / (ENEMY_MOVE_SPEED * speed);
				timer += Time.deltaTime;
				float moveRate = timer / time;
				if (moveRate  >= 1) {
					moveRate = 1;
					obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
					obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", false);
					if (target != null && skill!=null) {
						obj.GetComponentInChildren<Animator> ().SetTrigger (a_name);
						StartCoroutine( Damage (target, skill, a_time));
					}
					yield break;
				}
				obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
			}
			catch(MissingReferenceException){
				ReturnPos ();
				yield break;
			}
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
			try{
				timer += Time.deltaTime;
				float counter = timer / time;
				if (counter >= 1) {
					float s_power = 1;	//精霊の力
					if (CheckFlag (ConditionStatus.POWER_UP))
						s_power = 1.5f;
					JobBase jb = target.GetComponent<JobBase> ();
					int damage = Math.Max ((int)((_attack + skill.s_power) * s_power) - jb._defence, 0);
                    jb.Set_HP(damage);
					StartCoroutine (SkillRecast (skill, skill.s_recast));
					yield break;
				}
			}
			catch(MissingReferenceException){
				ReturnPos ();
				yield break;
			}
			yield return new WaitForEndOfFrame ();
		}
	}
	/// <summary>
	/// Loading this instance.
	/// </summary>
	public IEnumerator Loading(float time){
		while (true) {
			controller.Excute (this);
			yield return new WaitForSeconds (time);
		}
	}
	#endregion
}
