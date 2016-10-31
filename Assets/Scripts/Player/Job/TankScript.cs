using UnityEngine;
using System.Collections;

public class TankScript : JobBase {

	// Use this for initialization
	void Start () {
		//-----test
		playerControl = PlayerControl.Instance;
		playerControl.controller = BattelMode.Instance;
		playerControl.ChangeMode (WalkMode.Instance);
		//---------
	}
	
	// Update is called once per frame
	void Update () {
		//test-----
		playerControl.controller.Excute ();
		//test-----
	}
}
