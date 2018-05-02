using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public class ModelAnimeImplAnimator : IModelAnimeImpl {
        protected Animator mAnimator = null;

        public ModelAnimeImplAnimator(Animator _animator) {
            mAnimator = _animator;
        }

        // public void ShowForwardAnime() {
		// 	mAnimator.SetTrigger("forward");
		// }

		// public void ShowBackwardAnime() {
		// 	mAnimator.SetTrigger("backward");
		// }

		// public void ShowIdleAnime() {
		// 	mAnimator.SetTrigger("idle");
		// }

        public void ShowAttack(short attackMode) {
            string stateName;
			if(attackMode == 1) {
				stateName = "skill";
			} else {
				stateName = "attack";
			}
			mAnimator.SetTrigger(stateName);
        }
        public void ShowIdle() {
            mAnimator.SetTrigger("idle");
        }
        public void ShowHit() {
            mAnimator.SetTrigger("hit");
        }
        public void ShowMoveForward() {
            mAnimator.SetTrigger("forward");
        }
        public void ShowMoveBackward() {
            mAnimator.SetTrigger("backward");
        }
    }
}