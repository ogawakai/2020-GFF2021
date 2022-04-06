using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RunGame
{
    public class ShowStar : MonoBehaviour
    {
        // [wang]���̉摜(��)
        [SerializeField]
        private Sprite onSprite = null;
        // [wang]���̉摜(����)
        [SerializeField]
        private Sprite offSprite = null;
        //[wang]���̉摜(On)
        [SerializeField]
        private Sprite onLine = null;
        //[wang]���̉摜(Null)
        [SerializeField]
        private Sprite offLine = null;

        //[wang]����\������Image�R���|�[�l���g���w�肵�܂��B
        [SerializeField]
        private Image[] images = null;
        //[wang]����\������Image�R���|�[�l���g���w�肵�܂��B
        [SerializeField]
        private Image[] line = null;

        //[ogawa]�X�e�[�W�̃����N
        [SerializeField]
        private Image[] Rank;

        //[ogawa]���̍ő吔
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
            //[wang]�\�����鐯�̐�
            int starCount = StageScene.Instance.StarCount;
            //Debug.Log("StarCount:"+ starCount);

            //[ogawa]�]���̕���
            //if(MaxStarCount * 0.75)
            //{
            //  Rank[0].;
            //}

            //[wang]�摜��on off
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
