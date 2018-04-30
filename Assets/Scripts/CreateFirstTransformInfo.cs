using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFirstTransformInfo : MonoBehaviour {
	public GameObject Line;
	public GameObject LineLaser;
	public static List<Vector3> pointList= new List<Vector3>();
	public static float rot;


	private float maxLineLenght;
	private bool isItFirstDot=true; 
	private Vector3 secondDotpos;
	private bool isMousePressed=false;
	private GameObject Dot1,Dot2;
	private Vector3 DotPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (rot);
		if (Input.GetMouseButtonDown (0)) {
			var Mousepos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Mousepos.z = 0;
			pointList.Add (Mousepos);
			if (pointList.Count > 1) {
				maxLineLenght = Mathf.Sqrt (Mathf.Pow ((pointList [0].x - pointList [1].x), 2) + Mathf.Pow ((pointList [0].y - pointList [1].y), 2));
				isMousePressed = true;
			}
        }
		if (isMousePressed == true) {
			if (maxLineLenght <= 2) {
				secondDotpos = pointList [1];
			}
			if (maxLineLenght > 2) {
				var mangle = Mathf.Atan2 ((pointList [1].y - pointList [0].y), (pointList [1].x - pointList [0].x));
				secondDotpos = new Vector3 (pointList [0].x + (2 * Mathf.Cos (mangle)), (pointList [0].y + (2 * Mathf.Sin (mangle))), 0);
			}
			if (isItFirstDot == true) {
				Dot1=Instantiate (Line, pointList [0],Quaternion.identity) as GameObject;
				DotPosition = Dot1.transform.position;
				DotPosition.z = 1;

				Debug.Log (DotPosition);
				isItFirstDot = false;
			}
			if (isItFirstDot == false && pointList.Count > 1) {
				Dot2=Instantiate (Line, secondDotpos, Quaternion.identity) as GameObject;
				isItFirstDot = true;
				isMousePressed = false;
			}

		}
		DegreeCalculate ();
	}
	void DegreeCalculate()
	{
		var difference = new Vector2 (pointList [0].x, pointList [0].y) - new Vector2 (pointList [1].x, pointList [1].y);
		if (difference.y < Vector2.left.y) {
			AngleArrange (difference,1);
			pointList.Clear ();
		} else {
			AngleArrange (difference, -1);
			pointList.Clear ();
		}
	}
	void AngleArrange(Vector2 WhereTo,float InOrOut)
	{
		rot = Vector2.Angle (Vector2.left,WhereTo)*InOrOut;
		var rot1 = Dot1.transform.rotation;
		rot1.eulerAngles = new Vector3 (rot, -90, 90);
		Dot1.transform.rotation = rot1;
		var rot2 = Dot2.transform.rotation;
		rot2.eulerAngles = new Vector3 (rot, -90, 90);
		Dot2.transform.rotation = rot2;
		var LineLaserTransform=Instantiate (LineLaser, DotPosition, Quaternion.identity) as GameObject;
		var LineLaserRotation = LineLaserTransform.transform.rotation;
		LineLaserRotation.eulerAngles = new Vector3 (0, 0, rot);
		LineLaserTransform.transform.rotation = LineLaserRotation;
	}

}