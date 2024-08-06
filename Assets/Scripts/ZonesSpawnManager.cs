using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
using System;

public class ZonesSpawnManager : MonoBehaviourPunCallbacks {
    [SerializeField] private GameObject spawnZonePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float initialPixelsToMove = 21f;
    private int cols = 6;
    private int rows = 6;
    private bool zonesAlreadySpawned = false;

    public static ZonesSpawnManager Instance { get; private set; }

    public event Action<List<GameObject>> SpawnCatsOnZonesSpawned;
    private List<GameObject> spawnedZones = new List<GameObject>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        if (PhotonNetwork.IsMasterClient && !zonesAlreadySpawned) {
            SpawnZones();
        }
    }

    public void SpawnZones() {
        if (zonesAlreadySpawned)
            return;

        float rowPixelsPosition = initialPixelsToMove;

        for (int spawnZoneRowsCounter = 0; spawnZoneRowsCounter < rows; spawnZoneRowsCounter++) {
            float colPixelsPosition = initialPixelsToMove;

            for (int spawnZoneColsCounter = 0; spawnZoneColsCounter < cols; spawnZoneColsCounter++) {
                Vector3 spawnPosition = new Vector3(
                    spawnPoint.position.x + colPixelsPosition,
                    spawnPoint.position.y - rowPixelsPosition,
                    0
                );

                GameObject spawnedZone = PhotonNetwork.InstantiateRoomObject(spawnZonePrefab.name, spawnPosition, Quaternion.identity);
                spawnedZone.transform.SetParent(spawnPoint, true);
                spawnedZones.Add(spawnedZone);

                colPixelsPosition += initialPixelsToMove;
            }

            rowPixelsPosition += initialPixelsToMove;
        }

        zonesAlreadySpawned = true;

        PhotonView.Get(this).RPC("OnZonesSpawned", RpcTarget.All);
    }

    [PunRPC]
    private void OnZonesSpawned() {
        zonesAlreadySpawned = true;
        SpawnCatsOnZonesSpawned?.Invoke(spawnedZones);
    }

    public override void OnJoinedRoom() {
        base.OnJoinedRoom();
        if (zonesAlreadySpawned) {
            OnZonesSpawned();
        }
    }
}