using System;
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

        public void Enqueue(int sFrameId, Dictionary<ulong, COperation> opts)
        {
            if (!_optQueue.TryAdd(sFrameId, opts)) throw new Exception("Logic error");
        }
    }
}