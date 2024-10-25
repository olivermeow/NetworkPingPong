using Infarastructure.GameStateMachine;
using Infarastructure.GameStateMachine.States;
using Infrastucture.StateMachine;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.UI.Buttons
{
    public class HostButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            NetworkManager.singleton.StartHost();
            _gameStateMachine.Enter<WaitingPlayersState>();
        }
    }
}
