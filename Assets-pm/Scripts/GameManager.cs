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

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject panel;
    public GameObject confetti;
    int wind;

    public AudioSource piraat1;
    public AudioSource piraat2;
    public AudioSource piraat3;

    public SpriteRenderer renderer;
    public SpriteChanger sc;

    public List<GameObject> all_confetti;

    private int shipState;
    private bool readyForDialogue;

    private bool tutorial2 = false;
    void Start()
    {
        wind = 0;
        shipState = 0;
        readyForDialogue = true;
    }

    void Update()
    {
        if (tutorial2) UpdateTutorial2();
    }

    public void UpdateTutorial2()
    {
        if (readyForDialogue)
            switch (shipState)
            {
                case 0:
                    //piraat1.Play();
                    readyForDialogue = false;
                    break;

                case 1:
                    piraat1.Stop();
                    //piraat2.Play();
                    readyForDialogue = false;
                    break;

                case 2:
                    piraat2.Stop();
                    //piraat3.Play();
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

            renderer.sprite = sc.ChangeSprite(shipState);
        }

        Debug.Log(Input.mousePosition);

        if (Input.anyKeyDown && wind <= 0)
        {
            wind = 115;
        }

        if (wind > 0)
        {
            Vector3 change = new Vector3(0.002f, 0.002f, 0f);
            ship.transform.localScale += change;

            wind--;
        }
        else if (shipState == 3)
        {
            CreateConfetti();
            panel.SetActive(true);
        }
    }

    public void Tutorial2()
    {
        tutorial2 = true;
    }

    public void CreateConfetti()
    {
        if (confetti == null)
        {
            Debug.LogError("Confetti prefab is not assigned in the Inspector!");
            return;
        }

        all_confetti.Add(Instantiate(confetti));
        all_confetti[all_confetti.Count - 1].name = $"Confetti_Clone{ all_confetti.Count - 1}";

        var renderer = all_confetti[all_confetti.Count - 1].GetComponent<Renderer>();
        if (renderer != null)
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    renderer.material.color = Color.red;
                    break;

                case 1:
                    renderer.material.color = Color.orange;
                    break;

                case 2:
                    renderer.material.color = Color.yellow;
                    break;

                case 3:
                    renderer.material.color = Color.green;
                    break;

                case 4:
                    renderer.material.color = Color.blue;
                    break;

            }
        }
            
        all_confetti[all_confetti.Count - 1].SetActive(true);
        all_confetti[all_confetti.Count - 1].GetComponent<ConfettiManager>().Activate();
    }
}