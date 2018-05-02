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

        protected IModelAnimeImpl mAnimeImpl;




        public float moveDuration = 0.3f;
		public Dir faceDir = Dir.Left; 

		protected AnimeEvent mAnimeEvent;

		public delegate void Callback();

		protected Callback mHitCallback;
		protected Callback mEndCallback;

		protected Callback mMoveCallback;
		

		protected Vector2 mOriginPosition;
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
			mOriginPosition = (Vector2) transform.position;

            SetupMoveAction();

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
            if(mStartMove) {
			    UpdateMove();	
            }
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



		public void Move(Vector2 targetPos) {
			ShowMoveAnime();
			MoveTo(targetPos, moveDuration, () => {
				ShowIdleAnime();
			});
		}

		public void Hit(Callback endCallback = null)
		{
			mEndCallback = () => {
				if(endCallback != null) {
					endCallback();
				}
				mEndCallback = null;
			};

			ShowHitAnime();
		}

		public void Attack(short style, Callback hitCallback = null, Callback endCallback = null)
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

		public void MoveForward(Vector2 targetPos, Callback callback = null) {
			mTargetPosition = targetPos;
			mOriginPosition = transform.position;
			ShowForwardAnime();
			MoveTo(mTargetPosition, moveDuration, callback);
		}

		public void MoveBack(Callback callback = null) {
			ShowBackwardAnime();
			MoveTo(mOriginPosition, moveDuration, () => {
				Debug.Log("MoveBack Finished!");
				ShowIdleAnime();
				if(callback != null) {
					callback();
				}
			});
		}

        #endregion
		
		

		
        // #region Movement Logic
        protected MoveAction mMoveAction;
		protected bool mStartMove = false;

        public void MoveTo(Vector2 targetPos, float duration, Callback callback) {
			mMoveAction.Reset();
			mMoveAction.name = "moveTo:" + targetPos;
			mMoveAction.startPosition = transform.position;

			Vector3 endPos = transform.position;
			endPos.x = targetPos.x; endPos.y = targetPos.y;
			mMoveAction.endPosition = endPos;

			mMoveAction.SetDuration(duration);

			mMoveCallback = callback;
			mMoveAction.Start();
			mStartMove = true;
		}

        void UpdateMove() {
			if(mStartMove == false) {
				return;
			} 
			mMoveAction.Update(Time.deltaTime);
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
			mMoveAction.targetObject = gameObject;
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