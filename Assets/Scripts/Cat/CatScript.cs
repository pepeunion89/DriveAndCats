using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatScript : MonoBehaviour {
    [SerializeField] public AudioSource meowSound;
    [SerializeField] public Button catchButton;
    private Player currentPlayer = null;
    private PhotonView photonView;

    private void Awake() {
        photonView = GetComponent<PhotonView>();

        catchButton.onClick.AddListener(CatchCat);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PhotonView>() != null && collision.GetComponent<PhotonView>().IsMine) {
            if (!meowSound.isPlaying) {
                meowSound.Play();
            }
            currentPlayer = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<PhotonView>() != null && collision.GetComponent<PhotonView>().IsMine) {
            meowSound.Stop();
            currentPlayer = null;
        }
    }

    [PunRPC]
    private void CatchCat() {
        if (currentPlayer != null) {
            currentPlayer.GetInventory().AddCat();

            if (PhotonNetwork.IsMasterClient) {
                PhotonNetwork.Destroy(gameObject);
            } else {
                photonView.RPC("RequestDestroy", RpcTarget.MasterClient, photonView.ViewID);
            }
        }        
    }

    [PunRPC]
    private void RequestDestroy(int viewID) {
        PhotonView view = PhotonView.Find(viewID);
        if (view != null && PhotonNetwork.IsMasterClient) {
            PhotonNetwork.Destroy(view.gameObject);
        }
    }
}