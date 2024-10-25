using System.Collections.Generic;
using Gameplay.Types;
using UnityEngine;

namespace Gameplay.Gate
{
    public class GateGroup : MonoBehaviour
    {
        [SerializeField] private List<Gate> gates;
    
        public Gate GetByType(SideType sideType)
        {
            var view = gates.Find(Gates => Gates.SideType == sideType);
            return view;
        }
    }
}