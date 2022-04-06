using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RunGame
{
    
    public class TimeController : MonoBehaviour
    {
        //[wang] �J�E���g�̎���
        public float countTime;
        //[wang] ���Ԃ̕\��
        public Text countText; 

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //[wang]�c�莞�Ԃ̌v�Z
            countTime -= Time.deltaTime;

            //[wang]�c�莞�Ԃ�\������
            countText.text = "�c��" + countTime.ToString("f1") + "�b";

            //[wang]time up
            if (countTime < 0)
            {
                countText.text = "";
                StageScene.Instance.GameOver();//�Q�[���I�[�o�[�Ɉڍs
            }
        }
    }
}