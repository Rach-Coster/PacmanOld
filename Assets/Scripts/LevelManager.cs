using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{ 
    // Start is called before the first frame update
    private void Start()
    {
    }
    private void Update()
    {
        
    }

    public void LevelOneScene()
    { 
        SceneManager.LoadSceneAsync("LevelOne");
    }

}
