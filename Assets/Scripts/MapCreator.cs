using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {
	public GameObject grassTile;
	// Use this for initialization
	void Start () {
		for (float x = -9; x <= 9.85f; x = x + 0.85f) {
			for (float y = -5; y < 5.4f; y = y + 0.85f) {
				var ground=Instantiate (grassTile, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				ground.transform.parent = this.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
