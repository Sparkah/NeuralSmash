using System.Collections.Generic;

namespace Content.HordeSurvival.EnemySpawner
{
    [System.Serializable]
    public class Wave
    {
        public float TimerSecondsThisWave;
        public List<Batches> Batches;
    }
}