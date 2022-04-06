using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // �Q�[���I�[�o�[UI��\���܂��B
    public class GameOver : MonoBehaviour
    {
        // �����I���{�^�����w�肵�܂��B
        [SerializeField]
        private Button selectedButton = null;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(OnStart());
        }

        // �Q�[���I�[�o�[���o�����s���܂��B
        IEnumerator OnStart()
        {
            // �Q�b�ԑ�����󂯕t���Ȃ�
            yield return new WaitForSeconds(2);
            // YES�{�^����I����Ԃɐݒ肷��
            selectedButton.Select();
        }

        // Yes�{�^�����N���b�N�����ۂɌĂяo����܂��B
        public void OnClickYesButton()
        {
            StageScene.Instance.Retry();
        }

        // No�{�^�����N���b�N�����ۂɌĂяo����܂��B
        public void OnClickNoButton()
        {
            StageScene.Instance.GiveUp();
        }
    }
}