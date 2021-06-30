using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;   

public class GameManager : MonoBehaviour
{
    [Header("生命系統")]
    public GameObject lifestm;

    [Header("選單")]
    public GameObject menu;

    [Header("裝備欄")]
    public GameObject eqi;

    public Animator ani;

    [Header("經驗值")]
    [Range(100,1000000000)]
    public int exp;

    [Header("金錢")]
    [Range(10, 100000)]
    public int coin;

    public int expmax = 44000;
    public int coinmax = 999999999;
    public int atk = 99999;
    public int def = 99999;
    public int maxdag = 999999999;

    public GameObject Endimage;
    private bool IsrevUI;


    public int killenemy;


    public bool menukey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }

    public bool hpbarkey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Tab);
        }
    }

    public bool eqikey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.I);
        }
    }


    private void End()
    {
        if (killenemy >= 3)
        {
            Endimage.SetActive(true);
            
        }
        

    }

    private void Start()
    {
        IsrevUI = false;
        killenemy = 0;


    }

    


    private void Update()
    {
        End();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!IsrevUI) 
            {
                lifestm.SetActive(true);
                IsrevUI = true;
            }
            else
            {
                lifestm.SetActive(false);
                IsrevUI = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            eqi.SetActive(true);
        }

    }








}
