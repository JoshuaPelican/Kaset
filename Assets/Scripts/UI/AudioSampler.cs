using UnityEngine;

public class AudioSampler : MonoBehaviour
{
    public AudioSource source;
    public float updateStep = 0.001f;
    public int sampleDataLength = 1024;
    public int sizeFactor;

    public Level level;
    private SpawnManager spawnManager;

    private float currentUpdateTime;

    private float[] clipSampleData;


    private void Start()
    {
        clipSampleData = new float[sampleDataLength];
        spawnManager = SpawnManager.instance;
        source.clip.LoadAudioData();
    }

    private void Update()
    {
        //Time increases since last sample
        currentUpdateTime += Time.deltaTime;
        //Check how frequently we want the audio to be sampled
        if (currentUpdateTime >= updateStep && source.isPlaying)
        {
            //Debug.Log(GetLoudness(source));
            SeparateLoudness(GetLoudness(source));
        }
    }

    public int GetLoudness(AudioSource _audioSource)
    {
        //Reset counter
        currentUpdateTime = 0;
        //Get the samples of the audio clip at the current time in the song
        source.clip.GetData(clipSampleData, _audioSource.timeSamples);
        //Reset loudness
        float clipLoudness = 0;
        //Takes all samples and adds them together
        foreach (var sample in clipSampleData)
        {
            clipLoudness += sample;
        }

        //Averages the samples to get the loudness
        clipLoudness /= sampleDataLength;
        clipLoudness = Mathf.Abs(clipLoudness);
        //Magnifies the loudness to be useable
        clipLoudness *= sizeFactor;
        clipLoudness = Mathf.Pow(clipLoudness, level.audioMulti);
        clipLoudness = Mathf.RoundToInt(clipLoudness);
        return (int)clipLoudness;
    }

    public void SeparateLoudness(int loudness)
    {
        if(loudness >= level.audioThresholdSmall && loudness < level.audioThresholdMedium)
        {
            spawnManager.SpawnEnemy((int)level.smallEnemy, 1);
        }
        else if (loudness >= level.audioThresholdMedium && loudness < level.audioThresholdLarge)
        {
            spawnManager.SpawnEnemy((int)level.mediumEnemy, 1);
        }
        else if (loudness >= level.audioThresholdLarge)
        {
            spawnManager.SpawnEnemy((int)level.largeEnemy, 1);
        }
    }
}
