using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    
    public static LevelSelector Instance;
   

    //buttons 
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;

    //public Game objects

    public GameObject ImageLevel1;
    public GameObject ImageLevel2;
    public GameObject ImageLevel3;
    public GameObject ImageLevel4;
    public GameObject ImageLevel5;

    //public Text Assets 

    public TextAsset text1;
    public TextAsset text2;
    public TextAsset text3;
    public TextAsset text4;
    public TextAsset text5;



    public TMPro.TextMeshProUGUI Scoretext;
    




    private void Start()
    {



        level1.onClick.AddListener(HelloLevel1);
        level2.onClick.AddListener(HelloLevel2);
        level3.onClick.AddListener(HelloLevel3);
        level4.onClick.AddListener(HelloLevel4);
        level5.onClick.AddListener(HelloLevel5);



    }


    public void HelloLevel1()
    {
        GlobalRecords.GlobalRecordHolder.Instance.LevelSelection = 1;
        GlobalRecords.GlobalRecordHolder.Instance.LoadmyScene(text1);
        SceneManager.LoadScene("GameScene");
    }
    public void HelloLevel2()
    {
        GlobalRecords.GlobalRecordHolder.Instance.LevelSelection = 2;
        GlobalRecords.GlobalRecordHolder.Instance.LoadmyScene(text2);
        SceneManager.LoadScene("GameScene");

    }
    public void HelloLevel3()
    {
        GlobalRecords.GlobalRecordHolder.Instance.LevelSelection = 3;
        GlobalRecords.GlobalRecordHolder.Instance.LoadmyScene(text3);
        SceneManager.LoadScene("GameScene");

    }
    public void HelloLevel4()
    {
        GlobalRecords.GlobalRecordHolder.Instance.LevelSelection = 4;
        GlobalRecords.GlobalRecordHolder.Instance.LoadmyScene(text4);
        SceneManager.LoadScene("GameScene");
    }
    public void HelloLevel5()
    {
        GlobalRecords.GlobalRecordHolder.Instance.LevelSelection = 5;
        GlobalRecords.GlobalRecordHolder.Instance.LoadmyScene(text5);
        SceneManager.LoadScene("GameScene");

    }



  



    private void LateUpdate()
    {
        Scoretext.text = "Score: " + GlobalRecords.GlobalRecordHolder.Instance.GlobalScore.ToString();


        if (GlobalRecords.GlobalRecordHolder.Instance.Level1>0)
        {
            EnableStar(ImageLevel1);

        }
        if (GlobalRecords.GlobalRecordHolder.Instance.Level2 > 0)
        {

            EnableStar(ImageLevel2);
        }
        if (GlobalRecords.GlobalRecordHolder.Instance.Level3 > 0)
        {

            EnableStar(ImageLevel3);
        }
        if (GlobalRecords.GlobalRecordHolder.Instance.Level4 > 0)
        {
            EnableStar(ImageLevel4);

        }
        if (GlobalRecords.GlobalRecordHolder.Instance.Level5 > 0)
        {
            EnableStar(ImageLevel5);

        }
    }


   public void EnableStar(GameObject startvalue)
    {


        startvalue.SetActive(true);
    }
}
