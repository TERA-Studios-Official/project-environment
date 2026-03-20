// (WIP) GameManager.cs
//
// Description:
// Manages the main game functionalities.
//
// Author:
// t.teulings
//
// Date of last amendment:
// 18/03/2026

using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject panel;
    int wind;

    public AudioSource piraat1;
    public AudioSource piraat2;
    public AudioSource piraat3;

    private int shipState;
    private bool readyForDialogue;

    void Start()
    {
        wind = 0;
        shipState = 0;
        readyForDialogue = true;
    }

    void Update()
    {
        if (readyForDialogue)
            switch(shipState)
            {
                case 0:
                    piraat1.Play();
                    readyForDialogue = false;
                    break;

                case 1:
                    piraat1.Stop();
                    piraat2.Play();
                    readyForDialogue = false;
                    break;

                case 2:
                    piraat2.Stop();
                    piraat3.Play();
                    readyForDialogue = false;
                    break;

                default:
                    piraat1.Stop();
                    piraat2.Stop();
                    piraat3.Stop();
                    readyForDialogue = false;
                    break;
            }

        if (wind == 1)
        {
            readyForDialogue = true;
            shipState++;
        }

        Debug.Log(Input.mousePosition);

        if (Input.anyKeyDown && wind == 0)
        {
            wind = 230;
        }

        if (wind > 0)
        {
            Vector3 change = new Vector3(0.02f, 0f, 0f);
            ship.transform.position += change;

            wind--;
        }
        else if (ship.transform.position.x >= 7f)
        {
            panel.SetActive(true);
        }
    }
}