using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
	public class BattleModel : Model {
		public override void SetupAnimeImpl() {
			Animator animator = GetComponentInChildren<Animator>();

			mAnimeImpl = new ModelAnimeImplAnimator(animator);
		}
	}
}