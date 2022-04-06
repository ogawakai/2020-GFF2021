using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



namespace RunGame
{
    public class UIManeger : MonoBehaviour
    {
        //[wang]3ステージ分のUIを格納する変数
        //[wang]インスペクターウィンドウからゲームオブジェクトを設定する
        [SerializeField] GameObject Star;
        [SerializeField] GameObject Star2;
        [SerializeField] GameObject Star3;


        // Start is called before the first frame update
        void Start()
        {
            UpdateStarUI();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateStarUI();
        }

        private void UpdateStarUI()
        {
            //[wang]ステージごとUI切り替え 
            int stageNumber = StageScene.StageNo;
            switch (stageNumber)
            {
                case 0:
                    Star.SetActive(true);
                    Star2.SetActive(false);
                    Star3.SetActive(false);
                    break;
                case 1:
                    Star.SetActive(false);
                    Star2.SetActive(true);
                    Star3.SetActive(false);
                    break;
                case 2:
                    Star.SetActive(false);
                    Star2.SetActive(false);
                    Star3.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}