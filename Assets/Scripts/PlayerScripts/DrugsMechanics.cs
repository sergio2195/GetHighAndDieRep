﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsMechanics : MonoBehaviour
{
    private bool drugActive;
    private float speedDrug, speedNormal, cocaineJump, jumpNormal;
    [SerializeField] private float methDelay = 1f;
    private bool speedActive, cocaineActive, hashActive, methActive;

    private Animator anim;
    [SerializeField] private float timeDrugActive = 5f, timeHashActive = 1f;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject panelColorDrugs;


    [SerializeField]
    private float restAmountCocaine = 0.125f, restAmountHash = 0.125f, restAmountSpeed = 0.125f, restAmountMeth = 0.125f;
    private float restAmount = 0.125f;
    private bool restLife;

    [SerializeField]
    private Camera mainCamera;
    private bool makeRipple = false;
    private bool changeColorPanelCocaine = false;
    private bool changeColorPanelHash = false;
    private bool changeColorPanelMeth = false;
    private bool changeColorPanelSpeed = false;

    //DASH VARAIABLES
    [SerializeField]
    float speed, delay = 0.05f;

    private RaycastHit2D hit;
    [SerializeField]
    private LayerMask groundLayer;
    Vector3 direction = Vector3.zero;

    void Start()
    {
        drugActive = false;
        speedDrug = GetComponent<playerMovement>().GetMovementSpeed() * 2;
        speedNormal = GetComponent<playerMovement>().GetMovementSpeed();

        cocaineJump = GetComponent<playerMovement>().GetJumpVel() * 1.5f;
        jumpNormal = GetComponent<playerMovement>().GetJumpVel();

        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (makeRipple)
        {
            mainCamera.GetComponent<RipplePostProcessor>().MakeRipple();
        }

        if (changeColorPanelCocaine)
        {
            panelColorDrugs.GetComponent<ChangeColorPanel>().ChangeColorCocaine();
        }

        else if (changeColorPanelSpeed)
        {
            panelColorDrugs.GetComponent<ChangeColorPanel>().ChangeColorSpeed();
        }


        else if (changeColorPanelMeth)
        {
            panelColorDrugs.GetComponent<ChangeColorPanel>().ChangeColorMeth();
        }


        else if (changeColorPanelHash)
        {
            panelColorDrugs.GetComponent<ChangeColorPanel>().ChangeColorHash();
        }

        else
            panelColorDrugs.GetComponent<ChangeColorPanel>().SetActivePanelFalse();


        if (Input.GetAxis("Cocaina") != 0 && !cocaineActive)
        {
            cocaineActive = true;
            StartCoroutine(cocaineAnim());
            restLife = true;
            canvas.GetComponent<PlayerManager>().RestAmount(restAmountCocaine);
        }

        if (Input.GetAxis("Hash") != 0 && !hashActive)
        {

            hashActive = true;
            StartCoroutine(hashAnim());
            restLife = true;
            canvas.GetComponent<PlayerManager>().RestAmount(restAmountHash);
        }

        if (Input.GetAxis("Speed") != 0 && !speedActive)
        {
            speedActive = true;
            StartCoroutine(speedAnim());
            restLife = true;
            canvas.GetComponent<PlayerManager>().RestAmount(restAmountSpeed);
        }

        if (Input.GetAxis("Meth") != 0 && !methActive && (GetComponent<playerMovement>().horizontalMove > 0 || GetComponent<playerMovement>().horizontalMove < 0)) 
        {
            methActive = true;
            StartCoroutine(MethAnim());
        }

        if (cocaineActive || hashActive || methActive || speedActive)
            drugActive = true;

        else
        {
            drugActive = false;
        }

        if (restLife)
            canvas.GetComponent<PlayerManager>().RestLife();

        if (canvas.GetComponent<PlayerManager>().GetLifeBarRest() != canvas.GetComponent<PlayerManager>().GetLifeBarRest() - restAmount)
        {
            restLife = !restLife;
        }

    }

    public IEnumerator StartSpeed()
    {
        GetComponent<playerMovement>().SetMovementSpeed(speedDrug);
        yield return new WaitForSeconds(timeDrugActive);
        speedActive = false;
        GetComponent<playerMovement>().SetMovementSpeed(speedNormal);
    }

    public IEnumerator speedAnim()
    {
        anim.SetBool("isCristal", true);
        makeRipple = !makeRipple;
        changeColorPanelSpeed = !changeColorPanelSpeed;
        yield return new WaitForSeconds(1.06f);
        makeRipple = !makeRipple;
        changeColorPanelSpeed = !changeColorPanelSpeed;
        anim.SetBool("isCristal", false);

        StartCoroutine(StartSpeed());
    }

    public IEnumerator StartCocaine()
    {
        GetComponent<playerMovement>().SetJumpVel(cocaineJump);
        yield return new WaitForSeconds(timeDrugActive);
        GetComponent<playerMovement>().SetJumpVel(jumpNormal);
        cocaineActive = false;
    }

    public IEnumerator cocaineAnim()
    {
        anim.SetBool("isCocaine", true);
        makeRipple = !makeRipple;
        changeColorPanelCocaine = !changeColorPanelCocaine;
        yield return new WaitForSeconds(1f);
        anim.SetBool("isCocaine", false);
        makeRipple = !makeRipple;
        changeColorPanelCocaine = !changeColorPanelCocaine;

        StartCoroutine(StartCocaine());
    }

    public IEnumerator StartHash()
    {
        TimeManager.DoSlowMotion(true);
        yield return new WaitForSeconds(timeHashActive);
        TimeManager.DoSlowMotion(false);

        hashActive = false;
    }

    public IEnumerator hashAnim()
    {
        anim.SetBool("isSmoking", true);
        makeRipple = !makeRipple;
        changeColorPanelHash = !changeColorPanelHash;
        yield return new WaitForSeconds(1f);
        makeRipple = !makeRipple;
        changeColorPanelHash = !changeColorPanelHash;
        anim.SetBool("isSmoking", false);

        StartCoroutine(StartHash());
    }

    public IEnumerator StartMeth()
    {
        dashMeth();
        yield return new WaitForSeconds(methDelay);
        methActive = false;
        changeColorPanelMeth = false;
        makeRipple = false;
    }

    public IEnumerator MethAnim()
    {
        anim.SetBool("isCristal", true);
        yield return new WaitForSeconds(.1f);
        anim.SetBool("isCristal", false);
        StartCoroutine(StartMeth());
    }

   private void dashMeth()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (direction != Vector3.zero)
        {
            detectWallDash(direction);
        }
    }

   private void detectWallDash(Vector3 dir)
    {
        hit = Physics2D.Raycast(transform.position + dir * 10f, dir, 0.01f, groundLayer);
        if (hit.collider == null)
        {
            changeColorPanelMeth = true;
            makeRipple = true;
            restLife = true;
            canvas.GetComponent<PlayerManager>().RestAmount(restAmountMeth);
            Vector3 offset = new Vector3(transform.position.x + 10f * dir.x, transform.position.y, transform.position.z);
            transform.position = offset;
        }
    }

    public bool GetDrugActive()
    {
        return drugActive;
    }

    public bool GetMethActive()
    {
        return methActive;
    }

    public bool GetCocaineActive()
    {
        return cocaineActive;
    }

    public bool GetHashActive()
    {
        return hashActive;
    }

    public bool GetSpeedActive()
    {
        return speedActive;
    }

    public float GetTimeDrugActive()
    {
        return timeDrugActive;
    }

}