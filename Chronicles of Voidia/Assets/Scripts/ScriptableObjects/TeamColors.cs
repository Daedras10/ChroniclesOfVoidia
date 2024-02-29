using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class TeamColors : ScriptableObject
    {
        public static TeamColors Instance { get; private set; }
        
        [SerializeField] private List<Color> colors;
        
        public IReadOnlyList<Color> Colors => colors;
        
    }
}