using UnityEngine;

namespace RunGame
{
    // �w�i�摜���X�N���[��������@�\��\���܂��B
    public class Backgrounds : MonoBehaviour
    {
        // �w�i�̃X�v���C�g �p�l�����w�肵�܂��B
        [SerializeField]
        private Transform[] sprites = null;

        // �w�i�X�v���C�g�ꖇ�������Unit�T�C�Y
        Vector3 gridSize;

        // �v���C���[
        Transform player;

        // Start is called before the first frame update
        void Start()
        {
            // �v���C���[���^�O�Ō���
            player = GameObject.FindGameObjectWithTag("Player").transform;
            // 0�Ԗڂ̉摜����\���T�C�Y�iUnit�P�ʁj���擾
            gridSize = sprites[0].GetComponent<SpriteRenderer>().bounds.size;

            UpdateSprites();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateSprites();
        }

        // �R���̃p�l������ׂ�
        private void UpdateSprites()
        {
            // �v���C���[�̃O���b�h�ʒu���v�Z
            var playerGridX = Mathf.FloorToInt(player.position.x / gridSize.x);
            // �R���̃p�l������ׂ�
            for (int index = 0; index < sprites.Length; index++)
            {
                sprites[index].position =
                    new Vector3((index + playerGridX) * gridSize.x, 0, 0);
            }
        }
    }
}