using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Animation anim = GetComponent<Animation>();
		
		Debug.Log("AutoPlay: clip=" + anim.clip);
		//anim.Play(AnimationPlayMode.Queue);
		anim.wrapMode = WrapMode.Loop;
		//anim.Play("player_attack");
		anim.Play();
		//  foreach (AnimationState state in anim) {
        //     state.speed = 0.5F;
        // }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
