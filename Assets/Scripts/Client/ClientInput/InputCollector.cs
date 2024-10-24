using Client.Appearance;
using Client.Buffers;
using Client.CommunicateObjs;
using Client.Net;
using Logic;
using Server.ServerUtils;
using Unity.Netcode;
using UnityEngine;

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
            if (Time.time - _lastOperationTime >= OperationInterval)
            {
                SFrameId.IncreaseId();
                GenerateNewOperation();
                _lastOperationTime = Time.time;
            }
        }

        private void GenerateNewOperation()
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            COperation opt = new COperation
            {
                ClientId = NetworkManager.Singleton.LocalClientId,
                Move = new Vector3(_input.x, 0, _input.z)
            };
            SyncOperationServerRpc(opt);
            AppearanceMgr.Instance.ExecuteLocalOperation(opt);
        }
        
        [ServerRpc]
        private void SyncOperationServerRpc(COperation cOperation) 
        {
            Debug.Log(2);
            ReceiveOperationClientRpc(SFrameId.NowId,cOperation);
        }
        
        [ClientRpc]
        private void ReceiveOperationClientRpc(int sFrameId, COperation cOperation)
        {
            Debug.Log(3);
            NetInputBuffer.Instance.Enqueue(sFrameId, cOperation);
            if (IsOwner)
            {
                OptCompromise(cOperation);
            }
            GameLogicCenter.Instance.PushGame();
        }

        private void OptCompromise(COperation cOperation)
        {
            //todo: compare authoritative operation with local predicted operation
        }
    }
}