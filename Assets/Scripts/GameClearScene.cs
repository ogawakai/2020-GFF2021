using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // �Q�[���N���A�[��ʂ̐i�s�𐧌䂵�܂��B
    public class GameClearScene : MonoBehaviour
    {
        // Animator�R���|�[�l���g���Q�Ƃ��Ă����ϐ�
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            // �R���|�[�l���g���Q��
            animator = GetComponent<Animator>();

            StartCoroutine(OnStart());
        }

        IEnumerator OnStart()
        {
            // �Q�b�Ԃ������͂��󂯕t���Ȃ�
            yield return new WaitForSeconds(2);
            // ����L�[�̓��͂�ҋ@
            while (true)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    break;
                }
                yield return null;
            }
            // FadeOut�A�j���[�V�����֑J��
            animator.SetTrigger("FadeOut");
            // �t�F�[�h�A�E�g�̏I���܂łP�b�ԑҋ@����
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Title");
        }
    }
}