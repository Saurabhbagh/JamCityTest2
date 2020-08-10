using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AstarFinder 
{
    List<Node> OpenList = new List<Node>();
    List<Node> ClosedList = new List<Node>();
    Node start;
    Node end;
    Node[,] grid;
    public List<Node> Path = new List<Node>();

    public bool isReachable; 
    /// <summary>
    /// A* finder algo.
    /// </summary>
    /// <param name="Grid"></param>
    /// <param name="startx"></param>
    /// <param name="starty"></param>
    /// <param name="endx"></param>
    /// <param name="endy"></param>
    public void Finder( int[,] Grid ,int startx, int starty, int endx, int endy)
    {
        //grid setup
        grid = new Node[Grid.GetLength(0), Grid.GetLength(1)];
        for (int i =0; i<Grid.GetLength(0);i++)
        {
            for(int j=0; j<Grid.GetLength(1); j++)
            {
                bool obstacle;
                //populating grid value
                if(Grid[i,j]>0)
                {

                    obstacle = false;
                }
                else
                {

                    obstacle = true;
                }
                grid[i, j] = new Node(Grid[i, j], Mathf.Infinity, Mathf.Infinity, Mathf.Infinity, i , j,obstacle);

            }

        }
        //adding neighbours
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                //populating grid value
                grid[i, j].Neighbours(i, j,grid);

            }

        }

        start = grid[startx,starty];
          
        end = grid[endx, endy];
        start.gcost = 0;
        start.hcost = Heuristic(start, end);
        start.fcost = start.gcost + start.hcost;
        OpenList.Add(start);

        while(OpenList.Count>0)
        {
            int LowestIndex = 0;
            //we can keep going 
            for (int i=0; i < OpenList.Count;i++)
            {
                if(OpenList[i].fcost<OpenList[LowestIndex].fcost)
                {
                    LowestIndex = i;

                }

            }
            Node current = OpenList[LowestIndex];

            if (current == end)
            {
                Node temp = current;
                Path.Add(temp);
                while (temp.camefrom!=null)
                {

                    Path.Add(temp.camefrom);
                    temp = temp.camefrom;

                }

                Debug.Log("reached");
                isReachable = true;
            }

            OpenList.Remove(current);
            ClosedList.Add(current);

            foreach (Node neighbour in current.neighbour)
            {
                if (!ClosedList.Contains(neighbour) && !neighbour.wall)
                {

                    float newscore = current.gcost + 1;
                    if(OpenList.Contains(neighbour))
                    {

                        if(newscore<neighbour.gcost)
                        {
                            neighbour.gcost = newscore;
                        }
                    }
                    else
                    {

                        neighbour.gcost = newscore;
                        OpenList.Add(neighbour);
                    }

                    neighbour.hcost = Heuristic(neighbour, end);
                    neighbour.fcost = neighbour.gcost + neighbour.hcost;
                    neighbour.camefrom = current;
                }

               

            }


        }


        //path finding 
        foreach (Node nd in Path)
        {

            Debug.Log("Node" + nd.i + ":" + nd.j);

        }


        // Node Calcultor 

        NodeCalulator(Path, isReachable);
    }

    /// <summary>
    ///  to calculate the number of turns and is rechable 
    /// </summary>
    /// <param name="Path"></param>
    /// <param name="isReachable"></param>


    void NodeCalulator(List<Node> Path, bool isReachable)
        {
        if (!isReachable)
        {
            GlobalRecords.GlobalRecordHolder.Instance.Isreachable = false;
            GlobalRecords.GlobalRecordHolder.Instance.NumberofTurn =-1;
            return;

        }
        else
        {
            GlobalRecords.GlobalRecordHolder.Instance.Isreachable = true;
            int turns=0;
            int vertical=0;
            int horizontal = 0;
            int xdiff;
            int ydiff;
            int oldstate=-1;
            int direction;
            for (int i=0;i<Path.Count-1;i++)
            {

                //check which parameter is the same 

                xdiff = Mathf.Abs(Path[i].i - Path[i + 1].i);
                ydiff = Mathf.Abs(Path[i].j - Path[i + 1].j);


                if (xdiff == 1)
                {
                    direction = 1;


                }
                else
                    direction = 0;

                if (i > 0)
                {

                    if(oldstate!=direction)
                    {

                        turns++;
                    }
                }

                oldstate = direction;


               





/*

                if (Path[i].i>Path[i+1].i || Path[i].i < Path[i + 1].i)
                {
                    turns++;
                }
                if (Path[i].j > Path[i + 1].j || Path[i].j < Path[i + 1].j)
                {

                    turns++;
                }
               
    */

            }
            GlobalRecords.GlobalRecordHolder.Instance.NumberofTurn = turns;
        }
            
        }










    /// <summary>
    /// calculate the Heuristic value
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>


    public float Heuristic(Node a, Node b)
    {

        return Vector3.Distance(new Vector3(a.i, a.j, 0), new Vector3(b.i, b.j, 0));

    }
}

/// <summary>
/// Node properties for the  objects in the  grid. 
/// </summary>

public class Node
{


     int value;
     public   float hcost;
     public   float fcost;
     public   float gcost;
    //Node value
     public int i;
     public int j;
   public bool wall ;   
    public List<Node> neighbour;

    public Node camefrom;


    public Node(int Value, float Hcost, float gcost, float fcost, int X, int Y, bool obstacle)
    {

        this.value = Value;
        this.hcost = Hcost;
        this.fcost = gcost;
        this.fcost = fcost;
        this.i = X;
        this.j = Y;
        this.neighbour = new List<Node>();
        this.wall = obstacle;
    }
    /// <summary>
    /// setting up the neighbours. 
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="grid"></param>
    public void Neighbours(int X, int Y,Node[,] grid)
    {

        //setting the grids where you can go to .
        if (X < grid.GetLength(0) - 1)
            neighbour.Add(grid[X + 1, Y]);
        if (X > 0)
            neighbour.Add(grid[X - 1, Y]);
        if (Y < grid.GetLength(1) - 1)
            neighbour.Add(grid[X, Y + 1]);
        if (Y > 0)
            neighbour.Add(grid[X, Y - 1]);

    }
     
}