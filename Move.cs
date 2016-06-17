using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W)) {

            Debug.Log("sadfsdfsdf");
            gameObject.transform.position += Vector3.down*2;
        }

        if (Input.GetKey(KeyCode.S)) {

            Debug.Log("sadfsdfsdf");
            gameObject.transform.position += Vector3.up* 2;
        }

        if (Input.GetKey(KeyCode.A)) {

            Debug.Log("sadfsdfsdf");
            gameObject.transform.position += Vector3.left * 2;
        }


        if (Input.GetKey(KeyCode.D)) {

            Debug.Log("sadfsdfsdf");
            gameObject.transform.position += Vector3.right * 2;
        }

    }
}
