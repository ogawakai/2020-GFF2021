using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // ポーズUIのボタン操作などを管理します。
    public class Pause : MonoBehaviour
    {
        // 最初に選択状態に設定するボタンを指定します。
        [SerializeField]
        private Button selectedButton = null;

        // Start is called before the first frame update
        void Start()
        {
            selectedButton.Select();
        }

        // 「RESUME」ボタンがクリックされた際に呼び出されます。
        public void OnClickResumeButton()
        {
            StageScene.Instance.Resume();
            Destroy(gameObject);
        }

        // 「RETRY」ボタンがクリックされた際に呼び出されます。
        public void OnClickRestartButton()
        {
            StageScene.Instance.Retry();
            Destroy(gameObject);
        }

        // 「EXIT」ボタンがクリックされた際に呼び出されます。
        public void OnClickExitButton()
        {
            StageScene.Instance.GiveUp();
            Destroy(gameObject);
        }
    }
}