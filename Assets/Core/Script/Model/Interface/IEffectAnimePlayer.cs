using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public abstract class IEffectAnimePlayer {

        protected int mEffectID = 0;

        public void setEffectID(int effectID) {
            mEffectID = effectID;
        }

        // Animation 
        public abstract void PlayOnce(AnimeCallback endCallback, AnimeCallback hitCallback);
        public abstract void PlayRepeat(int repeatCount, AnimeCallback endCallback, AnimeCallback hitCallback);
        public abstract void PlayForever();
        public abstract void Move(Vector3 from, Vector3 to, float duration, AnimeCallback callback);        
        //public abstract void Play(string effectName, AnimeCallback hitCallback, AnimeCallback endCallback);    
        public virtual void Update(float delta) {} 
    }
}