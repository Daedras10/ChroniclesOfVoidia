using UnityEngine;

namespace DefaultNamespace
{
    public class DebugLineRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        private GameObject start;
        private GameObject end;
        
        public void Init(GameObject start, GameObject end)
        {
            this.start = start;
            this.end = end;
        }
        
        private void Update()
        {
            var isValid = start != null && end != null;
            lineRenderer.enabled = isValid;
            
            if (!isValid) return;
            
            var from = start.transform.position;
            var to = end.transform.position;
            
            // var yOffSet = 1f;
            // from.y += yOffSet;
            // to.y += yOffSet;
            from.y = 1.35f;
            to.y = 1.35f;
            
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, from);
            lineRenderer.SetPosition(1, to);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}