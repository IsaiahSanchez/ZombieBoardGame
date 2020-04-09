using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startButtonText;
    [SerializeField] private GameObject deleteButton;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            startButtonText.text = "Continue";
            deleteButton.SetActive(true);
        }
        else
        {
            startButtonText.text = "Start";
            deleteButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        AudioManager.instance.playSound("knock", new Vector2(0, 0));
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        AudioManager.instance.playSound("knock", new Vector2(0, 0));
        Application.Quit();
    }

    public void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            AudioManager.instance.playSound("knock", new Vector2(0, 0));
            File.Delete(Application.persistentDataPath + "/gamesave.save");
            deleteButton.SetActive(false);
        }
    }
}
