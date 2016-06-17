using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public float speed=10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Rotation ();
		//this.gameObject.transform.Rotate (0,0,speed*Time.deltaTime*10);
	}

	void Rotation(){
		this.gameObject.transform.Rotate (0,0,speed*Time.deltaTime*10);
	    

	}
}
