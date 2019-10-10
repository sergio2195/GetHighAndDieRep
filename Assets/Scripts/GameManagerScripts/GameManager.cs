using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int actualLevel;
    public int actualWorld;

    public static int enemiesKilled = 0;
    public static int playerLife = 1;

    void Awake(){
        if(instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        actualLevel = 0;
        actualWorld = 1;
    }
}
