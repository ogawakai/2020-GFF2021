using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // タイトル画面の進行を制御します。
    public class TitleScene : MonoBehaviour
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

        // Update is called once per frame
        void Update()
        {

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
            // 1秒間待機
            yield return new WaitForSeconds(1);
            // 次のシーンを読み込む
            SceneManager.LoadScene("Stage");
        }
    }
}