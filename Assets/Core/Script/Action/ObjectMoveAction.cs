using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem
{
    public class ObjectMoveAction : MoveAction
    {
        public Transform parentTransform = null;
        public GameObject objectPrefab; // Face Right 

        protected override void OnStart()
        {
            if (name != "")
            {
                name = "ObjectMove";
            }

            targetObject = SpawnNewObject();
            base.OnStart();
        }

        protected override void OnDone()
        {
            GameObject.Destroy(targetObject);
            targetObject = null;
        }

        protected GameObject SpawnNewObject()
        {
            if (objectPrefab == null)
            {
                return null;
            }

            GameObject newObject = GameObject.Instantiate(objectPrefab);
            if (parentTransform != null)
            {
                newObject.transform.SetParent(parentTransform);
            }

            return newObject;
        }

    }
}