using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
	public class BattleModel : Model {
		public override void SetupAnimeImpl() {
			// Choose Different Implement you want 
			mAnimeImpl = new ModelAnimeImplAnimator(gameObject);
			// mAnimeImpl = new ModelAnimeImplAnimatorDOT(gameObject);	// Animator + DOTTween
			// mAnimeImpl = new ModelAnimeImplSpine(gameObject);		// Spine 
		}
	}
}