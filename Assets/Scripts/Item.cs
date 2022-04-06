using UnityEngine;

namespace RunGame
{
    // �v���C���[���l���\�ȃA�C�e����\���܂��B
    public class Item : MonoBehaviour
    {
        // ���̃A�C�e�����l�������ۂɃX�R�A�ɉ��_�����l���w�肵�܂��B
        [SerializeField]
        private int addScore = 1;
        // ���̃I�u�W�F�N�g���g���K�[�G���A�ɐN�������ۂɌĂяo����܂��B
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // �X�R�A�ɉ��_
                StageScene.Instance.AddStar(addScore);
                // �A�C�e�����폜
                Destroy(gameObject);
            }
        }
    }
}