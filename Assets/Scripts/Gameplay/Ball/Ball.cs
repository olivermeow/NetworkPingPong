using Mirror;
using UnityEngine;

namespace Gameplay.Ball
{
    public class Ball : MonoBehaviour
    {
        public float speed = 30;
        public Rigidbody2D rigidbody2d;
        

        private void Start()
        {
            rigidbody2d.velocity = Vector2.right * speed;
        }

        public void Stop()
        {
            rigidbody2d.velocity = Vector2.zero;
        }
        float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
        {
            return (ballPos.y - racketPos.y) / racketHeight;
        }

        // only call this on server
        [ServerCallback]
        void OnCollisionEnter2D(Collision2D col)
        {
            float y = HitFactor(transform.position,
                col.transform.position,
                col.collider.bounds.size.y);
            
            float x = col.relativeVelocity.x > 0 ? 1 : -1;
            
            Vector2 dir = new Vector2(x, y).normalized;

            // Set Velocity with dir * speed
#if UNITY_6000_0_OR_NEWER
                rigidbody2d.linearVelocity = dir * speed;
#else
            rigidbody2d.velocity = dir * speed;
#endif
        }
    }
    
}