using UnityEngine;
using System.Collections;

public class AttackerScript : JobBase {
	#region Properties


	#endregion

	// Use this for initialization
	void Start () {
		battelStatus = BattelStatus.NORMAL;
		//-----test
		controller = BattelMode.Instance;
		//---------
	}
	
	// Update is called once per frame
	void Update () {
		//test-----
		controller.Excute (this);
		//test-----
	}
	#region Function
	override public void Skill1(){
		Debug.Log ("Attacker Skill1");
	}
	override public void Skill2(){}
	override public void Skill3(){}
	override public void Skill4(){}
	#endregion
}
