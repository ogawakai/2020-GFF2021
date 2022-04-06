using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame
{
    public class GameClearStar : MonoBehaviour
    {

        //[ogawa]¯‚Ì‘æ“¾”‚ğ•\¦
        [SerializeField]
        private GameObject[] star;

        // Start is called before the first frame update
        void Start()
        {
            //[ogawa]¯‚Ì‘æ“¾”‚ª0ŒÂ‚Ì
            if (StageScene.allstar == 0)
            {
                star[0].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª1ŒÂ‚Ì
            else if (StageScene.allstar == 1)
            {
                star[1].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª2ŒÂ‚Ì
            else if (StageScene.allstar == 2)
            {
                star[2].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª3ŒÂ‚Ì
            else if (StageScene.allstar == 3)
            {
                star[3].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª4ŒÂ‚Ì
            else if (StageScene.allstar == 4)
            {
                star[4].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª5ŒÂ‚Ì
            else if (StageScene.allstar == 5)
            {
                star[5].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª6ŒÂ‚Ì
            else if (StageScene.allstar == 6)
            {
                star[6].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª7ŒÂ‚Ì
            else if (StageScene.allstar == 7)
            {
                star[7].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª8ŒÂ‚Ì
            else if (StageScene.allstar == 8)
            {
                star[8].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª9ŒÂ‚Ì
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
