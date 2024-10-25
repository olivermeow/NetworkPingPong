using System;
using Gameplay.Types;
using Mirror;
using UnityEngine;

namespace Gameplay.Gate
{
    public class Gate : NetworkBehaviour
    {
        [field: SerializeField] public SideType SideType;

        public event Action<SideType> Goooooal;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Ball.Ball>(out Ball.Ball ball))
            {
                Debug.Log("ball detected");
                Goooooal?.Invoke(SideType);
            }
        }
    }
}