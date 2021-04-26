using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioListener al;

    public GameObject muteText;

    public static bool mute = false;

    private void Start()
    {
        al = GetComponent<AudioListener>();

        mute = false;
        AudioListener.volume = mute ? 0.0f : 1.0f;
        muteText.SetActive(mute);
    }

    void Update()
    {
        if (Input.GetButtonDown("Mute"))
        {
            mute = !mute;
            AudioListener.volume = mute ? 0.0f : 1.0f;
            muteText.SetActive(mute);
        }
    }
}
