using System;
using System.Collections.Generic;
using Client.ClientUtils;
using Client.CommunicateObjs;
using UnityEngine;

namespace Client.Appearance
{
    public class AppearanceMgr : MonoBehaviour
    {
        public static AppearanceMgr Instance;
        void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }

        [SerializeField] private Transform localPlayer;
        private Dictionary<int, Tuple<AppearanceState, COperation>> _appearanceStates = new();
        private float _lastOperationTime;
        private const float FrameInterval = 1f;
        
        void Start()
        {
        
        }

        void Update()
        {
            if (Time.time - _lastOperationTime >= FrameInterval)
            {
                // GenerateNewOperation();
                _lastOperationTime = Time.time;
            }
        }

        public void ExecuteLocalOperation(COperation cOperation)
        {
            AppearanceState state = new AppearanceState
            {
                NowPos = new Vector3(localPlayer.position.x, localPlayer.position.y, localPlayer.position.z)
            };
            _appearanceStates.Add(FrameId.NextId, new Tuple<AppearanceState, COperation>(state, cOperation));
        }
    }
}
