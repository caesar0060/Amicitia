using UnityEngine;
using System.Collections;

public class PlayerControl : PlayerOrAI {
	// 移動モードのインスタンス
	private static PlayerControl instance;
	/// <summary>
	/// 移動モードのインスタンスを取得
	/// </summary>
	/// <value>移動のインスタンス</value>
	public static PlayerControl Instance{
		get {
			if(instance == null)
				instance = new PlayerControl();
			return instance;
		}
	}
	override public void ChangeMode(Controller newMode){
		controller = newMode;
	}
}
