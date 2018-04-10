using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationDrop : MonoBehaviour {
    public int RotateSleep = 60;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right, Time.deltaTime);
        
      transform.Rotate(Vector3.up, RotateSleep * Time.deltaTime, Space.World);
        //transform.Rotate(Vector3.up, Time.deltaTime, Space.World);
    }
}
