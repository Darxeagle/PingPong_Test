using System.Collections;
using System.Collections.Generic;
using Assets.Common.Scripts;
using Assets.Common.Scripts.Events;
using Assets.Common.Scripts.UnityDI;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Assets.Scripts.PingPongTest
{
    public class PhotonServerController : LoadBalancingClient, IDependent
    {
        [Dependency] public MonoProvider MonoProvider { set; private get; }

        public EventWrap Disconnected { get; private set; }
        public EventWrap CreatedGame { get; private set; }
        public EventWrap JoinedGame { get; private set; }
        public EventWrap<byte, Dictionary<byte, object>> ReceivedEvent { get; private set; }

        private bool _createdRoom;
        private bool _joinedRoom;
        private bool _gameActive;

        public PhotonServerController()
        {
            Disconnected = new EventWrap();
            CreatedGame = new EventWrap();
            JoinedGame = new EventWrap();
            ReceivedEvent = new EventWrap<byte, Dictionary<byte, object>>();
        }

        public void OnInjected()
        {
            MonoProvider.FixedUpdateEvent.AddListener(OnUpdate);
            MonoProvider.ApplicationQuit.AddListener(Disconnect);
        }

        private void OnUpdate(float obj)
        {
            Service();

            if (!(_createdRoom || _joinedRoom)) return;

            Debug.Log(CurrentRoom);

            if (!_gameActive && CurrentRoom != null && CurrentRoom.PlayerCount == 2)
            {
                _gameActive = true;
                if (_createdRoom) CreatedGame.Dispatch();
                if (_joinedRoom) JoinedGame.Dispatch();
            }
            if (_gameActive && (CurrentRoom == null || CurrentRoom.PlayerCount < 2))
            {
                Disconnected.Dispatch();
            }
        }

        public void FindGame()
        {
            MonoProvider.StartCoroutine(FindGameEnum());
        }

        IEnumerator FindGameEnum()
        {
            this.AppId = "03d698e8-1b5f-4d94-8419-3c52a58a0afa";  // set your app id here
            this.AppVersion = "1.0";  // set your app version here

            if (!ConnectToRegionMaster("eu"))
            {
                Disconnected.Dispatch();
            }

            while (!IsConnectedAndReady)
            {
                yield return null;
            }

            OpJoinRoom("pingPong");
            yield return new WaitForSeconds(1f);
            if (CurrentRoom != null)
            {
                _joinedRoom = true;
                yield break;
            }

            OpCreateRoom("pingPong", new RoomOptions() {MaxPlayers = 2}, TypedLobby.Default);
            yield return new WaitForSeconds(1f);
            if (CurrentRoom != null)
            {
                _createdRoom = true;
                yield break;
            }

            Disconnected.Dispatch();
        }

        public void SendEvent(byte code, Dictionary<byte, object> data)
        {
            Hashtable hash = new Hashtable();
            foreach (var o in data)
            {
                hash.Add(o.Key, o.Value);
            }
            OpRaiseEvent(code, data, true, RaiseEventOptions.Default);
        }

        public override void OnEvent(EventData photonEvent)
        {
            base.OnEvent(photonEvent);
            if (photonEvent.Code < 200)
            {
                if (photonEvent.Parameters.ContainsKey(ParameterCode.CustomEventContent))
                    ReceivedEvent.Dispatch(photonEvent.Code, photonEvent.Parameters[ParameterCode.CustomEventContent] as Dictionary<byte, object>);
                else
                    ReceivedEvent.Dispatch(photonEvent.Code, new Dictionary<byte, object>());
            }
        }
    }
}   
