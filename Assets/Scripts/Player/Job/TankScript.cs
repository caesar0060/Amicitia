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
		ChangeMode (WorldMode.Instance);
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
	public void Skill1(){
		Debug.Log ("Tank Skill1");
	}
	#endregion
}
