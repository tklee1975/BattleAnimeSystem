using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSound : MonoBehaviour {
	private AudioSource mAudioSource;
	public AudioClip[] clipList;
	
	// Use this for initialization
	void Start () {
		mAudioSource = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayClip(int index) {
		if(clipList == null) {
			return;
		}
		if(index >= clipList.Length) {
			return;
		}

		mAudioSource.clip = clipList[index];
		mAudioSource.Play();
	}
}
