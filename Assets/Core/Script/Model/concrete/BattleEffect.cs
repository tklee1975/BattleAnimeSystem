using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
	public class BattleEffect : Effect {
		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		protected override AnimePlayerFactory GetAnimePlayerFactory()
		{
			return new DefaultAnimePlayerFactory();
		//	base.Awake();
		}
			
	}
}