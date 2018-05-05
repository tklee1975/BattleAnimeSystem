using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
    
    // Generic Model 
    public class DefaultAnimePlayerFactory : AnimePlayerFactory {
        
        public override IModelAnimePlayer CreateModelPlayer(GameObject obj) {
            return new ModelAnimePlayerAnimator(obj);
        }   

        public override IEffectAnimePlayer CreateEffectPlayer(GameObject obj) {
            return new EffectAnimePlayerAnimator(obj);
        }    

         public override ITextAnimePlayer CreateTextPlayer(GameObject obj) {
            return new TextAnimePlayerAnimator(obj);
        }       
    }
}