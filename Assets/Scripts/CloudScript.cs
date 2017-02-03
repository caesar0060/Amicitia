using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {
    float num = 0;
    float time = 0;
	// Use this for initialization
	void Start () {
        time = Time.time;
	}
	
	// Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, num, 0));
        num = (Time.time - time) * 2;
        if (num > 360)
            num = 0;
    }
}
