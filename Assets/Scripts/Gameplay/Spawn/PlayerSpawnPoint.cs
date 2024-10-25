using Gameplay.Types;
using UnityEngine;

namespace Gameplay.Spawn
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
        [field: SerializeField] public SideType SideType { get; private set; }
    }
}