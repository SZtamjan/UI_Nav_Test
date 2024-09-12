using NPCsSystems.Souls;

namespace GameEventSystem
{
    public static class GameEvents
    {
        public delegate void OnEnemyKilled(Enemy enemy);
        public static OnEnemyKilled EnemyKilled;
    }
}