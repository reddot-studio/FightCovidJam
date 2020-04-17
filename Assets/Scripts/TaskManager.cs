using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public GameObject taskPrefab;
    public List<Task> symptomsList;

    public List<Task> activeSymptoms;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in symptomsList)
        {
            Debug.Log(item.symptom);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Task item in symptomsList)
            {
                ActivateSymptom(item);
            }
        }
    }

    void ActivateSymptom(Task symptom)
    {
        activeSymptoms.Insert(0, symptom);
    }
}
