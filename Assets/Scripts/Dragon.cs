using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {
    float num = 0;
    float time = 0;
    // Use this for initialization
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 old_pos = this.transform.position;
        float x = Mathf.Rad2Deg * Mathf.Sin(num);
        float z = Mathf.Rad2Deg * Mathf.Cos(num);;
        this.transform.position = new Vector3(x, 20, z);
        Quaternion rotationPos = Quaternion.LookRotation(this.transform.position - old_pos);
        this.transform.rotation = rotationPos;
        num = (Time.time - time);
        if (num > 360)
            num = 0;
    }
}
