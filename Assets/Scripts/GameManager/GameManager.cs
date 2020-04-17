using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cure
{
    TaparteBoca,
    LavarseManos,
    LlamarCentroSanitario,
    UsarMascarilla,
    EvitarGente,
    Aislarte,
    NoTocarCara
}

public class GameManager : MonoBehaviour
{
    //All symptoms available
    public List<Symptom> availableSymptoms;

    //Active symptoms
    public List<Symptom> activeSymptoms;

   
    private void Start()
    {
        InvokeRepeating("AddActiveSymptom", 0f, 5f);
    }


    void AddActiveSymptom()
    {
        int index = Random.Range(0, availableSymptoms.Count);
        Debug.Log(availableSymptoms.Count);
        Symptom symptom = availableSymptoms[index];

        activeSymptoms.Insert(0, symptom);
        Debug.Log("Added: " + symptom.symptom);

    }
}
