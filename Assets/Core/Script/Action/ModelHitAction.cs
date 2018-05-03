using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class ModelHitAction : ModelAction {
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