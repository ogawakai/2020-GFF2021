using UnityEngine;
using UnityEngine.UI;

namespace RunGame
{
    // ステージ開始後のカウントダウン演出の進行制御を管理します。
    public class CountDown : MonoBehaviour
    {
        // ステージ番号表示用のUIを指定します。
        [SerializeField]
        private Text stageNoUI = null;

        // Start is called before the first frame update
        void Start()
        {
            //stageNoUI.text = string.Format("STAGE {0}", StageScene.StageNo);
            OnStartStageEvent();
        }

        // アニメーション内のキーイベントで呼び出されます。
        public void OnStartStageEvent()
        {
            StageScene.Instance.StartStage();
            //[ogawa]1.3秒後に削除
            Destroy(gameObject, 1.3f);
        }
    }
}