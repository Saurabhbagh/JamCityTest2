using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GridRunner : MonoBehaviour
{
    
    public TextAsset text;
    public Button Quit;
    public Button Hint;
    public Button GoBackToManMenu;
    public TMPro.TextMeshProUGUI LocalScore;
    public TMPro.TextMeshProUGUI GlobalScores;
    public Camera camera;
    public int one = 0;
    public int two = 0;

    public WinningScrip script;
    public LoserScript script2;
    RaycastHit hit;
    GameObject objectHit;
    GameObject Nexthitobject;
    public bool isSelected;
    int breadth;
    int height;

    int count = 0;

    int localScore = 0;
    bool emptyGrid;
    public AudioSource voice;
    List<bool> Values= new List<bool>();
    // Start is called before the first frame update

    public void Awake()
    {
        Quit.onClick.AddListener(QuitMethod);
        Hint.onClick.AddListener(HintMethod);
        GoBackToManMenu.onClick.AddListener(MenuMenuMethod);
    }

    public void Start()
    {
        voice = GetComponent<AudioSource>();
        if (text==null)
        {
            text = GlobalRecords.GlobalRecordHolder.Instance.Text;

        }
        
        string[] buffer = text.ToString().Split('\n');
         //Lets get the length and breadth of the buffer. 
        breadth = buffer[1].Length - 1;
         // Debug.Log(buffer[1]);
        height = buffer.Length;
        //Debug.Log(breadth + ":" + height);


        // width and height from the text file 

        Grid mygrid = new Grid(breadth, height, 5f, buffer);



    }


    private void Update()
    {
        ScoreUpdate();
        if (Input.GetMouseButtonDown(0))
        {
            MyClick();
        }
       

        if (count == 2)
        {
            AlgoLogic();
            count=0;

        }
        Debug.Log("update count"+count);


        if (Values.Count<1 )
        {
            for (int p = 0; p < breadth; p++)
            {

                for (int j = 0; j < height; j++)
                {
                    // string yo="TextObject:" + p.ToString() + ":" + j.ToString();
                    GameObject hello = GameObject.Find("TextObject:" + p.ToString() + ":" + j.ToString()); //time consuming task
                    if (hello.GetComponent<TMPro.TextMeshPro>().text != "")
                    {
                        // obstacle
                        Values.Add(false);
                        // hello.GetComponent<TMPro.TextMeshPro>().text = "0";
                    }
                    else
                    {
                        // hello.GetComponent<TMPro.TextMeshPro>().text = "1";
                        //travel
                        Values.Add(true);
                    }




                }
            }

        }

        if (Values.Contains(false)|| Values.Count==0 )
        {
            //stage not complete or just has one value left ;
            var list = Values;
            var q = from x in list
                    group x by x into g
                    let count = g.Count()
                    orderby count descending
                    select new { Value = g.Key, Count = count };
            List<int> hello = new List<int>();
            foreach (var x in q)
            {
                hello.Add(x.Count);
            }

            if (hello[1]==1)
            {
                MenuMenuMethod();
                    return;
            }
            hello.Clear();
            emptyGrid = false;
            Values.Clear();

        }
        else
        {
            emptyGrid = true;

            OnCompletion();
            //you have completed the game. 
            
            this.gameObject.SetActive(false);

           script.RunWinning();
            //SceneManager.LoadScene("Main Menu");
        }
       
    


    }
    /// <summary>
    /// Twco clicks on the Alphabets
    /// </summary>
    public void  MyClick()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(count==0)
        {
            if (Physics.Raycast(ray, out hit))
            {
                objectHit = hit.transform.gameObject;

                Debug.Log("hitting something "+ objectHit.name);
                if (objectHit.GetComponent<TMPro.TextMeshPro>().text == "")
                    return;
                objectHit.GetComponent<TMPro.TextMeshPro>().fontSize = 10;
                StartCoroutine(playAudio());
                count = count + 1;
                isSelected = true;
            }

           
        }
        else if(count==1)
        {
            if (Physics.Raycast(ray, out hit))
            {
                Nexthitobject =hit.transform.gameObject;
                Debug.Log("hitting something ");
                if (Nexthitobject.GetComponent<TMPro.TextMeshPro>().text == "")
                    return;
                // Do something with the object that was hit by the raycast.
                Nexthitobject.GetComponent<TMPro.TextMeshPro>().fontSize = 10;
                StartCoroutine(playAudio());
                count = count + 1;
            }
           
        }
        else
        {

            count = 0;
        }
        
    }

    /// <summary>
    /// Algorithm logic 
    /// </summary>
    void AlgoLogic()
    {
        //Get the position of the fist hit
        //Debug.Log(objectHit.transform.position);
        Debug.Log(count);
        //Get the position of the second hit
        //Debug.Log(Nexthitobject.transform.position);


        //if the hit are same object - return to normal position
        //Deselection of tile
        if(GameObject.Equals(objectHit,Nexthitobject))
        {
            count = 0;
            Nexthitobject.GetComponent<TMPro.TextMeshPro>().fontSize = 5;
            return;

        }
        
        else//if the hit are different object 
        {
            string a = Nexthitobject.GetComponent<TMPro.TextMeshPro>().text;
            string b = objectHit.GetComponent<TMPro.TextMeshPro>().text;
            //with diffent value  -- hit penalty 
            if (a != b)
            {

                Penalty();
                
            }

            ///same value -- see the position difference
            else
            {

                Vector3 positiondiff = objectHit.transform.position - Nexthitobject.transform.position;
                Debug.Log(positiondiff);
                TurnCalculator(breadth, height, objectHit.transform.position, Nexthitobject.transform.position);
                if(!GlobalRecords.GlobalRecordHolder.Instance.Isreachable)
                {
                    Penalty();
                }
                else
                {
                    if(GlobalRecords.GlobalRecordHolder.Instance.NumberofTurn>2)
                    {
                        Penalty();
                    }

                    else
                    {

                        ScoreAdded();

                    }

                }
                    
            }

            //loop through the grid  and for the position in general in the middle ,  if those have a object return 

            // if there is no object  count the number of translation or turns

            // if turns>3 penalty 

            //if turns <3 , score 

        }





    }

    //will calculate the turns and set it in the global Record holder
    void TurnCalculator(int breadth, int height , Vector3 pos1, Vector3 pos2)
    {

        int startx = Math.Abs((int)pos1.x);
        int starty = Math.Abs((int)pos1.y);
        int endx = Math.Abs((int)pos2.x);
        int endy = Math.Abs((int)pos2.y);



        int[,] PrivateGrid = new int[breadth,height];


        for (int p=0; p< breadth; p++)
        {

            for(int j=0; j<height; j++)
            {
               // string yo="TextObject:" + p.ToString() + ":" + j.ToString();
                GameObject hello = GameObject.Find("TextObject:" + p.ToString() + ":" + j.ToString());
                if(hello.GetComponent<TMPro.TextMeshPro>().text!="")
                {
                    // obstacle
                    PrivateGrid[p, j] = 0;
                   // hello.GetComponent<TMPro.TextMeshPro>().text = "0";
                }
                else
                {
                    // hello.GetComponent<TMPro.TextMeshPro>().text = "1";
                    //travel
                    PrivateGrid[p, j] = 1;
                }
                
                


            }
        }

        PrivateGrid[startx, starty] = 1;
        PrivateGrid[endx, endy] = 1;


        
        AstarFinder find =new AstarFinder();
        find.Finder(PrivateGrid,startx,starty,endx,endy);
       
        //Debug.Log(PrivateGrid);

    }

    /// <summary>
    /// calculates teh penalty score.
    /// </summary>
    void Penalty()
    {

        localScore = GlobalRecords.GlobalRecordHolder.Instance.LocalScore;
        if(localScore-10<0)
        {

            localScore = localScore + 0;
        }
        else
        {

            localScore = localScore - 10;
        }
        GlobalRecords.GlobalRecordHolder.Instance.LocalScore = localScore;

        count = 0;
        Nexthitobject.GetComponent<TMPro.TextMeshPro>().fontSize = 5;
        objectHit.GetComponent<TMPro.TextMeshPro>().fontSize = 5;

    }

    /// <summary>
    /// Calculates the score added.
    /// </summary>
    void ScoreAdded()
    {

        localScore = localScore + 15;
       
        Nexthitobject.GetComponent<TMPro.TextMeshPro>().fontSize = 5;
        objectHit.GetComponent<TMPro.TextMeshPro>().fontSize = 5;
        Nexthitobject.GetComponent<TMPro.TextMeshPro>().text = "";
        objectHit.GetComponent<TMPro.TextMeshPro>().text = "";
        GlobalRecords.GlobalRecordHolder.Instance.LocalScore = localScore;
        count = 0;
    }

    void ScoreUpdate()
    {

        //GlobalRecords.GlobalRecordHolder.Instance.LocalScore = 20;
      
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

        if (emptyGrid == false)
        {
            GlobalRecords.GlobalRecordHolder.Instance.LocalScore = 0;
            this.gameObject.SetActive(false);
            script2.LosingScript();

        }
        else
        {
            SceneManager.LoadScene("Main Menu");

        }
        
    }
    //Hint not enabled.
    private void HintMethod()
    {
        //check for the nearest  or closest 
        throw new NotImplementedException();
    }

    private void QuitMethod()
    {
        GlobalRecords.GlobalRecordHolder.Instance.LocalScore = 0;
        if (emptyGrid==false)
        {

            script2.LosingScript();

        }
        Application.Quit();
    }


    IEnumerator playAudio()
    {
              
        voice.Play();
        yield return new WaitForSeconds(voice.clip.length);
       
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
