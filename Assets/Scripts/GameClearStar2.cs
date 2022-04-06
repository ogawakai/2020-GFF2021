using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame
{
    public class GameClearStar2 : MonoBehaviour
    {

        //[ogawa]¯‚Ì‘æ“¾”‚ğ•\¦
        [SerializeField]
        private GameObject[] star;

        // Start is called before the first frame update
        void Start()
        {
            //[ogawa]¯‚Ì‘æ“¾”‚ª10ŒÂ‚Ì
            if (StageScene.allstar == 10)
            {
                star[0].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª11ŒÂ‚Ì
            else if (StageScene.allstar == 11)
            {
                star[1].SetActive(true);
            }
            //[ogawa]¯‚Ì‘æ“¾”‚ª12ŒÂ‚Ì
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
