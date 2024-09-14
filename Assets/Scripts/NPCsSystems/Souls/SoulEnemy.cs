using GameEventSystem;
using Interfaces;
using NPCsSystems.Enemies;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NPCsSystems.Souls
{
    public class SoulEnemy : MonoBehaviour , IEnemy
    {
        [FormerlySerializedAs("InteractionPanelObject")] [SerializeField] private GameObject interactionPanelObject;
        [FormerlySerializedAs("ActionsPanelObject")] [SerializeField] private GameObject actionsPanelObject;
        [FormerlySerializedAs("EnemySpriteRenderer")] [SerializeField] private SpriteRenderer enemySpriteRenderer;

        public GameObject InteractionPanelObjectProperty => interactionPanelObject;
        public GameObject ActionsPanelObjectProperty => actionsPanelObject;
        
        private SpawnPoint EnemyPosition;

   
        public void SetupEnemy(Sprite sprite,SpawnPoint spawnPoint)
        {
            enemySpriteRenderer.sprite = sprite;
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
            interactionPanelObject.SetActive(active);
        }

        private void ActiveActionPanel(bool active)
        {
            actionsPanelObject.SetActive(active);
        }

        private void SelectActionButton()
        {
            actionsPanelObject.transform.GetChild(0).GetComponent<Button>().Select();
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
            SelectActionButton();
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
}