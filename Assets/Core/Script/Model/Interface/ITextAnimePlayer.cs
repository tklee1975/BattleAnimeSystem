using System.Collections;
using System.Collections.Generic;


namespace BattleAnimeSystem { 
    // Generic Model 
	public abstract class ITextAnimePlayer {

        // Animation 
        public abstract void SetText(string content);
        public abstract void Play(GameText.Style style, AnimeCallback endCallback);
        public virtual void Update(float delta) {} 
    }
}