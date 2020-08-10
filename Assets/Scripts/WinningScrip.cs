﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinningScrip : MonoBehaviour
{
    //from the update of the gridrunner 
    public GameObject panel;
    public GameObject Score;
    public GameObject ObjectHodler;
    public Button BacktomainMenu;

    public void Start()
    {
        BacktomainMenu.onClick.AddListener(MenuMenuMethod);


    }

  /// <summary>
  /// When the user wins
  /// </summary>
    public void RunWinning()
        
    {
        ObjectHodler.SetActive(false);
        panel.SetActive(true);
        Score.GetComponent<TMPro.TextMeshPro>().text = "Lcoal Score::"+GlobalRecords.GlobalRecordHolder.Instance.LocalScore.ToString()+"\n"+
                                                                      "Global Score::" + GlobalRecords.GlobalRecordHolder.Instance.GlobalScore.ToString() + "\n";

    }

    private void MenuMenuMethod()
    {
        GlobalRecords.GlobalRecordHolder.Instance.LocalScore = 0;
        SceneManager.LoadScene("Main Menu");
    }
}
