using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;
    void Start()
    {
        Debug.Log("Connecting to the server...");
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connected succesfull.");
    }

    public override void OnConnectedToMaster() {

        base.OnConnectedToMaster();

        Debug.Log("Connected.");

        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby() {

        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 10 }, TypedLobby.Default);

        Debug.Log("You are at Lobby");

    }

    public override void OnJoinedRoom() {

        base.OnJoinedRoom();

        Debug.Log("You are at Room1");

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

        _player.GetComponent<PlayerSetup>().IsLocalPlayer();

    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.LogError($"Failed to join room: {message}");
    }

}
