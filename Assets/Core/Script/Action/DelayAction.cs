using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class DelayAction : AnimeAction
    {
        public float delayTime = 1.0f;

        protected override void OnStart()
        {
            name = "delay";
            SetDuration(delayTime);
        }
    }
}