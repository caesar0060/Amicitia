using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class JobBase : StatusControl {
	#region Properties
	// プレイヤー攻撃のときの移動の必要な時間
	private static float PLAYER_MOVE_TIME = 1.0f;
	// ボタンの距離
	private static float BUTTON_DISTANCE = 0.5f;
	// HP
	public int p_hp;
    // MAX HP
    public int p_maxHP;
	// 攻撃力
	public int p_attack;
	// 防御力
	public int p_defence;
    // Job
    public JobType p_type;
	//ターゲット
	[HideInInspector] public GameObject _target;
	//インスタンスを保存するコントローラ
	public Controller controller;
	// Skillを保存用配列
	public GameObject[] p_skillList;
	public P_Delegate[] p_funcList;
	// 位置の最初値
	[HideInInspector] public Vector3 startPos;
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
			Debug.LogError ("same");
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
	/// 異常状態を設定し、カウントする
	/// </summary>
	/// <param name="c_status">C status.</param>
	/// <param name="time">Time.</param>
	public void SetCondition (ConditionStatus c_status, float time){
		Set_c_Status (c_status);
		StartCoroutine(StatusCounter(c_status ,time));
	} 
	/// <summary>
	/// ボタンを生成する
	/// </summary>
	public void skillBtnGenerate(){
		float a = 360 / p_skillList.Length;
		Vector3 pos;
		Transform parent = this.transform.GetChild (0);
		for (int i = 0; i < p_skillList.Length; i++) {
			pos = new Vector3 (Mathf.Sin (Mathf.Deg2Rad * a * i) * BUTTON_DISTANCE , 
				Mathf.Cos (Mathf.Deg2Rad * a * i) * BUTTON_DISTANCE , 0);
			GameObject p_skillBtn = Instantiate (p_skillList [i],Vector3.zero,Camera.main.transform.rotation) as GameObject;
			p_skillBtn.transform.SetParent (parent);
			p_skillBtn.transform.localPosition = pos;
			p_skillBtn.GetComponentInChildren<Text> ().text = p_skillBtn.GetComponentInChildren<SkillScript> ().s_name;
			p_skillBtn.GetComponentInChildren<SkillScript> ().skillMethod = p_funcList [i];
		}
	}
	/// <summary>
	/// ボタンを隠す
	/// </summary>
	public void HideSkillBtn(){
		Transform parent = this.transform.GetChild (0);
		for (int i = 0; i < parent.childCount; i++) {
			parent.GetChild (i).GetComponent<Collider> ().enabled = false;
		}
		parent.GetComponent<Canvas> ().enabled = false;
	}
	/// <summary>
	/// ボタンを削除する
	/// </summary>
	public void RemoveSkillBtn(){
		Transform parent = this.transform.GetChild (0);
		for (int i = 0; i < parent.childCount; i++) {
			Destroy (parent.GetChild (i).gameObject);
		}
	}
	/// <summary>
	/// ボタンを表す
	/// </summary>
	public void ShowSkillBtn(){
		Transform parent = this.transform.GetChild (0);
		for (int i = 0; i < parent.childCount; i++) {
			if(parent.GetChild (i).GetComponent <SkillScript>().isRecast == false)
				parent.GetChild (i).GetComponent<Collider> ().enabled = true;
		}
		parent.GetComponent<Canvas> ().enabled = true;
	}
	/// <summary>
	/// 最初の位置に戻る
	/// </summary>
	public void ReturnPos(){
		StartCoroutine(LerpMove(this.gameObject, this.transform.position, startPos, 2));
		ChangeMode (ReadyMode.Instance);
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
	/// <param name="btn">ボタン.</param>
	/// <param name="time">リーキャストタイム.</param>
	public IEnumerator SkillRecast(GameObject btn, float time){
		btn.GetComponent<SkillScript> ().isRecast = true;
		Image img = btn.transform.FindChild("Image"). GetComponent<Image> ();
		img.fillAmount = 1;
		btn.GetComponent<Collider> ().enabled = false;
		float startTime = Time.time;
		while(true){
			float rate = 1 - (Time.time - startTime) / time;
			if(rate <=0){
				img.fillAmount = rate;
				btn.GetComponent<Collider> ().enabled = true;
				btn.GetComponent<SkillScript> ().isRecast = false;
				yield break;
			}
			img.fillAmount = rate;
			yield return new WaitForEndOfFrame();
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
	public IEnumerator LerpMove(GameObject obj, Vector3 startPos, Vector3 endPos, float speed =1, GameObject target =null, SkillScript sc = null, float a_time = 1){
		float timer = 0;
		if(target != null)
			endPos.z -= target.GetComponent<CapsuleCollider> ().radius;
		obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", true);
		while (true) {
			timer += Time.deltaTime * speed;
			float moveRate = timer / PLAYER_MOVE_TIME;
			if (moveRate  >= 1) {
				moveRate = 1;
				obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
				obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", false);
				if (target != null && sc!=null) {
					obj.GetComponentInChildren<Animator> ().SetTrigger ("Attack");
					StartCoroutine( Damage (target, sc, a_time));
				}
				yield break;
			}
			obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
			yield return new WaitForEndOfFrame ();
		}
	}
	/// <summary>
	/// Damage target
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="sc">Skill Script.</param>
	private IEnumerator Damage(GameObject target, SkillScript sc, float time){
		float timer = 0;
		while (true) {
			timer += Time.deltaTime;
			float counter = timer / time;
			if (counter >= 1) {
				float s_power = 1;	//精霊の力
				if (CheckFlag (ConditionStatus.POWER_UP))
					s_power = 1.5f;
				EnemyBase eb = target.GetComponent<EnemyBase> ();
				int damage = Math.Max ((int)((p_attack + sc.s_power) * s_power) - eb.e_defence, 0);
				eb.e_hp -= damage;
				StartCoroutine (SkillRecast (sc.gameObject, sc.s_recast));
				yield break;
			}
			yield return new WaitForEndOfFrame ();
		}
	}
	#endregion
}
