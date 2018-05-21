using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleAnimeSystem { 
    public class EffectParticle : MonoBehaviour {
        public float hitTime = 0f;


        protected AnimeCallback mHitCallback;
        protected AnimeCallback mEndCallback;
        
        protected ParticleSystem mParticleSystem;
        protected float mParticleDuration = 0;

        protected bool mParticlePlaying = false;
        protected float mParticleTimeElapse = 0;
        protected bool mHasHit = false;
        

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            mParticleSystem = gameObject.GetComponent<ParticleSystem>();
            mParticleSystem.Stop();
            mParticleDuration = mParticleSystem.main.duration;
            mParticleTimeElapse = 0;
            mParticlePlaying = false;
        }

        // Use this for initialization
        void Start () {
           
        }

        
        // Update is called once per frame
        void Update () {
            UpdateParticle(Time.deltaTime);

        }

        public AnimeCallback EndCallback { 
            set {
                mEndCallback = value;
            }
        }

        public AnimeCallback HitCallback { 
            set {
                mHitCallback = value;
            }
        }


        public void Play(AnimeCallback endCallback = null, AnimeCallback hitCallback = null) {
            if(endCallback != null) {
                mEndCallback = endCallback;
            }
            if(hitCallback != null) {
                mHitCallback = hitCallback;
            }

            /// --- 
            mHasHit = false;
            mParticleSystem.Play();
            mParticleTimeElapse = 0;
            mParticlePlaying = true;
        }

        public void Stop() {
            mParticleSystem.Stop();
            mParticlePlaying = false;
        }

        void OnParticlePlayEnd() {
            mParticlePlaying = false;
            if(mEndCallback != null) {
                mEndCallback();
            }
        }

        void OnHit() {
            if(mHitCallback != null) {
                mHitCallback();
            }
            mHasHit = true;
        }


        void UpdateParticle(float delta) {
            // Debug.Log("UpdateParticle: delta=" + delta + " playing=" + mParticlePlaying);
            if(mParticlePlaying == false) {
                return;
            }
            
            mParticleTimeElapse += delta;
            
            if(mHasHit == false) {
                if(mParticleTimeElapse >= hitTime) {
                    OnHit();
                }
            }


            if(mParticleTimeElapse >= mParticleDuration) {
                OnParticlePlayEnd();
            }
        }
    }
}