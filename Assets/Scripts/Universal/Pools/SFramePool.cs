using System.Collections.Generic;
using Client.CommunicateObjs;
using Universal.CSCommunication;

namespace Universal.Pools
{
    //Restore all frame info on server side
    public class SFramePool
    {
        private Dictionary<int, SFrameInfo> _optQueue;
        public Dictionary<int, SFrameInfo> OptQueue => _optQueue;
        private SFramePool()
        {
            _optQueue = new Dictionary<int, SFrameInfo>();
            _optQueue.Add(0, new SFrameInfo());
        }

        public static SFramePool Instance { get; } = new();
    }
}
