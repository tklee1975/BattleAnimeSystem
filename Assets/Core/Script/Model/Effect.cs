using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BattleAnimeSystem { 
    // Generic Model 

    /**
     *
     * 
     */
	public abstract class Effect : MonoBehaviour {
		 
		public int effectID = 1;
		
		public PositionType positionType = PositionType.BodyCenter;
		
		protected AnimePlayerFactory mAnimePlayerFactory = null;
        protected IEffectAnimePlayer mAnimePlayer;


		protected int mOnHitCount = 0;

		protected AnimeCallback mOnHitCallback = null;
		protected AnimeCallback mOnEndCallback = null;

        // Unity Core: Awake, Start, Update, 
		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake()
		{
            // 
            SetupAnimePlayer();
			mAnimePlayer.setEffectID(effectID);	       // 
			mOnHitCount = 0;
		}

		public void Reset() {
			mOnHitCount = 0;
		}

		protected abstract AnimePlayerFactory GetAnimePlayerFactory();

		void SetupAnimePlayer() {
			mAnimePlayerFactory = GetAnimePlayerFactory();

			if(mAnimePlayerFactory == null) {
				mAnimePlayer = null;
				Debug.Log("Model [" + gameObject.name + "] missing animePlayerFactory");
				return;
			}
			mAnimePlayer = mAnimePlayerFactory.CreateEffectPlayer(gameObject);
		}
        


		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			mAnimePlayer.Update(Time.deltaTime);
		}


		//protected voi

       


        #region Model Action Logic 

		

		public virtual void Move(Vector3 from, Vector3 to, float duration, AnimeCallback endCallback) {
			mAnimePlayer.Move(from, to, duration, endCallback);
		}

		void HandleEndCallback() {
			Debug.Log("HandleEndCallback: hitCount=" + mOnHitCount);
			if(mOnHitCount == 0) {	// Do OnHit callback before End 
				if(mOnHitCallback != null) {
					mOnHitCallback();
				}
			}

			if(mOnEndCallback != null) {
				mOnEndCallback();
			}
		}

		void HandleHitCallback() {
			mOnHitCount++;
			if(mOnHitCallback != null) {
				mOnHitCallback();
			}
		}

		public virtual void PlayOnce(AnimeCallback endCallback, AnimeCallback hitCallback) {
			mOnEndCallback = endCallback;
			mOnHitCallback = hitCallback;
			mAnimePlayer.PlayOnce(HandleEndCallback, HandleHitCallback);
		}

		public virtual void PlayRepeat(int repeat, AnimeCallback endCallback, AnimeCallback hitCallback) {
			mOnEndCallback = endCallback;
			mOnHitCallback = hitCallback;
			mAnimePlayer.PlayRepeat(repeat, HandleEndCallback, HandleHitCallback);
		}

		#endregion
    }
}