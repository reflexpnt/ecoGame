// Copyright 2022 Niantic, Inc. All Rights Reserved.
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Niantic.ARVoyage.SnowballToss
{
    /// <summary>
    /// A snowring is a target for the player to throw a snowball through for points.
    /// (The SnowballTossManager class instantiates multiple snowrings at a time.)
    /// InitSnowring() searches for a good position in the world to place its snowring.
    /// Success() is called by SnowringThruRing, a collider just behind the snowring's open center,
    /// triggered when a snowball is thrown through the snowring.
    /// Update() periodically detects if the snowring now intersects with newly-discovered
    /// environment mesh, and auto-expires the snowring if so.
    /// </summary>
    public class Bidone : MonoBehaviour
    {
        // dev flags
        private bool editorDebugButtons = false;
        private bool extraDebugLogging = false;

        private SnowballTossManager snowballTossManager = null;
        private SnowringThruRing snowringThruRing = null;

        public static float minLifetimeSecs = 15f;
        public static float maxLifetimeSecs = 17f;
        private float minPlacementDist = 1f;
        private float maxPlacementDist = 2.75f;
        private float minXAxisAnge = 0;
        private float maxXAxisAngle = 15f;
        private const float maxScale = 0.5f;

        private int currentSector = 0;

        private const int numPlacementSectors = 360 / 30;
        private const float degreesPerSector = 360f / (float)numPlacementSectors;
        private List<int> sectorSearchOrder = new List<int>();
        private int sectorSearchCtr = 0;
        private int numVectorsSearched = 0;
        private float timeStartedSampling = 0f;
        private int numSamplesTried = 0;
        private const int tooManySamplesTried = 300;
        private const float secsTillRecheckForNewlyFoundMesh = 0.5f;
        private float recheckMeshOverlapTime = 0f;

        private const float spacingRadius = 0.4f;
        private const float samplingDistIncrement = spacingRadius / 2f;
        private Collider[] overlappingColliders = new Collider[10];

        private float startTime = 0f;
        private float endTime = 0f;
        private bool isPlaced = false;
        private bool isSuccess = false;
        private bool isExpiring = false;

        private AudioManager audioManager;

        #region Visual Effects

        [Header("Visual Effects")]
        [SerializeField] GameObject ringBurstPrefab;

        private Material ringMaterial;

        #endregion // Visual Effects

        #region Animation Parameters

        [Header("Animation Parameters")]
        [SerializeField] AnimationCurve revealCurve;
        [SerializeField] AnimationCurve successCurve;
        [SerializeField] AnimationCurve expireCurve;

        // Animation state durations.
        private float revealDuration = 20f / 30f;
        private float revealDelay = 0f;
        private float successDuration = 20f / 30f;
        private float successDelay = .1f;
        private float expireDuration = 20f / 30f;
        private float expireDelay = 0f;

        #endregion // Animation Parameters


        private void Awake()
        {
            // Verify valid duration and distance values
            //maxLifetimeSecs = Mathf.Max(minLifetimeSecs, maxLifetimeSecs);
           // maxPlacementDist = Mathf.Max(minPlacementDist, maxPlacementDist);

            timeStartedSampling = Time.time;

            // Find material for score effect.
            //MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
            //if (meshRenderer) ringMaterial = meshRenderer.material;

            audioManager = SceneLookup.Get<AudioManager>();
        }


        // Possibly called multiple times, no more than once per frame, 
        // until a valid ring placement is found
       


        


        // called by manager just before instantiating another snowring,
        // to try to avoid the new ring being placed in our sector
      
       


        // Pick a vector in a sector, preferring sectors with no existing rings.
        // Search along that vector for a position to place this ring,
        // such that there is empty space (no environment mesh) between player and the ring
        
     


        // Called from child thru-ring collider
        public void Success()
        {
            if (isSuccess) return;

            Debug.Log("Snowring succeeding due to snowball");
            isSuccess = true;

            // SFX
            audioManager.PlayAudioAtPosition(AudioKeys.SFX_RingScore_Indicator, this.gameObject.transform.position);

            // Destroy is called at end of routine.
            StartCoroutine(SuccessRoutine(successDuration, successDelay));
        }


        

       


      

        public IEnumerator SuccessRoutine(float duration = 1f, float delay = 0f)
        {
            System.Action onStart = () =>
            {
                snowballTossManager?.SnowRingSucceeded();

              
            };

            System.Action<float> onUpdate = (float t) =>
            {
        
            };

            System.Action onComplete = () =>
            {
                //DestroySnowring();
            };

            yield return InterpolationUtil.LinearInterpolation(gameObject, gameObject,
                duration, delay, postWait: 0f,
                onStart, onUpdate, onComplete);
        }

        



    }
}
