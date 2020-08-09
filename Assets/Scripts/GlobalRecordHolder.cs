using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalRecords
{
    public class GlobalRecordHolder : MonoBehaviour
    {
        public static GlobalRecordHolder Instance;
        // public string username = "Saurabh";
        public int GlobalScore ;
        public int LocalScore;
        public int Level1 ;
        public int Level2 ;
        public int Level3 ;
        public int Level4 ;
        public int Level5 ;
        public int LevelSelection = 0;

        public int NumberofTurn;
        public bool Isreachable; 
        //public Text Assets 

        public TextAsset Text;
        
        void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }


        public void LoadmyScene(TextAsset text)
        {
            Text = text;
            //GridRunner LevelSecene = new GridRunner();
            //LevelSecene.Mystart(Text);


        }

    }
}

