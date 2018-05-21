using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public class EffectAnimePlayerParticleSystem : IEffectAnimePlayer {
        protected GameObject mGameObject = null;        // the main Particle
        protected Transform mTransform;

        protected int mRepeatCount = 0;

        protected AnimeCallback mHitCallback;
        protected AnimeCallback mEndCallback;
        
        protected EffectParticle mEffectParticle;
        protected float mParticleDuration = 0;

        protected bool mParticlePlaying = false;
        protected float mParticleTimeElapse = 0;

        public EffectAnimePlayerParticleSystem(GameObject gameObject) {
            mGameObject = gameObject;

            if(mGameObject == null) {
                return;
            }

            mEffectParticle = gameObject.GetComponent<EffectParticle>();
            if(mEffectParticle != null) {
                mEffectParticle.HitCallback = OnHit;
                mEffectParticle.EndCallback = OnEnd;
            }
            //mParticleSystem.d
         
            SetupMoveAction();
        }
        protected void OnHit() {
            if(mHitCallback != null) {
                mHitCallback();
            }
        }
        protected void OnEnd() {
            // Debug.Log("End Event Received");
            mRepeatCount--;
            if(mRepeatCount >= 1) {
                PlayEffect();
                return;
            }
          
            if(mEndCallback != null) {
                mEndCallback();
            }
        }

        protected void PlayEffect() {
            if(mEffectParticle != null) {
                mEffectParticle.Play();
            }
        }

        protected void StopEffect() {
            if(mEffectParticle != null) {
                mEffectParticle.Stop();
            }
        }

        protected void OnAnimeEvent(string eventName) {
           // No Event
        } 

        //         public abstract void PlayOnce(Model.Callback endCallback, Model.Callback hitCallback);
        // public abstract void PlayRepeat(int repeatCount, Model.Callback endCallback, Model.Callback hitCallback);


        public override void PlayOnce(AnimeCallback endCallback, AnimeCallback hitCallback)
        {
            mHitCallback = hitCallback;
            mEndCallback = endCallback;
            mRepeatCount = 0;
            PlayEffect();
        }

        public override void PlayRepeat(int repeatCount, AnimeCallback endCallback, AnimeCallback hitCallback)
        {
             // Error Handling 
            if(mEffectParticle == null) {
                Debug.Log("Debug: PlayRepeat: mEffectParticle is null");
                if(endCallback != null) {
                    endCallback();
                }
                return;
            }

            mHitCallback = hitCallback;
            mEndCallback = endCallback;
            mRepeatCount = repeatCount;
            PlayEffect();
        }

        public override void PlayForever()
        {
            //mParticleSystem 
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