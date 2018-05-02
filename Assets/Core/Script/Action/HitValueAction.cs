using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class HitValueAction : AnimeAction
    {
        public int hitValue = 999999;
        public float playDuration = 1.0f;
        public Transform parentTransform = null;
        public Vector3 position = Vector3.zero;
        public GameObject valueTextPrefab = null;

        protected ValueAnimation mValueAnimation = null;    // derivated by prefab

        public HitValueAction()
        {
            name = "hitValue";
        }

        protected override void OnStart()
        {

            if (valueTextPrefab == null)
            {
                Debug.Log("HitValueAction: missing valueText prefab");
                MarkAsDone();
                return;
            }

            CreateValueTextObject();
            if (mValueAnimation == null)
            {
                Debug.Log("HitValueAction: missing ValueAnimation Component");
                MarkAsDone();
                return;
            }

            SetDuration(mValueAnimation.duration);

            PlayValueAnimation();
        }

        protected void CreateValueTextObject()
        {
            GameObject obj = GameObject.Instantiate(valueTextPrefab);
            if (parentTransform != null)
            {
                obj.transform.SetParent(parentTransform);
            }
            obj.transform.localPosition = position;

            ValueAnimation valueAnime = obj.GetComponent<ValueAnimation>();
            if (valueAnime != null)
            {
                valueAnime.autoDestroy = true;
            }

            mValueAnimation = valueAnime;
        }

        protected void PlayValueAnimation()
        {
            if (mValueAnimation != null)
            {
                mValueAnimation.Show(hitValue);
            }
        }
    }
}