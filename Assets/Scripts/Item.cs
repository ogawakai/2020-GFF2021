using UnityEngine;

namespace RunGame
{
    // プレイヤーが獲得可能なアイテムを表します。
    public class Item : MonoBehaviour
    {
        // このアイテムを獲得した際にスコアに加点される値を指定します。
        [SerializeField]
        private int addScore = 1;
        // 他のオブジェクトがトリガーエリアに侵入した際に呼び出されます。
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // スコアに加点
                StageScene.Instance.AddStar(addScore);
                // アイテムを削除
                Destroy(gameObject);
            }
        }
    }
}