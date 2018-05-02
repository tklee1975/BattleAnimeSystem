using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class AnimeEvent : MonoBehaviour
    {
        public delegate void AnimeEndCallback();
        public delegate void EventCallback(string value);

        protected EventCallback mCallback = null;
        public EventCallback Callback
        {
            set
            {
                mCallback = value;
            }
        }

        // Used by the Animation Clip 
        public void OnEvent(string value)
        {
            if (mCallback != null)
            {
                mCallback(value);
            }
        }

        public void OnEndEvent()
        {
            if (mEndCallback != null)
            {
                mEndCallback();
            }
        }

        protected AnimeEndCallback mEndCallback = null;
        public AnimeEndCallback EndCallback
        {
            set
            {
                mEndCallback = value;
            }
        }

    }
}