using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{

    private GameObject SettingsMenu;
    private GameObject Menu;

    // Update is called once per frame

    private void Awake()
    {
        SettingsMenu = GameObject.Find("SettingsMenu");
        Menu = GameObject.Find("Menu");
    }
    private void Start()
    {
        Menu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Menu.activeSelf && !SettingsMenu.activeSelf)
        {
            Menu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Menu.activeSelf || Input.GetKeyDown(KeyCode.Escape) && SettingsMenu.activeSelf)
        {
            Menu.SetActive(false);  
            SettingsMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
