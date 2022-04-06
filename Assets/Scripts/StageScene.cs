using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // ステージ画面の進行を制御します。
    public class StageScene : MonoBehaviour
    {
        // UI表示用のCanvasを指定します。
        [SerializeField]
        private Transform uiRoot = null;
        // カウントダウンUIのプレハブを指定します。
        [SerializeField]
        private GameObject countDownPrefab = null;
        // ゲームオーバーUIのプレハブを指定します。
        [SerializeField]
        private GameObject gameOverPrefab = null;
        // ステージクリアーUIのプレハブを指定します。
        [SerializeField]
        private GameObject stageClearPrefab = null;
        // ポーズUIのプレハブを指定します。
        [SerializeField]
        private GameObject pausePrefab = null;
        // 画面切り替え用のイメージを指定します。
        [SerializeField]
        private Image transitionImage = null;
        // 画面切り替え用の黒テクスチャーを指定します。
        [SerializeField]
        private Sprite blackSprite = null;
        // 画面切り替え用の白テクスチャーを指定します。
        [SerializeField]
        private Sprite whiteSprite = null;

        // ステージの総数を指定します。
        [SerializeField]
        private int stageCount = 3;

        //[ogawa]星の総数
        public static int allstar;

        // 現在プレイしているステージ番号を取得または設定します。
        public static int StageNo
        {
            get { return stageNo; }
            set { stageNo = value; }
        }
        // 現在プレイしているステージ番号
        static int stageNo = 0;

        // ステージ画面内の進行状態を表します。
        enum SceneState
        {
            // ステージ開始演出中
            Start,
            // ステージプレイ中
            Play,
            // ゲームオーバーが確定していて演出中
            GameOver,
            // ステージクリアーが確定していて演出中
            StageClear,
        }
        SceneState sceneState = SceneState.Start;

        // [wang]現在の星の数を取得します。
        public int StarCount { get; private set; }

        // [wang]星のカウントを増やす
        public void AddStar(int value)
        {
            StarCount += value;
            Debug.Log(allstar);
        }

        // [wang]現在の線の数を取得します。
        public int LineCount { get; private set; }

        // [wang]線のカウントを増やす
        public void AddLine(int value)
        {
            LineCount += value;
        }


        // ポーズ状態の場合はtrue、プレイ状態の場合はfalse
        bool isPaused = false;

        // コンポーネントを参照しておく変数
        Animator animator;
        // AnimatorパラメーターID
        static readonly int fadeOutId = Animator.StringToHash("FadeOut");

        #region インスタンスへのstaticなアクセスポイント
        // このクラスのインスタンスを取得します。
        public static StageScene Instance { get; private set; }

        // AwakeメソッドはStartメソッドよりも先に実行されます。
        // 他のオブジェクトの初期化よりも優先して実行しておかな
        // いといけない初期化処理を記述します。
        private void Awake()
        {
            // 自分自身を唯一のインスタンスとして登録
            Instance = this;

            // Resourcesフォルダーからステージのプレハブを読み込む
            var prefabName = string.Format("Stage{0}", stageNo);
            var stage = Resources.Load<GameObject>(prefabName);
            // ステージを生成する
            Instantiate(stage, transform);
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            // コンポーネントを参照
            //animator = GetComponent<Animator>();

            // カウントダウン演出を開始
            sceneState = SceneState.Start;
            Instantiate(countDownPrefab, uiRoot);
        }

        // ステージのプレイを開始します。
        public void StartStage()
        {
            sceneState = SceneState.Play;
            //Debug.Log("スタート！");
        }

        // Update is called once per frame
        void Update()
        {
            switch (sceneState)
            {
                case SceneState.Play:
                    if (!isPaused)
                    {
                        // ポーズ
                        if (Input.GetButtonDown("Cancel"))
                        {
                            Pause();
                        }
                    }
                    break;
                case SceneState.Start:
                case SceneState.GameOver:
                case SceneState.StageClear:
                default:
                    break;
            }
        }

        #region ステージクリアー
        // このステージをステージクリアーとします。
        public void StageClear()
        {
            // ステージプレイ中のみ
            if (sceneState == SceneState.Play)
            {
                sceneState = SceneState.StageClear;
                allstar += StarCount;
                // StageClearプレハブをCanvasにインスタンス生成
                Instantiate(stageClearPrefab, uiRoot);
            }
        }

        // 次のステージを読み込みます。
        public void LoadNextScene()
        {
            StageNo++;
            // 次のステージへ
            if (StageNo < stageCount)
            {
                // "Stage"シーンをリロードする
                SceneManager.LoadScene("Stage");
            }
            // ゲームクリアーへ
            else
            {
                //[ogawa]ゲームクリア時に現在のステージの初期化
                StageNo = 0;

                // [ogawa]"GameClear"シーンをロードする
                SceneManager.LoadScene("GameClear");
                if (allstar < 9)
                {
                    SceneManager.LoadScene("GameClear");
                }
                // [ogawa]"GameClear2"シーンをロードする
                else if (allstar >= 10)
                {
                    PlayerPrefs.SetInt("allstar", allstar);
                    SceneManager.LoadScene("GameClear2");
                }
            }
        }
        #endregion

        #region ゲームオーバー
        // このステージをゲームオーバーとします。
        public void GameOver()
        {
            // ステージプレイ中のみ
            if (sceneState == SceneState.Play)
            {
                sceneState = SceneState.GameOver;
                // GameOverプレハブをCanvasにインスタンス生成
                Instantiate(gameOverPrefab, uiRoot);
            }
        }

        // このステージをリトライします。
        public void Retry()
        {
            // ポーズ中の場合は先に解除する
            Resume();
            SceneManager.LoadScene("Stage");
        }

        

        // このステージをギブアップしてタイトル画面に戻ります。
        public void GiveUp()
        {
            // ポーズ中の場合は先に解除する
            Resume();
            StageNo = 0;
            SceneManager.LoadScene("Title");
        }
        #endregion

        #region ポーズ
        // このステージをポーズします。
        private void Pause()
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
                Instantiate(pausePrefab, uiRoot);
            }
        }

        // ポーズ状態を解除します。
        public void Resume()
        {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1;
            }
        }
        #endregion
    }
}