using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // ゲームオーバーUIを表します。
    public class GameOver : MonoBehaviour
    {
        // 初期選択ボタンを指定します。
        [SerializeField]
        private Button selectedButton = null;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(OnStart());
        }

        // ゲームオーバー演出を実行します。
        IEnumerator OnStart()
        {
            // ２秒間操作を受け付けない
            yield return new WaitForSeconds(2);
            // YESボタンを選択状態に設定する
            selectedButton.Select();
        }

        // Yesボタンをクリックした際に呼び出されます。
        public void OnClickYesButton()
        {
            StageScene.Instance.Retry();
        }

        // Noボタンをクリックした際に呼び出されます。
        public void OnClickNoButton()
        {
            StageScene.Instance.GiveUp();
        }
    }
}