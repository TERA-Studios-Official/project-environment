using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;
using UnityEngine.UIElements;

public class Level3_Manager : MonoBehaviour
{
    public TextMeshProUGUI countdown;
    public ScreenManager ScreenManager;
    public Animator boatAnimator;
    public Transform Boat;
    public Animator waterAnimator;
    public Transform Sea1;
    public Transform Sea2;
    public Transform Island;
    public Transform Waypoint1;
    public Transform Waypoint2;
    public Transform Waypoint3;
    public Transform Waypoint4;
    public Transform Waypoint5;
    public bool startLevel3 = false;
    public bool windCharge = false;
    public bool blowing = false;
    public bool blowReset = false;
    public int timesBlown = 0;
    public bool levelDone = false;
    public bool level3Completed = false;

    public float speed = 0f;
    public float speedMax = 4f;
    public float acc = 0.0025f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedMax = 4f;
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
            boatAnimator.SetBool("isBlowing", true);
            waterAnimator.SetBool("isBlowing", true);
        }
        else if (Input.anyKey == false && blowing == true)
        {
            blowing = false;
            boatAnimator.SetBool("isBlowing", false);
            waterAnimator.SetBool("isBlowing", false);
            Boat.rotation = Quaternion.Euler(0, 0, 0);
            windCharge = false;
            startLevel3 = true;
            timesBlown++;

            if (timesBlown >= 3)
            {
                startLevel3 = false;
                levelDone = true;
            }
        }

        if(levelDone == true)
        {
            MoveIsland();
            blowing = true;
            boatAnimator.SetBool("isBlowing", true);
            waterAnimator.SetBool("isBlowing", true);
        }

        if (blowing == true)
        {
            blowReset = true;
            acc = 0.0025f;

        }
        else
        {
            speed = 0f;

            if (blowReset == true && levelDone == false)
            {
                blowReset = false;
            }
        }

        MoveSea1();
        MoveSea2();
    }

    void MoveSea1()
    {
        Vector3 diff = Waypoint2.position - Sea1.position;
        Sea1.up = diff;
        if (diff.magnitude > 0.1f)
        {
            if (speed < speedMax)
            {
                speed = speed + acc;
            }
            else
            {
                speed = speedMax;
            }

            Sea1.Translate(Vector3.up * speed * Time.deltaTime);
            Sea1.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Sea1.position = Waypoint1.position;
        }
    }

    void MoveSea2()
    {
        Vector3 diff = Waypoint2.position - Sea2.position;
        Sea2.up = diff;
        if (diff.magnitude > 0.1f)
        {
            if (speed < speedMax)
            {
                speed = speed + acc;
            }
            else
            {
                speed = speedMax;
            }

            Sea2.Translate(Vector3.up * speed * Time.deltaTime);
            Sea2.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Sea2.position = Waypoint1.position;
        }
    }

    void MoveIsland()
    {
        Vector3 diff = Waypoint3.position - Island.position;
        Island.up = diff;
        if (diff.magnitude > 0.1f)
        {
            if (speed < speedMax)
            {
                speed = speed + acc;
            }
            else
            {
                speed = speedMax;
            }

            Island.Translate(Vector3.up * speed * Time.deltaTime);
            Island.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            speed = 0f;
            StartCoroutine(LevelComplete());
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
        startLevel3 = false;
        windCharge = false;
        blowing = false;
        timesBlown = 0;
        levelDone = false;
        level3Completed = true;
        ScreenManager.ToLevelSelect();

        Sea1.position = Waypoint4.position;
        Sea2.position = Waypoint2.position;
        Island.position = Waypoint5.position;
        Island.rotation = Quaternion.Euler(0, 0, 0);
    }
}
