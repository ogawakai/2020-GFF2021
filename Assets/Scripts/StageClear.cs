using System.Collections;
using UnityEngine;

namespace RunGame
{
    // �X�e�[�W�N���A�[UI�̐i�s�𐧌䂵�܂��B
    public class StageClear : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(OnStart());
        }

        // �X�e�[�W�N���A�[���o�����s���܂��B
        IEnumerator OnStart()
        {
            // �Q�b�ԑ�����󂯕t���Ȃ�
            yield return new WaitForSeconds(2);
            // ����L�[�̓��͂�҂��󂯂�
            while (true)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    break;
                }
                yield return null;
            }
            // ���̃X�e�[�W��ǂݍ���
            StageScene.Instance.LoadNextScene();
        }
    }
}