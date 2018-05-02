using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
	public class HitAction : AnimeAction {
		public BattleModel actor;

		
		protected override void OnStart() {
			if(name == "") {
				name = "BattleHit";
			}
			
			SetEndByCondition();		// When for 
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