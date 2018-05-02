using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class SequenceAction : AnimeAction
    {
        protected List<AnimeAction> mActionList = new List<AnimeAction>();

        protected List<AnimeAction> mPendingList = new List<AnimeAction>();
        protected AnimeAction mActiveAction = null;


        protected override void OnStart()
        {
            if (name == "")
            {
                name = "sequence:size=" + mActionList.Count;
            }

            SetDuration(-1);

            ResetPendingList();
            PopPendingAction();
        }


        protected override void OnUpdate()
        {
            if (mActiveAction != null)
            {
                mActiveAction.Update(mDeltaTime);

                if (mActiveAction.IsDone() == false)
                {
                    return;     // wait for next update 
                }
                else
                {
                    mActiveAction = null;
                }
            }

            // -- activeAction is null or activeAction is done 

            // if no more pending 
            if (HasPendingAction() == false)
            {
                MarkAsDone();
            }
            else
            {
                PopPendingAction();
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

        // Pending List Management 

        protected void ResetPendingList()
        {
            mPendingList.Clear();
            foreach (AnimeAction action in mActionList)
            {
                mPendingList.Add(action);
            }
        }

        protected bool HasPendingAction()
        {
            return mPendingList.Count > 0;
        }

        protected void PopPendingAction()
        {
            if (mPendingList.Count == 0)
            {
                return;     // Prevent out-of-bound problem!
            }
            mActiveAction = mPendingList[0];
            mPendingList.RemoveAt(0);

            if (mActiveAction != null)
            {
                mActiveAction.Start();
            }
        }

        #endregion
    }
}