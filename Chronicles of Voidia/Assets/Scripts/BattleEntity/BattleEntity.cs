using System;
using Interfaces;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace BattleEntity
{
    public class BattleEntity : MonoBehaviour, ISelectable
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject selectionIndicator;
        
        //[Header("Settings")]
        public static event Action<BattleEntity> OnBattleEntitySelected;
        public static event Action<BattleEntity> OnBattleEntityDeselected;

        private void Start()
        {
            Init();
        }
        
        protected virtual void Init()
        {
            Deselect();
        }

        private void Update()
        {
            UpdateAction();
        }

        protected virtual void UpdateAction()
        {
        }

        public void Select()
        {
            if (selectionIndicator != null) selectionIndicator.SetActive(true);
            OnBattleEntitySelected?.Invoke(this);
        }

        public void Deselect()
        {
            if (selectionIndicator != null) selectionIndicator.SetActive(false);
            OnBattleEntityDeselected?.Invoke(this);
        }
    }
    
    public class Target
    {
        public Vector3 Point { get; }
        public BattleEntity Entity { get; }
        
        public bool IsPoint => Entity == null;
        public Vector3 Position => IsPoint ? Point : Entity.transform.position;
        
        public Target(Vector3 point)
        {
            Point = point;
        }
        
        public Target(UnitVisual entity)
        {
            Entity = entity;
        }
    }
}