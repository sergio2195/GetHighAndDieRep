using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeAnim : MonoBehaviour
{
    private GameObject player;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player.GetComponent<DrugsMechanics>().GetMethActive())
            anim.SetBool("meth", true);
        else if (player.GetComponent<DrugsMechanics>().GetHashActive())
            anim.SetBool("hash", true);
        else if (player.GetComponent<DrugsMechanics>().GetCocaineActive())
            anim.SetBool("cocaine", true);
        else if (player.GetComponent<DrugsMechanics>().GetSpeedActive())
            anim.SetBool("speed", true);
    }
}
