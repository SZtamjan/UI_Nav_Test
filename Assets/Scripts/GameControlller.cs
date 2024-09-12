using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameLocalization
{
    SWAMPS,
    DUNGEON,
    CASTLE,
    CITY,
    TOWER
}

public class GameControlller : MonoBehaviour
{
    #region Singleton
    private static GameControlller instance;

    public static GameControlller Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameControlller>();
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private GameLocalization currentGameLocalization;

    public GameLocalization CurrentGameLocalization
    {
        get
        {
            return currentGameLocalization;
        }

        set
        {
            currentGameLocalization = value;
        }
    }

    private bool isPaused;

    public bool IsPaused
    {

        get
        {
            return isPaused;
        }
        set
        {
            isPaused = value;
            if (isPaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
    


    public bool IsCurrentLocalization(GameLocalization localiztion)
    {
        return CurrentGameLocalization == localiztion;
    }
 
}


