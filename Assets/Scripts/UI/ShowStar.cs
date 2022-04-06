using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RunGame
{
    public class ShowStar : MonoBehaviour
    {
        // [wang]星の画像(青い)
        [SerializeField]
        private Sprite onSprite = null;
        // [wang]星の画像(黒い)
        [SerializeField]
        private Sprite offSprite = null;
        //[wang]線の画像(On)
        [SerializeField]
        private Sprite onLine = null;
        //[wang]線の画像(Null)
        [SerializeField]
        private Sprite offLine = null;

        //[wang]星を表示するImageコンポーネントを指定します。
        [SerializeField]
        private Image[] images = null;
        //[wang]線を表示するImageコンポーネントを指定します。
        [SerializeField]
        private Image[] line = null;

        //[ogawa]ステージのランク
        [SerializeField]
        private Image[] Rank;

        //[ogawa]星の最大数
        [SerializeField]
        int MaxStarCount;

        // Start is called before the first frame update
        void Start()
        {
            UpdateStar();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateStar();
        }

        private void UpdateStar()
        {
            //[wang]表示する星の数
            int starCount = StageScene.Instance.StarCount;
            //Debug.Log("StarCount:"+ starCount);

            //[ogawa]評価の分割
            //if(MaxStarCount * 0.75)
            //{
            //  Rank[0].;
            //}

            //[wang]画像のon off
            for (int i = 0; i < images.Length; i++)
            {
                if (i < starCount) 
                {
                    images[i].sprite = onSprite;
                    
                }
                else 
                {
                    images[i].sprite = offSprite;
                }
            }
        }
    }
}
