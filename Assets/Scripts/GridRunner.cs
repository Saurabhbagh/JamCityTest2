using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GridRunner : MonoBehaviour
{

     TextAsset text;
    public Button Quit;
    public Button Hint;
    public Button GoBackToManMenu;
    public TMPro.TextMeshProUGUI LocalScore;
    public TMPro.TextMeshProUGUI GlobalScores;
    public Camera camera;
    public int one = 0;
    public int two = 0;
    RaycastHit hit;
    GameObject objectHit;
    GameObject Nexthitobject;
    // Start is called before the first frame update

    public void Awake()
    {
        Quit.onClick.AddListener(QuitMethod);
        Hint.onClick.AddListener(HintMethod);
        GoBackToManMenu.onClick.AddListener(MenuMenuMethod);
    }

    public void Start()
    {
        text = GlobalRecords.GlobalRecordHolder.Instance.Text;
        string[] buffer = text.ToString().Split('\n');
        //Lets get the length and breadth of the buffer. 
        int breadth = buffer[1].Length - 1;
        Debug.Log(buffer[1]);
        int height = buffer.Length;
        Debug.Log(breadth + ":" + height);


        // width and height from the text file 

        Grid mygrid = new Grid(breadth, height, 5f, buffer);



    }


    private void Update()
    {
      
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if(one ==0 && two==0)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    objectHit = hit.transform.gameObject;

                    Debug.Log("hitting something ");
                    objectHit.GetComponent<TMPro.TextMeshPro>().fontSize = 10;
                }
                one = 1;


            }
            
            if(one==1 && two==0)
            {

                if (Physics.Raycast(ray, out hit))
                {
                    Nexthitobject = hit.transform.gameObject;
                    Debug.Log("hitting something ");
                    // Do something with the object that was hit by the raycast.
                    Nexthitobject.GetComponent<TMPro.TextMeshPro>().fontSize = 10;
                }
                two = 1;

            }

            if (one == 1 && two == 1)
            {
                if(Nexthitobject ==null || objectHit ==null)
                {

                    return;
                }

                string a = Nexthitobject.GetComponent<TMPro.TextMeshPro>().text;
                string b = objectHit.GetComponent<TMPro.TextMeshPro>().text;
                if (a == b)
                {

                    Destroy(objectHit);
                    Destroy(Nexthitobject);
                    GlobalRecords.GlobalRecordHolder.Instance.LocalScore += 10;
                    one = 0;
                    two = 0;

                }
                else
                {
                    if(GlobalRecords.GlobalRecordHolder.Instance.LocalScore==0)
                    {

                    }
                    else
                    {

                        int temp = GlobalRecords.GlobalRecordHolder.Instance.LocalScore;
                          
                        if(temp -15<0)
                        {


                        }
                        else
                        {
                            GlobalRecords.GlobalRecordHolder.Instance.LocalScore -= 15;

                        }


                    }



                  }

                ScoreUpdate();
            }


        }
    }



    void ScoreUpdate()
    {

        GlobalRecords.GlobalRecordHolder.Instance.LocalScore = 20;
        //to be implemented
        LocalScore.text = "Local Score :" + GlobalRecords.GlobalRecordHolder.Instance.LocalScore.ToString();


        //Global Score
        if (GlobalRecords.GlobalRecordHolder.Instance.LocalScore> GlobalRecords.GlobalRecordHolder.Instance.GlobalScore)
        {
            GlobalRecords.GlobalRecordHolder.Instance.GlobalScore = GlobalRecords.GlobalRecordHolder.Instance.LocalScore;

        }

        GlobalScores.text = "Higgest Score :" + GlobalRecords.GlobalRecordHolder.Instance.GlobalScore.ToString();
    }

    private void MenuMenuMethod()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void HintMethod()
    {
        throw new NotImplementedException();
    }

    private void QuitMethod()
    {
        Application.Quit();
    }

   

    void OnCompletion()
    {
        if (GlobalRecords.GlobalRecordHolder.Instance.LevelSelection == 1)
            GlobalRecords.GlobalRecordHolder.Instance.Level1 = 1;
        else if (GlobalRecords.GlobalRecordHolder.Instance.LevelSelection == 2)
            GlobalRecords.GlobalRecordHolder.Instance.Level2 = 1;
        else if (GlobalRecords.GlobalRecordHolder.Instance.LevelSelection == 3)
            GlobalRecords.GlobalRecordHolder.Instance.Level3 = 1;
        else if (GlobalRecords.GlobalRecordHolder.Instance.LevelSelection == 4)
            GlobalRecords.GlobalRecordHolder.Instance.Level4 = 1;
        else if (GlobalRecords.GlobalRecordHolder.Instance.LevelSelection == 5)
            GlobalRecords.GlobalRecordHolder.Instance.Level5 = 1;
        else
            Debug.Log("Did not complete");

    }
    


}
