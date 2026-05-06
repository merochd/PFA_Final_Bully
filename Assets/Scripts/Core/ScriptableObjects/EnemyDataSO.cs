using UnityEngine;

namespace Bully.Core
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "Bully/AI/EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        public float moveSpeed = 3.5f;
        public int attackDamage = 10;
        public float attackCooldown = 1.5f;
        public float detectionRange = 10f;
    }
}