using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public class ModelAnimeImplAnimator : IModelAnimeImpl {
        protected GameObject mGameObject = null;
        protected Animator mAnimator = null;
        protected Transform mTransform;


        public ModelAnimeImplAnimator(GameObject gameObject) {
            mGameObject = gameObject;

            if(mGameObject == null) {
                return;
            }

            mAnimator = mGameObject.GetComponentInChildren<Animator>();
            mTransform = mGameObject.transform;

            SetupMoveAction();
        }

        #region Key Battle Animation 

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

        #endregion


        #region Update 


        public void Update(float deltaTime) {


            if(mStartMove) {
				UpdateMove(deltaTime);
			} 
        }


        #endregion

        #region Movement Logic

          // #region Movement Logic
        protected MoveAction mMoveAction;
		protected bool mStartMove = false;
        protected Model.Callback mMoveCallback;

        public void Move(Vector3 from, Vector3 to, float duration, Model.Callback callback) {

            // 
			mMoveAction.Reset();
			mMoveAction.name = "move: " + from + "_" + to;
			mMoveAction.startPosition = from;
			mMoveAction.endPosition = to;



			mMoveAction.SetDuration(duration);

			mMoveCallback = callback;
			mMoveAction.Start();
			mStartMove = true;
		}

        void UpdateMove(float deltaTime) {
			if(mStartMove == false) {
				return;
			} 
			mMoveAction.Update(deltaTime);
			if(mMoveAction.IsDone()) {
				//Debug.Log("UpdateMove: " + mMoveAction.name + " done!");
				mStartMove = false;
				if(mMoveCallback != null) {
					mMoveCallback();
				}				
			}
		}


        protected void SetupMoveAction() {
            // For Moving 
			mMoveAction = new MoveAction();
			mMoveAction.name = "BattleModel.move";
			mMoveAction.autoFlip = false;
			mMoveAction.targetObject = mGameObject;
        }

        #endregion

        // #endregion
    }
}