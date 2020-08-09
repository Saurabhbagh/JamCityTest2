using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; // Using code monkey asset for proper grid creation
using System;
using UnityEngine.UI;

public class Grid  :MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;
    public string[] Buffer;
    public Gametile Object1;
    public Gametile Object2;
    public Gametile Object3; 

    int [,] gridview;
    string[,] Buff;
    public Grid(int width , int height , float cellsize, string[] buffer)
    {

        this.width = width;
        this.height = height;
        this.cellSize = cellsize;
        
        gridview = new int[width, height];
        Buff = new string[width + 2, height];

        Debug.Log(width + "::::" + height);
        for (int q=0; q<buffer.Length; q++)
        {
            int k = 0;
            char[] yo = buffer[q].ToCharArray();    
             foreach(char ch in yo)
            {

                Buff[k, q] = ch.ToString();
                k++;

            }
        }



        System.Random _random = new System.Random();

        for ( int i =0; i < gridview.GetLength(0); i++)
        {
            for (int j = 0; j < gridview.GetLength(1); j++)
            {
                if (Buff[i, j] == "X")
                {// change it randomly later 
                    int num = _random.Next(0, 2);

                    if (num == 1)
                    {
                        GameObjectCreater("A", i, j);
                        

                    }

              
                    else if (num == 2)
                    {

                        GameObjectCreater("B", i, j);
                        
                    }
                    else
                    {

                        GameObjectCreater("C", i, j);
                        


                    }

                }
                
                else
                {
                    GameObjectCreater("", i, j);



                }



                //  UtilsClass.CreateWorldText(gridview[i, j].ToString(), null,GetPostion(i,j),20, Color.red,TextAnchor.MiddleCenter);

            }

        }

    }



    public void GameObjectCreater(string text, int i, int j )
    {

        GameObject helloword = new GameObject();
        helloword.AddComponent<BoxCollider>();
        helloword.AddComponent<TMPro.TextMeshPro>();
        helloword.GetComponent<TMPro.TextMeshPro>().text = text;
        helloword.GetComponent<TMPro.TextMeshPro>().fontSize = 5f;
        helloword.transform.position = GetPostion(i, j);
        helloword.GetComponent<TMPro.TextMeshPro>().margin = new Vector4(9.613768f, 2.16469f, 9.772934f, 2.101021f);
        helloword.name = "TextObject:" + i.ToString() + ":" + j.ToString();

    }


    public Vector3 GetPostion(int x , int y )
    {

        return new Vector3(-x, -y);
    }


}
