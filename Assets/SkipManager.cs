using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipManager : MonoBehaviour
{

    [Header("Skip Settings")]
    public int numOfTabs = 0;
    public int maxNumOfTabs = 2;

    public float timeToTap = 3;
    float timer;

    public static bool isEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!isEnabled)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(numOfTabs < maxNumOfTabs)
                numOfTabs++;

            if (numOfTabs == 1)
            {
                timer = timeToTap;
            }
            if (numOfTabs >= maxNumOfTabs)
            {
                //SkipScene
                Debug.Log("Skip");

                IntroManager.state = 2;
                isEnabled = false;
                UIManager.RestartGame(2);

            }
        }

        if (numOfTabs > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                numOfTabs = 0;
            }
        }
    }
}
