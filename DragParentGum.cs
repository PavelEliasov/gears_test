using UnityEngine;
using System.Collections;

public class DragParentGum : MonoBehaviour {
    public GameObject _childGum;
    Vector2 defaultPos;
    Vector3 [] defaultPosOfChild;
    Vector3 [] defaultScaleOfChild;
    Quaternion [] defaultRotateOfChild;
    bool isMouseUp=false;
    bool isMouseUp2 = false;
    // Use this for initialization
    void Start () {
        defaultPosOfChild = new Vector3[] {transform.GetChild(0).transform.position,
                                           transform.GetChild(1).transform.position,
                                           transform.GetChild(2).transform.position };

        defaultScaleOfChild= new Vector3[] {transform.GetChild(0).transform.localScale,
                                           transform.GetChild(1).transform.localScale,
                                           transform.GetChild(2).transform.localScale };

        defaultRotateOfChild= new Quaternion[] {transform.GetChild(0).transform.rotation,
                                                transform.GetChild(1).transform.rotation,
                                                transform.GetChild(2).transform.rotation };

       // Debug.Log(transform.GetChild(1).transform.position);
        _childGum.GetComponent<CircleCollider2D>().enabled = false;
        defaultPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void GoToSlot() {
        transform.position = defaultPos;
        transform.rotation = Quaternion.identity;
        DefaultState();
       

    }

    void DefaultState() {
        for (int i=0;i<defaultPosOfChild.Length;++i) {

           // Debug.Log(defaultPosOfChild[i]);
            transform.GetChild(i).transform.position = defaultPosOfChild[i];
            transform.GetChild(i).transform.localScale = defaultScaleOfChild[i];
            transform.GetChild(i).transform.rotation = defaultRotateOfChild[i];
           // transform.GetChild(i).transform.localScale = defaultPosOfChild[i].localScale;
            //transform.GetChild(i).transform.rotation = defaultPosOfChild[i].rotation;
        }

    }

    void OnMouseDrag() {
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10f);
    }
    void OnMouseUp() {
        isMouseUp = true;
        isMouseUp2 = true;

        //_childGum.GetComponent<CircleCollider2D>().enabled = true;
    }

    void OnMouseDown() {
        isMouseUp = false;
    }

    void OnTriggerStay2D(Collider2D other) {

        //Debug.Log("safasdfd");
        if (other.gameObject.tag == "Gear" || other.gameObject.tag =="BusyGear") {
            //Debug.Log ((gameObject.transform.position-other.transform.position).sqrMagnitude>1.1f);

          //  if ((gameObject.transform.position - other.transform.position).sqrMagnitude < 1.5f) {
                //OnMouseDrag();

                Vector2 twoDPos = other.transform.position;
                gameObject.transform.position = twoDPos;
            if (isMouseUp == true) {
                other.gameObject.GetComponent<GearAction>().GumCount++;
                other.gameObject.tag = "BusyGear";
                _childGum.GetComponent<CircleCollider2D>().enabled = true;
                isMouseUp = false;
            }
     

        }
        else {
          
            // Debug.Log(other.gameObject.tag);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (isMouseUp2==true) {
            other.gameObject.GetComponent<GearAction>().GumCount--;
            Debug.Log("Exit");
            isMouseUp2 = false;
        }
        
        _childGum.GetComponent<CircleCollider2D>().enabled = false;
    }

}
