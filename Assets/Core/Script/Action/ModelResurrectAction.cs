using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class ModelResurrectAction : ModelAction {


		public static ModelResurrectAction Create(Model actor) {
			ModelResurrectAction action = new ModelResurrectAction();
			action.actor = actor;
			return action;
		}

		protected override void OnStart() {
			if(name == "") {
				name = "BattleResurrect";
			}
			
			SetEndByCondition();		// End by Calling 'MarkAsDone'
			if(actor == null) {
				MarkAsDone();
				return;
			}

			actor.Resurrect(false, () => {
				MarkAsDone();
			});
		}
	}
}