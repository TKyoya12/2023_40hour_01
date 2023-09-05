using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //�v���C���[�O�̂Ƃ���Ƀv���C���[�P�Ɉȍ~
    //�Q�[���X�e�[�g
    public enum States//�񋓌^
    {
        Title,
        GamePlaying,
        GamePause,
        GameOver,
        GameClear,
    }
    public States state = States.Title;
    //�C���X�y�N�^�\���p
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip gamePlayBGM;
    [SerializeField] private AudioClip gameOverSE;
    [SerializeField] private AudioClip ganeClearSE;
    [SerializeField] private float retaertIntervalTime;
    [SerializeField] private float cleartIntervalTime;
    #region�@�V���O���g���L�q
    public static GameManager Instance;//�������g�̃N���X���郁�����Ƀ��[�h�����

    private void Awake()//�ŏ��Ɉ�񂾂��Ă΂��A�X�^�[�g����ɌĂ΂��A�I�u�W�F�N�g���A�N�e�B�u���
    {
        if (Instance == null)
        {
            //static�C���X�^���X�Ɏ������g�̃Q�[���I�u�W�F�N�g����
            Instance = this;

            //���̃Q�[���I�u�W�F�N�g��j�󂵂Ȃ��A�A�ǂ̃V�[������낤���I�u�W�F�N�g���c��
            DontDestroyOnLoad(gameObject);
        }
        else

        {
            //���łɑ��݂��Ă���Ȃ�폜�i�P�̂ݑ��݁j
            Destroy(gameObject);
        }

    }
    #endregion

    //�R���|�[�l���g�擾�p
    private AudioSource audioSource;
    private void Start()
    {
        //�R���|�[�l���g���擾
        audioSource = GetComponent<AudioSource>();

        //�f�o�b�N�p�F�V�[���ʏ���������
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            state = States.Title;
            audioSource.clip = titleBGM;
            audioSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            state = States.GamePlaying;
            audioSource.clip = gamePlayBGM;
            audioSource.Play();
        }
    }

    public void Update()
    {
        switch (state)
        {
            case States.GamePlaying:
                //p�{�^���������ꂽ�Ƃ���
                if (Input.GetKeyDown(KeyCode.P))
                {
                    //�|�[�Y��ʂ�\��
                    GamePause();
                }

                break;
        }
        //Debug.Log(Time.timeScale);

        //Esc�L�[����������Q�[�����I��        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuit();
        }
    }
    //�Q�[���I�[�o�[����
    public void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[");
        state = States.GameOver;

        //�Q�[���I�[�o�[SE���Đ�
        audioSource.PlayOneShot(gameOverSE);

        //�Q�[�������X�^�[�g
        Invoke(nameof(Restart), retaertIntervalTime);
    }
    public void GameClear()
    {
        state = States.GameClear;
        //�Q�[���N���ASE���Đ�
        audioSource.PlayOneShot(ganeClearSE);

        //�Q�[�������X�^�[�g
        Invoke(nameof(ChangSceneGamePlaying), retaertIntervalTime);
    }
    //<summary>
    //���X�^�[�g����
    //</summary>
    public void Restart()
    {
        //�X�e�[�g���Q�[���v���C���ɕύX
        state = States.GamePlaying;

        //���݂̃V�[�����擾���ă��[�h����
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }
    //�^�C�g���V�[����
    public void ChangeTitleScene()
    {
        //�X�e�[�g�̕ύX
        state = States.GamePlaying;

        //�^�C���X�P�[����߂�
        Time.timeScale = 1f;
        //�X�e�[�g��ύX
        state = States.Title;

        //BGM�ύX
        audioSource.clip = gamePlayBGM;
        audioSource.Play();

        //�Q�[���v���C�V�[�������[�h
        SceneManager.LoadScene("TitleScene");
    }


    public void ChangSceneGamePlaying()
    {

        //�X�e�[�g��ύX
        state = States.GamePlaying;

        //BGM�ύX
        audioSource.clip = gamePlayBGM;
        audioSource.Play();

        //�Q�[���v���C�V�[�������[�h
        SceneManager.LoadScene("GameScene");
    }

    //<summary>
    //�Q�[������
    //</summary>
    public void GamePause()
    {
        //�X�e�[�g�̕ύX
        state = States.GamePause;

        //�^�C���X�P�[���̕ύX
        Time.timeScale = 0f;
    }

    //<summary>
    //�Q�[���ĊJ����
    //</summary>
    public void GameRrsume()
    {
        //�X�e�[�g�̕ύX
        state = States.GamePlaying;

        //�^�C���X�P�[���̕ύX
        Time.timeScale = 1f;
    }

    //�Q�[���I������
    public void GameQuit()
    {
        Application.Quit();
    }
}
