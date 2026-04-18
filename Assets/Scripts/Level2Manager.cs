using System.Collections;
using TMPro;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class Level2_Manager : MonoBehaviour
{
    public TextMeshProUGUI countdown;
    public ScreenManager ScreenManager;
    public Animator Model1_Animator;
    public Animator Model2_Animator;
    public Animator Model3_Animator;
    public Transform Model1;
    public Transform Model2;
    public Transform Model3;
    public Transform Waypoint1;
    public Transform Waypoint2;
    public Transform Waypoint3;
    public bool startLevel2 = false;
    public bool windCharge = false;
    public bool blowing = false;
    public int timesBlown = 0;
    public bool levelDone = false;
    public bool level2Completed = false;
    
    public float speed_Model1 = 0f;
    public float speed_Model2 = 0f;
    public bool Model2_Ready = false;
    public float speed_Model3 = 0f;
    public bool Model3_Ready = false;

    public int Model1_State = 0;
    public int Model2_State = 0;
    public int Model3_State = 0;

    void Update()
    {

        if (Input.anyKeyDown && startLevel2 == true)
        {
            startLevel2 = false;
            StartCoroutine(StartCountdown());
        }

        if (Input.anyKey == true && windCharge == true)
        {
            blowing = true;

            if(timesBlown == 0)
            {
                Model1_Animator.SetInteger("Model1_State", 1);
                Model1_State = 1;
            }
            else if (timesBlown == 1)
            {
                Model2_Animator.SetInteger("Model2_State", 1);
                Model2_State = 1;
            }
            if (timesBlown == 2)
            {
                Model3_Animator.SetInteger("Model3_State", 1);
                Model3_State = 1;
            }
        }
        else if (Input.anyKey == false && blowing == true)
        {
            blowing = false;
            windCharge = false;
            startLevel2 = true;
            timesBlown++;

            if (timesBlown == 1)
            {
                Model1_Animator.SetInteger("Model1_State", 2);
                Model1_State = 2;
                Model2_State = -1;

            }
            if (timesBlown == 2)
            {
                Model2_Animator.SetInteger("Model2_State", 2);
                Model2_State = 2;
                Model3_State = -1;
            }
            if (timesBlown == 3)
            {
                Model3_Animator.SetInteger("Model3_State", 2);
                Model3_State = 2;
            }

            if (timesBlown >= 3)
            {
                startLevel2 = false;
                levelDone = true;
                StartCoroutine(LevelComplete());
            }
        }

        MoveModel1();
        MoveModel2();
        MoveModel3();
    }

    void MoveModel1()
    {        
        if(Model1_State == 0)
        {
            speed_Model1 = 0;
        }
        
        if (Model1_State == 2)
        {
            Vector3 diff = Waypoint3.position - Model1.position;
            Model1.right = diff;
            if (diff.magnitude > 0.1f)
            {
                if (speed_Model1 < 7)
                {
                    speed_Model1 = speed_Model1 + 0.0025f;
                }
                else
                {
                    speed_Model1 = 7;
                }

                Model1.Translate(Vector3.right * speed_Model1 * Time.deltaTime);
                Model1.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void MoveModel2()
    {
        if (Model2_State == 0)
        {
            speed_Model2 = 0f;
            Model2.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (Model2_State == 1 && Model2_Ready == true)
        {
            speed_Model2 = 0f;
            Model2.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (Model2_State == -1)
        {
            Vector3 diff = Waypoint2.position - Model2.position;
            Model2.right = diff;
            if (diff.magnitude > 0.1f)
            {
                if (speed_Model2 < 7f)
                {
                    speed_Model2 = speed_Model2 + 0.0025f;
                }
                else
                {
                    speed_Model2 = 7f;
                }

                Model2.Translate(Vector3.right * speed_Model2 * Time.deltaTime);
                Model2.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                Model2_Ready = true;
                Model2.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        else if (Model2_State == 2)
        {
            Vector3 diff = Waypoint3.position - Model2.position;
            Model2.right = diff;
            if (diff.magnitude > 0.1f)
            {
                if (speed_Model2 < 7f)
                {
                    speed_Model2 = speed_Model2 + 0.0025f;
                }
                else
                {
                    speed_Model2 = 7f;
                }

                Model2.Translate(Vector3.right * speed_Model2 * Time.deltaTime);
                Model2.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void MoveModel3()
    {
        if (Model3_State == 0)
        {
            speed_Model3 = 0;
            Model3.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Model3_State == 1 || Model3_Ready == true)
        {
            speed_Model3 = 0;
            Model3.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Model3_State == -1)
        {
            Vector3 diff = Waypoint2.position - Model3.position;
            Model3.right = diff;
            if (diff.magnitude > 0.1f)
            {
                if (speed_Model3 < 6)
                {
                    speed_Model3 = speed_Model3 + 0.0025f;
                }
                else
                {
                    speed_Model3 = 6;
                }

                Model3.Translate(Vector3.right * speed_Model3 * Time.deltaTime);
                Model3.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                Model3_Ready = true;
                Model3.rotation = Quaternion.Euler(0, 0, 0);
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
        startLevel2 = false;
        windCharge = false;
        blowing = false;
        timesBlown = 0;
        levelDone = false;
        level2Completed = true;
        ScreenManager.ToLevelSelect();

        Model1_Animator.SetInteger("Model1_State", 0);
        Model1_State = 0;
        Model1.position = Waypoint2.position;
        Model1.rotation = Quaternion.Euler(0, 0, 0);
        Model2_Animator.SetInteger("Model2_State", 0);
        Model2_State = 0;
        Model2.position = Waypoint1.position;
        Model2.rotation = Quaternion.Euler(0, 0, 0);
        Model2_Ready = false;
        Model3_Animator.SetInteger("Model3_State", 0);
        Model3_State = 0;
        Model3.position = Waypoint1.position;
        Model3.rotation = Quaternion.Euler(0, 0, 0);
        Model3_Ready = false;
    }
}