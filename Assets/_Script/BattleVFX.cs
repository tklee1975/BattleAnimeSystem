using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleAnimeSystem;

public class BattleVFX : MonoBehaviour {

	
	private Animator mAnimator = null;
	private AnimeEvent mAnimeEvent = null;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{	
		mAnimator = GetComponentInChildren<Animator>();
		mAnimeEvent = transform.Find("Body").GetComponent<AnimeEvent>();
	}

	// Use this for initialization
	void Start () {
		//transform.Find("Body").GetComponent<SpriteRenderer>().sprite = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetAnimeEndCallback(AnimeEvent.AnimeEndCallback callback) {
		if(mAnimeEvent != null) {
			mAnimeEvent.EndCallback = callback;
		}
	}
	
	public void SetAnimeEventCallback(AnimeEvent.EventCallback callback) {
		if(mAnimeEvent != null) {
			mAnimeEvent.Callback = callback;
		}
	}

	public void Play(string vfxName) {
		if(mAnimator == null) {
			Debug.Log("BattleVFX: mAnimator is null");
			return;
		}

		mAnimator.SetTrigger(vfxName);
	}

}
