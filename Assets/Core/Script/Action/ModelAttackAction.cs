using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class ModelAttackAction : ModelAction {
		public AnimeAction onHitAction = null;
		public short style = 0;

		public bool isMoving = true;
		public Vector2 targetPostion = new Vector2(0, 0);
		
		protected override void OnStart() {
			if(name == "") {
				name = "BattleAttack";
			}
			
			SetEndByCondition();		// When for 

			if(isMoving) {
				StartMovingAttack();
			} else {
				StartStandingAttack();
			}
		}

		protected void StartMovingAttack() {

			Vector2 endPos = targetPostion;

			AnimeCallback hitCallback = () => {
				OnAttackHit();
			};


			AnimeCallback endCallback = () => {
				Debug.Log(" Move Bak done");
				MarkAsDone();
			};

			Debug.Log("Start Moving Fwd");
			actor.MoveForward(endPos, () => {
				Debug.Log("Start Attack");
				actor.Attack(style, hitCallback, ()=> {
					Debug.Log("Start Move Bak");
					actor.MoveBack(endCallback);
				});
			});
		}

		protected void StartStandingAttack() {
			actor.Attack(style, OnAttackHit, MarkAsDone);
		}

		protected void OnAttackHit() {
			// Debug.Log("AttackAction: OnAttackHit");
			if(onHitAction != null) {
				AddSubAction(onHitAction);
			}
		}
	}

}