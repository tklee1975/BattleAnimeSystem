using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public abstract class IModelAnimePlayer {

        // Animation 
        public abstract void ShowAttack(short attackMode, AnimeCallback endCallback, AnimeCallback hitCallback);
        public abstract void ShowIdle();
        public abstract void ShowHit(AnimeCallback endCallback);     // Model is being hit
        //public abstract void ShowMoveForward();
        //public abstract void ShowMoveBackward();    
        public abstract void ShowDie(AnimeCallback endCallback);
        public abstract void Resurrect(bool animated, AnimeCallback endCallback);  // Reborn, exit from Die state

        public virtual void Update(float delta) {} 

        // Move 
        public abstract void Move(Model.MoveType moveType, Vector3 from, Vector3 to, float duration, AnimeCallback callback);        

        // void Update(float delta);
    }
}