using UnityEngine;

namespace RunGame
{
    // プレイヤーの動きを制御します。FF
    public class Player : MonoBehaviour
    {
        // 通常時の移動速度を指定します。
        [SerializeField]
        private float walkSpeed = 4;
        // ダッシュ時の移動速度を指定します。
        [SerializeField]
        private float runSpeed = 8;
        // 現在の移動速度
        float speed = 4;
        // ジャンプ力を指定します。
        [SerializeField]
        private Vector2 jumpPower = new Vector2(0, 6);
        // 壁への側面衝突時のノックバック力を指定します。
        [SerializeField]
        Vector2 knockBackPower = new Vector2(-0.5f, 1);
        // 地面と接触している場合はtrue、空中にいる場合はfalse
        [SerializeField]
        private bool isGrounded = true;
        // 地面レイヤーを指定します。
        [SerializeField]
        private LayerMask groundLayer = default;
        // 地面との判定をするためのチェッカーを指定します。
        [SerializeField]
        private Transform groundChecker = null;
        // 正面のノックバック判定をするためのチェッカーを指定します。
        [SerializeField]
        private Transform wallChecker = null;

        // [ogawa]後ろのノックバック判定をするためのチェッカーを指定します。
        [SerializeField]
        private Transform backWallChecker = null;

        //[ogawa]float型のpowerという変数を作り、3fを代入する
        private float power = 3.0f;

        //[wang]ハムスタージェット
        [SerializeField]
        private float flightPower = 4.0f;
        //[wang]滑空状態
        private bool isflight = false;
        //[wang]揚力
        float upforce = 3;
        //[wang]ジャンプ制限用
        int jumpcount = 0;

        // プレイヤーの状態を表します。
        enum PlayerState
        {
            // [ogawa]待機状態
            Wait,
            // 通常の走行状態
            Walk,
            // 地面から浮いてジャンプ中の状態
            Jump,
            //[wang]Dush
            Dush,
            //[wang][ogawa]
            // 滑空状態
            Flight,
        }
        // 現在のプレイヤー状態
        [SerializeField]
        PlayerState currentState = PlayerState.Wait;

        // ダッシュ時のサウンドを指定します。
        [SerializeField]
        private AudioClip soundOnRun = null;

        //[ogawa]ジャンプ時のサウンドを指定します。
        [SerializeField]
        private AudioClip soundOnJump = null;


        // 転倒によるゲームオーバーと判定するタイムアウト時間（秒）を指定します。
        [SerializeField]
        float tumbleTimeout = 1.5f;
        // 転倒している累積時間(秒)
        float tumbleTime = 0;

        // コンポーネントを事前に参照する変数
        new Rigidbody2D rigidbody;
        AudioSource audioSource;
        Animator animator;
        //AnimatorのパラメーターID
        //[ogawa]待機アニメーション
        static readonly int waitId = Animator.StringToHash("Wait");
        static readonly int walkId = Animator.StringToHash("Walk");
        static readonly int jumpId = Animator.StringToHash("Jump");
        //[wang]滑空アニメーション
        static readonly int flightId = Animator.StringToHash("Flight");
   

        // Start is called before the first frame update
        void Start()
        {
            // コンポーネントを取得
            rigidbody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }
        // Waitステートに遷移させます。
        private void SetWaitState()
        {
            animator.SetTrigger(waitId);
            currentState = PlayerState.Wait;
        }

        // Walkステートに遷移させます。
        private void SetWalkState()
        {
            animator.SetTrigger(walkId);
            speed = walkSpeed;
            currentState = PlayerState.Walk;
            //[wang]ジャンプ回数リセット
            jumpcount = 0;
            //Debug.Log("歩いた");
        }

        private void SetDushState()
        {
            //[wang]一時除外
            //animator.SetTrigger(dushId);
            currentState = PlayerState.Dush;
        }

        // Jumpステートに遷移させます。
        private void SetJumpState()
        {
            animator.SetTrigger(jumpId);
            currentState = PlayerState.Jump;
        }

