using System.Collections;
using TMPro;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class Level3_Manager : MonoBehaviour
{
    public TextMeshProUGUI countdown;
    public ScreenManager ScreenManager;
    public bool startLevel3 = false;
    public bool windCharge = false;
    public bool blowing = false;
    public bool blowReset = false;
    public int timesBlown = 0;
    public bool levelDone = false;
    public bool level3Completed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown && startLevel3 == true)
        {
            startLevel3 = false;
            StartCoroutine(StartCountdown());
        }

        if (Input.anyKey == true && windCharge == true)
        {
            blowing = true;
        }
        else if (Input.anyKey == false && blowing == true)
        {
            blowing = false;
            windCharge = false;
            startLevel3 = true;
            timesBlown++;

            if (timesBlown >= 3)
            {
                startLevel3 = false;
                levelDone = true;
                StartCoroutine(LevelComplete());
            }
        }

        if (blowing == true)
        {
            countdown.text = "Blowing";
            blowReset = true;
        }
        else
        {
            if(blowReset == true && levelDone == false)
            {
                countdown.text = "";
                blowReset = false;
            }
        }
    }

    /*
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && startLevel3 == true)
        {
            startLevel3 = false;
            StartCoroutine(StartCountdown());
        }

        if (Input.GetKeyDown(KeyCode.Space) && windCharge == true)
        {
            blowing = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && windCharge == true)
        {
            blowing = false;
            windCharge = false;
            startLevel3 = true;
            timesBlown++;

            if (timesBlown >= 3)
            {
                startLevel3 = false;
                levelDone = true;
                StartCoroutine(LevelComplete());
            }
        }
    }
     * */

    public IEnumerator StartCountdown()
    {
        countdown.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "1";
        windCharge = true;
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Go!";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "";
    }

    public IEnumerator LevelComplete()
    {
        countdown.text = "Goed Gedaan!";
        yield return new WaitForSecondsRealtime(4);
        countdown.text = "";
        startLevel3 = false;
        windCharge = false;
        blowing = false;
        timesBlown = 0;
        levelDone = false;
        level3Completed = true;
        ScreenManager.ToLevelSelect();
    }
}
