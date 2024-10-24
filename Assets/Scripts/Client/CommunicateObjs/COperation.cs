using Unity.Netcode;
using UnityEngine;

namespace Client.CommunicateObjs
{
    public class COperation : INetworkSerializable
    {
        // //Whether this key is needed, if the predicted of client conflict with this operation, client need to recast
        // public bool NeedRecast;
        public ulong ClientId { get; set; }

        private Vector3 _move;
        public Vector3 Move
        {
            get => _move;
            set => _move = value;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _move);
        }
    }

    public enum MoveOperation
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}
