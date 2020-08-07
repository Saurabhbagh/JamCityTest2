using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalRecords
{
    public class GlobalRecordHolder : MonoBehaviour
    {
        public static GlobalRecordHolder Instance;
        // public string username = "Saurabh";
        public int GlobalScore = 0;
        public int LocalScore = 0;
        public int Level1 = 0;
        public int Level2 = 0;
        public int Level3 = 0;
        public int Level4 = 0;
        public int Level5 = 0;
        public int LevelSelection = 0;
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

