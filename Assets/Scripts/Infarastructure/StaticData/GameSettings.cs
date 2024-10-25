using UnityEngine;

namespace Infarastructure.StaticData
{
    [CreateAssetMenu(menuName = "Data/Game Settings", fileName = "New Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField] public int RoundCount { get; private set; }
        [field: SerializeField] public int ScoreToWin { get; private set; }
    }
}
