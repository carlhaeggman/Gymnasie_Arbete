using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{

    private GameObject SettingsMenu;
    private GameObject Menu;

    // Update is called once per frame
    private void Start()
    {
        SettingsMenu = GameObject.Find("SettingsMenu");
        Menu = GameObject.Find("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !Menu.activeSelf)
        {
            Menu.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && Menu.activeSelf || Input.GetKeyDown(KeyCode.Escape) && SettingsMenu.activeSelf)
        {
            Menu.SetActive(false);
            SettingsMenu.SetActive(false);
        }
    }
}
