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

    public float symptomRate;
    float currentTime;

    private void Start()
    {
        currentTime = symptomRate;
    }
    public void Update()
    {
        //Create symptom X time
        currentTime -= Time.deltaTime;
        if(currentTime < 0.0f)
        {
            AddActiveSymptom();
            currentTime = symptomRate;
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoCure(Cure.LlamarCentroSanitario);
        }
    }

    void AddActiveSymptom()
    {
        int index = Random.Range(0, availableSymptoms.Count);
        Symptom symptom = availableSymptoms[index];
        activeSymptoms.Add(symptom);
        Debug.Log("Added: " + symptom.symptom);

    }



    public void DoCure(Cure cure)
    {
        if(cure == activeSymptoms[0].cure)
        {
            activeSymptoms.RemoveAt(0);
        }
    }
}
