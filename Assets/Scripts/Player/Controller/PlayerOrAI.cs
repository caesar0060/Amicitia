using UnityEngine;
using System.Collections;

public class PlayerOrAI {

	//インスタンスを保存するコントローラ
	public Controller controller;

	virtual public void ChangeMode(Controller newMode){
		controller.Exit ();
		controller = newMode;
		controller.Enter ();
	}
}
