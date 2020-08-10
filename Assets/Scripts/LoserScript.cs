using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoserScript : MonoBehaviour
{

    //from the update of the gridrunner 
    public GameObject panel;
    public GameObject Score;
    public GameObject ObjectHodler;
    public Button BacktomainMenu;
    // Start is called before the first frame update
    void Start()
    {
        BacktomainMenu.onClick.AddListener(MenuMenuMethod);
    }



    private void MenuMenuMethod()
    {
        GlobalRecords.GlobalRecordHolder.Instance.LocalScore = 0;
        SceneManager.LoadScene("Main Menu");
    }
    /// <summary>
    /// When the user loses.
    /// </summary>
   public void LosingScript()
    {

        ObjectHodler.SetActive(false);
        panel.SetActive(true);
    }
}
