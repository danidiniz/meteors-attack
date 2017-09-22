using UnityEngine;
using System.Collections;

public class LevelMode : MonoBehaviour
{

    public static string levelMode;
    

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void _SetLevelMode(string level)
    {
        LevelMode.levelMode = level;
    }
}
