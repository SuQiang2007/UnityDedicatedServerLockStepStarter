using System.Collections.Generic;
using Client.ClientUtils;
using Client.CommunicateObjs;
using Client.Net;
using Server.ServerUtils;
using UnityEngine;

namespace Logic
{
    public class GameLogicCenter
    {
        public static GameLogicCenter Instance { get; } = new GameLogicCenter();

        private int _lastFrameId;
        public void PushGame()
        {
            Debug.Log(4);
            NetInputBuffer.Instance.OptQueue.TryGetValue(SFrameId.NowId, out var opt);
            if(opt == null) return;
            Debug.Log(5);

            DoPushGame(_lastFrameId,opt);
            _lastFrameId++;
        }

        private void DoPushGame(int frameId, Dictionary<ulong, COperation> frameOpts)
        {
            Debug.Log($"DoPushGame===>{frameId}");
            foreach (var opt in frameOpts)
            {
                Debug.Log($"DoPushGame===>{opt.Key}=>{opt.Value}");
            }
        }
    }
}
