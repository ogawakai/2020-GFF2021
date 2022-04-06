using UnityEngine;

namespace RunGame
{
    // 対象を追尾するカメラを表します。
   public class ChaseCamera : MonoBehaviour
    {
        // 追尾対象のTransformコンポーネントを指定します。
        [SerializeField, Tooltip("追尾対象を指定します。")]
        private Transform target = null;
        // 追尾対象からのずれを指定します。
        [SerializeField]
        private Vector2 offset = new Vector2(6.5f, 1.5f);

        // Update is called once per frame
        void Update()
        {
            // カメラの位置座標
            var position = transform.position;
            // プレイヤーのTransform.position.x座標とあわせる
            position.x = target.position.x + offset.x;
            position.y = target.position.y + offset.y;
            transform.position = position;
        }
    }
}