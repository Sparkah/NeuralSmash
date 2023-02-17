using UnityEngine.AI;

namespace Content.Game.Enemies
{
    public interface IEnemyDisabler
    {
        public void RemoveEnemyFromList(NavMeshAgent navMeshAgent);
    }
}