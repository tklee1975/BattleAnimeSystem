using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class ModelAttackAction : ModelAction {
		public enum PositionType {
			UsePosition,
			UseModel,
		};

		public AnimeAction onHitAction = null;
		public short style = 0;
		public PositionType postionType = PositionType.UsePosition;
		public bool isMoving = true;
		public Vector2 targetPostion = new Vector2(0, 0);
		public Model targetModel = null;
		
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

		Vector2 GetMoveTargetPosition() {
			if(postionType == PositionType.UseModel) {
				return targetModel == null ? Vector2.zero 
						: actor.GetCloseAttackPosition(targetModel.GetPosition(), style);
			} else {
				return targetPostion;
			}
		}

		protected void StartMovingAttack() {

			Vector2 endPos = GetMoveTargetPosition();

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
				actor.Attack(style, ()=> {
					Debug.Log("Start Move Bak");
					actor.MoveBack(endCallback);
				}, hitCallback);
			});
		}

		protected void StartStandingAttack() {
			actor.Attack(style, MarkAsDone, OnAttackHit);
		}

		protected void OnAttackHit() {
			// Debug.Log("AttackAction: OnAttackHit");
			if(onHitAction != null) {
				AddSubAction(onHitAction);
			}
		}
	}

}