using UnityEngine;

namespace RunGame
{
    // �Ώۂ�ǔ�����J������\���܂��B
   public class ChaseCamera : MonoBehaviour
    {
        // �ǔ��Ώۂ�Transform�R���|�[�l���g���w�肵�܂��B
        [SerializeField, Tooltip("�ǔ��Ώۂ��w�肵�܂��B")]
        private Transform target = null;
        // �ǔ��Ώۂ���̂�����w�肵�܂��B
        [SerializeField]
        private Vector2 offset = new Vector2(6.5f, 1.5f);

        // Update is called once per frame
        void Update()
        {
            // �J�����̈ʒu���W
            var position = transform.position;
            // �v���C���[��Transform.position.x���W�Ƃ��킹��
            position.x = target.position.x + offset.x;
            position.y = target.position.y + offset.y;
            transform.position = position;
        }
    }
}