using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    // Generic Model 
	public abstract class AnimePlayerFactory : ScriptableObject {

        // Animation 
        public abstract IModelAnimePlayer CreateModelPlayer(GameObject obj);        
        public abstract IEffectAnimePlayer CreateEffectPlayer(GameObject obj);        
        public abstract ITextAnimePlayer CreateTextPlayer(GameObject obj);        
    }
}