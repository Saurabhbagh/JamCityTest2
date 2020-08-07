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
                if(Buff[i,j]=="X")
                {// change it randomly later 
                   int num = _random.Next(0, 2);

                    if(num==1)
                    {
                        
                        // GameObject helloword = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject helloword = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        helloword.AddComponent<BoxCollider>();
                        helloword.AddComponent<TMPro.TextMeshPro>();
                        helloword.GetComponent<TMPro.TextMeshPro>().text ="A";
                        helloword.GetComponent<TMPro.TextMeshPro>().fontSize = 5f;
                        helloword.transform.position = GetPostion(i, j);
                        
                        helloword.GetComponent<TMPro.TextMeshPro>().margin =new Vector4(9.613768f, 2.16469f, 9.772934f, 2.101021f);

                                          }
                    else if (num == 2)
                        {
                        //GameObject helloword = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        GameObject helloword = new GameObject();
                        helloword.AddComponent<BoxCollider>(); 
                        helloword.AddComponent<TMPro.TextMeshPro>();
                        helloword.GetComponent<TMPro.TextMeshPro>().text = "B";
                        //helloword.GetComponent<RawImage>().texture = Object2.Image;
                        helloword.GetComponent<TMPro.TextMeshPro>().fontSize = 5f;
                        helloword.transform.position = GetPostion(i, j);
                        helloword.GetComponent<TMPro.TextMeshPro>().margin = new Vector4(9.613768f, 2.16469f, 9.772934f, 2.101021f);

                    }
                    else
                    {


                        //GameObject helloword = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        GameObject helloword = new GameObject();
                        helloword.AddComponent<BoxCollider>();
                        helloword.AddComponent<TMPro.TextMeshPro>();
                        helloword.GetComponent<TMPro.TextMeshPro>().text = "C";

                        var ren = helloword.GetComponent<TMPro.TextMeshPro>().renderer;

                        // helloword.GetComponent<RawImage>().texture = Object3.Image;
                        helloword.GetComponent<TMPro.TextMeshPro>().fontSize = 5f;
                        //- (int)(ren.bounds.size.x * 2)
                        helloword.transform.position = GetPostion(i, j);
                     helloword.GetComponent<TMPro.TextMeshPro>().margin =new Vector4(9.613768f, 2.16469f, 9.772934f, 2.101021f);



                    }


                }

              
                
                //  UtilsClass.CreateWorldText(gridview[i, j].ToString(), null,GetPostion(i,j),20, Color.red,TextAnchor.MiddleCenter);
               
            }

        }

    }






    public Vector3 GetPostion(int x , int y )
    {

        return new Vector3(-x, -y);
    }


}
