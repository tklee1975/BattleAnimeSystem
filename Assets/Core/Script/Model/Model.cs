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

		public enum MoveType {
			Forward,
			Backward,
		};


        const float kFrontZPosition = -5;    // the z position to make the character bring to front

		protected AnimePlayerFactory mAnimePlayerFactory = null;
        protected IModelAnimePlayer mAnimePlayer;

        public float moveDuration = 0.3f;
		public Dir faceDir = Dir.Left; 

		protected Vector3 mOriginPosition;
		protected Vector2 mTargetPosition;

		protected int mDebugCounter;		// For debugging
		
        // Required Implementation
        //public abstract BoundsInt GetAttackBound(); // For 


        // Unity Core: Awake, Start, Update, 
		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake()
		{
			
            // Position 
			mOriginPosition = transform.position;

			// for Debugging
			mDebugCounter = 0;

            // 
            SetupAnimePlayer();       // 
		}

		protected abstract AnimePlayerFactory GetAnimePlayerFactory();

		void SetupAnimePlayer() {
			mAnimePlayerFactory = GetAnimePlayerFactory();

			if(mAnimePlayerFactory == null) {
				mAnimePlayer = null;
				Debug.Log("Model [" + gameObject.name + "] missing animePlayerFactory");
				return;
			}
			mAnimePlayer = mAnimePlayerFactory.CreateModelPlayer(gameObject);
		}
        


		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			if(mAnimePlayer == null) {
				return;
			}
			mAnimePlayer.Update(Time.deltaTime);
		}

		void OnDrawGizmos() {
			//Debug.Log("OnDrawGizmos!");
			Gizmos.color = new Color(1, 0, 0, 0.5F);
        	Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 1));

			// Center Position 
			Gizmos.color = new Color(0, 1, 0, 0.5F);
        	Gizmos.DrawCube(GetCenterPosition(), new Vector3(0.5f, 0.5f, 1));

			// Center Position 
			for(int i=0; i<2; i++) {
				Gizmos.color = new Color(1, 0, 0, 0.5F);
				//Gizmos.DrawCube(GetLaunchPosition(), new Vector3(0.5f, 0.5f, 1));
				//Gizmos.DrawIcon(GetLaunchPosition(), "icon_bow.png", true);
				Gizmos.DrawSphere(GetLaunchPosition(i), 0.2f);
			}

		}

        #region Position Information 
		public Vector2 GetPosition() {
			//Debug.Log("Mode.position=" + transform.position + " / " + transform.localPosition);
			return transform.position;
		}

        public Vector2 GetOriginPosition() {
            return mOriginPosition;
        }

	 	public virtual Vector2 GetCenterPosition() {    // the 
		 	return (transform.position + new Vector3(0, 1, 0));
        }

		public virtual Vector2 GetPositionByType(PositionType type) {
			if(type == PositionType.Ground) {
				return GetPosition();
			} else {
				return GetCenterPosition();
			}
		}


        public virtual Vector2 GetCloseAttackPosition(Vector3 targetPos, int attackStyle=0) {    // the 
            Vector2 launchPos = GetLaunchPosition(attackStyle);
			float attackRange = Mathf.Abs(transform.position.x - launchPos.x);
			
			Vector2 attackPos = targetPos;


            attackPos.x += attackRange * GetSideFactor();

            return attackPos;
        }

		public virtual Vector2 GetLaunchPosition(int index) {    // the 
            Vector2 pos = GetCenterPosition();
            pos.x -= 0.5f * GetSideFactor();
			//pos.y 

            return pos;
        }

        public virtual Vector2 GetLaunchPosition() {    // the 
           	return GetLaunchPosition(0);
        }

        public virtual Vector2 GetAttackPosition() {    // the 
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

		

		public virtual void Hit(AnimeCallback endCallback = null)
		{
			mAnimePlayer.ShowHit(endCallback);
		}

		public virtual void Attack(short style,  AnimeCallback endCallback = null, AnimeCallback hitCallback = null)
		{
			mAnimePlayer.ShowAttack(style, endCallback, hitCallback);
		}

		public virtual void Move(Vector2 targetPos) {
			//ShowMoveAnime();
			MoveForward(targetPos, () => {
				ShowIdleAnime();
			});
		}

		public virtual void MoveForward(Vector2 targetPos, AnimeCallback callback = null) {
			Vector3 endPos = VectorUtil.CombineVectorWithZ(targetPos, kFrontZPosition);
			mOriginPosition = transform.position;

			// 
			mAnimePlayer.Move(MoveType.Forward, transform.position, endPos, moveDuration,
						() => {
							Debug.Log("MoveForward Finished!");
							//ShowIdleAnime();
							if(callback != null) {
								callback();
							}
					});
		}

		public virtual void MoveBack(AnimeCallback callback = null) {
			mAnimePlayer.Move(MoveType.Backward, transform.position, mOriginPosition, moveDuration,
						() => {
							Debug.Log("MoveBack Finished!");
							transform.position = mOriginPosition;
							ShowIdleAnime();
							if(callback != null) {
								callback();
						}
					});
		}

		public virtual void Die(AnimeCallback endCallback) {
			mAnimePlayer.ShowDie(endCallback);
		}

		public virtual void Resurrect(bool animated, AnimeCallback endCallback) {
			mAnimePlayer.Resurrect(animated, endCallback);
		}


        #endregion
		
		

		
        // #region Movement Logic
		void Move(Vector3 from, Vector3 to, float duration, AnimeCallback callback)
		{
			//mAnimePlayer.Move(from, to, duration, callback);
		}
      
        public void MoveTo(Vector3 targetPos, float duration, AnimeCallback callback) {
			
		
			
		}


        // #endregion

		public void ShowIdleAnime() {
			mAnimePlayer.ShowIdle();
		}

    }
}