using UnityEngine;

namespace RunGame
{
    // �v���C���[�̓����𐧌䂵�܂��BFF
    public class Player : MonoBehaviour
    {
        // �ʏ펞�̈ړ����x���w�肵�܂��B
        [SerializeField]
        private float walkSpeed = 4;
        // �_�b�V�����̈ړ����x���w�肵�܂��B
        [SerializeField]
        private float runSpeed = 8;
        // ���݂̈ړ����x
        float speed = 4;
        // �W�����v�͂��w�肵�܂��B
        [SerializeField]
        private Vector2 jumpPower = new Vector2(0, 6);
        // �ǂւ̑��ʏՓˎ��̃m�b�N�o�b�N�͂��w�肵�܂��B
        [SerializeField]
        Vector2 knockBackPower = new Vector2(-0.5f, 1);
        // �n�ʂƐڐG���Ă���ꍇ��true�A�󒆂ɂ���ꍇ��false
        [SerializeField]
        private bool isGrounded = true;
        // �n�ʃ��C���[���w�肵�܂��B
        [SerializeField]
        private LayerMask groundLayer = default;
        // �n�ʂƂ̔�������邽�߂̃`�F�b�J�[���w�肵�܂��B
        [SerializeField]
        private Transform groundChecker = null;
        // ���ʂ̃m�b�N�o�b�N��������邽�߂̃`�F�b�J�[���w�肵�܂��B
        [SerializeField]
        private Transform wallChecker = null;

        // [ogawa]���̃m�b�N�o�b�N��������邽�߂̃`�F�b�J�[���w�肵�܂��B
        [SerializeField]
        private Transform backWallChecker = null;

        //[ogawa]float�^��power�Ƃ����ϐ������A3f��������
        private float power = 3.0f;

        //[wang]�n���X�^�[�W�F�b�g
        [SerializeField]
        private float flightPower = 4.0f;
        //[wang]������
        private bool isflight = false;
        //[wang]�g��
        float upforce = 3;
        //[wang]�W�����v�����p
        int jumpcount = 0;

        // �v���C���[�̏�Ԃ�\���܂��B
        enum PlayerState
        {
            // [ogawa]�ҋ@���
            Wait,
            // �ʏ�̑��s���
            Walk,
            // �n�ʂ��畂���ăW�����v���̏��
            Jump,
            //[wang]Dush
            Dush,
            //[wang][ogawa]
            // ������
            Flight,
        }
        // ���݂̃v���C���[���
        [SerializeField]
        PlayerState currentState = PlayerState.Wait;

        // �_�b�V�����̃T�E���h���w�肵�܂��B
        [SerializeField]
        private AudioClip soundOnRun = null;

        //[ogawa]�W�����v���̃T�E���h���w�肵�܂��B
        [SerializeField]
        private AudioClip soundOnJump = null;


        // �]�|�ɂ��Q�[���I�[�o�[�Ɣ��肷��^�C���A�E�g���ԁi�b�j���w�肵�܂��B
        [SerializeField]
        float tumbleTimeout = 1.5f;
        // �]�|���Ă���ݐώ���(�b)
        float tumbleTime = 0;

        // �R���|�[�l���g�����O�ɎQ�Ƃ���ϐ�
        new Rigidbody2D rigidbody;
        AudioSource audioSource;
        Animator animator;
        //Animator�̃p�����[�^�[ID
        //[ogawa]�ҋ@�A�j���[�V����
        static readonly int waitId = Animator.StringToHash("Wait");
        static readonly int walkId = Animator.StringToHash("Walk");
        static readonly int jumpId = Animator.StringToHash("Jump");
        //[wang]����A�j���[�V����
        static readonly int flightId = Animator.StringToHash("Flight");
   

        // Start is called before the first frame update
        void Start()
        {
            // �R���|�[�l���g���擾
            rigidbody = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }
        // Wait�X�e�[�g�ɑJ�ڂ����܂��B
        private void SetWaitState()
        {
            animator.SetTrigger(waitId);
            currentState = PlayerState.Wait;
        }

        // Walk�X�e�[�g�ɑJ�ڂ����܂��B
        private void SetWalkState()
        {
            animator.SetTrigger(walkId);
            speed = walkSpeed;
            currentState = PlayerState.Walk;
            //[wang]�W�����v�񐔃��Z�b�g
            jumpcount = 0;
            //Debug.Log("������");
        }

        private void SetDushState()
        {
            //[wang]�ꎞ���O
            //animator.SetTrigger(dushId);
            currentState = PlayerState.Dush;
        }

        // Jump�X�e�[�g�ɑJ�ڂ����܂��B
        private void SetJumpState()
        {
            animator.SetTrigger(jumpId);
            currentState = PlayerState.Jump;
        }

        // Flight�X�e�[�g�ɑJ�ڂ����܂��B
        private void SetFlightState()
        {
            animator.SetTrigger(flightId);
            currentState = PlayerState.Flight;
        }

