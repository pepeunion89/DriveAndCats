using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{

    [SerializeField] public AudioSource meowSound;
    private Player currentPlayer;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<Player>() != null) {
            if (!meowSound.isPlaying) {
                meowSound.Play();
            }                
            currentPlayer = collision.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
            meowSound.Stop();
    }


}
