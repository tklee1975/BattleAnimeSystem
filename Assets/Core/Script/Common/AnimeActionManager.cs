using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class AnimeActionManager : MonoBehaviour
    {

        #region Singleton 
        // Note: this singleon implementation inspired by Unity 2DGameKit
        public static AnimeActionManager Instance
        {
            get
            {
                if (sInstance != null) {
                    return sInstance;
                }
                sInstance = FindObjectOfType<AnimeActionManager>();
                if (sInstance != null) {
                    return sInstance;
                }

                Create ();
                return sInstance;
            }
        }

        protected static AnimeActionManager sInstance;

        public static AnimeActionManager Create ()
        {
            GameObject dataManagerGameObject = new GameObject("AnimeActionManager");
            DontDestroyOnLoad(dataManagerGameObject);
            sInstance = dataManagerGameObject.AddComponent<AnimeActionManager>();
            return sInstance;
        }

        void Awake()
        {
            if (Instance != this) {
                Destroy(gameObject);
            }
        }



        #endregion



        //------------------        

        //protected List<AnimeAction

        public AnimeAction currentAction = null;

        protected List<AnimeAction> mActionList = new List<AnimeAction>();
        protected List<AnimeAction> mDoneList = new List<AnimeAction>();
        protected List<AnimeAction> mNewActionQueue = new List<AnimeAction>();

        public void Reset()
        {
            mActionList.Clear();
            mDoneList.Clear();
            mNewActionQueue.Clear();
        }

        public void RunAction(AnimeAction action)
        {
            Reset();
            StartAction(action);
        }

        public void StartAction(AnimeAction action)
        {
            if (action == null)
            {
                return;
            }

            action.SetManager(this);
            action.Start();
            mActionList.Add(action);
        }

        public void QueueNewAction(AnimeAction action)
        {
            if (action == null)
            {
                return;
            }
            Debug.Log("INFO: ActionManager: " + action.name + " is queued");

            // action.SetManager(this);
            // action.Start();

            mNewActionQueue.Add(action);    // update loop will run it 
        }

        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            mDoneList.Clear();



            // 
            foreach (AnimeAction action in mActionList)
            {
                if (action.IsStarted() == false)
                {
                    continue;
                }

                action.Update(Time.deltaTime);

                if (action.IsDone())
                {
                    mDoneList.Add(action);
                }
            }


            // Remove the Done List 
            foreach (AnimeAction action in mDoneList)
            {
                mActionList.Remove(action);
            }

            StartQueuedActions();
        }

        void StartQueuedActions()
        {
            foreach (AnimeAction action in mNewActionQueue)
            {
                StartAction(action);
            }

            mNewActionQueue.Clear();
        }   
    }


}