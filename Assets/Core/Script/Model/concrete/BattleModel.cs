using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BattleAnimeSystem { 
	public class BattleModel : Model {
		[Header("Position Setting")]
		public Transform[] launchPositionTF;

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		protected override AnimePlayerFactory GetAnimePlayerFactory()
		{
			return new DefaultAnimePlayerFactory();
		//	base.Awake();
		}
			
		Transform GetLaunchTF(int index) {
			if(index >= launchPositionTF.Length) {
				index = launchPositionTF.Length - 1;
			}

			return launchPositionTF[index];
		}

		public override Vector2 GetLaunchPosition(int index) {
			if(launchPositionTF == null) {
				return base.GetLaunchPosition();
			}
			return GetLaunchTF(index).position;
		}
	}
}