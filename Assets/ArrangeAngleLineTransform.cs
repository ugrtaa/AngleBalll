using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeAngleLineTransform : MonoBehaviour {
	public SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = this.GetComponent<SpriteRenderer> ();
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		var angletan = (CreateFirstTransformInfo.pointList [0].y - CreateFirstTransformInfo.pointList [CreateFirstTransformInfo.pointList.Count].y) / (CreateFirstTransformInfo.pointList [0].x - CreateFirstTransformInfo.pointList [CreateFirstTransformInfo.pointList.Count].x);
		var angle = Mathf.Atan (angletan) * 100;
		var totalpos = CreateFirstTransformInfo.pointList[CreateFirstTransformInfo.pointList.Count].x - this.transform.position.x;
		var temp = sprite.size;
		var rota = transform.rotation;
		rota.eulerAngles = new Vector3 (0, 0, angle);
		transform.rotation = rota;
		temp.x = totalpos;
		sprite.size = temp;
		Debug.Log ("Uu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
