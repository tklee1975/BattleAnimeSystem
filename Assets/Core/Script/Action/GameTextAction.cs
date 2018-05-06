using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class GameTextAction : AnimeAction {
		public GameObject textPrefab = null;
		public Transform parentTransform = null;        
		
		public GameText.Style textStyle = GameText.Style.Default;
		public string text = "";

		// Properties for All Type 
		public Vector3 spawnPostion = new Vector3(0, 0, -5);	// start position for Projectile
		
		protected GameText mGameText = null;



		// Static Method 
		public static GameTextAction Create(GameObject prefab, string text,
											 Vector3 position, Transform parent=null,
												GameText.Style style= GameText.Style.Default)
		{	
			GameTextAction action = new GameTextAction();

			action.text = text;
			action.textPrefab = prefab;
			action.spawnPostion = position;
			action.parentTransform = parent;
			action.textStyle = style;

			return action;
		}


		// 
		protected override void OnStart() {
			if(name == "") {
				name = "GameTextAction";
			}
			
			SetEndByCondition();		// When for 
			SpawnObject();

			if(mGameText == null) {
				MarkAsDone();
				return;
			}

			if(textStyle != GameText.Style.Default) {
				mGameText.style = textStyle;
			}
			mGameText.text = text;
			mGameText.Play(OnEndEvent);
			Debug.Log("GameTextAction started: mGameText.style=" + mGameText.style);
			// if(Type.Projectile == effectType) {
			// 	mEffect.Move(spawnPostion, endPosition, moveDuration, OnEndEvent);
			// } else {	// default is Point Type 
			// 	mEffect.PlayRepeat(repeat, OnEndEvent, OnHitEvent);
			// }
		}

		void OnHitEvent() {			
		}

		void OnEndEvent() {
			GameObject.Destroy(mGameText.gameObject);
			MarkAsDone();
			
		}

		protected void SpawnObject() {
			if(textPrefab == null) {
				return;
			}

			GameObject obj = GameObject.Instantiate(textPrefab);

			if(parentTransform != null) {
				obj.transform.SetParent(parentTransform);
			}

			obj.transform.position = spawnPostion;
		
			mGameText = obj.GetComponent<GameText>();
		}
	}

}