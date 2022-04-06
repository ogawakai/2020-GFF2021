using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // �X�e�[�W�J�n��̃J�E���g�_�E�����o�̐i�s������Ǘ����܂��B
    public class CountDown : MonoBehaviour
    {
        // �X�e�[�W�ԍ��\���p��UI���w�肵�܂��B
        [SerializeField]
        private Text stageNoUI = null;

        // Start is called before the first frame update
        void Start()
        {
            //stageNoUI.text = string.Format("STAGE {0}", StageScene.StageNo);
            OnStartStageEvent();
        }

        // �A�j���[�V�������̃L�[�C�x���g�ŌĂяo����܂��B
        public void OnStartStageEvent()
        {
            StageScene.Instance.StartStage();
            //[ogawa]1.3�b��ɍ폜
            Destroy(gameObject, 1.3f);
        }
    }
}