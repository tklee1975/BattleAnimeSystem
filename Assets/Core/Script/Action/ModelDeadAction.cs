using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class ModelDieAction : ModelAction {


		public static ModelDieAction Create(Model actor) {
			ModelDieAction action = new ModelDieAction();
			action.actor = actor;
			return action;
		}

		protected override void OnStart() {
			if(name == "") {
				name = "BattleDead";
			}
			
			SetEndByCondition();		// End by Calling 'MarkAsDone'
			if(actor == null) {
				MarkAsDone();
				return;
			}

			actor.Die(() => {
				MarkAsDone();
			});
		}
	}
}