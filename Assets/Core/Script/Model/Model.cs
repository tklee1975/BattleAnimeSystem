using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BattleAnimeSystem { 
    // Generic Model 

    /**
     *
     * 
     */
	public abstract class Model : MonoBehaviour {
        public enum Dir {
			Left,
			Right,
		};

        const float kFrontZPosition = -5;    // the z position to make the character bring to front

        protected IModelAnimeImpl mAnimeImpl;

        public float moveDuration = 0.3f;
		public Dir faceDir = Dir.Left; 

		protected AnimeEvent mAnimeEvent;

		public delegate void Callback();

		// 
		protected Callback mHitCallback;
		protected Callback mEndCallback;
		protected Callback mMoveCallback;
		

		protected Vector3 mOriginPosition;
		protected Vector2 mTargetPosition;

		protected int mDebugCounter;		// For debugging
		
        // Required Implementation
        public abstract void SetupAnimeImpl();      // For Animation Logic
        //public abstract BoundsInt GetAttackBound(); // For 


        // Unity Core: Awake, Start, Update, 
		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake()
		{
			mAnimeEvent = GetComponentInChildren<AnimeEvent>();
			// Animation Event Callback 
			if(mAnimeEvent != null) {
				mAnimeEvent.EndCallback = OnAnimeEnd;        // called by end_clip
				mAnimeEvent.Callback = OnAnimeEvent;         // 
			}

            // Position 
			mOriginPosition = transform.position;

			// for Debugging
			mDebugCounter = 0;

            // 
            SetupAnimeImpl();       // 
		}



		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			mAnimeImpl.Update(Time.deltaTime);
		}


        #region Animation Events 

		protected virtual void OnAnimeEvent(string evt) {
			Debug.Log("OnAnimeEvent_" + (mDebugCounter++) + ": " + evt);
			if(evt.ToLower() == "hit") {
				if(mHitCallback != null) {
					mHitCallback();
				}
			}
		}

		protected virtual void OnAnimeEnd() {
			if(mEndCallback != null) {
				mEndCallback();
			}		
		}

        #endregion

		//protected voi

        #region Position Information 
        public Vector2 GetOriginPosition() {
            return mOriginPosition;
        }

        public Vector2 GetCloseAttackPosition(Vector3 targetPos) {    // the 
            Vector2 attackPos = targetPos;
            attackPos.x = 2 * GetSideFactor();

            return attackPos;
        }

         public Vector2 GetLaunchPosition() {    // the 
            Vector2 pos = mOriginPosition;
            pos.x -= 2 * GetSideFactor();

            return pos;
        }


        public Vector2 GetAttackPosition() {    // the 
            Vector2 attackPos = mOriginPosition;
            attackPos.x -= 1 * GetSideFactor();

            return attackPos;
        }


        public int GetSideFactor() {  
            int factor = transform.localScale.x < 0 ? -1 : 1;

            if(faceDir == Dir.Right) {
				factor = -factor;
			}

            return factor;
        }


        #endregion


		public Vector3 GetHitPosition() {
			float offsetX = -1;
			if(faceDir == Dir.Right) {
				offsetX = -offsetX;
			}
			if(transform.localScale.x < 0) { offsetX *= -1; }
			return transform.position + new Vector3(offsetX, 0, 0);
		}


        #region Model Action Logic 

		public virtual void Move(Vector2 targetPos) {
			ShowMoveAnime();
			MoveTo(targetPos, moveDuration, () => {
				ShowIdleAnime();
			});
		}

		public virtual void Hit(Callback endCallback = null)
		{
			mEndCallback = () => {
				if(endCallback != null) {
					endCallback();
				}
				mEndCallback = null;
			};

			ShowHitAnime();
		}

		public virtual void Attack(short style, Callback hitCallback = null, Callback endCallback = null)
		{
			mHitCallback = hitCallback;
			mEndCallback = () => {
				Debug.Log("Attack:EndCallback");
				if(endCallback != null) {
					endCallback();
				}
				mEndCallback = null;
				mHitCallback = null;
			};

			ShowAttackAnime(style);
		}

		public virtual void MoveForward(Vector2 targetPos, Callback callback = null) {
			Vector3 endPos = VectorUtil.CombineVectorWithZ(targetPos, kFrontZPosition);
			mOriginPosition = transform.position;

			// 
			ShowForwardAnime();

			Move(transform.position, endPos, moveDuration, callback);
		}

		public virtual void MoveBack(Callback callback = null) {
			ShowBackwardAnime();

			Move(transform.position, mOriginPosition, moveDuration, () => {
						Debug.Log("MoveBack Finished!");
						ShowIdleAnime();
						if(callback != null) {
							callback();
						}
					}
			);
		}

        #endregion
		
		

		
        // #region Movement Logic
		void Move(Vector3 from, Vector3 to, float duration, Model.Callback callback)
		{
			mAnimeImpl.Move(from, to, duration, callback);
		}
      
        public void MoveTo(Vector3 targetPos, float duration, Callback callback) {
			
		
			
		}


        // #endregion

        #region Animation Logic
    	public void ShowAttackAnime(short style) {
			mAnimeImpl.ShowAttack(style);
		}

        public void ShowMoveAnime() {
			ShowForwardAnime();
		}

		public void ShowForwardAnime() {
			mAnimeImpl.ShowMoveForward();
		}

		public void ShowBackwardAnime() {
			mAnimeImpl.ShowMoveBackward();
		}

		public void ShowIdleAnime() {
			mAnimeImpl.ShowIdle();
		}

        public void ShowHitAnime() {
			mAnimeImpl.ShowHit();
		}

        #endregion


    }
}