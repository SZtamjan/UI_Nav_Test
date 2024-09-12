using Singleton;
using UnityEngine;

public enum GameLocalization
{
    SWAMPS,
    DUNGEON,
    CASTLE,
    CITY,
    TOWER
}

namespace Controllers
{
    public class GameController : Singleton<GameController>
    {
        [SerializeField] private GameLocalization currentGameLocalization;

        public GameLocalization CurrentGameLocalization
        {
            get => currentGameLocalization;

            set => currentGameLocalization = value;
        }

        private bool isPaused;

        public bool IsPaused
        {
            get => isPaused;
            set
            {
                isPaused = value;
                Time.timeScale = isPaused ? 0 : 1;
            }
        }

        public bool IsCurrentLocalization(GameLocalization localization)
        {
            return CurrentGameLocalization == localization;
        }
    }
}