using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class MoveAction : AnimeAction
    {
        public Vector3 startPosition = Vector3.zero;
        public Vector3 endPosition = Vector3.one;
        public bool isWorldPosition = false;
        public GameObject targetObject;
        public bool autoFlip = true;

        protected Transform mTransform = null;

        protected Transform mModelTransform = null;

        public void Setup(Vector3 from, Vector3 to, float duration, bool worldPos = true) {
            Reset();
            startPosition = from;
            endPosition = to;
            SetDuration(duration);
            isWorldPosition = worldPos;
        }

        protected override void OnStart()
        {
            if (name == "")
            {
                name = "Move";
            }

            if (targetObject == null)
            {
                MarkAsDone();
            }

            mTransform = targetObject.transform;

            mModelTransform = mTransform.Find("Model");
            if (mModelTransform == null)
            {
                mModelTransform = mTransform;
            }

            if (autoFlip)
            {
                Flip();
            }
        }

        protected void Flip()
        {
            if (mModelTransform == null)
            {
                return;
            }

            float sign = endPosition.x > startPosition.x ? 1 : -1;
            Vector3 scale = mModelTransform.localScale;
            scale.x = Mathf.Abs(scale.x) * sign;
            mModelTransform.localScale = scale;
        }

        protected override void OnUpdate()
        {

            //float timeElapses
            Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, GetTimeElapsedRatio());

            if (isWorldPosition)
            {
                mTransform.position = currentPos;
            }
            else
            {
                mTransform.localPosition = currentPos;
            }


        }
    }
}