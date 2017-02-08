using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class JobBase : StatusControl {
	#region Properties
    private static Vector3[] Kiren_BTN = new Vector3[] {new Vector3(0,0.6f,0),
		new Vector3(-0.6f,0.5f,0), new Vector3(-0.6f,0,0), new Vector3(-0.5f,-0.5f,0)
	};
	// プレイヤー攻撃のときの移動の必要な時間
	private static float PLAYER_MOVE_SPEED = 5.0f;
	// ボタンの距離
	private static float BUTTON_DISTANCE = 0.7f;
	//インスタンスを保存するコントローラ
	public Controller controller;
	// Skillを保存用配列
	public GameObject[] p_skillList;
	public P_Delegate[] p_funcList;
	// 位置の最初値
	[HideInInspector] public Vector3 startPos;
	//使うスキルを登録する
	[HideInInspector] public SkillScript skillUsing;
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

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (this.battelStatus == BattelStatus.NOT_IN_BATTEL)
        {
            GameObject other_go = other.gameObject;
            if (other_go.layer == LayerMask.NameToLayer("Enemy"))
            {
                PlayerRoot pr = PlayerRoot.Instance;
                pr.ChangeMode(T_Wait.Instance);
                pr.p_jb._target = other_go;
                if (other_go.GetComponentInParent<EnemyPoint>().battelEnemyList.Count > 0)
                    pr.battelEnemyList = other_go.GetComponentInParent<EnemyPoint>().battelEnemyList;
                FadeManager.Instance.LoadLevel("BattelScene", 2, BattelStart.Instance);
				pr.transform.position = this.transform.position;
                other_go.transform.GetComponentInParent<EnemyPoint>().isEnemyDead = true;
				Destroy(other_go);
            }
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
			pos = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * a * i) * BUTTON_DISTANCE , 
				Mathf.Sin (Mathf.Deg2Rad * a * i) * BUTTON_DISTANCE , 0);
			GameObject p_skillBtn = Instantiate (p_skillList [i],Vector3.zero,Camera.main.transform.rotation) as GameObject;
            if (this._type == JobType.Leader)
                pos = Kiren_BTN[i];
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
            try
            {
                parent.GetChild(i).GetComponent<Collider>().enabled = false;
            }
            catch (MissingComponentException)
            {
                continue;
            }
		}
		parent.GetComponent<Canvas> ().enabled = false;
	}
	/// <summary>
	/// ボタンを削除する
	/// </summary>
	public void RemoveSkillBtn(){
		Transform parent = this.transform.GetChild (0);
		for (int i = 0; i < parent.childCount; i++) {
            try
            {
			Destroy (parent.GetChild (i).gameObject);
            }
            catch (NullReferenceException)
            {
                continue;
            }
		}
	}
	/// <summary>
	/// ボタンを表す
	/// </summary>
	public void ShowSkillBtn(){
		Transform parent = this.transform.GetChild (0);
		for (int i = 0; i < parent.childCount; i++) {
            try
            {
                if (parent.GetChild(i).GetComponent<SkillScript>().isRecast == false)
                    parent.GetChild(i).GetComponent<Collider>().enabled = true;
            }
            catch(NullReferenceException)
            {
                continue;
            }
		}
		parent.GetComponent<Canvas> ().enabled = true;
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
	}
    /// <summary>
    /// 死亡しているかどうかをチェック
    /// </summary>
    public void CheckDead()
    {
        if (_hp <= 0)
        {
            this.Set_b_Status(BattelStatus.DEAD);
            this.GetComponent<CapsuleCollider>().enabled = false;
            PlayerRoot.Instance.partyList.Remove(this.gameObject);
            this.GetComponentInChildren<Animator>().SetTrigger("Dead");
            //Destroy (this.gameObject);
        }
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
        if (CheckFlag(ConditionStatus.SLOW))
            time *= 2;
		btn.GetComponent<SkillScript> ().isRecast = true;
		Image img = btn.transform.FindChild("Image"). GetComponent<Image> ();
		img.fillAmount = 1;
		btn.GetComponent<Collider> ().enabled = false;
		float startTime = Time.time;
		while(true){
            float rate = 1 - (Time.time - startTime) / time;
            if (rate <= 0)
            {
                img.fillAmount = rate;
                btn.GetComponent<SkillScript>().isRecast = false;
                if (btn.GetComponentInParent<Canvas>().isActiveAndEnabled)
                    btn.GetComponent<Collider>().enabled = true;
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
	public IEnumerator LerpMove(GameObject obj, Vector3 startPos, Vector3 endPos, float speed =1, GameObject target =null, SkillScript sc = null, string a_name = "", float a_time = 1){
		float timer = 0;
		obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", true);
		while (true) {
			float distance = Vector3.Distance (startPos, endPos);
			float time = distance / (PLAYER_MOVE_SPEED * speed);
			timer += Time.deltaTime;
			float moveRate = timer / time;
			if (target != null) {
                try
                {
                    Vector3 dir = target.transform.position - this.transform.position;
                    RotateToTarget(target);
                    endPos = target.transform.position - Vector3.Normalize(dir) * target.GetComponent<CapsuleCollider>().radius;
                }
                catch (MissingReferenceException)
                {
					ReturnPos ();
					yield break;
                }
			}
			if (moveRate  >= 1) {
				moveRate = 1;
				obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
				obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", false);
				if (target != null && sc != null) {
                    // switch() 単体？複数？
					try {
						obj.GetComponentInChildren<Animator> ().SetTrigger (a_name);
						StartCoroutine (Damage (target, sc, a_time));
					} catch (MissingReferenceException) {
						ReturnPos ();
						yield break;
					}
				}
				else { 
					this.transform.rotation = Quaternion.Euler (Vector3.zero);
					GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().ReadyNextAttack();
					ChangeMode(ReadyMode.Instance);
					}
				yield break;
			}
			obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
            Vector3 moveRot = endPos - this.transform.position;
            Quaternion q = Quaternion.LookRotation(moveRot.normalized, Vector3.up);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, q, 0.5f);
			yield return new WaitForEndOfFrame ();
		}
	}
	/// <summary>
	/// Damageを与える
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="sc">Skill Script.</param>
	/// <param name="time">Damageを与える時間</param>
	/// <returns></returns>
	public IEnumerator Damage(GameObject target, SkillScript sc, float time){
        if (sc.s_targetNum == TargetNum.MUTIPLE)
        {
            CreateRange();
            GameObject.FindGameObjectWithTag("Range").GetComponent<SphereCollider>().radius = sc.s_range;
        }
		float timer = 0;
		while (true) {
			timer += Time.deltaTime;
			float counter = timer / time;
			if (counter >= 1) {
                try
                {
                    float s_power = 1;	//精霊の力
                    if (CheckFlag(ConditionStatus.POWER_UP))
                        s_power = 1.5f;
                    if (sc.s_targetNum == TargetNum.MUTIPLE)
                    {
                        foreach (var r_target in GameObject.FindGameObjectWithTag("Range").GetComponent<RangeDetect>().targets)
                        {
                            if (r_target.layer != LayerMask.NameToLayer("Enemy"))
                                continue;
                            EnemyBase eb = r_target.GetComponent<EnemyBase>();
                            int damage = Math.Max((int)((_attack + sc.s_power) * s_power) - eb._m_defence, 0);
                            eb.Set_HP(damage);
                            r_target.GetComponentInChildren<Animator>().SetTrigger("isDamage");
                            Vector3 pos = target.transform.position;
                            pos.y += target.GetComponent<CapsuleCollider>().height;
                            GameObject effectObj = Instantiate(Resources.Load("Prefabs/Magic/Hit_Effect"), pos, Quaternion.identity) as GameObject;
                        }
                    }
                    else
                    {
                        EnemyBase eb = target.GetComponent<EnemyBase>();
                        int damage = Math.Max((int)((_attack + sc.s_power) * s_power) - eb._m_defence, 0);
                        eb.Set_HP(damage);
                        target.GetComponentInChildren<Animator>().SetTrigger("isDamage");
                        Vector3 pos = target.transform.position;
                        pos.y += target.GetComponent<CapsuleCollider>().height;
                        GameObject effectObj = Instantiate(Resources.Load("Prefabs/Magic/Hit_Effect"), pos, Quaternion.identity) as GameObject;
                    }
                    if (GameObject.FindGameObjectWithTag("Range"))
                        DeleteRange();
                    StartCoroutine(SkillRecast(sc.gameObject, sc.s_recast));
                    ReturnPos();
					yield break;
                }
                catch (MissingReferenceException)
                {
					yield break;
                }
			}
			yield return new WaitForEndOfFrame ();
		}
	}
    /// <summary>
    /// Magic Damageを与える
    /// </summary>
    /// <param name="target">Target</param>
    /// <param name="sc">Skill Script</param>
    /// <param name="a_time">エフェクトを出す時間</param>
    /// <param name="effect">エフェクトのpath</param>
    /// <param name="e_time">Damageを与える時間</param>
    /// <returns></returns>
    public IEnumerator MagicDamage(GameObject target, SkillScript sc, float a_time, string effect, float e_time)
    {
		
		if (sc.s_targetNum == TargetNum.MUTIPLE)
		{
			CreateRange();
			GameObject.FindGameObjectWithTag("Range").GetComponent<SphereCollider>().radius = sc.s_range;
		}
		float timer = 0;
		bool useMagic = false;
		while (true)
		{
			timer += Time.deltaTime;
			if (timer >= a_time && !useMagic)
			{
				GameObject effectObj = Instantiate(Resources.Load(effect), target.transform.position, Quaternion.identity) as GameObject;
				effectObj.transform.parent = this.transform;
				useMagic = true;
			}
			if (timer >= e_time)
			{
				try
				{ 
				    float s_power = 1;	//精霊の力
				    if (CheckFlag(ConditionStatus.MAGIC_UP))
					    s_power = 1.5f;
				    if (sc.s_targetNum == TargetNum.MUTIPLE)
				    {
					    foreach (var r_target in GameObject.FindGameObjectWithTag("Range").GetComponent<RangeDetect>().targets)
					    {
						    if (r_target.layer != LayerMask.NameToLayer("Enemy"))
							    continue;
						    EnemyBase eb = r_target.GetComponent<EnemyBase>();
						    int damage = Math.Max((int)((_attack + sc.s_power) * s_power) - eb._m_defence, 0);
						    eb.Set_HP(damage);
						    r_target.GetComponentInChildren<Animator>().SetTrigger("isDamage");
                            Vector3 pos = target.transform.position;
                            pos.y += target.GetComponent<CapsuleCollider>().height;
                            GameObject effectObj = Instantiate(Resources.Load("Prefabs/Magic/Hit_Effect"), pos, Quaternion.identity) as GameObject;
					    }
				    }
				    else
				    {
                        StatusControl status = target.GetComponent<StatusControl>();
                        int damage = 0;
                        if (target.layer == LayerMask.NameToLayer("Player"))
                        {
                            damage = (int)((_attack + sc.s_power) * s_power * -1);
                        }
                        else
                        {
                            damage = Math.Max((int)((_attack + sc.s_power) * s_power) - status._m_defence, 0);
                            target.GetComponentInChildren<Animator>().SetTrigger("isDamage");
                            Vector3 pos = target.transform.position;
                            pos.y += target.GetComponent<CapsuleCollider>().height;
                            GameObject effectObj = Instantiate(Resources.Load("Prefabs/Magic/Hit_Effect"), pos, Quaternion.identity) as GameObject;

                        }
                        status.Set_HP(damage);
				    }
				    if (GameObject.FindGameObjectWithTag("Range"))
					    DeleteRange();
				    StartCoroutine(SkillRecast(sc.gameObject, sc.s_recast));
				    yield break;
				}
				catch(MissingReferenceException)
				{
					yield break;
				}
			}
			yield return new WaitForEndOfFrame();
		}
    }
    /// <summary>
    /// 異常状態を与える
    /// </summary>
    /// <param name="target">Target</param>
    /// <param name="sc">Skill Script</param>
    /// <param name="a_time">エフェクトを出す時間</param>
    /// <param name="effect">エフェクトのpath</param>
    /// <param name="e_time">Damageを与える時間</param>
    /// <returns></returns>
    public IEnumerator StatusMagic(GameObject target, SkillScript sc, float a_time, string effect, float e_time, ConditionStatus status)
    {

        if (sc.s_targetNum == TargetNum.MUTIPLE)
        {
            CreateRange();
            GameObject.FindGameObjectWithTag("Range").GetComponent<SphereCollider>().radius = sc.s_range;
        }
        float timer = 0;
        bool useMagic = false;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= a_time && !useMagic)
            {
                GameObject effectObj = Instantiate(Resources.Load(effect), target.transform.position, Quaternion.identity) as GameObject;
                effectObj.transform.parent = this.transform;
                useMagic = true;
            }
            if (timer >= e_time)
            {
                try
                {
                    if (sc.s_targetNum == TargetNum.MUTIPLE)
                    {
                        foreach (var r_target in GameObject.FindGameObjectWithTag("Range").GetComponent<RangeDetect>().targets)
                        {
                            // TODO: Player / Enemy を判断できるように
                            if (r_target.layer != LayerMask.NameToLayer("Player"))
                                continue;
                            JobBase jb = r_target.GetComponent<JobBase>();
                            jb.Set_c_Status(status);
                            jb.StartCoroutine(jb.StatusCounter(status, sc.s_effectTime));
                        }
                    }
                    else
                    {
                        if (target.layer == LayerMask.NameToLayer("Player"))
                        {
                            JobBase jb = target.GetComponent<JobBase>();
                            jb.Set_c_Status(status);
                            jb.StartCoroutine(jb.StatusCounter(status, sc.s_effectTime));
                        }
                    }
                    if (GameObject.FindGameObjectWithTag("Range"))
                        DeleteRange();
                    StartCoroutine(SkillRecast(sc.gameObject, sc.s_recast));
                    yield break;
                }
                catch (MissingReferenceException)
                {
                    yield break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
	#endregion
}
