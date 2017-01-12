using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangeDetect : MonoBehaviour
{
    #region Properties
    public List<GameObject> targets = new List<GameObject>();
    #endregion
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.layer == LayerMask.NameToLayer("Player") || obj.layer == LayerMask.NameToLayer("Enemy"))
        {
            targets.Add(obj);
        }
    }
}
