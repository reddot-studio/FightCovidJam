using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleChange : MonoBehaviour
{
    public float draggDistance = 20;
    public Animator animationCamera;
    Vector3 startPosition;

    public LayerMask raycastMask;

    bool rotate = false;

    // Update is called once per frame
    void Update()
    {

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
                if((Input.mousePosition - startPosition).x > 0)
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

                rotate = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, raycastMask))
            {
                _hit.transform.gameObject.GetComponent<Animator>().SetBool("pushed", true);
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
