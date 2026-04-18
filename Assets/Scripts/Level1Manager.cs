// Level1Manager.cs
//
// Description:
// Manages the chompy minigame.
//
// Date of last amendment:
// 17/04/2026

using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class Level1_Manager : MonoBehaviour
{
    public TextMeshProUGUI countdown;
    public ScreenManager ScreenManager;
    public Collider2D Bug;
    public Animator bugAnimator;
    public Collider2D Berry;
    public Animator berryAnimator;
    public GameObject Wind;
    public Animator hoornAnimator;
    public Transform Leaf;
    public Transform Waypoint1;
    public Transform Waypoint2;
    public bool startLevel1 = false;
    public bool windCharge = false;
    public bool blowing = false;
    public int timesBlown = 0;
    public float speed = 0f;
    public float acc = 0.002f;
    public float fallSpeed = 0f;
    public float fallAcc = 0.002f;
    public bool levelDone = false;
    public bool level1Completed = false;

    void Start()
    {
        Wind.SetActive(false);
    }

    void Update()
    {
        berryAnimator.SetInteger("berryState", timesBlown);

        if (Input.anyKeyDown && startLevel1 == true)
        {
            startLevel1 = false;
            StartCoroutine(StartCountdown());
        }

        if (Input.anyKey == true && windCharge == true)
        {
            blowing = true;
            Wind.SetActive(true);
            hoornAnimator.SetBool("isBlowing", true);
        }
        else if (Input.anyKey == false && blowing == true)
        {
            blowing = false;
            Wind.SetActive(false);
            hoornAnimator.SetBool("isBlowing", false);
            windCharge = false;
            startLevel1 = true;
            timesBlown++;

            if (timesBlown >= 3)
            {
                startLevel1 = false;
                bugAnimator.SetBool("done", true);
                levelDone = true;
                StartCoroutine(LevelComplete());
            }
        }

        if (blowing == true)
        {
            Vector3 diff = Waypoint2.position - Leaf.position;
            Leaf.up = diff;

            fallSpeed = 0;
            fallAcc = 0.002f;

            if(speed < 8.0f)
            {
                speed = speed + acc;
                acc = acc * 1.0025f;
            }

            if (diff.magnitude > 0.1f)
            {
                Leaf.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                bugAnimator.SetBool("berryNearby", true);
            }
        }
        else
        {
            bugAnimator.SetBool("berryNearby", false);
            speed = 0;
            acc = 0.002f;

            Vector3 diff = Waypoint1.position - Leaf.position;
            Leaf.up = diff;

            if(fallSpeed < 10)
            {
                fallSpeed = fallSpeed + fallAcc;
                fallAcc = fallAcc * 1.0065f;
            }

            if (diff.magnitude > 0.1f)
            {
                Leaf.Translate(Vector3.up * fallSpeed * Time.deltaTime);
            }
            if (timesBlown > 0 || level1Completed == true)
            {
                Leaf.rotation *= Quaternion.Euler(180, 0, 0);
            }
        }
    }

    public IEnumerator StartCountdown()
    {
        countdown.text = "3";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "2";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "1";
        windCharge = true;
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Blaas!";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "";
    }

    public IEnumerator LevelComplete()
    {
        countdown.text = "Goed Gedaan!";
        yield return new WaitForSecondsRealtime(4);
        countdown.text = "";
        startLevel1 = false;
        windCharge = false;
        blowing = false;
        timesBlown = 0;
        levelDone = false;
        level1Completed = true;
        ScreenManager.ToLevelSelect();
    }
}
