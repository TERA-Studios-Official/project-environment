// ScreenManager.cs
//
// Description:
// Manages screen actions.
//
// Date of last amendment:
// 17/04/2026

using System;
using UnityEngine;
using TMPro;

public class ScreenManager : MonoBehaviour
{
    public GameObject[] Screens;
    public Level1_Manager Level1_Manager;
    public Level2_Manager Level2_Manager;
    public Level3_Manager Level3_Manager;

    public void ToLevelSelect()
    {
        HideAllScreens();
        Screens[0].SetActive(true);
    }

    void HideAllScreens()
    {
        foreach(var screen in Screens)
        {
            screen.SetActive(false);
        }
    }

    public void Level1Start()
    {
        HideAllScreens();
        Screens[1].SetActive(true);
        Level1_Manager.startLevel1 = true;
    }

    public void Level2Start()
    {
        HideAllScreens();
        Screens[2].SetActive(true);
        Level2_Manager.startLevel2 = true;
    }

    public void Level3Start()
    {
        HideAllScreens();
        Screens[3].SetActive(true);
        Level3_Manager.startLevel3 = true;
    }
}
