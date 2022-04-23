using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using Managers;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class PlayerSpawner : MonoBehaviour
    {
        public Action<Player> OnPlayerSpawnDone;

        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private Player playerPrefab;

        private void Start()
        {
            SpawnUser();
        }
       
        private void SpawnUser()
        {
            var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count - 1)];
            spawnPoints.Remove(randomSpawnPoint);
            var playerObj = Instantiate(playerPrefab, 
                randomSpawnPoint.transform.position, 
                Quaternion.identity);

            GameManager.Instance.player = playerObj;
            OnPlayerSpawnDone?.Invoke(playerObj);
        }
    }
}