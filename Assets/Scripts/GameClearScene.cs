using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // ゲームクリアー画面の進行を制御します。
    public class GameClearScene : MonoBehaviour
    {
        // Animatorコンポーネントを参照しておく変数
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            // コンポーネントを参照
            animator = GetComponent<Animator>();

            StartCoroutine(OnStart());
        }

        IEnumerator OnStart()
        {
            // ２秒間だけ入力を受け付けない
            yield return new WaitForSeconds(2);
            // 決定キーの入力を待機
            while (true)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    break;
                }
                yield return null;
            }
            // FadeOutアニメーションへ遷移
            animator.SetTrigger("FadeOut");
            // フェードアウトの終了まで１秒間待機する
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Title");
        }
    }
}