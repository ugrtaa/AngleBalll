using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour {
	private Animator anim;
	private float HitCount;
	private float SqrtofHit=0;
	private bool completeDieAnim=false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		HitCount += Time.deltaTime;
		if (HitCount > 1 * SqrtofHit) {
			if (1 * SqrtofHit > 5 && completeDieAnim!=true) {
				anim.SetBool ("TimeHasCome", true);
				completeDieAnim=true;
			}
			SqrtofHit++;

		}
	}
	void DieAnimStop()
	{
		anim.SetBool ("TimeHasCome", false);
		Destroy (gameObject);
	}
}
