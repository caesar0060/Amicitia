using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Set
{
	public float probability;	//確率
	public string[] enemy_set;		//配置
}
[Serializable]
public class SetCollect
{
	public Set[] sets;		
}
[Serializable]
public class Skill{
	public string s_name;		//名前
    public float s_power;       //効果量
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
    // 使うスキルのアイコンを表示する時間
    private static float SHOW_ICON_TIME = 2.0f;
    // 移動のスピード
    public float ENEMY_MOVE_SPEED = 5.0f;
	//インスタンスを保存するコントローラ
	public E_Controller controller = null;
	//前のインスタンスを保存するコントローラ
	public E_Controller preivousController = null;
    // monsterの名前
    public string e_name;
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
	//エンカウントすると出現する敵の構成、nullするとランダムになる
	public GameObject[] enemylist;
    // スキルを表示するアイコン
    public GameObject skillIcon;
    // 移動しているかどうか
    public bool isMove = false;
    public Coroutine coroutine;
	#endregion

    public void OnTriggerStay(Collider other)
    {
        switch (battelStatus)
        {
            case BattelStatus.NOT_IN_BATTEL:
                GameObject other_go = other.gameObject;
                if (other_go.layer == LayerMask.NameToLayer("Player"))
                {
                    if (_target == null)
                    {
                        if (CheckIsFront(other_go))
                            _target = other_go;
                    }
                    else if (_target == other_go)
                    {
                        if (!CheckIsFront(other_go))
                            _target = null;
                    }
                }
                break;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        switch (battelStatus)
        {
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
	public void CreateSkillList(E_Delegate[] sa, string sd){
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
            this.GetComponentInChildren<Animator>().SetTrigger("isDead");
			e_pr.enemyList.Remove (this.gameObject);
            foreach (var e in PlayerRoot.Instance.evnet_list)
            {
                if (e.target_name == this.e_name)
                {
                    e.target_count++;
                    if (e.target_count > e.target_num)
                        e.target_count = e.target_num;
                }
            }
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
		StartCoroutine(LerpMove(this.gameObject, this.transform.position, startPos, 2, "return"));
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
        if (CheckFlag(ConditionStatus.SLOW))
            time *= 2;
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
	public IEnumerator LerpMove(GameObject obj, Vector3 startPos, Vector3 endPos, float speed = 1, string method = "move", GameObject target =null, Skill skill = null, string a_name = "", float a_time = 1){
		float timer = 0;
		obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", true);
		while (true) {
			try{
				if (target != null) {
					Vector3 dir = target.transform.position - this.transform.position;
					RotateToTarget(target);
                    endPos = target.transform.position - Vector3.Normalize(dir) * this.GetComponent<CapsuleCollider>().radius;
                    if (method == "move")
                    {
                        float dis = Vector3.Distance(Vector3.zero, this.transform.localPosition);
                        if (dis >= 5)
                            endPos = this.transform.position;
                    }
				}
				float distance = Vector3.Distance (startPos, endPos);
				float time = distance / (ENEMY_MOVE_SPEED * speed);
				timer += Time.deltaTime;
				float moveRate = timer / time;
				if (moveRate  >= 1) {
					moveRate = 1;
					obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
					obj.GetComponentInChildren<Animator> ().SetBool ("isMoved", false);
                    switch (method)
                    {
                        case "move":
                            Debug.Log(isMove);
                            isMove = false;
                            break;
                        case "attack":
                            if (target != null && skill!=null) {
						        obj.GetComponentInChildren<Animator> ().SetTrigger (a_name);
						        StartCoroutine( Damage (target, skill, a_time));
					        }
                            break;
                        case "return":
                            this.transform.localRotation = Quaternion.Euler(Vector3.zero);
						    GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().ReadyNextAttack();
						    ChangeMode(preivousController);
                            break;
                    }
					yield break;
				}
				obj.transform.position = Vector3.Lerp (startPos, endPos, moveRate);
                Vector3 moveRot = endPos - this.transform.position;
                Quaternion q = Quaternion.LookRotation(moveRot.normalized, Vector3.up);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, q, 0.5f);
                
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
		if (skill.s_range > 0)
		{
			CreateRange();
			GameObject.FindGameObjectWithTag("Range").GetComponent<SphereCollider>().radius = skill.s_range;
		}
		float timer = 0;
		while (true) {
			try{
				timer += Time.deltaTime;
				float counter = timer / time;
				if (counter >= 1) {
					float s_power = 1;	//精霊の力
					if (CheckFlag (ConditionStatus.POWER_UP))
						s_power = 1.5f;
					if (skill.s_range > 0)
                    {
                        foreach (var r_target in GameObject.FindGameObjectWithTag("Range").GetComponent<RangeDetect>().targets)
                        {
                            if (r_target.layer != LayerMask.NameToLayer("Player"))
                                continue;
                            JobBase jb = r_target.GetComponent<JobBase>();
							int damage = Math.Max ((int)((_attack + skill.s_power) * s_power) - jb._defence, 0);
                            if (jb.CheckFlag(ConditionStatus.ALL_DAMAGE_DOWN_20))
                                damage = damage * 80 / 100;
                            else if (jb.CheckFlag(ConditionStatus.ALL_DAMAGE_DOWN))
                                damage = damage * 90 / 100;
							jb.Set_HP(damage);
                            r_target.GetComponentInChildren<Animator>().SetTrigger("Damage");
                            Vector3 pos = r_target.transform.position;
                            pos.y += r_target.GetComponent<CapsuleCollider>().height;
                            GameObject effectObj = Instantiate(Resources.Load("Prefabs/Magic/Hit_Effect"), pos, Quaternion.identity) as GameObject;
                        }
                    }
                    else
                    {
						JobBase jb = target.GetComponent<JobBase> ();
						int damage = Math.Max ((int)((_attack + skill.s_power) * s_power) - jb._defence, 0);
                        if (jb.CheckFlag(ConditionStatus.ALL_DAMAGE_DOWN_20))
                            damage = damage * 80 / 100;
                        else if (jb.CheckFlag(ConditionStatus.ALL_DAMAGE_DOWN))
                            damage = damage * 90 / 100;
						jb.Set_HP(damage);
                        target.GetComponentInChildren<Animator>().SetTrigger("Damage");
                        Vector3 pos = target.transform.position;
                        pos.y += target.GetComponent<CapsuleCollider>().height;
                        GameObject effectObj = Instantiate(Resources.Load("Prefabs/Magic/Hit_Effect"), pos, Quaternion.identity) as GameObject;
					}
					if (GameObject.FindGameObjectWithTag("Range"))
						DeleteRange();
					StartCoroutine (SkillRecast (skill, skill.s_recast));
                    ReturnPos();
					yield break;
				}
			}
			catch(MissingReferenceException){
				yield break;
			}
			yield return new WaitForEndOfFrame ();
		}
	}
	/// <summary>
	/// Magic Damage the specified target
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="skill">Skill.</param>
	/// <param name="time">攻撃のタイミング.</param>
	public IEnumerator MagicDamage(GameObject target, Skill skill, float a_time, string effect, float e_time)
	{
		if (skill.s_range > 0)
		{
			CreateRange();
			GameObject.FindGameObjectWithTag("Range").GetComponent<SphereCollider>().radius = skill.s_range;
		}
		float timer = 0;
		bool useMagic = false;
		while (true)
		{
			try
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
					float s_power = 1;	//精霊の力
					if (CheckFlag(ConditionStatus.MAGIC_UP))
						s_power = 1.5f;
					if (skill.s_range > 0)
					{
						foreach (var r_target in GameObject.FindGameObjectWithTag("Range").GetComponent<RangeDetect>().targets)
						{
							if (r_target.layer != LayerMask.NameToLayer("Player"))
								continue;
                            JobBase jb = r_target.GetComponent<JobBase>();
							int damage = Math.Max((int)((_attack + skill.s_power) * s_power) - jb._defence, 0);
                            if (jb.CheckFlag(ConditionStatus.ALL_DAMAGE_DOWN_20))
                                damage = damage * 80 / 100;
                            else if(jb.CheckFlag(ConditionStatus.ALL_DAMAGE_DOWN))
                                damage = damage * 90 / 100;
							jb.Set_HP(damage);
							r_target.GetComponentInChildren<Animator>().SetTrigger("Damage");
                            Vector3 pos = r_target.transform.position;
                            pos.y += r_target.GetComponent<CapsuleCollider>().height;
                            GameObject effectObj = Instantiate(Resources.Load("Prefabs/Magic/Hit_Effect"), pos, Quaternion.identity) as GameObject;
						}
					}
					else
					{
                        StatusControl status = target.GetComponent<StatusControl>();
                        int damage = 0;
                        if (target.layer == LayerMask.NameToLayer("Enemy"))
                        {
                            damage = (int)((_attack + skill.s_power) * s_power * -1);
                        }
                        else
                        {
                            damage = Math.Max((int)((_attack + skill.s_power) * s_power) - status._defence, 0);
                            if (status.CheckFlag(ConditionStatus.ALL_DAMAGE_DOWN_20))
                                damage = damage * 80 / 100;
                            else if (status.CheckFlag(ConditionStatus.ALL_DAMAGE_DOWN))
                                damage = damage * 90 / 100;
                            target.GetComponentInChildren<Animator>().SetTrigger("Damage");
                            Vector3 pos = target.transform.position;
                            pos.y += target.GetComponent<CapsuleCollider>().height;
                            GameObject effectObj = Instantiate(Resources.Load("Prefabs/Magic/Hit_Effect"), pos, Quaternion.identity) as GameObject;
                        }
						status.Set_HP(damage);
					}
					if (GameObject.FindGameObjectWithTag("Range"))
						DeleteRange();
                    StartCoroutine(SkillRecast(skill, skill.s_recast));
					yield break;
				}
			}
			catch (MissingReferenceException)
			{
				yield break;
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
    public IEnumerator StatusMagic(GameObject target, Skill skill, float a_time, string effect, float e_time, ConditionStatus status)
    {

        if (skill.s_range > 0)
        {
            CreateRange();
            GameObject.FindGameObjectWithTag("Range").GetComponent<SphereCollider>().radius = skill.s_range;
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
                    if (skill.s_range > 0)
                    {
                        foreach (var r_target in GameObject.FindGameObjectWithTag("Range").GetComponent<RangeDetect>().targets)
                        {
                            // TODO: Player / Enemy を判断できるように
                            if (r_target.layer != LayerMask.NameToLayer("Enemy"))
                                continue;
                            EnemyBase eb = r_target.GetComponent<EnemyBase>();
                            eb.Set_c_Status(status);
                            eb.StartCoroutine(eb.StatusCounter(status, skill.s_effectTime));
                        }
                    }
                    else
                    {
                        if (target.layer == LayerMask.NameToLayer("Enemy"))
                        {
                            EnemyBase eb = target.GetComponent<EnemyBase>();
                            eb.Set_c_Status(status);
                            eb.StartCoroutine(eb.StatusCounter(status, skill.s_effectTime));
                        }
                    }
                    if (GameObject.FindGameObjectWithTag("Range"))
                        DeleteRange();
                    StartCoroutine(SkillRecast(skill, skill.s_recast));
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
	/// <summary>
	/// Loading this instance.
	/// </summary>
	public IEnumerator Loading(float time){
		while (true) {
			controller.Excute (this);
			yield return new WaitForSeconds (time);
		}
	}
    /// <summary>
	/// スキルを使う.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="skill">使うスキル.</param>
    public IEnumerator SkillUse(GameObject target, Skill skill)
    {
        float startTime = Time.time;
        GameObject icon = Instantiate(skillIcon) as GameObject;
        icon.transform.SetParent(this.transform.GetChild(0));
        float y = this.GetComponent<CapsuleCollider>().height;
        icon.transform.localPosition = new Vector3(0, y, 0);
        icon.GetComponentInChildren<Text>().text = skill.s_name;
        while (true)
        {
            float time = Time.time - startTime;
            if (time / SHOW_ICON_TIME >= 1)
            {
                Destroy(icon);
                ChangeMode(E_SkillMode.Instance);
                _target = target; skillUsing = skill;
                GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().attackList.Add(this.gameObject);
                skill.isRecast = true;
                yield break;
            }
            yield return new WaitForSeconds(SHOW_ICON_TIME);
        }
    }
    /// <summary>
    /// スキルを使う.
    /// </summary>
    /// <param name="target">Target.</param>
    /// <param name="skill">使うスキル.</param>
    public IEnumerator SkillUse(GameObject target, Skill skill, E_Controller controller)
    {
        float startTime = Time.time;
        GameObject icon = Instantiate(skillIcon) as GameObject;
        icon.transform.SetParent(this.transform.GetChild(0));
        icon.transform.localPosition = new Vector3(0, 1, 0);
        icon.GetComponentInChildren<Text>().text = skill.s_name;
        while (true)
        {
            float time = Time.time - startTime;
            if (time / SHOW_ICON_TIME >= 1)
            {
                Destroy(icon);
                ChangeMode(controller);
                _target = target; skillUsing = skill;
                GameObject.FindGameObjectWithTag("PartyRoot").GetComponent<PartyRoot>().attackList.Add(this.gameObject);
                skill.isRecast = true;
                yield break;
            }
            yield return new WaitForSeconds(SHOW_ICON_TIME);
        }
    }
	#endregion
}
