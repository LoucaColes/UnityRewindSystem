using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundAudioComponent : MonoBehaviour {

    public AudioSource m_audioSource; // Reference to the audio source attached to object

	// Use this for initialization
	void Start () {
        m_audioSource = gameObject.GetComponent<AudioSource>(); // Get reference to audio source
	}

    // Used to play the music normally
    public void PlayNormalAudio()
    {
        m_audioSource.pitch = 1.0f;
    }

    // Used to play the music reversed
    public void PlayReverseAudio()
    {
        m_audioSource.pitch = -1.0f;
    }

    // Used to reset music back to normal
    public void ResetAudio()
    {
        PlayNormalAudio();
    }
}
