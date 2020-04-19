using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public enum Cure
{
    TaparteBoca,
    LavarseManos,
    LlamarCentroSanitario,
    UsarMascarilla,
    EvitarGente,
    Aislart
}

public class GameManager : MonoBehaviour
{
    public GameObject symptomsPanel;
    public GameObject symptomPrefab;

    public static GameManager instance;

    //All symptoms available
    public List<Symptom> availableSymptoms;

    //Active symptoms
    public List<Symptom> activeSymptoms;


    [Header("Settings")]
    public float symptomRate;
    float currentTime;

    public int score = 0;
    public float health = 6;

    public int scoreForGoodAnswer = 100;

    public float healthLossForWrongAnswer = 1;
    public float healthLossForTimeOut = 0.5f;

    float timeToFail = 0;
    public float maxTimeToAnswer = 5;

    bool startTimer = false;

    public static Action onLose;

    public Slider infectionSlider;
    public TextMeshProUGUI scoreText;

    [Header("Difficulty settings")]
    public int difficultyLevel = 0;
    public int maxDifficultyLevel = 5;
    int currentSpawns = 0;
    //Synt added until we level up
    public int difficultyIncrement = 10;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    { 
        currentTime = symptomRate;
        infectionSlider.maxValue = health;
        infectionSlider.value = infectionSlider.maxValue;

        currentSpawns = 0;

        scoreText.text = "Score: 0";

        ResetTimeOut();
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

        if (startTimer)
        {
            if (timeToFail <= 0)
            {
                DecrementHealth(healthLossForTimeOut);
                activeSymptoms.RemoveAt(0);
                Destroy(symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 1).gameObject);
                if (symptomsPanel.transform.childCount > 1)
                {
                    GameObject lastSymptom = symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 2).gameObject;
                    lastSymptom.GetComponent<SymptomObject>().ChangeColor();
                }
                ResetTimeOut();
            }
            else
            {
                timeToFail -= Time.deltaTime;
                GameObject lastSymptom = symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 1).gameObject;
                lastSymptom.GetComponent<SymptomObject>().symptomTime = timeToFail / maxTimeToAnswer;
                //Debug.Log(timeToFail / maxTimeToAnswer);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoCure(Cure.LlamarCentroSanitario);
        }

        
    }

    void AddActiveSymptom()
    {
        int index = UnityEngine.Random.Range(0, availableSymptoms.Count);
        Symptom symptom = availableSymptoms[index];

        if (activeSymptoms.Count < 5)
        {

            activeSymptoms.Add(symptom);
            GameObject instance = Instantiate(symptomPrefab,symptomsPanel.transform);       
            instance.transform.SetAsFirstSibling();

            SymptomObject symptomObject = instance.GetComponent<SymptomObject>();
            symptomObject.SetUp(symptom);
            
            GameObject lastSymptom =  symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 1).gameObject;
            lastSymptom.GetComponent<SymptomObject>().ChangeColor();

            //Difficulty mec
            currentSpawns++;
            if(currentSpawns >= difficultyIncrement && difficultyLevel < maxDifficultyLevel)
            {
                difficultyLevel++;

                currentSpawns = 0;
            }


            Debug.Log("Added: " + symptom.symptom);

            if (!startTimer)
                startTimer = true;
        }
    }



    public void DoCure(Cure cure)
    {
        if(activeSymptoms.Count > 0)
        {
            if (cure == activeSymptoms[0].cure)
            {
                IncrementScore(scoreForGoodAnswer);
            }
            else
            {
                DecrementHealth(healthLossForWrongAnswer);
            }

            ResetTimeOut();
            activeSymptoms.RemoveAt(0);
            Destroy(symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 1).gameObject);
            if (symptomsPanel.transform.childCount > 1)
            {
                GameObject lastSymptom = symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 2).gameObject;
                lastSymptom.GetComponent<SymptomObject>().ChangeColor();
            }

        }
    }

    public void RestartScore()
    {
        score = 0;
    }

    public void ResetTimeOut()
    {
        timeToFail = maxTimeToAnswer;
    }

    public void IncrementScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }

    public void DecrementHealth(float value)
    {
        health -= value;
        infectionSlider.value = health;

        if(health <= 0)
        {
            onLose?.Invoke();
        }

    }
}
