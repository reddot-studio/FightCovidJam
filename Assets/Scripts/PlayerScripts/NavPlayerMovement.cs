using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NavPlayerMovement : MonoBehaviour
{

    public NavMeshAgent agent;
    Animator anim;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit))
            {
                agent.SetDestination(_hit.point);
            }
        }

        //if (Input.touchCount > 0)
        //{
        //    //If touching UI ignore touch ( UI still works )
        //    if (EventSystem.current.IsPointerOverGameObject())
        //        return;

        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Ray ray = cam.ScreenPointToRay(touch.position);
        //        RaycastHit _hit;

        //        if (Physics.Raycast(ray, out _hit))
        //        {
        //            agent.SetDestination(_hit.point);
        //        }
        //    }

        //}




        if (agent.velocity.magnitude > 0.3f)
        {
            anim.SetBool("isWalking", true);
        }
        if(agent.remainingDistance < 0.5f)
        {
            anim.SetBool("isWalking", false);
        }
    }
}
