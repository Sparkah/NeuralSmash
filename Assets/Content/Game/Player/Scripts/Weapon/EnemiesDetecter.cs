using System.Collections.Generic;
using Content.Game.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace Content.Game.Player.Scripts.Weapon
{
    public class EnemiesDetecter : MonoBehaviour
    {
        protected List<NavMeshAgent> _enemiesInRange = new List<NavMeshAgent>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out NavMeshAgent enemyNavMesh))
            {
                _enemiesInRange.Add(enemyNavMesh);
                enemyNavMesh.GetComponentInChildren<Enemy>().OnEnemyDeath += RemoveEnemyFromList;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out NavMeshAgent enemyNavMesh))
            {
               // Debug.Log("removing enemy from list" + _enemiesInRange.Count + "name " + gameObject.name);
                _enemiesInRange.Remove(enemyNavMesh);
            }
        }

        private void RemoveEnemyFromList(NavMeshAgent enemyNavMesh)
        {
            _enemiesInRange.Remove(enemyNavMesh);
            //Debug.Log("removing enemy from list _enemiesCoun = " + _enemiesInRange.Count + " name " + gameObject.name);
        }
    }
}