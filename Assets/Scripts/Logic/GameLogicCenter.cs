using System.Collections.Generic;
using Client.ClientUtils;
using Client.CommunicateObjs;
using Client.Net;
using Newtonsoft.Json;
using Server.ServerUtils;
using UnityEngine;

namespace Logic
{
    public class GameLogicCenter
    {
        public static GameLogicCenter Instance { get; } = new GameLogicCenter();

        public void PushGame(int frameId)
        {
            NetInputBuffer.Instance.OptQueue.TryGetValue(frameId, out var opt);
            if(opt == null) return;

            DoPushGame(frameId,opt);
        }

        private void DoPushGame(int frameId, Dictionary<ulong, COperation> frameOpts)
        {
            Debug.Log($"DoPushGame===>{frameId}");
            foreach (var opt in frameOpts)
            {
                Debug.Log($"DoPushGame===>{opt.Key}");
                
                foreach (KeyValuePair<ulong, COperation> entry in frameOpts)
                {
                    ulong clientId = entry.Key;      // 获取键（ulong类型的客户端ID）
                    COperation operation = entry.Value; // 获取值（COperation类型的操作）

                    // 处理操作
                    Debug.Log($"ClientID: {clientId}, Operation: {operation.Move}");
                }
            }
        }
    }
}