        // Update is called once per frame
        void Update()
        {
            //�v���C���[�̏�Ԃ��Ƃ�Update������U�蕪����
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

        //[ogawa]Wait�X�e�[�g�̍X�V����
        private void UpdateForWaitState()
        {
            // �W�����v
            //[ogawa]�t���[�Y�΍�ň�񏜊O
            //if (Input.GetButtonDown(""))
            //{
            //    SetJumpState();
            //   //rigidbody.velocity = new Vector3(0, 40, 0);
            //}
            //[ogawa]�@WASD�ړ�
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (y > 0 && jumpcount < 1)
            {
                Vector2 JumpHoukou = new Vector2(0, y * 10);
                rigidbody.AddForce(JumpHoukou, ForceMode2D.Impulse);
                jumpcount =1;
            }


            //���s
            if (x!=0)
            {
                SetWalkState();
            }
        }

        // Walk�X�e�[�g�̍ۂ̃t���[���X�V�����ł��B
        private void UpdateForWalkState()
        {
            // �W�����v
            //[wang]�t���[�Y�΍�ň�񏜊O
            //if (Input.GetButtonDown("Jump"))
            //{
            //    SetJumpState();
            //   rigidbody.velocity = new Vector3(0, 40, 0);
            //}

            //�_�b�V��
            //[wang]�t���[�Y�΍�ň�񏜊O
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    SetDushState();
            //}

            //[ogawa]�@WASD�ړ�
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
            //[wang]�n���X�^�[���]
            if (x == 1||x==-1)
            {
                transform.localScale = new Vector3(x, 1, 1);
                //Debug.Log("���o�[�X");
            }
            else if(x==0)
            {
                SetWaitState();
            }
        }

        // Jump�X�e�[�g�̍ۂ̃t���[���X�V�����ł��B
        private void UpdateForJumpState()
        {
            isGrounded = false;
            float x = Input.GetAxisRaw("Horizontal");

            Vector2 IdouHoukou = new Vector2(x * power, rigidbody.velocity.y);
            if (!isflight)
            {
                rigidbody.velocity = IdouHoukou;
            }
            //[wang] ������
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

        //[ogawa]Flight�X�e�[�g�̍X�V����
        private void UpdateForFlightState()
        {
            Debug.Log("���");
        }


        private void UpdateForDushState()
        {
            
        }

        //[ogawa]�G�ɓ��������玀�S
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") )
            {
                StageScene.Instance.GameOver();
            }
        }



        // �Œ�t���[�����[�g�ŌĂяo�����X�V�����ł��B
        private void FixedUpdate()
        {

            //[wang]�g��
            if (!isflight)
            {
                rigidbody.AddForce(Vector2.up * upforce, ForceMode2D.Force);
            }
            // ���n����
            var result = Physics2D.OverlapBox(
                groundChecker.position,
                groundChecker.lossyScale,
                groundChecker.rotation.eulerAngles.z,
                groundLayer);
            // �ڒn���Ă���ꍇ
            if (result)
            {
                // ���񒅒n�����ꍇ
                if (!isGrounded)
                {
                    // �]�|���Ă��Ȃ��ꍇ
                    if (!IsTumble())
                    {
                        isGrounded = true;
                        isflight = false;
                        //[wang]�W�����v�񐔃��Z�b�g
                        jumpcount = 0;
                        //Debug.Log("FixedUpdate -> SetWalkState");
                        SetWaitState();
                    }
                }
            }
            // �󒆂ɕ��V���Ă���ꍇ
            else
            {
                isGrounded = false;
                // Walk�ARun�Ȃ�Jump�ȊO�̃X�e�[�g�ő��𓥂݊O�����ꍇ
                if (currentState != PlayerState.Jump)
                {
                    // �R���R�������~
                    audioSource.Stop();
                    SetJumpState();
                }
            }

            // [ogawa]��1�Ƃ̏Փ˔���
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
                // �ǂɉ�����Փˁ@���@�]�|��ԂłȂ�
                if (!IsTumble())
                {
                    // �m�b�N�o�b�N
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
            
            // [ogawa]��2�Ƃ̏Փ˔���
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
                // �ǂɉ�����Փˁ@���@�]�|��ԂłȂ�
                if (!IsTumble())
                {
                    // �m�b�N�o�b�N
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

            // �]�|��Ԃ𔻒�
            if (IsTumble())
            {
                // �]�|��Ԃ��L�[�v����Ă��鎞�Ԃ��v������
                tumbleTime += Time.fixedDeltaTime;
                if (tumbleTime > tumbleTimeout)
                {
                    // �Q�[���I�[�o�[�Ƃ���
                    StageScene.Instance.GameOver();
                }
            }
            // �]�|��Ԃ̔��������
            else
            {
                tumbleTime = 0;
            }
        }

        // �]�|��Ԃ̏ꍇ��true��Ԃ��܂��B
        bool IsTumble()
        {
            // �]�|����
            var zAngle = Mathf.Repeat(transform.eulerAngles.z, 360);
            if (zAngle < 60 || zAngle > 300)
            {
                return false;
            }
            return true;
        }

        // �v���C���[�����̃I�u�W�F�N�g�̃g���K�[�ɐN�������ۂɌĂяo����܂��B
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �S�[������
            if (collision.CompareTag("Finish"))
            {
                StageScene.Instance.StageClear();
            }
            // �Q�[���I�[�o�[����
            else if (collision.CompareTag("GameOver"))
            {
                StageScene.Instance.GameOver();
            }
        }
    }
}