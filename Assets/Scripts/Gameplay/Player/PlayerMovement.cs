using Mirror;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed = 0.5f;

        private float _verticalInput;
        
        public bool BlockedInput { get; private set; }

        void FixedUpdate()
        {
            if (!isLocalPlayer)
                return;

            if (BlockedInput)
            {
                return;
            }

            _verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(0, _verticalInput) * speed * Time.fixedDeltaTime;
        }

        public void BlockInput()
        {
            BlockedInput = true;
        }
        public void UnblockInput()
        {
            BlockedInput = false;
        }
    }
}
