using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Buttons
{
    public class ClientButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
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
            NetworkManager.singleton.StartClient();
        }
    }
}
