using System;
using Interfaces;
using UnityEngine;

namespace BattleEntity
{
    public class BattleEntity : MonoBehaviour, ISelectable
    {
        [Header("Components")]
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject selectionIndicator;
        [SerializeField] private Renderer selectionRenderer;

        [field: Header("Settings")]
        [field: SerializeField] public Teams Team { get; private set; } = Teams.Neutral;
        public static event Action<BattleEntity> OnBattleEntitySelected;
        public static event Action<BattleEntity> OnBattleEntityDeselected;
        public static event Action<BattleEntity> OnBattleEntityDestroyed;
        
        public event Action<BattleEntity> BattleEntityDestroyed; 

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
            UpdateTeamColor();
            if (selectionIndicator != null) selectionIndicator.SetActive(true);
            OnBattleEntitySelected?.Invoke(this);
        }

        public void Deselect()
        {
            if (selectionIndicator != null) selectionIndicator.SetActive(false);
            OnBattleEntityDeselected?.Invoke(this);
        }

        protected virtual void UpdateTeamColor()
        {
            selectionRenderer.material.color = Team switch
            {
                Teams.Player => Color.black,
                Teams.Enemy => Color.red,
                Teams.Neutral => Color.white,
                Teams.Ally => Color.green,
                _ => Color.white
            };
        }

        public void Destroy()
        {
            OnBattleEntityDestroyed?.Invoke(this);
            BattleEntityDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
        
        public static bool IsEnemy(Teams team1, Teams team2)
        {
            return team1 != team2 && team1 != Teams.Neutral && team2 != Teams.Neutral && !IsAlly(team1, team2);
        }
        
        public static bool IsAlly(Teams team1, Teams team2)
        {
            return team1 == team2 || (team1 == Teams.Player && team2 == Teams.Ally) || (team1 == Teams.Ally && team2 == Teams.Player);
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
    
    public enum Teams
    {
        Player = 0,
        Enemy,
        Neutral,
        Ally,
    }
}