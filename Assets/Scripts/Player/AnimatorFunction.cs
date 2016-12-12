using UnityEngine;
using System.Collections;

public class AnimatorFunction : MonoBehaviour {
	
	public void Return(){
		switch (this.transform.parent.gameObject.layer) {
		case 8:
			this.transform.parent.gameObject.GetComponent<JobBase> ().ReturnPos ();
			break;
		case 12:
			this.transform.parent.gameObject.GetComponent<EnemyBase> ().ReturnPos ();
			break;
		}
	}
}
