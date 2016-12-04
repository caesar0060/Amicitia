using UnityEngine;
using System.Collections;

public class Animation_Function : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Return(){
		this.transform.parent.GetComponent<JobBase> ().ReturnPos ();
	}
}
