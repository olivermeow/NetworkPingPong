using System.Collections.Generic;
using Gameplay.Types;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class SpawnPointGroup : MonoBehaviour
    {
        [SerializeField] private List<PlayerSpawnPoint> playerSpawnPoints;
    
        public PlayerSpawnPoint GetByType(SideType sideType)
        {
            var point = playerSpawnPoints.Find(playerSpawnPoint => playerSpawnPoint.SideType == sideType);
            return point;
        }
    }
}