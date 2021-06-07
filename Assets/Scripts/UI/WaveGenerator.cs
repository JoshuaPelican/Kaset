using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    #region Singleton

    public static WaveGenerator instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance found!");
            return;
        }

        instance = this;
    }

    #endregion

    public float separation;
    public int maxPieces;

    public Transform waveFront;
    public GameObject wavePiece;
    public Transform waveContainer;

    public List<GameObject> wave = new List<GameObject>();

    private void Start()
    {
        waveFront.transform.position = new Vector2(36, 0);
    }

    public void GenerateWave(float height)
    {
        waveFront.position = new Vector2(waveFront.position.x + separation, height);
        GameObject newWavePiece = Instantiate(wavePiece, new Vector2(waveFront.position.x + separation, height - wavePiece.transform.localScale.y/2), Quaternion.identity, waveContainer);
        if(wave.Count >= maxPieces)
        {
            Destroy(wave[0]);
        }
        wave.Add(newWavePiece);
    }
}
