using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame
{
    public class GameClearStar : MonoBehaviour
    {

        //[ogawa]���̑��擾����\��
        [SerializeField]
        private GameObject[] star;

        // Start is called before the first frame update
        void Start()
        {
            //[ogawa]���̑��擾����0�̎�
            if (StageScene.allstar == 0)
            {
                star[0].SetActive(true);
            }
            //[ogawa]���̑��擾����1�̎�
            else if (StageScene.allstar == 1)
            {
                star[1].SetActive(true);
            }
            //[ogawa]���̑��擾����2�̎�
            else if (StageScene.allstar == 2)
            {
                star[2].SetActive(true);
            }
            //[ogawa]���̑��擾����3�̎�
            else if (StageScene.allstar == 3)
            {
                star[3].SetActive(true);
            }
            //[ogawa]���̑��擾����4�̎�
            else if (StageScene.allstar == 4)
            {
                star[4].SetActive(true);
            }
            //[ogawa]���̑��擾����5�̎�
            else if (StageScene.allstar == 5)
            {
                star[5].SetActive(true);
            }
            //[ogawa]���̑��擾����6�̎�
            else if (StageScene.allstar == 6)
            {
                star[6].SetActive(true);
            }
            //[ogawa]���̑��擾����7�̎�
            else if (StageScene.allstar == 7)
            {
                star[7].SetActive(true);
            }
            //[ogawa]���̑��擾����8�̎�
            else if (StageScene.allstar == 8)
            {
                star[8].SetActive(true);
            }
            //[ogawa]���̑��擾����9�̎�
            else if (StageScene.allstar == 9)
            {
                star[9].SetActive(true);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
