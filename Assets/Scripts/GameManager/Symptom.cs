using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Symptom",menuName = "Symptom")]
public class Symptom : ScriptableObject
{
    public string symptom;
    public Cure cure;
    public bool isDone;

}


