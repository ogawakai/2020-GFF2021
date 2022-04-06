using System.Collections;
using UnityEngine;

namespace RunGame
{
    // ステージクリアーUIの進行を制御します。
    public class StageClear : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(OnStart());
        }

        // ステージクリアー演出を実行します。
        IEnumerator OnStart()
        {
            // ２秒間操作を受け付けない
            yield return new WaitForSeconds(2);
            // 決定キーの入力を待ち受ける
            while (true)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    break;
                }
                yield return null;
            }
            // 次のステージを読み込む
            StageScene.Instance.LoadNextScene();
        }
    }
}