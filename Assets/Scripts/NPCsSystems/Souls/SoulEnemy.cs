using GameEventSystem;
using NPCsSystems.Enemies;
using UnityEngine;

namespace NPCsSystems.Souls
{
    public class SoulEnemy : MonoBehaviour , Enemy
    {
        [SerializeField] private GameObject InteractionPanelObject;
        [SerializeField] private GameObject ActionsPanelObject;
        [SerializeField] private SpriteRenderer EnemySpriteRenderer;

        public GameObject InteractionPanelObjectProperty => InteractionPanelObject;
        public GameObject ActionsPanelObjectProperty => ActionsPanelObject;
        
        private SpawnPoint EnemyPosition;

   
        public void SetupEnemy(Sprite sprite,SpawnPoint spawnPoint)
        {
            EnemySpriteRenderer.sprite = sprite;
            EnemyPosition = spawnPoint;
            gameObject.SetActive(true);
        }

        public SpawnPoint GetEnemyPosition()
        {
            return EnemyPosition;
        }

        public GameObject GetEnemyObject()
        {
            return this.gameObject;
        }

        private void ActiveCombatWithEnemy()
        {
            ActiveInteractionPanel(false);
            ActiveActionPanel(true);
        }

        private void ActiveInteractionPanel(bool active)
        {
            InteractionPanelObject.SetActive(active);
        }

        private void ActiveActionPanel(bool active)
        {
            ActionsPanelObject.SetActive(active);
        }

        private void UseBow()
        {
            // USE BOW
            GameEvents.EnemyKilled?.Invoke(this);
        }

        private void UseSword()
        {
            GameEvents.EnemyKilled?.Invoke(this);
            // USE SWORD
        }
        
        #region OnClicks
        public void Combat_OnClick()
        {
            ActiveCombatWithEnemy();
        }

        public void Bow_OnClick()
        {
            UseBow();
        }

        public void Sword_OnClick()
        {
            UseSword();
        }
        #endregion


    }


    public interface Enemy
    {
        SpawnPoint GetEnemyPosition();
        GameObject GetEnemyObject();
    }
}