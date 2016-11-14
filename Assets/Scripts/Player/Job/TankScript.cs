using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TankScript : JobBase {
	#region Properties
	private int hit_count;		//撃たれた回数
	#endregion

	// Use this for initialization
	void Start () {
		p_funcList = new Delegate[]{ Skill1, Skill1, Skill1, Skill1, Skill1, Skill1 };
		Set_b_Status (BattelStatus.NORMAL);
		//-----test
		controller = WorldMode.Instance;
		controller.Enter (this);
		//---------		
		/*foreach (ConditionStatus status in Enum.GetValues(typeof(ConditionStatus))) {
			if (!CheckFlag (status))
				Debug.Log (Enum.GetName (typeof(ConditionStatus), status));
		}*/
	}

	// Update is called once per frame
	void Update () {
		//test-----
		controller.Excute (this);
		//test-----
	}

	#region Function
	public void Skill1(GameObject target = null, float time = 0){
		Debug.Log ("Tank Skill1");
	}
	/// <summary>
	/// カウンター
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Counter(GameObject target = null, float time = 0){
		Set_c_Status(ConditionStatus.PULL);
		StatusCounter (ConditionStatus.PULL, time);
	}
	/// <summary>
	/// イース
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill2(GameObject target = null, float time = 0){
		Set_c_Status(ConditionStatus.ALL_DAMAGE_DOWN);
		StatusCounter (ConditionStatus.ALL_DAMAGE_DOWN, s_script.s_effectTime);
	}
	/// <summary>
	/// 肩代わり
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill3(GameObject target = null, float time = 0){
	}
	/// <summary>
	/// 魔法、物理攻撃１０％カット
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="time">効果時間.</param>
	public void Skill4(GameObject target = null, float time = 0){
		JobBase jb = target.GetComponent<JobBase> ();
		jb.Set_c_Status (ConditionStatus.ALL_DAMAGE_DOWN);
	}
	#endregion
}