        // Flightステートに遷移させます。
        private void SetFlightState()
        {
            animator.SetTrigger(flightId);
            currentState = PlayerState.Flight;
        }

        // Update is called once per frame
        void Update()
        {
            //プレイヤーの状態ごとにUpdate処理を振り分ける
            switch (currentState)
            {
                case PlayerState.Wait:
                    UpdateForWaitState();
                    break;
                case PlayerState.Walk:
                    UpdateForWalkState();
                    break;
                case PlayerState.Jump:
                    UpdateForJumpState();
                    break;
                case PlayerState.Flight:
                    UpdateForFlightState();
                    break;
                case PlayerState.Dush:
                    UpdateForDushState();
                    break;
                default:
                    break;
            }
        }

        //[ogawa]Waitステートの更新処理
        private void UpdateForWaitState()
        {
            // ジャンプ
            //[ogawa]フリーズ対策で一回除外
            //if (Input.GetButtonDown(""))
            //{
            //    SetJumpState();
            //   //rigidbody.velocity = new Vector3(0, 40, 0);
            //}
            //[ogawa]　WASD移動
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (y > 0 && jumpcount < 1)
            {
                Vector2 JumpHoukou = new Vector2(0, y * 10);
                rigidbody.AddForce(JumpHoukou, ForceMode2D.Impulse);
                jumpcount =1;
            }


            //走行
            if (x!=0)
            {
                SetWalkState();
            }
        }

        // Walkステートの際のフレーム更新処理です。
        private void UpdateForWalkState()
        {
            // ジャンプ
            //[wang]フリーズ対策で一回除外
            //if (Input.GetButtonDown("Jump"))
            //{
            //    SetJumpState();
            //   rigidbody.velocity = new Vector3(0, 40, 0);
            //}

            //ダッシュ
            //[wang]フリーズ対策で一回除外
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    SetDushState();
            //}

            //[ogawa]　WASD移動
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Vector2 IdouHoukou = new Vector2(x, 0);
            rigidbody.velocity = IdouHoukou * power;
            if (y >= 0.2)
            {
                Vector2 JumpHoukou = new Vector2(0,  10);
                rigidbody.AddForce(JumpHoukou, ForceMode2D.Impulse);
                jumpcount=1;
                Debug.Log(jumpcount);
            }
            //[wang]ハムスター反転
            if (x == 1||x==-1)
            {
                transform.localScale = new Vector3(x, 1, 1);
                //Debug.Log("リバース");
            }
            else if(x==0)
            {
                SetWaitState();
            }
        }

        // Jumpステートの際のフレーム更新処理です。
        private void UpdateForJumpState()
        {
            isGrounded = false;
            float x = Input.GetAxisRaw("Horizontal");

            Vector2 IdouHoukou = new Vector2(x * power, rigidbody.velocity.y);
            if (!isflight)
            {
                rigidbody.velocity = IdouHoukou;
            }
            //[wang] 滑空状態
            if ((Input.GetButtonDown("Jump")) && (jumpcount < 3))
            {
                //animator.SetTrigger(flightId);
                //rigidbody.drag = 10;

                if (gameObject.transform.localScale.x == 1)
                {
                    rigidbody.AddForce(transform.TransformDirection(flightPower * transform.right), ForceMode2D.Impulse);

                }
                else if (gameObject.transform.localScale.x == -1)
                {
                    rigidbody.AddForce(transform.TransformDirection(-flightPower * transform.right), ForceMode2D.Impulse);
                }
                jumpcount++;
                isflight = true;
                SetFlightState();
            }
        }

        //[ogawa]Flightステートの更新処理
        private void UpdateForFlightState()
        {
            Debug.Log("飛んだ");
        }


        private void UpdateForDushState()
        {
            
        }

        //[ogawa]敵に当たったら死亡
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") )
            {
                StageScene.Instance.GameOver();
            }
        }



