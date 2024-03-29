﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAngleChange : MonoBehaviour
{
    public float draggDistance = 20;
    public Animator animationCamera;
    Vector3 startPosition;

    public LayerMask raycastMask;

    bool rotate = false;
    public Text fpsText;
    float deltaTime = 0.0f;

    private void Start()
    {
        draggDistance = Screen.currentResolution.width / draggDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //If gameOver disable update
        if (GameManager.instance.gameOver)
            return;

        //---------------------//

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fpsText.text = (1.0f / deltaTime).ToString("F0");

        if (Input.GetMouseButtonDown(0) && !rotate)
        {
            startPosition = Input.mousePosition;
            rotate = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            rotate = false;
        }

        if (Input.GetMouseButton(0) && rotate)
        {
            if((Input.mousePosition - startPosition).magnitude >= draggDistance)
            {
                //Debug.Log((Input.mousePosition - startPosition));
                if((Input.mousePosition - startPosition).x < 0)
                {
                    switch (animationCamera.GetInteger("turnDirection"))
                    {
                        case 0:
                            animationCamera.SetInteger("turnDirection", 1);
                            break;

                        case 1:
                            break;

                        case 2:
                            animationCamera.SetInteger("turnDirection", 0);
                            break;
                    }
                }
                else
                {
                    switch (animationCamera.GetInteger("turnDirection"))
                    {
                        case 0:
                            animationCamera.SetInteger("turnDirection", 2);
                            break;

                        case 1:
                            animationCamera.SetInteger("turnDirection", 0);
                            break;

                        case 2:                
                            break;
                    }
                }
                //AUDIO
                SFX.instance.PlayAudioClip(SFX.instance.swipe);

                rotate = false;
            }
        }

        if (GameManager.instance.canPlay)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit _hit;

                if (Physics.Raycast(ray, out _hit, Mathf.Infinity, raycastMask))
                {
                    _hit.transform.gameObject.GetComponent<Animator>().SetBool("pushed", true);

                    bool answer = GameManager.instance.DoCure(_hit.transform.gameObject.GetComponent<ButtonHolder>().buttonCure);

                    _hit.transform.gameObject.GetComponent<ButtonHolder>().ChangeState(answer);

                }
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{

        //    switch (animationCamera.GetInteger("turnDirection"))
        //    {
        //        case 0:
        //            animationCamera.SetInteger("turnDirection", 1);
        //            break;

        //        case 1:
        //            animationCamera.SetInteger("turnDirection", 0);
        //            break;

        //        case 2:
        //            animationCamera.SetInteger("turnDirection", 0);
        //            break;
        //    }
        //}
    }
}
