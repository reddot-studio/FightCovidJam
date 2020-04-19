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
    public bool gameOver;
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
    [HideInInspector]
    public float currentTimeToStart = 0f;

    public float timeToStart;
    public bool canPlay;

    public static bool tutorial = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        canPlay = false;

        currentTimeToStart = timeToStart;
        if(IntroManager.state == 1)
        {
            gameOver = true;
        }
        else if(IntroManager.state == 2)
        {
            gameOver = false;

        }

        currentTime = symptomRate;
        infectionSlider.maxValue = health;
        infectionSlider.value = 0;

        currentSpawns = 0;

        scoreText.text = "Score: 0";

        ResetTimeOut();
    }
    public void Update()
    {

        if(tutorial)
        {
            timeToStart = 0;
            currentTimeToStart = 0;
            canPlay = true;
        }


        //If gameOver disable update
        if (gameOver)
            return;
        if (currentTimeToStart > 0)
        {
            currentTimeToStart -= Time.deltaTime;
            if (currentTimeToStart <= 0)
            {
                canPlay = true;
                SFX.instance.PlayAudioClip(SFX.instance.background);
            }
            return;
        }
        //-------------------------///


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
                SFX.instance.PlayAudioClip(SFX.instance.incorrect);
            }
            else
            {
                timeToFail -= Time.deltaTime;
                if (symptomsPanel.transform.childCount > 0)
                {
                    GameObject lastSymptom = symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 1).gameObject;
                    lastSymptom.GetComponent<SymptomObject>().symptomTime = timeToFail / maxTimeToAnswer;
                }
            }
        }
      
    }

    void AddActiveSymptom()
    {
        int index = UnityEngine.Random.Range(0, availableSymptoms.Count);
        Symptom symptom = availableSymptoms[index];

        if (activeSymptoms.Count < 5)
        {

            activeSymptoms.Add(symptom);
            if(symptomsPanel.transform.childCount == 0)
            {
                ResetTimeOut();
            }
            GameObject instance = Instantiate(symptomPrefab,symptomsPanel.transform);       
            instance.transform.SetAsFirstSibling();

            SymptomObject symptomObject = instance.GetComponent<SymptomObject>();
            symptomObject.SetUp(symptom);

            if (symptomsPanel.transform.childCount > 0)
            {
                GameObject lastSymptom = symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 1).gameObject;
                lastSymptom.GetComponent<SymptomObject>().ChangeColor();
            }

            //Difficulty mec
            currentSpawns++;
            if(currentSpawns >= difficultyIncrement && difficultyLevel < maxDifficultyLevel)
            {
                difficultyLevel++;

                maxTimeToAnswer -= 0.5f;
                symptomRate -= 0.2f;

                currentSpawns = 0;
            }


           // Debug.Log("Added: " + symptom.symptom);

            if (!startTimer)
                startTimer = true;


            //Sound
            SFX.instance.PlayAudioClip(SFX.instance.new_task);

        }
    }



    public bool DoCure(Cure cure)
    {
        bool correct = false;
        if (activeSymptoms.Count > 0)
        {
            if (cure == activeSymptoms[0].cure)
            {
                IncrementScore(scoreForGoodAnswer);
                SFX.instance.PlayAudioClip(SFX.instance.correct);
                correct = true;
            }
            else
            {
                DecrementHealth(healthLossForWrongAnswer);
                SFX.instance.PlayAudioClip(SFX.instance.incorrect);
                correct = false;
            }

            ResetTimeOut();
            activeSymptoms.RemoveAt(0);
            if (symptomsPanel.transform.childCount > 0)
            {
                Destroy(symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 1).gameObject);
            }

            if (symptomsPanel.transform.childCount > 1)
            {
                GameObject lastSymptom = symptomsPanel.transform.GetChild(symptomsPanel.transform.childCount - 2).gameObject;
                lastSymptom.GetComponent<SymptomObject>().ChangeColor();
            }

        }
        return correct;
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
        infectionSlider.value += value;

        if(health <= 0)
        {
            onLose?.Invoke();
            gameOver = true;
        }

    }

    public void ClearHS()
    {
        PlayerPrefs.DeleteAll();
    }
}
