using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCanvasManager : MonoBehaviour
{

    public GameObject toggleArrow, sliderRotation;

    public GameObject settingsPanel, shopPanel, player, heart;

    void Awake()
    {
        sliderRotation = GameObject.Find("Slider rotation speed");

        // Caso o rotation speed ainda não tenha sido setado (iniciou o game pela primeira vez, etc)
        if (PlayerPrefs.GetFloat("Rotation speed") == 0)
        {
            // Seta um valor
            PlayerPrefs.SetFloat("Rotation speed", 500.0f);
            sliderRotation.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Rotation speed");
            PlayerRotationCanvas.speedRotation = PlayerPrefs.GetFloat("Rotation speed");
        }
        else
        {
            sliderRotation.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Rotation speed");
            PlayerRotationCanvas.speedRotation = PlayerPrefs.GetFloat("Rotation speed");
        }
    }

    public void _OnToggleChanged()
    {

    }

    public void _SliderChange()
    {
        PlayerPrefs.SetFloat("Rotation speed", sliderRotation.GetComponent<Slider>().value);
        PlayerRotationCanvas.speedRotation = PlayerPrefs.GetFloat("Rotation speed");
    }

    public void _HideShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        heart.SetActive(!heart.activeSelf);
    }

    public void _HideSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        player.SetActive(!player.activeSelf);
    }

    public void _BuyHeart()
    {
        int playersHearts = PlayerPrefs.GetInt("playerLife");
        int playersStars = PlayerPrefs.GetInt("playerStars");

        if (playersStars >= 12000)
        {
            Debug.Log("Comprou um heart");
            PlayerPrefs.SetInt("playerStars", playersStars - 12000);
            PlayerPrefs.SetInt("playerLife", playersHearts + 1);
        }
    }
}
