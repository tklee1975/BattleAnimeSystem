using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public interface IModelAnimeImpl {
        void ShowAttack(short attackMode);
        void ShowIdle();
        void ShowHit();     // Model is being hit
        void ShowMoveForward();
        void ShowMoveBackward();    
    }
}