        // 固定フレームレートで呼び出される更新処理です。
        private void FixedUpdate()
        {

            //[wang]揚力
            if (!isflight)
            {
                rigidbody.AddForce(Vector2.up * upforce, ForceMode2D.Force);
            }
            // 着地判定
            var result = Physics2D.OverlapBox(
                groundChecker.position,
                groundChecker.lossyScale,
                groundChecker.rotation.eulerAngles.z,
                groundLayer);
            // 接地している場合
            if (result)
            {
                // 今回着地した場合
                if (!isGrounded)
                {
                    // 転倒していない場合
                    if (!IsTumble())
                    {
                        isGrounded = true;
                        isflight = false;
                        //[wang]ジャンプ回数リセット
                        jumpcount = 0;
                        //Debug.Log("FixedUpdate -> SetWalkState");
                        SetWaitState();
                    }
                }
            }
            // 空中に浮遊している場合
            else
            {
                isGrounded = false;
                // Walk、RunなどJump以外のステートで足を踏み外した場合
                if (currentState != PlayerState.Jump)
                {
                    // コロコロ音を停止
                    audioSource.Stop();
                    SetJumpState();
                }
            }

            // [ogawa]壁1との衝突判定
            result = Physics2D.OverlapBox(
                wallChecker.position,
                wallChecker.lossyScale,
                wallChecker.rotation.eulerAngles.z,
                groundLayer);
            if (result)
            {
                var velocity = rigidbody.velocity;
                velocity.x = 0;
                rigidbody.velocity = velocity;
                // 壁に横から衝突　＝　転倒状態でない
                if (!IsTumble())
                {
                    // ノックバック
                    //rigidbody.AddForce(transform.TransformDirection(knockBackPower), ForceMode2D.Impulse);
                    if (transform.localScale.x < 0)
                    {
                        rigidbody.AddForce(transform.TransformDirection(-knockBackPower), ForceMode2D.Impulse);
                    }
                    else if (transform.localScale.x > 0)
                    {
                        rigidbody.AddForce(transform.TransformDirection(knockBackPower), ForceMode2D.Impulse);
                    }
                }
    
            }
            
            // [ogawa]壁2との衝突判定
            result = Physics2D.OverlapBox(
                backWallChecker.position,
                backWallChecker.lossyScale,
                backWallChecker.rotation.eulerAngles.z,
                groundLayer);
            if (result)
            {
                var velocity = rigidbody.velocity;
                velocity.x = 0;
                rigidbody.velocity = velocity;
                // 壁に横から衝突　＝　転倒状態でない
                if (!IsTumble())
                {
                    // ノックバック
                    //rigidbody.AddForce(transform.TransformDirection(knockBackPower), ForceMode2D.Impulse);
                    if (transform.localScale.x < 0)
                    {
                        rigidbody.AddForce(transform.TransformDirection(knockBackPower), ForceMode2D.Impulse);
                    }
                    else if (transform.localScale.x > 0)
                    {
                        rigidbody.AddForce(transform.TransformDirection(-knockBackPower), ForceMode2D.Impulse);
                    }
                }

            }

            // 転倒状態を判定
            if (IsTumble())
            {
                // 転倒状態がキープされている時間を計測する
                tumbleTime += Time.fixedDeltaTime;
                if (tumbleTime > tumbleTimeout)
                {
                    // ゲームオーバーとする
                    StageScene.Instance.GameOver();
                }
            }
            // 転倒状態の判定を解除
            else
            {
                tumbleTime = 0;
            }
        }

        // 転倒状態の場合はtrueを返します。
        bool IsTumble()
        {
            // 転倒判定
            var zAngle = Mathf.Repeat(transform.eulerAngles.z, 360);
            if (zAngle < 60 || zAngle > 300)
            {
                return false;
            }
            return true;
        }

        // プレイヤーが他のオブジェクトのトリガーに侵入した際に呼び出されます。
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ゴール判定
            if (collision.CompareTag("Finish"))
            {
                StageScene.Instance.StageClear();
            }
            // ゲームオーバー判定
            else if (collision.CompareTag("GameOver"))
            {
                StageScene.Instance.GameOver();
            }
        }
    }
}