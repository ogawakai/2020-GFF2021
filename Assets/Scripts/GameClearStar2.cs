using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame
{
    public class GameClearStar2 : MonoBehaviour
    {

        //[ogawa]���̑��擾����\��
        [SerializeField]
        private GameObject[] star;

        // Start is called before the first frame update
        void Start()
        {
            //[ogawa]���̑��擾����10�̎�
            if (StageScene.allstar == 10)
            {
                star[0].SetActive(true);
            }
            //[ogawa]���̑��擾����11�̎�
            else if (StageScene.allstar == 11)
            {
                star[1].SetActive(true);
            }
            //[ogawa]���̑��擾����12�̎�
            else if (StageScene.allstar == 12)
            {
                star[2].SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
