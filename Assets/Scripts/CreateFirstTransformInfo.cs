using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFirstTransformInfo : MonoBehaviour {
	public GameObject Line1;
	public GameObject Line2;
	public GameObject LineLaser;
	public static List<Vector3> pointList= new List<Vector3>();
	public static float rot;
	public GameObject LineCollision;


	private float maxLineLenght;
	private bool isItFirstDot=true; 
	private Vector3 secondDotpos;
	private bool isMousePressed=false;
	private GameObject Dot1,Dot2;
	private Vector3 DotPosition;
	private GameObject LineHolder;

	// Use this for initialization
	void Start () {
		if (LineHolder == null) {
			LineHolder = new GameObject ();
			LineHolder.name="LineHolder";
		}
	}
	
	// Update is called once per frame
	void Update () {
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
				Dot1=Instantiate (Line1, pointList [0],Quaternion.identity) as GameObject;
				Dot1.transform.parent = LineHolder.transform;
				DotPosition = Dot1.transform.position;
				DotPosition.z = 1;
				isItFirstDot = false;
			}
			if (isItFirstDot == false && pointList.Count > 1) {
				Dot2=Instantiate (Line2, secondDotpos, Quaternion.identity) as GameObject;
				Dot2.transform.parent = LineHolder.transform;
				isItFirstDot = true;
				isMousePressed = false;
			}

		}
		if (isMousePressed == false && pointList.Count>1) {
			DegreeCalculate ();
		}
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
		rot1.eulerAngles = new Vector3 (0, 0, rot);
		Dot1.transform.rotation = rot1;
		var rot2 = Dot2.transform.rotation;
		rot2.eulerAngles = new Vector3 (0, 0, rot);
		Dot2.transform.rotation = rot2;
		InstantiateLaser (LineLaser);
		InstantiateLaserCollider (LineCollision);

	}
	void InstantiateLaser(GameObject Gameobje)
	{
		var LineLaserTransform=Instantiate (Gameobje, DotPosition, Quaternion.identity) as GameObject;
		LineLaserTransform.transform.parent = LineHolder.transform;
		var LineLaserRotation = LineLaserTransform.transform.rotation;
		LineLaserRotation.eulerAngles = new Vector3 (90+rot, -90, -90);
		LineLaserTransform.transform.rotation = LineLaserRotation;
		var LocalScale = LineLaserTransform.transform.localScale;
		if (maxLineLenght > 2) {
			LocalScale.x = 1;
		}
		else {
			LocalScale.x = maxLineLenght/2;
		}
		LineLaserTransform.transform.localScale = LocalScale;

	}
	void InstantiateLaserCollider(GameObject Gameobje)
	{
		var LineLaserTransform=Instantiate (Gameobje, DotPosition, Quaternion.identity) as GameObject;
		LineLaserTransform.transform.parent = LineHolder.transform;
		var LineLaserRotation = LineLaserTransform.transform.rotation;
		LineLaserRotation.eulerAngles = new Vector3 (0, 0, rot);
		LineLaserTransform.transform.rotation = LineLaserRotation;
		var LocalScale = LineLaserTransform.transform.localScale;
		if (maxLineLenght > 2) {
			LocalScale.x = 2;
		}
		else {
			LocalScale.x = maxLineLenght;
		}
		LineLaserTransform.transform.localScale = LocalScale;
	}

}