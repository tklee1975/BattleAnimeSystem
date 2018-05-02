using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
//Target: Animator have a state d


*/
namespace BattleAnimeSystem { 
    public class AnimatorAction : AnimeAction {
        public Animator animator;
        public AnimeEvent animeEvent;                   // Auto
        public string triggerState = "";
        public AnimeAction onHitAction = null;
        public float playDuration = -1;                 // default: 
        public bool doneWhenHit = false;

        protected override void OnStart()
        {
            if(animator == null) {
                Debug.Log("ERROR: AnimatorAction missing animator");
                MarkAsDone();
            }
            
            animeEvent = animator.GetComponent<AnimeEvent>();
            if(animeEvent != null) {
                animeEvent.EndCallback = OnAnimeEnd;        // called by end_clip
                animeEvent.Callback = OnAnimeEvent;         // 

                animator.SetTrigger(triggerState);
            }

            SetDuration(playDuration);                  // 

        }

        protected override void OnUpdate() {
        //  UpdateHitAction();
        }

        protected void UpdateHitAction() {
            if(onHitAction == null) {
                return;
            }
            if(onHitAction.IsStarted() == false) {
                return;
            }
            if(onHitAction.IsDone()) {
                return;
            }
            onHitAction.Update(mDeltaTime);
        }

        public override bool IsDone() {
            if(onHitAction != null) {
                if(onHitAction.IsDone() == false) {
                    return false;
                }
            }

            return mIsDone;
        }



        protected virtual void OnAnimeEnd() {
            //Debug.Log("OnAnimeEnd:" + name);
            
            MarkAsDone();
        }

        protected virtual void OnAnimeEvent(string eventName) {
            if(eventName.ToLower() == "hit") {
                Debug.Log("Receive hit event");
                if(doneWhenHit) {
                    MarkAsDone();
                    return;
                }


                
                if(onHitAction != null) {
                    //Debug.Log("Start OnHitAction");
                    //onHitAction.Start();
                    StartAction(onHitAction);
                }
            }
        }

}
}