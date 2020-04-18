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
    public Symptom symptom;

    public void SetUp(Symptom symp)
    {
        symptom = symp;
        symptomText.text = symptom.symptom;
    }

    public void ChangeColor()
    {
        symptomText.color = Color.black;
        spriteRenderer.color = Color.white;
    }
}
