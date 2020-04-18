using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SymptomObject : MonoBehaviour
{
    public TextMeshProUGUI symptomText;
    public float symptomTime;

    public Symptom symptom;

    public void SetUp(Symptom symp)
    {
        symptom = symp;
        symptomText.text = symptom.symptom;
    }
}
