using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleHit : MonoBehaviour {
	public GameObject Dot;

	private float HitCount;
	private float SqrtofHit=0;
	private ParticleSystem particle;
	// Use this for initialization
	void Start () {
		particle = GetComponent<ParticleSystem> ();
		 HitCount = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnParticleCollision(GameObject game)
	{
		if (game.tag == "Dot2") {
			
			HitCount += Time.deltaTime;
			if (HitCount > 1 * SqrtofHit) {
				if (1 * SqrtofHit > 4) {
					Destroy (game);
					particle.Stop();
				}
				SqrtofHit++;

			}
				
		}
	}
}
