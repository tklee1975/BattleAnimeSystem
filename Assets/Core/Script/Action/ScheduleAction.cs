using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Schedule the Action 
namespace BattleAnimeSystem
{
    public class ScheduleAction : AnimeAction
    {
        protected class ScheduleItem
        {
            public float delay;
            public AnimeAction action;
        };


        protected List<ScheduleItem> mScheduleList = new List<ScheduleItem>();
        protected List<ScheduleItem> mPendingList = new List<ScheduleItem>();

        public void Schedule(float delay, AnimeAction action)
        {
            ScheduleItem scheduleItem = new ScheduleItem();
            scheduleItem.delay = delay;
            scheduleItem.action = action;

            mScheduleList.Add(scheduleItem);
        }



        protected override void OnStart()
        {
            if (name == "")
            {
                name = "ScheduleAction: count=" + mScheduleList.Count;
            }

            SetupPendingList();

            SetDuration(-1);
        }


        protected void SetupPendingList()
        {
            mPendingList.Clear();
            foreach (ScheduleItem item in mScheduleList)
            {
                mPendingList.Add(item);
            }
        }

        protected void CheckAndStartAction()
        {
            List<ScheduleItem> startedList = new List<ScheduleItem>();

            foreach (ScheduleItem item in mPendingList)
            {
                if (mTimeElapse < item.delay)
                {
                    //Debug.Log("timeElapse=" + mTimeElapse + " delay=" + item.delay +  " item=" + item.action );
                    continue;
                }

                // Start
                StartScheduledAction(item);
                startedList.Add(item);
            }

            // Remove those started from Pending List
            foreach (ScheduleItem item in startedList)
            {
                mPendingList.Remove(item);
            }
        }

        protected void StartScheduledAction(ScheduleItem item)
        {
            //StartAction(item.action);
            AddSubAction(item.action);
        }


        protected override void OnUpdate()
        {

            CheckAndStartAction();

            //Debug.Log("PendingCount=" + mPendingList.Count);
            if (mPendingList.Count == 0)
            {
                MarkAsDone();
            }

        }

    }
}