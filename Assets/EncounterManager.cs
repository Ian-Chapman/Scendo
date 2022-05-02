using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterAIEncounter()
    {
        SceneManager.LoadScene("EncounterScene");
    }

    public void ExitAIEncounter()
    {
        SceneManager.LoadScene("Overworld"); //instead of doing this, need to load in players position and the overworld
    }
}
