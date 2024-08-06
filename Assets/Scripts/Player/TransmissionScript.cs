using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionScript : MonoBehaviour {

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    private enum AudioState { StartCar, Idle, Accelerating, FullSpeed }
    private AudioState currentState;

    private Coroutine accelerationCoroutine;

    private void Start() {
        currentState = AudioState.StartCar;
        audioSource.clip = audioClips[0];
        audioSource.Play();
        StartCoroutine(TransitionToIdleAfterStartCar());
    }

    public void TransmissionChanged(Vector2 direction) {
        if (direction != Vector2.zero) {
            if (currentState == AudioState.Idle) {
                SetAudioState(AudioState.Accelerating);
                if (accelerationCoroutine != null) {
                    StopCoroutine(accelerationCoroutine);
                }
                currentState = AudioState.Accelerating;
                accelerationCoroutine = StartCoroutine(AccelerationTimer());
            }
        } else if (currentState != AudioState.Idle && currentState != AudioState.StartCar) {
            SetAudioState(AudioState.Idle);
            if (accelerationCoroutine != null) {
                StopCoroutine(accelerationCoroutine);
                accelerationCoroutine = null;
            }
        }
    }

    void SetAudioState(AudioState newState) {
        if (currentState == newState) return;

        currentState = newState;
        switch (currentState) {
            case AudioState.Idle:
                audioSource.clip = audioClips[1];
                audioSource.loop = true;
                break;
            case AudioState.Accelerating:
                audioSource.clip = audioClips[2];
                break;
            case AudioState.FullSpeed:
                audioSource.clip = audioClips[3];
                audioSource.loop = true;
                break;
        }
        audioSource.Play();
    }

    IEnumerator TransitionToIdleAfterStartCar() {
        yield return new WaitForSeconds(audioClips[0].length);
        SetAudioState(AudioState.Idle);
    }

    IEnumerator AccelerationTimer() {
        yield return new WaitForSeconds(audioClips[1].length);
        if (currentState == AudioState.Accelerating) {
            SetAudioState(AudioState.FullSpeed);
            currentState = AudioState.FullSpeed;
        }
    }

}
