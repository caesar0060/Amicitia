using UnityEngine;
using System.Collections;

public class TankScript : JobBase {
	#region Properties


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
	void OnTriggerStay(Collider other){
		switch(battelStatus){
		case BattelStatus.NOT_IN_BATTEL:
			GameObject other_go = other.gameObject;
			if(other_go.layer == LayerMask.NameToLayer("Water")){
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

	void OnTriggerExit(Collider other){
		switch(battelStatus){
		case BattelStatus.NOT_IN_BATTEL:
			if (_target == other.gameObject)
				_target = null;
			break;
		}
	}
	#region Function
	override public void Skill1(){
		Debug.Log ("Tank Skill1");
	}
	override public void Skill2(){}
	override public void Skill3(){}
	override public void Skill4(){}
	#endregion
}
