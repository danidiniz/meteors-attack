using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    GoogleMobileAdsDemoScript adInstance;
    public GameObject AdMob;

    public GameObject loadingPanel, loadingText;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (AdMob != null)
            adInstance = AdMob.GetComponent<GoogleMobileAdsDemoScript>();
    }

    public void _ChangeToScene(string name)
    {
        Application.LoadLevel(name);
    }

    public void _Restart()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    // Destrói a publicidade ao restartar ou ir para o menu
    public void _DestroyAd()
    {
        adInstance.destroyAd();
    }

    public void _Loading()
    {
        loadingPanel.SetActive(true);
        loadingText.SetActive(true);
        _DestroyAd();
    }
}
