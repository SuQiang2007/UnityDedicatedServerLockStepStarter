using System.Collections;
using System.Collections.Generic;
using Client.ClientUtils;
using Client.CommunicateObjs;
using UnityEngine;

namespace Client.Net
{
    public class NetInputBuffer
    {
        private Dictionary<int, Dictionary<ulong, COperation>> _optQueue;
        public Dictionary<int, Dictionary<ulong, COperation>> OptQueue => _optQueue;
        private NetInputBuffer()
        {
            _optQueue = new Dictionary<int, Dictionary<ulong, COperation>>();
        }

        public static NetInputBuffer Instance { get; } = new();

        public void Enqueue(int sFrameId, COperation op)
        {
            if(!_optQueue.ContainsKey(sFrameId)) _optQueue.Add(sFrameId, new Dictionary<ulong, COperation>());
            _optQueue[sFrameId][op.ClientId] = op;
        }
    }
}