using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFirstTransformInfo : MonoBehaviour {
	public GameObject Line;
	public static List<Vector3> pointList= new List<Vector3>();

	private float maxLineLenght;
	private bool isItFirstDot=true; 
	private Vector3 secondDotpos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			var Mousepos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Mousepos.z = 0;
			pointList.Add (Mousepos);
            if (pointList.Count > 1) ;
            maxLineLenght = Mathf.Sqrt(Mathf.Pow((pointList[0].x - pointList[1].x), 2) + Mathf.Pow((pointList[0].y - pointList[1].y), 2));
        }
        //Mathf.Abs (((pointList [0].x * pointList [0].x) - (pointList [pointList.Count - 1].x * pointList [pointList.Count - 1].x)) + ((pointList [0].y * pointList [0].y) - (pointList [pointList.Count - 1].y * pointList [pointList.Count - 1].y)))
        if (maxLineLenght <= 5) {
			secondDotpos = pointList [1];
		}
		if (maxLineLenght > 5) {
			//var mvalue = (pointList [1].y - pointList [0].y) / (pointList [1].x - pointList [0].x);
			var mangle = Mathf.Atan2 ((pointList[1].y - pointList[0].y), (pointList[1].x - pointList[0].x));
			secondDotpos=new Vector3(pointList[0].x+(5*Mathf.Cos(mangle)),(pointList[0].y+(5*Mathf.Sin(mangle))),0);
		}
		Debug.Log (maxLineLenght);
		if (isItFirstDot == true) {
			Instantiate (Line, pointList[0], Quaternion.identity);
			isItFirstDot = false;
		}
		if (isItFirstDot == false) {
			Instantiate (Line, secondDotpos, Quaternion.identity);
			pointList.Clear();
			isItFirstDot = true;
			pointList.Clear ();
		}
	}
}