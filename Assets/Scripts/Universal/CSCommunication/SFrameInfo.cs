using System.Collections.Generic;
using Client.CommunicateObjs;
using Unity.Netcode;

namespace Universal.CSCommunication
{
    public class SFrameInfo : INetworkSerializable
    {
        private Dictionary<ulong, COperation> _operations = new Dictionary<ulong, COperation>();
        public Dictionary<ulong, COperation> Operations => _operations;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            // 序列化字典的大小
            int count = _operations.Count;
            serializer.SerializeValue(ref count);

            if (serializer.IsWriter)
            {
                // 序列化键值对
                foreach (var kvp in _operations)
                {
                    ulong key = kvp.Key;
                    COperation value = kvp.Value;
                
                    // 序列化键
                    serializer.SerializeValue(ref key);
                
                    // 序列化值
                    serializer.SerializeValue(ref value);
                }
            }
            else
            {
                // 反序列化键值对
                _operations.Clear();
                for (int i = 0; i < count; i++)
                {
                    ulong key = 0;
                    COperation value = new COperation();
                
                    // 反序列化键
                    serializer.SerializeValue(ref key);
                
                    // 反序列化值
                    serializer.SerializeValue(ref value);
                
                    // 将键值对添加到字典
                    _operations[key] = value;
                }
            }
        }
    }
}