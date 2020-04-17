using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cure
{
    LavadoManos,
    TaparseCara
}

public class GameManager : MonoBehaviour
{

    //All symptoms available
    public List<Task> symptomsLists;

    private void Awake()
    {
        symptomsLists = new List<Task>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
