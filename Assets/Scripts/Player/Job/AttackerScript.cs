using UnityEngine;
using System.Collections;

public class AttackerScript : JobBase {
	#region Properties


	#endregion

	// Use this for initialization
	void Start () {
		p_funcList = new Delegate[]{ Skill1, Skill1, Skill1, Skill1, Skill1, Skill1 };
		Set_b_Status (BattelStatus.NORMAL);
		//-----test
		controller = WorldMode.Instance;
		controller.Enter (this);
		//---------
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
