using UnityEngine;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private RectTransform panel;


        private void Start()
        {
            UpdateSelectionIndicator(Vector2.zero, Vector2.zero, false);
        }
        
        private void OnEnable()
        {
            UnitManager.DrawSelectionBox += UpdateSelectionIndicator;
        }
        
        private void OnDisable()
        {
            UnitManager.DrawSelectionBox -= UpdateSelectionIndicator;
        }
        
        

        private void UpdateSelectionIndicator(Vector2 position1, Vector2 position2, bool show)
        {
            panel.gameObject.SetActive(show);
            if (!show) return;
            
            var min = new Vector2(Mathf.Min(position1.x, position2.x), Mathf.Min(position1.y, position2.y));
            var max = new Vector2(Mathf.Max(position1.x, position2.x), Mathf.Max(position1.y, position2.y));
            
            var newScale = max - min;
            var newPosition = min + newScale / 2;
            
            panel.position = newPosition;
            panel.sizeDelta = new Vector3(newScale.x, newScale.y);
            
        }
    }
}