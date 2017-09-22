using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour
{

    public GameObject easyAudio, mediumAudio, hardAudio;

    void Start()
    {
        if (Application.loadedLevelName.Equals("Level"))
        {
            if (LevelMode.levelMode.Equals("Easy"))
            {
                easyAudio.GetComponent<AudioSource>().Play();
            }
            else if (LevelMode.levelMode.Equals("Medium"))
            {
                mediumAudio.GetComponent<AudioSource>().Play();
            }
            else
            {
                hardAudio.GetComponent<AudioSource>().Play();
            }
        }
    }

    // Fade e para a música
    public IEnumerator fadeMusic()
    {
        if (LevelMode.levelMode.Equals("Easy"))
        {
            while (easyAudio.GetComponent<AudioSource>().volume > 0)
            {
                easyAudio.GetComponent<AudioSource>().volume -= 0.05f;
                yield return new WaitForSeconds(0.5f);
            }
            easyAudio.GetComponent<AudioSource>().Stop();
        }
        if (LevelMode.levelMode.Equals("Medium"))
        {
            while (mediumAudio.GetComponent<AudioSource>().volume > 0)
            {
                mediumAudio.GetComponent<AudioSource>().volume -= 0.05f;
                yield return new WaitForSeconds(0.5f);
            }
            mediumAudio.GetComponent<AudioSource>().Stop();
        }
        if (LevelMode.levelMode.Equals("Hard"))
        {
            while (hardAudio.GetComponent<AudioSource>().volume > 0)
            {
                hardAudio.GetComponent<AudioSource>().volume -= 0.05f;
                yield return new WaitForSeconds(0.5f);
            }
            hardAudio.GetComponent<AudioSource>().Stop();
        }
    }

    public void stopAudios()
    {
        if (LevelMode.levelMode.Equals("Easy"))
        {
            easyAudio.GetComponent<AudioSource>().Stop();
        }
        else if (LevelMode.levelMode.Equals("Medium"))
        {
            mediumAudio.GetComponent<AudioSource>().Stop();
        }
        else
        {
            hardAudio.GetComponent<AudioSource>().Stop();
        }
    }

}
