using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
	public class BattleModel : Model {
		[Header("Position Setting")]
		public Transform launchPositionTF;

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		protected override AnimePlayerFactory GetAnimePlayerFactory()
		{
			return new DefaultAnimePlayerFactory();
		//	base.Awake();
		}
			

		public override Vector2 GetLaunchPosition() {
			if(launchPositionTF == null) {
				return base.GetLaunchPosition();
			}
			return launchPositionTF.position;
		}
	}
}