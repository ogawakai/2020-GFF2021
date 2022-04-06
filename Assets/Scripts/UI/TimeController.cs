using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RunGame
{
    
    public class TimeController : MonoBehaviour
    {
        //[wang] カウントの時間
        public float countTime;
        //[wang] 時間の表示
        public Text countText; 

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //[wang]残り時間の計算
            countTime -= Time.deltaTime;

            //[wang]残り時間を表示する
            countText.text = "残り" + countTime.ToString("f1") + "秒";

            //[wang]time up
            if (countTime < 0)
            {
                countText.text = "";
                StageScene.Instance.GameOver();//ゲームオーバーに移行
            }
        }
    }
}