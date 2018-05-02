using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class SimpleAnimationAction : AnimeAction
    {

        public AnimationClip clip = null;
        public int repeat = 1;                  // repeat=0: forever 
        public bool destroySelf = true;
        public Vector3 spawnPosition = Vector3.zero;

        public Transform parentTransform = null;

        protected SimpleAnimation mAnimation = null;
        protected GameObject mObject = null;

        protected override void OnStart()
        {
            if (name != "")
            {
                name = "SimpleAnimationAction";
            }

            if (clip == null)
            {
                Debug.Log("SimpleAnimationAction: clip is null");
                MarkAsDone();
                return;
            }

            mObject = SpawnNewObject();
            if (mAnimation == null)
            {
                Debug.Log("SimpleAnimationAction: mAnimation is null");
                MarkAsDone();
                return;
            }

            if (repeat == 0)
            {
                SetDuration(-1);    // Forever 
            }
            else
            {
                SetDuration(repeat * clip.length);
            }

            Debug.Log("SimpleAnimationAction: duration=" + mDuration);
            PlayClip();
            // targetObject = SpawnNewObject();	
            // base.OnStart();	
        }

        protected override void OnDone()
        {
            if (destroySelf && mObject != null)
            {
                GameObject.Destroy(mObject);
                mObject = null;
                mAnimation = null;
            }
            else
            {
                if (mAnimation != null)
                {
                    mAnimation.Stop();
                }
            }

        }

        protected void PlayClip()
        {
            if (mAnimation != null)
            {
                mAnimation.PlayClip(clip);
            }
        }

        protected GameObject SpawnNewObject()
        {
            if (clip == null)
            {
                return null;
            }

            GameObject newObject = new GameObject();

            newObject.name = "NewAnime";
            newObject.AddComponent<SpriteRenderer>();
            newObject.AddComponent<Animator>();
            newObject.AddComponent<SimpleAnimation>();

            mAnimation = newObject.GetComponent<SimpleAnimation>();
            // Add to parent 
            if (parentTransform != null)
            {
                newObject.transform.SetParent(parentTransform);
            }
            newObject.transform.position = spawnPosition;

            return newObject;
        }

    }
}