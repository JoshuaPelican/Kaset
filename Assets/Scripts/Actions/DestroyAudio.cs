using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    public AudioSource source;
    private void Update()
    {
        if (!source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
