using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // �|�[�YUI�̃{�^������Ȃǂ��Ǘ����܂��B
    public class Pause : MonoBehaviour
    {
        // �ŏ��ɑI����Ԃɐݒ肷��{�^�����w�肵�܂��B
        [SerializeField]
        private Button selectedButton = null;

        // Start is called before the first frame update
        void Start()
        {
            selectedButton.Select();
        }

        // �uRESUME�v�{�^�����N���b�N���ꂽ�ۂɌĂяo����܂��B
        public void OnClickResumeButton()
        {
            StageScene.Instance.Resume();
            Destroy(gameObject);
        }

        // �uRETRY�v�{�^�����N���b�N���ꂽ�ۂɌĂяo����܂��B
        public void OnClickRestartButton()
        {
            StageScene.Instance.Retry();
            Destroy(gameObject);
        }

        // �uEXIT�v�{�^�����N���b�N���ꂽ�ۂɌĂяo����܂��B
        public void OnClickExitButton()
        {
            StageScene.Instance.GiveUp();
            Destroy(gameObject);
        }
    }
}