using System.Collections.Generic;
using Client.Appearance;
using Client.CommunicateObjs;
using Client.Net;
using Logic;
using Server.ServerUtils;
using Unity.Netcode;
using UnityEngine;
using Universal.CSCommunication;
using Universal.Pools;

namespace Client.ClientInput
{
    public class InputCollector : NetworkBehaviour
    {
        private Vector3 _input;

        private const float OperationInterval = 1f;

        private float _lastOperationTime;

        void Start()
        {
            _lastOperationTime = Time.time;
        }

        void Update()
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            if (Time.time - _lastOperationTime >= OperationInterval)
            {
                //Add new frame info and broadcast old one
                int newFrameId = SFrameId.IncreaseId();
                SFramePool.Instance.OptQueue.Add(newFrameId, new SFrameInfo());
                
                GenerateNewOperation();
                
                BroadcastFrameServerRpc(SFrameId.NowId - 1);
                
                _lastOperationTime = Time.time;
            }
        }

        private void GenerateNewOperation()
        {
            COperation opt = new COperation
            {
                ClientId = NetworkManager.Singleton.LocalClientId,
                Move = new Vector3(_input.x, 0, _input.z)
            };
            SFramePool.Instance.OptQueue.TryGetValue(SFrameId.NowId, out SFrameInfo sFrameDic);
            if (sFrameDic == null)
            {
                Debug.LogError("----=-=-=--=");
            }
            
            sFrameDic?.Operations.Add(opt.ClientId, opt);
        }

        [ServerRpc]
        private void BroadcastFrameServerRpc(int sFrameId)
        {
            SFramePool.Instance.OptQueue.TryGetValue(sFrameId, out SFrameInfo sFrameInfo);
            ReceiveOperationClientRpc(SFrameId.NowId,sFrameInfo);
        }

        [ClientRpc]
        private void ReceiveOperationClientRpc(int sFrameId, SFrameInfo sFrameInfo)
        {
            var sFrameDic = sFrameInfo.Operations;
            NetInputBuffer.Instance.Enqueue(sFrameId, sFrameDic);
            if (IsOwner)
            {
                OptCompromise(sFrameDic);
            }
            GameLogicCenter.Instance.PushGame(sFrameId);
        }

        private void OptCompromise(Dictionary<ulong, COperation> sFrameDic)
        {
            //todo: compare authoritative operation with local predicted operation
        }
    }
}