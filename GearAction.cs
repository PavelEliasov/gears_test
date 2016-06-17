using UnityEngine;
using System.Collections;

public class GearAction : MonoBehaviour {

    int gumCount;

    public int GumCount  {
        get {
            return gumCount;
        }

        set {
            if (value < 0) {
                gumCount = 0;
            }
            else {
                gumCount = value;
            }
          }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

      // Debug.Log(gumCount);
        if (gumCount==0) {
            this.gameObject.tag = "Gear";
        }
	}

	
}
