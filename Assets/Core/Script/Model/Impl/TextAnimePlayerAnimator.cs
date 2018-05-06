using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public class TextAnimePlayerAnimator : ITextAnimePlayer {
        protected GameObject mGameObject = null;
        protected Animator mAnimator;
        protected AnimeEvent mAnimeEvent;
        
        protected Transform mTransform;
        protected AnimeCallback mEndCallback;
        protected TextMesh mTextMesh;

        public TextAnimePlayerAnimator(GameObject gameObject) {
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

            mTextMesh = mGameObject.GetComponentInChildren<TextMesh>();        
        }

        protected void OnAnimeEvent(string eventName) {            
        } 

        protected void OnAnimeEnd() {        
            Debug.Log("OnAnimeEnd: ended");
            if(mEndCallback != null) {
                mEndCallback();
            }
        }

        public override void SetText(string content)
        {        
            if(mTextMesh != null) {
                mTextMesh.text = content;
            }
        }
       
        public override void Play(GameText.Style style, AnimeCallback endCallback)
        {

             // Error Handling 
            if(mAnimator == null) {
                if(endCallback != null) {
                    endCallback();
                }
                return;
            }

            mEndCallback = endCallback;
            string triggerName = style.ToString();
            Debug.Log("TextAnimePlayer: triggerName=" + triggerName);
            mAnimator.SetTrigger(triggerName);
        }

      


    }
}