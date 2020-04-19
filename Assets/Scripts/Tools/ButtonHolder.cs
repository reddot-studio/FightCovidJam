using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHolder : MonoBehaviour
{
    public Cure buttonCure;


    public MeshRenderer render;


    public Material blue;
    public Material red;
    public Material green;


    public void ChangeState(bool correct)
    {
        if (correct)
        {
            render.material = green;
        }
        else
        {
            render.material = red;
        }

        StartCoroutine(changeToIdle());
    }

    IEnumerator changeToIdle()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        render.material = blue;
    }


}
