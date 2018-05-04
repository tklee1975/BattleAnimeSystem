using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public interface IModelAnimePlayer {

        // Animation 
        void ShowAttack(short attackMode);
        void ShowIdle();
        void ShowHit();     // Model is being hit
        void ShowMoveForward();
        void ShowMoveBackward();    

        void Update(float delta);

        // Move 
        void Move(Vector3 from, Vector3 to, float duration, AnimeCallback callback);        

        // void Update(float delta);
    }
}