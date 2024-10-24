using System;
using System.Collections.Generic;
using Client.CommunicateObjs;
using UnityEngine;

namespace Client.Buffers
{
    /// <summary>
    /// Only used to manage local player state
    /// </summary>
    public class LocalInputBuffer
    {
        private Queue<Tuple<AppearanceState,COperation>> _optQueue;
        private LocalInputBuffer()
        {
            _optQueue = new Queue<Tuple<AppearanceState,COperation>>();
        }

        public static LocalInputBuffer Instance { get; } = new LocalInputBuffer();

        public Queue<Tuple<AppearanceState, COperation>> OptQueue => _optQueue;
    }
}
