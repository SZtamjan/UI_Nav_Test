using NPCsSystems.Enemies;
using UnityEngine;

namespace Interfaces
{
    public interface IEnemy
    {
        SpawnPoint GetEnemyPosition();
        GameObject GetEnemyObject();
    }
}