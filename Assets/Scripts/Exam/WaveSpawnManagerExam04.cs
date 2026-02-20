using UnityEngine;

public class WaveSpawnManagerExam04 : MonoBehaviour
{
    public Wave[] waveConfigurations;
    public WaveController waveController;

    public bool enableWaveCycling;

    private int currentWave = 0;
    private float waveEndTime = 0f;

    void Start()
    {
        waveController.StartWave(waveConfigurations[currentWave]);
        waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;
    }

    void Update()
    {
        if (Time.time >= waveEndTime && waveController.IsComplete())
        {
            currentWave++;

            if (currentWave >= waveConfigurations.Length)
            {
                if (enableWaveCycling)
                {
                    currentWave = 0;
                }
                else
                {
                    Debug.Log("All waves completed!");
                    return;
                }
            }

            waveController.StartWave(waveConfigurations[currentWave]);
            waveEndTime = Time.time + waveConfigurations[currentWave].waveInterval;
        }
    }
}