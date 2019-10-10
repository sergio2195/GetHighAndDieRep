using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperScript : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("DarkDefeat");
    }
}
