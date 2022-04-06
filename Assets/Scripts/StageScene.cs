using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RunGame
{
    // �X�e�[�W��ʂ̐i�s�𐧌䂵�܂��B
    public class StageScene : MonoBehaviour
    {
        // UI�\���p��Canvas���w�肵�܂��B
        [SerializeField]
        private Transform uiRoot = null;
        // �J�E���g�_�E��UI�̃v���n�u���w�肵�܂��B
        [SerializeField]
        private GameObject countDownPrefab = null;
        // �Q�[���I�[�o�[UI�̃v���n�u���w�肵�܂��B
        [SerializeField]
        private GameObject gameOverPrefab = null;
        // �X�e�[�W�N���A�[UI�̃v���n�u���w�肵�܂��B
        [SerializeField]
        private GameObject stageClearPrefab = null;
        // �|�[�YUI�̃v���n�u���w�肵�܂��B
        [SerializeField]
        private GameObject pausePrefab = null;
        // ��ʐ؂�ւ��p�̃C���[�W���w�肵�܂��B
        [SerializeField]
        private Image transitionImage = null;
        // ��ʐ؂�ւ��p�̍��e�N�X�`���[���w�肵�܂��B
        [SerializeField]
        private Sprite blackSprite = null;
        // ��ʐ؂�ւ��p�̔��e�N�X�`���[���w�肵�܂��B
        [SerializeField]
        private Sprite whiteSprite = null;

        // �X�e�[�W�̑������w�肵�܂��B
        [SerializeField]
        private int stageCount = 3;

        //[ogawa]���̑���
        public static int allstar;

        // ���݃v���C���Ă���X�e�[�W�ԍ����擾�܂��͐ݒ肵�܂��B
        public static int StageNo
        {
            get { return stageNo; }
            set { stageNo = value; }
        }
        // ���݃v���C���Ă���X�e�[�W�ԍ�
        static int stageNo = 0;

        // �X�e�[�W��ʓ��̐i�s��Ԃ�\���܂��B
        enum SceneState
        {
            // �X�e�[�W�J�n���o��
            Start,
            // �X�e�[�W�v���C��
            Play,
            // �Q�[���I�[�o�[���m�肵�Ă��ĉ��o��
            GameOver,
            // �X�e�[�W�N���A�[���m�肵�Ă��ĉ��o��
            StageClear,
        }
        SceneState sceneState = SceneState.Start;

        // [wang]���݂̐��̐����擾���܂��B
        public int StarCount { get; private set; }

        // [wang]���̃J�E���g�𑝂₷
        public void AddStar(int value)
        {
            StarCount += value;
            Debug.Log(allstar);
        }

        // [wang]���݂̐��̐����擾���܂��B
        public int LineCount { get; private set; }

        // [wang]���̃J�E���g�𑝂₷
        public void AddLine(int value)
        {
            LineCount += value;
        }


        // �|�[�Y��Ԃ̏ꍇ��true�A�v���C��Ԃ̏ꍇ��false
        bool isPaused = false;

        // �R���|�[�l���g���Q�Ƃ��Ă����ϐ�
        Animator animator;
        // Animator�p�����[�^�[ID
        static readonly int fadeOutId = Animator.StringToHash("FadeOut");

        #region �C���X�^���X�ւ�static�ȃA�N�Z�X�|�C���g
        // ���̃N���X�̃C���X�^���X���擾���܂��B
        public static StageScene Instance { get; private set; }

        // Awake���\�b�h��Start���\�b�h������Ɏ��s����܂��B
        // ���̃I�u�W�F�N�g�̏����������D�悵�Ď��s���Ă�����
        // ���Ƃ����Ȃ��������������L�q���܂��B
        private void Awake()
        {
            // �������g��B��̃C���X�^���X�Ƃ��ēo�^
            Instance = this;

            // Resources�t�H���_�[����X�e�[�W�̃v���n�u��ǂݍ���
            var prefabName = string.Format("Stage{0}", stageNo);
            var stage = Resources.Load<GameObject>(prefabName);
            // �X�e�[�W�𐶐�����
            Instantiate(stage, transform);
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            // �R���|�[�l���g���Q��
            //animator = GetComponent<Animator>();

            // �J�E���g�_�E�����o���J�n
            sceneState = SceneState.Start;
            Instantiate(countDownPrefab, uiRoot);
        }

        // �X�e�[�W�̃v���C���J�n���܂��B
        public void StartStage()
        {
            sceneState = SceneState.Play;
            //Debug.Log("�X�^�[�g�I");
        }

        // Update is called once per frame
        void Update()
        {
            switch (sceneState)
            {
                case SceneState.Play:
                    if (!isPaused)
                    {
                        // �|�[�Y
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

        #region �X�e�[�W�N���A�[
        // ���̃X�e�[�W���X�e�[�W�N���A�[�Ƃ��܂��B
        public void StageClear()
        {
            // �X�e�[�W�v���C���̂�
            if (sceneState == SceneState.Play)
            {
                sceneState = SceneState.StageClear;
                allstar += StarCount;
                // StageClear�v���n�u��Canvas�ɃC���X�^���X����
                Instantiate(stageClearPrefab, uiRoot);
            }
        }

        // ���̃X�e�[�W��ǂݍ��݂܂��B
        public void LoadNextScene()
        {
            StageNo++;
            // ���̃X�e�[�W��
            if (StageNo < stageCount)
            {
                // "Stage"�V�[���������[�h����
                SceneManager.LoadScene("Stage");
            }
            // �Q�[���N���A�[��
            else
            {
                //[ogawa]�Q�[���N���A���Ɍ��݂̃X�e�[�W�̏�����
                StageNo = 0;

                // [ogawa]"GameClear"�V�[�������[�h����
                SceneManager.LoadScene("GameClear");
                if (allstar < 9)
                {
                    SceneManager.LoadScene("GameClear");
                }
                // [ogawa]"GameClear2"�V�[�������[�h����
                else if (allstar >= 10)
                {
                    PlayerPrefs.SetInt("allstar", allstar);
                    SceneManager.LoadScene("GameClear2");
                }
            }
        }
        #endregion

        #region �Q�[���I�[�o�[
        // ���̃X�e�[�W���Q�[���I�[�o�[�Ƃ��܂��B
        public void GameOver()
        {
            // �X�e�[�W�v���C���̂�
            if (sceneState == SceneState.Play)
            {
                sceneState = SceneState.GameOver;
                // GameOver�v���n�u��Canvas�ɃC���X�^���X����
                Instantiate(gameOverPrefab, uiRoot);
            }
        }

        // ���̃X�e�[�W�����g���C���܂��B
        public void Retry()
        {
            // �|�[�Y���̏ꍇ�͐�ɉ�������
            Resume();
            SceneManager.LoadScene("Stage");
        }

        

        // ���̃X�e�[�W���M�u�A�b�v���ă^�C�g����ʂɖ߂�܂��B
        public void GiveUp()
        {
            // �|�[�Y���̏ꍇ�͐�ɉ�������
            Resume();
            StageNo = 0;
            SceneManager.LoadScene("Title");
        }
        #endregion

        #region �|�[�Y
        // ���̃X�e�[�W���|�[�Y���܂��B
        private void Pause()
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
                Instantiate(pausePrefab, uiRoot);
            }
        }

        // �|�[�Y��Ԃ��������܂��B
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