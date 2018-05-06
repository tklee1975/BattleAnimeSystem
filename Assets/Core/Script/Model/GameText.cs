using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BattleAnimeSystem { 
    // Generic Model 

    /**
     *
     * 
     */
	public abstract class GameText : MonoBehaviour {
		public enum Style {
			Default = 0,		// just used when do the setting
			Damage = 1,
			Buff = 2, 
		}

		public string text = "";
		public Style style = Style.Buff;
        
		protected AnimePlayerFactory mAnimePlayerFactory = null;
        protected ITextAnimePlayer mAnimePlayer;


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
			//mAnimePlayer.setEffectID(effectID);	       // 
		}

		public void Reset() {
		}

		protected abstract AnimePlayerFactory GetAnimePlayerFactory();

		void SetupAnimePlayer() {
			mAnimePlayerFactory = GetAnimePlayerFactory();

			if(mAnimePlayerFactory == null) {
				mAnimePlayer = null;
				Debug.Log("Model [" + gameObject.name + "] missing animePlayerFactory");
				return;
			}
			mAnimePlayer = mAnimePlayerFactory.CreateTextPlayer(gameObject);
		}
        


		// Use this for initialization
		void Start () {
			SetText(text);
		}
		
		// Update is called once per frame
		void Update () {
			mAnimePlayer.Update(Time.deltaTime);
		}


		//protected voi

       


        #region 

		public virtual void SetText(string text) {
			mAnimePlayer.SetText(text);
		}

		public virtual void Play(AnimeCallback endCallback) {
			mAnimePlayer.Play(style, endCallback);
		}

		public virtual void Play(Style _style, AnimeCallback endCallback) {
			mAnimePlayer.Play(_style, endCallback);
		}

		
		#endregion
    }
}