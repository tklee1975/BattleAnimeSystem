using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class VFXAction : AnimatorAction
    {
        public GameObject vfxPrefab = null;
        public string vfxName = "";
        public Transform parent = null;
        public Vector3 position = Vector3.zero;

        protected GameObject mCloneObject = null;

        protected override void OnStart()
        {
            if (vfxPrefab == null)
            {
                MarkAsDone();
                return;
            }

            if (vfxName == "")
            {
                MarkAsDone();
                return;
            }

            CreateObject();

            triggerState = vfxName;

            base.OnStart();
        }

        protected void CreateObject()
        {
            mCloneObject = GameObject.Instantiate(vfxPrefab);

            if (parent != null)
            {
                mCloneObject.transform.SetParent(parent);
            }

            mCloneObject.transform.position = position;

            animator = mCloneObject.GetComponentInChildren<Animator>();
        }


        protected override void OnDone()
        {
            GameObject.Destroy(mCloneObject);
            animator = null;
        }
    }
}