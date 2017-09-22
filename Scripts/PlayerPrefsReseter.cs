using UnityEngine;
using System.Collections;

public class PlayerPrefsReseter : MonoBehaviour
{

    public bool reset;

    void Awake()
    {
        if (reset)
            PlayerPrefs.DeleteAll();
    }
}