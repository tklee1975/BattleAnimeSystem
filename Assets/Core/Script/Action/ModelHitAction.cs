using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class ModelHitAction : ModelAction {
		public static ModelHitAction Create(Model actor) {
			ModelHitAction action = new ModelHitAction();
			action.actor = actor;
			return action;
		}

		protected override void OnStart() {
			if(name == "") {
				name = "BattleHit";
			}
			
			SetEndByCondition();		// End by Calling 'MarkAsDone'
			if(actor == null) {
				MarkAsDone();
				return;
			}

			actor.Hit(() => {
				MarkAsDone();
			});
		}
	}
}