using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CatsSpawnManager : MonoBehaviourPunCallbacks {
    [SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform catContainer;
    [SerializeField] private int maxCats = 36;

    private void Start() {
        ZonesSpawnManager.Instance.SpawnCatsOnZonesSpawned += SpawnCats;
    }

    private void SpawnCats(List<GameObject> listZonesSpawned) {
        int catsToSpawn = Mathf.Min(maxCats, listZonesSpawned.Count);
        List<int> chosenIndices = new List<int>();

        while (chosenIndices.Count < catsToSpawn) {
            int randomIndex = Random.Range(0, listZonesSpawned.Count);
            if (!chosenIndices.Contains(randomIndex)) {
                chosenIndices.Add(randomIndex);
                Vector3 spawnPosition = GetRandomBorderPosition(listZonesSpawned[randomIndex]);
                GameObject catSpawned = PhotonNetwork.InstantiateRoomObject(catPrefab.name, spawnPosition, Quaternion.identity);
                photonView.RPC("SetParentRPC", RpcTarget.AllBuffered, catSpawned.GetComponent<PhotonView>().ViewID, catContainer.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    private Vector3 GetRandomBorderPosition(GameObject zone) {
        Cuadra cuadra = zone.GetComponent<Cuadra>();
        if (cuadra == null) {
            return zone.transform.position;
        }

        // Elegir un lado aleatorio
        int randomSideIndex = Random.Range(0, cuadra.sidesList.Length);
        SpriteRenderer chosenSide = cuadra.sidesList[randomSideIndex];

        // Calcular una posición aleatoria a lo largo del lado elegido
        Vector3 spawnPosition = new Vector3(
            Random.Range(chosenSide.bounds.min.x, chosenSide.bounds.max.x),
            Random.Range(chosenSide.bounds.min.y, chosenSide.bounds.max.y),
            0
        );

        return spawnPosition;
    }

    [PunRPC]
    private void SetParentRPC(int catViewID, int containerViewID) {
        GameObject cat = PhotonView.Find(catViewID).gameObject;
        Transform container = PhotonView.Find(containerViewID).transform;
        cat.transform.SetParent(container, false);
    }
}