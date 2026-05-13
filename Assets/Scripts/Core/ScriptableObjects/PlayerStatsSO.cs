using UnityEngine;

namespace Bully.Core
{
    [CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Bully/Player/Stats")]
    public class PlayerStatsSO : ScriptableObject
    {
        [Header("Anchoring Settings")]
        public int maxAnchoring = 100;
        public int startingAnchoring = 100;

        [Header("Movement Settings")]
        public float baseMoveSpeed = 5f;
        public float coweringSpeedMultiplier = 0f;
        
        [Header("Cowering Settings")]
        [Range(0, 1)] public float coweringDamageReduction = 0.3f; // 30% de réduction
        [Range(0, 1)] public float itemDropChanceOnHit = 0.2f;    // 20% de chance
    }
}