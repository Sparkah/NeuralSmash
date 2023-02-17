using UnityEngine;

namespace Content.HordeSurvival.EnemySpawner
{
    [System.Serializable]
    public class Batches
    {
        public GameObject[] EnemiesToSpawnThisWave;
        public float SpawnSpeedSecondsThisWave;
        public int SpawnAmountThisWave;
    }
}