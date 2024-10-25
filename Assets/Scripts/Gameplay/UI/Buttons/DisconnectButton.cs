using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Buttons
{
    public class DisconnectButton : NetworkBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            if (isServer)
            {
                NetworkManager.singleton.StopHost();
                return;
            }
            NetworkManager.singleton.StopClient();
        }
    }
}
