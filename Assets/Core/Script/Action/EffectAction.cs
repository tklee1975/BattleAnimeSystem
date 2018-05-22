using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class EffectAction : AnimeAction {
		public enum Type {
			Point, 				// Spawn at particular point
			Projectile, 		// Flying object, e.g missile, bolt 
		};


		public GameObject effectPrefab = null;
		public Transform parentTransform = null;        
		public AnimeAction onHitAction = null;
		
		
		public Type effectType = Type.Point;

		// Properties for All Type 
		public Vector3 spawnPostion = new Vector3(0, 0, -5);	// start position for Projectile
		
		// Properties for Point Type 
		public int repeat = 0;

		// Properties for Projectile Type 
		public Vector3 endPosition = new Vector3(0, 0, 0);
		public float moveDuration;
		
		protected Effect mEffect = null;

		// Static Method 
		public static EffectAction CreatePointEffect(GameObject prefab, Vector3 position, Transform parent=null, int _repeat=0)
		{	
			EffectAction action = new EffectAction();

			action.effectType = Type.Point;
			action.effectPrefab = prefab;
			action.spawnPostion = position ;
			action.spawnPostion.z = -5;
			action.parentTransform = parent;
			action.repeat = _repeat;

			return action;
		}

		public static EffectAction CreateProjectileEffect(GameObject prefab, Vector3 from, Vector3 to, 
														float _duration, 
														Transform parent=null)
		{	
			EffectAction action = new EffectAction();

			action.effectType = Type.Projectile;
			action.effectPrefab = prefab;
			action.spawnPostion = from;
			action.endPosition = to;
			action.parentTransform = parent;
			action.moveDuration = _duration;

			return action;
		}

		// 
		protected override void OnStart() {
			if(name == "") {
				name = "EffectAction";
			}
			
			SetEndByCondition();		// When for 
			SpawnEffectObject();

			if(mEffect == null) {
				MarkAsDone();
				return;
			}

			if(Type.Projectile == effectType) {
				mEffect.Move(spawnPostion, endPosition, moveDuration, OnEndEvent);
			} else {	// default is Point Type 
				mEffect.PlayRepeat(repeat, OnEndEvent, OnHitEvent);
			}
		}

		void OnHitEvent() {
			if(onHitAction != null) {
				AddSubAction(onHitAction);
			}
		}

		void OnEndEvent() {
			GameObject.Destroy(mEffect.gameObject);
			MarkAsDone();
			
		}

		protected void SpawnEffectObject() {
			if(effectPrefab == null) {
				return;
			}

			GameObject obj = GameObject.Instantiate(effectPrefab);

			if(parentTransform != null) {
				obj.transform.SetParent(parentTransform);
			}

			obj.transform.position = spawnPostion;
		
			mEffect = obj.GetComponent<Effect>();
		}
	}

}