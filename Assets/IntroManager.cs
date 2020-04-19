using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{

    public GameObject world;
    public GameObject intro;

    public GameObject introAnim;

    public static int state = 0;

    public static IntroManager introManager;

    public GameObject[] deactive;
    public GameObject[] active;

    public Animator des;

    private void Awake()
    {
        introManager = this;
    }

    private void OnEnable()
    {
        switch (state)
        {
            case 0:
                state++;
                break;

            case 1:
                state++;
                UIManager.RestartGame(2);
                break;

            case 2:

                intro.SetActive(false);
                world.SetActive(true);
                foreach (GameObject item in deactive)
                {
                    item.SetActive(false);
                }

                foreach (GameObject item in active)
                {
                    item.SetActive(true);
                }
                des.enabled = false;
                SkipManager.isEnabled = false;
                break;
        }

    }
}
