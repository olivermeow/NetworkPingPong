using System.Collections.Generic;
using Gameplay.Types;
using UnityEngine;

namespace Gameplay.Views
{
    public class ScoreViewGroup : MonoBehaviour
    {
        [SerializeField] private List<ScoreView> scoreViews;
    
        public ScoreView GetByType(SideType sideType)
        {
            var view = scoreViews.Find(scoreView => scoreView.SideType == sideType);
            return view;
        }
    }
}