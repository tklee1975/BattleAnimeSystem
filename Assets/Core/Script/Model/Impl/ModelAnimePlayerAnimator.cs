using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public class ModelAnimePlayerAnimator : IModelAnimePlayer {
        protected GameObject mGameObject = null;
        protected Animator mAnimator = null;
        protected Transform mTransform;
        protected AnimeEvent mAnimeEvent;
        
        protected AnimeCallback mHitCallback;
        protected AnimeCallback mEndCallback;


        public ModelAnimePlayerAnimator(GameObject gameObject) {
            mGameObject = gameObject;

            if(mGameObject == null) {
                return;
            }

            mAnimator = mGameObject.GetComponentInChildren<Animator>();
            mTransform = mGameObject.transform;

            SetupAnimeEvent();
            SetupMoveAction();
        }

        void SetupAnimeEvent() {
             mAnimeEvent = mGameObject.GetComponentInChildren<AnimeEvent>();
            if(mAnimeEvent != null) {
                mAnimeEvent.EndCallback = OnAnimeEnd;
                mAnimeEvent.Callback = OnAnimeEvent;
            }

        }

        #region Animation Event handling 
        protected void OnAnimeEnd() {
            if(mEndCallback != null) {
                mEndCallback();
            }
        }

         protected void OnAnimeEvent(string eventName) {
            eventName = eventName.ToLower();

            if("hit" == eventName) {
                //Debug.Log("Hit Event Received");
                if(mHitCallback != null) {
                    mHitCallback();
                }    
            }
        } 

        #endregion

        #region Key Battle Animation 

        public override void ShowAttack(short attackMode, AnimeCallback endCallback, AnimeCallback hitCallback) {
            string stateName;
			if(attackMode == 1) {
				stateName = "skill";
			} else {
				stateName = "attack";
			}

            // Setup callback
            mEndCallback = endCallback;
            mHitCallback = hitCallback;

            // 
			mAnimator.SetTrigger(stateName);
        }
        public override void ShowIdle() {
            mAnimator.SetTrigger("idle");
        }
        public override void ShowHit(AnimeCallback endCallback) {
            mEndCallback = endCallback;
            mAnimator.SetTrigger("hit");
        }
        public void ShowMoveForward() {
            mAnimator.SetTrigger("forward");
        }
        public void ShowMoveBackward() {
            mAnimator.SetTrigger("backward");
        }

        public override void ShowDie(AnimeCallback endCallback) {
            mEndCallback = endCallback;
            mAnimator.SetTrigger("die");
        }
        public override void Resurrect(bool animated, AnimeCallback endCallback)  // Reborn, exit from Die state
        {
            // ken: no handling for animated resurrect yet
            mAnimator.SetTrigger("reset");
            if(endCallback != null) {
                endCallback();
            }
        }


        #endregion


        #region Update 


        public override void Update(float deltaTime) {


            if(mStartMove) {
				UpdateMove(deltaTime);
			} 
        }


        #endregion

        #region Movement Logic

          // #region Movement Logic
        protected MoveAction mMoveAction;
		protected bool mStartMove = false;
        protected AnimeCallback mMoveCallback;

        public override void Move(Model.MoveType moveType, 
                    Vector3 from, Vector3 to, float duration, AnimeCallback callback) {
            if(moveType == Model.MoveType.Forward) {
                ShowMoveForward();
            } else {
                ShowMoveBackward();
            }
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
				Debug.Log("UpdateMove: " + mMoveAction.name + " done!");
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