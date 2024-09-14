using Interfaces;
using NPCsSystems.Souls;

namespace GameEventSystem
{
    public static class GameEvents
    {
        public delegate void OnEnemyKilled(IEnemy enemy);
        public static OnEnemyKilled EnemyKilled;
    }
}