using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class EffectAction : AnimeAction {
		public GameObject effectPrefab = null;
		public Transform parentTransform = null;        
		public AnimeAction onHitAction = null;
		public short style = 0;

		public bool isMoving = true;
		public Vector3 targetPostion = new Vector3(0, 0, -5);
		public int repeat = 0;

		protected Effect mEffect = null;
		
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

			mEffect.PlayRepeat(repeat, OnEndEvent, OnHitEvent);
		}

		void OnHitEvent() {
			if(onHitAction != null) {
				AddSubAction(onHitAction);
			}
		}

		void OnEndEvent() {
			MarkAsDone();
			GameObject.Destroy(mEffect.gameObject);
		}

		protected void SpawnEffectObject() {
			if(effectPrefab == null) {
				return;
			}

			GameObject obj = GameObject.Instantiate(effectPrefab);

			if(parentTransform != null) {
				obj.transform.SetParent(parentTransform);
			}

			obj.transform.position = targetPostion;
		
			mEffect = obj.GetComponent<Effect>();
		}
	}

}