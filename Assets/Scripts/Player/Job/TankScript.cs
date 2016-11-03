using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankScript : JobBase {
	#region Properties
	private int hit_count;		//撃たれた回数
	#endregion

	// Use this for initialization
	void Start () {
		battelStatus = BattelStatus.NOT_IN_BATTEL;
		//-----test
		controller = WorldMode.Instance;
		ChangeMode (WalkMode.Instance);
		//---------
	}

	// Update is called once per frame
	void Update () {
		//test-----
		controller.Excute (this);
		//test-----
	}

	#region Function
	public static void Skill1(){
		Debug.Log ("Tank Skill1");
	}
	#endregion
}
