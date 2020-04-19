using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SymptomObject : MonoBehaviour
{
    public TextMeshProUGUI symptomText;
    public float symptomTime;

    public Image spriteRenderer;
    public Image timerFill;
    public Symptom symptom;

    private void Update()
    {
        timerFill.fillAmount = symptomTime;
    }

    public void SetUp(Symptom symp)
    {
        symptomTime = 1;
        symptom = symp;
        symptomText.text = symptom.symptom;
        
    }


    public void ChangeColor()
    {
        symptomText.color = Color.black;
        spriteRenderer.color = Color.white;
    }
}
