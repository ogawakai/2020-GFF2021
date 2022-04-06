using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // �^�C�g����ʂ̐i�s�𐧌䂵�܂��B
    public class TitleScene : MonoBehaviour
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

        // Update is called once per frame
        void Update()
        {

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
            // 1�b�ԑҋ@
            yield return new WaitForSeconds(1);
            // ���̃V�[����ǂݍ���
            SceneManager.LoadScene("Stage");
        }
    }
}