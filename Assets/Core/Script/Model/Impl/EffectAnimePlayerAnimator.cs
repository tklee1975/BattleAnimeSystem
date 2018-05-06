using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public class EffectAnimePlayerAnimator : IEffectAnimePlayer {
        protected GameObject mGameObject = null;
        protected Animator mAnimator;
        protected AnimeEvent mAnimeEvent;
        
        protected Transform mTransform;

        protected int mRepeatCount = 0;

        protected AnimeCallback mHitCallback;
        protected AnimeCallback mEndCallback;
        

        public EffectAnimePlayerAnimator(GameObject gameObject) {
            mGameObject = gameObject;

            if(mGameObject == null) {
                return;
            }

            mAnimator = mGameObject.GetComponentInChildren<Animator>();
            mTransform = mGameObject.transform;

            mAnimeEvent = mGameObject.GetComponentInChildren<AnimeEvent>();
            if(mAnimeEvent != null) {
                mAnimeEvent.EndCallback = OnAnimeEnd;
                mAnimeEvent.Callback = OnAnimeEvent;
            }

            SetupMoveAction();
        }

        protected void OnAnimeEnd() {
            Debug.Log("End Event Received");
            mRepeatCount--;
            if(mRepeatCount >= 1) {
                // Play Again
                return;
            }
            mAnimator.SetInteger("effectID", 0);

            if(mEndCallback != null) {
                mEndCallback();
            }
        }

        protected void PlayEffect() {
            Debug.Log("Try to play effect=" + mEffectID);
            mAnimator.SetInteger("effectID", mEffectID);
        }

        protected void StopEffect() {
            mAnimator.SetInteger("effectID", 0);
        }

        protected void OnAnimeEvent(string eventName) {
            eventName = eventName.ToLower();

            if("hit" == eventName) {
                Debug.Log("Hit Event Received");
                if(mHitCallback != null) {
                    mHitCallback();
                }    
            }
        } 

        //         public abstract void PlayOnce(Model.Callback endCallback, Model.Callback hitCallback);
        // public abstract void PlayRepeat(int repeatCount, Model.Callback endCallback, Model.Callback hitCallback);


        public override void PlayOnce(AnimeCallback endCallback, AnimeCallback hitCallback)
        {
            PlayRepeat(0, endCallback, hitCallback);
        }

        public override void PlayRepeat(int repeatCount, AnimeCallback endCallback, AnimeCallback hitCallback)
        {
             // Error Handling 
            if(mAnimator == null) {
                Debug.Log("Debug: PlayRepeat: animator is null");
                if(endCallback != null) {
                    endCallback();
                }
                return;
            }

            mHitCallback = hitCallback;
            mEndCallback = endCallback;
            mRepeatCount = repeatCount;
            mAnimator.SetBool("nonStop", false);
            PlayEffect();
        }

        public override void PlayForever()
        {
             // Error Handling 
            if(mAnimator == null) {               
                return;
            }

            mAnimator.SetBool("nonStop", true);
            PlayEffect();
        }
        

        public override void Update(float delta) {
            if(mStartMove) {
                UpdateMove(delta);
            }
        } 

        #region Movement Logic

          // #region Movement Logic
        protected MoveAction mMoveAction;
		protected bool mStartMove = false;
        protected AnimeCallback mMoveEndCallback;

        public override void Move(Vector3 from, Vector3 to, float duration, AnimeCallback callback) {

            // 
			mMoveAction.Setup(from, to, duration);
            PlayForever();            
			mMoveEndCallback = callback;
			mMoveAction.Start();
			mStartMove = true;
		}

        void UpdateMove(float deltaTime) {
			if(mStartMove == false) {
				return;
			} 
			mMoveAction.Update(deltaTime);
			if(mMoveAction.IsDone()) {
				mStartMove = false;
                StopEffect();
				if(mMoveEndCallback != null) {
					mMoveEndCallback();
				}				
			}
		}


        protected void SetupMoveAction() {
            // For Moving 
			mMoveAction = new MoveAction();
			mMoveAction.name = "Effect.move";
			mMoveAction.autoFlip = true;
			mMoveAction.targetObject = mGameObject;
        }

        #endregion

       
        // public void Play(string effectName, Model.Callback hitCallback, Model.Callback endCallback)
        // {
        //     if(mAnimator != null) {
        //         mAnimator.SetTrigger(effectName);
        //     }
        // }        



    }
}