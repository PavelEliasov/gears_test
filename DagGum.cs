using UnityEngine;
using System.Collections;

public class DagGum : MonoBehaviour {
	public GameObject parentObj;
	public GameObject MiddleGum;
	public float _minScaleDistance=101f;
	public float _maxScaleDistance=125f;
    bool inTriggerArea;
	Vector3 defaultPos;
    bool isMouseUp;
    bool isMouseUp2;
    bool isMouseDown;
    bool acsess;
    string tagofGear="Gear";
	// Use this for initialization
	void Start () {
        Messenger.AddListener("Disable",DisableColliders);
        Messenger.AddListener("Enable", EnableColliders);
        defaultPos = gameObject.transform.position;
		gameObject.tag = "Untagged";
	}
	
	// Update is called once per frame
	void Update () {
		Scale ();

       // Debug.Log(this.gameObject.tag);
        
		//Rotate ();
	}


    void DisableColliders() {

        Debug.Log("lllllllllll");
        if (gameObject.tag == "Untagged") {
            return;
        }
        this.gameObject.GetComponent<Collider2D>().enabled = false;

    }
    void EnableColliders() {
        //if (gameObject.tag == "Untagged") {
        //    return;
        //}
        Debug.Log("000000000000");
        this.gameObject.GetComponent<Collider2D>().enabled = true;


    }
 
	void OnMouseDrag(){
		//Debug.Log ("asadffaa");

		Rotate ();

		//Debug.Log((Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObj.transform.position).sqrMagnitude);
		if((Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObj.transform.position).sqrMagnitude<_minScaleDistance ||
		   (Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObj.transform.position).sqrMagnitude>_maxScaleDistance ){
			//gameObject.transform.position=defaultPos;
			//Debug.Log("asdasd");
				return;
			
		}
		gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0,0,10f);


		//Debug.Log (gameObject.transform.position);
		//Scale ();

	}
	void OnMouseDown(){

        Messenger.Broadcast("Disable");
        gameObject.tag = "Untagged";
        isMouseDown = true;
	}
	void OnMouseUp(){
        isMouseUp = true;
        isMouseUp2 = true;
        gameObject.tag = "Gum";
        Messenger.Broadcast("Enable");
        if (inTriggerArea==false) {
            // parentObj.SetActive(false);
           
            parentObj.GetComponent<DragParentGum>().GoToSlot();
        }
	}
	void Scale(){

		MiddleGum.transform.localScale = new Vector3 (Mathf.Pow((transform.position - parentObj.transform.position).sqrMagnitude,0.5f), 1, 1);


	}
	void Rotate(){
		float angle = Vector2.Angle (Vector2.right, 
		                             Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObj.transform.position);
		if( Camera.main.ScreenToWorldPoint(Input.mousePosition).y <parentObj.transform.position.y){
			
			parentObj.transform.eulerAngles=new Vector3(0,0,-angle);
		}
		else{
			parentObj.transform.eulerAngles=new Vector3(0,0,angle);
		}


	}

	void OnTriggerStay2D(Collider2D other) {

       // Debug.Log("safasdfd");
		if (other.gameObject.tag == tagofGear) {
            //Debug.Log ((gameObject.transform.position-other.transform.position).sqrMagnitude>1.1f);

           // Debug.Log("aaaaaaaaaaa");
            if (isMouseDown==true) {
                other.gameObject.GetComponent<GearAction>().GumCount--;
                isMouseDown = false;
                tagofGear = "Gear";
            }

			if(gameObject.tag=="Gum"){
				//OnMouseDrag();
				float angle = Vector2.Angle (Vector2.right, 
				                             transform.position - parentObj.transform.position);
				if( transform.position.y <parentObj.transform.position.y){
					
					parentObj.transform.eulerAngles=new Vector3(0,0,-angle);
				}
				else{
					parentObj.transform.eulerAngles=new Vector3(0,0,angle);
				}
				Vector2 twoDPos=other.transform.position;
				gameObject.transform.position=twoDPos;
				other.transform.Rotate(0,0,10*Time.deltaTime*10);
                if (isMouseUp==true) {

                    other.gameObject.GetComponent<GearAction>().GumCount++;
                    other.gameObject.tag = "BusyGear";//change tag for next call
                    tagofGear = "BusyGear";
                    isMouseUp = false;
                }
				//Debug.Log (other.gameObject.tag);

				
			}
			
		} else {
			//Debug.Log (other.gameObject.tag);
		}
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Gear" || other.gameObject.tag == "BusyGear") {
            inTriggerArea = true;
            parentObj.GetComponent<CircleCollider2D>().enabled = false;
        }

   }

    void OnTriggerExit2D(Collider2D other) {
        if (isMouseUp2 == true) {
            other.gameObject.GetComponent<GearAction>().GumCount--;
            Debug.Log("Exit");
            tagofGear = "Gear";
            isMouseUp2 = false;
        }

        inTriggerArea = false;
        if (other.gameObject.tag == "Gear"|| other.gameObject.tag == "BusyGear") {
            parentObj.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
