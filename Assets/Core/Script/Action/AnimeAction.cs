using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class AnimeAction
    {
        public string name = "";

        protected bool mIsDone = false;
        protected bool mAllDone = false;        // 
        protected bool mIsStarted = false;

        protected float mDuration = 0;      // duration = 0  mean instant action 
        protected float mTimeElapse = 0;    // used tell whether need to echo or not 

        protected float mDeltaTime;

        protected AnimeActionManager mManager;

        public void SetManager(AnimeActionManager _manager)
        {
            mManager = _manager;
        }

        public void StartAction(AnimeAction action)
        {
            if (mManager != null)
            {
                Debug.Log("ERROR: AnimeAction: queue action=" + action.name);
                mManager.QueueNewAction(action);
            }
            else
            {
                Debug.Log("ERROR: AnimeActionManager: manager is null");
            }
        }


        public virtual void Start()
        {
            mIsDone = false;
            mIsStarted = true;
            mTimeElapse = 0;

            Debug.Log("AnimeAction [" + name + "] started");
            OnStart();      // something the action will be done at 'OnStart'				
        }

        public virtual void Reset()
        {
            mSubActionList.Clear();
            mIsStarted = false;
            mIsDone = false;
            mAllDone = false;
            mTimeElapse = 0;
        }

        public virtual void Update(float deltaTime)
        {
            if (mAllDone)
            {
                return;
            }
            mDeltaTime = deltaTime;
            mTimeElapse += mDeltaTime;

            if (mIsDone == false)
            {
                if (CheckAndReduceTime())
                {
                    MarkAsDone();
                }

                OnUpdate();             // update current 
            }

            if (mSubActionList.Count > 0)
            {
                UpdateSubActions();     // update sub actions

                if (mIsDone && IsSubActionDone())
                {
                    MarkAllDone();
                }
            }
        }

        public virtual void Step()
        {

        }

        public virtual bool IsStarted()
        {
            return mIsStarted;
        }

        public virtual bool IsDone()
        {
            return mAllDone;
        }

        protected virtual void OnUpdate()
        {
            // For extension
        }

        protected virtual void OnStart()
        {
            // For extension
        }

        protected virtual void OnDone()
        {
            // For extension
        }

        protected void SetEndByCondition()
        {
            SetDuration(-1);
        }

        protected void MarkAllDone()
        {
            OnDone();
            mAllDone = true;
            Debug.Log("AnimeAction:" + name + " done");
        }

        protected void MarkAsDone()
        {
            if (mIsDone)
            {
                return;
            }

            mIsDone = true;
            if (IsSubActionDone())
            {
                MarkAllDone();
            }
        }

        #region Duration Logic Action 
        public void SetDuration(float _duration)
        {
            mDuration = _duration;
        }

        protected bool CheckAndReduceTime()
        {   // return: istimeUp
            if (mDuration <= -1)
            {
                return false;       // e
            }

            if (mTimeElapse >= mDuration)
            {
                return true;
            }

            return false;
        }


        public float GetTimeElapsed()
        {
            return mTimeElapse;
        }

        public float GetTimeElapsedRatio()
        {
            if (mDuration <= 0)
            {
                return 0;
            }

            return mTimeElapse / mDuration;
        }
		#endregion

        #region Sub Action 
        protected List<AnimeAction> mSubActionList = new List<AnimeAction>();

        protected void AddSubAction(AnimeAction action)
        {
            action.SetManager(mManager);
            action.Start();
            mSubActionList.Add(action);
        }

        protected bool IsSubActionDone()
        {
            foreach (AnimeAction action in mSubActionList)
            {
                if (action.IsDone() == false)
                {
                    return false;
                }
            }
            return true;
        }

        protected void UpdateSubActions()
        {
            foreach (AnimeAction action in mSubActionList)
            {
                action.Update(mDeltaTime);
            }

            //if(is)
        }


        #endregion
    }
}