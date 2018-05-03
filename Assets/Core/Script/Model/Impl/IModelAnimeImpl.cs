using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public interface IModelAnimeImpl {

        // Animation 
        void ShowAttack(short attackMode);
        void ShowIdle();
        void ShowHit();     // Model is being hit
        void ShowMoveForward();
        void ShowMoveBackward();    

        void Update(float delta);

        // Move 
        void Move(Vector3 from, Vector3 to, float duration, Model.Callback callback);        

        // void Update(float delta);
    }
}