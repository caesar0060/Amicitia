using UnityEngine;
using System.Collections;

public class E_TankScript : EnemyBase {
	#region Properties
	private int hit_count;		//撃たれた回数
	#endregion
	// Use this for initialization
	void Start () {
		//-----test
		controller = Defender1.Instance;
		controller.Enter (this);
		//---------	
	}
	
	// Update is called once per frame
	void Update () {
		//test-----
		controller.Excute (this);
		//test-----
	}
}
