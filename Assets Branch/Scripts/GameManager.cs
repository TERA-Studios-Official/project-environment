// (WIP) Tutorial2Manager.cs
//
// Description:
// Manages the main game functionalities.
//
// Author:
// t.teulings
//
// Date of last amendment:
// 03/04/2026

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial2Manager : MonoBehaviour
{
    public GameObject balloon;
    public GameObject panel;
    int wind;

    public AudioSource blowSound1;
    public AudioSource blowSound2;
    public AudioSource blowSound3;

    public SpriteRenderer renderer;
    public SpriteChanger sc;

    public GameObject confetti;
    public List<GameObject> allConfetti;

    private int balloonState;
    private bool readyForDialogue;

    private bool tutorial2 = false;
    void Start()
    {
        wind = 0;
        balloonState = 0;
        readyForDialogue = true;
    }

    void Update()
    {
        if (tutorial2) UpdateTutorial2();
    }

    public void StartTutorial2()
    {
        wind = 0;
        balloonState = 0;
        readyForDialogue = true;

        balloon.SetActive(true);

        tutorial2 = true;
    }

    public void UpdateTutorial2()
    {
        if (readyForDialogue)
            switch (balloonState)
            {
                case 0:
                    blowSound1.Play();
                    readyForDialogue = false;
                    break;

                case 1:
                    blowSound1.Stop();
                    blowSound2.Play();
                    readyForDialogue = false;
                    break;

                case 2:
                    blowSound2.Stop();
                    blowSound3.Play();
                    readyForDialogue = false;
                    break;

                default:
                    blowSound1.Stop();
                    blowSound2.Stop();
                    blowSound3.Stop();
                    readyForDialogue = false;
                    break;
            }

        if (wind == 1)
        {
            readyForDialogue = true;
            balloonState++;

            renderer.sprite = sc.ChangeSprite(balloonState);
        }

        if (Input.anyKeyDown && wind <= 0)
        {
            wind = 115;
        }

        if (wind > 0)
        {
            Vector3 change = new Vector3(0.002f, 0.002f, 0f);
            balloon.transform.localScale += change;

            wind--;
        }
        else if (balloonState == 3 && allConfetti.Count < 42)
        {
            CreateConfetti();
            panel.SetActive(true);
        }
    }

    public void CreateConfetti()
    {
        if (confetti == null)
        {
            Debug.LogError("Confetti prefab is not assigned in the Inspector!");
            return;
        }

        allConfetti.Add(Instantiate(confetti));
        allConfetti[allConfetti.Count - 1].name = $"Confetti_Clone{ allConfetti.Count - 1}";

        var renderer = allConfetti[allConfetti.Count - 1].GetComponent<Renderer>();
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
            
        allConfetti[allConfetti.Count - 1].SetActive(true);
        allConfetti[allConfetti.Count - 1].GetComponent<ConfettiManager>().Activate();
    }
}