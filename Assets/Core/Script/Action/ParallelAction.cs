using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class ParallelAction : AnimeAction
    {
        protected List<AnimeAction> mActionList = new List<AnimeAction>();


        protected override void OnStart()
        {
            if (name == "")
            {
                name = "parallelSize=" + mActionList.Count;
            }
            SetDuration(-1);

            StartActionList();
        }


        protected override void OnUpdate()
        {

            UpdateActionList();

            if (IsAllDone())
            {
                MarkAsDone();
            }

        }

        #region Action List Management 
        public void AddAction(AnimeAction action)
        {
            mActionList.Add(action);
        }

        public void Clear()
        {
            mActionList.Clear();
        }

        public void AddActionList(List<AnimeAction> actionList)
        {
            mActionList.Clear();
            foreach (AnimeAction action in actionList)
            {
                mActionList.Add(action);
            }
        }

        protected void StartActionList()
        {
            foreach (AnimeAction action in mActionList)
            {
                if (action.IsDone())
                {
                    continue;
                }
                Debug.Log("StartActionList: " + action.name + " started");
                action.Start();
            }
        }

        protected void UpdateActionList()
        {
            foreach (AnimeAction action in mActionList)
            {
                if (action.IsDone())
                {
                    continue;
                }
              //  Debug.Log("UpdateActionList: " + action.name + " delta=" + mDeltaTime);
                action.Update(mDeltaTime);
            }
        }

        protected bool IsAllDone()
        {
            foreach (AnimeAction action in mActionList)
            {
                Debug.Log("IsAllDone: " + action.name + " flag=" + action.IsDone());
                if (action.IsDone() == false)
                {
                    return false;
                }

            }
            return true;
        }
        #endregion
    }
}