using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //[SerializeField] private GameObject gameOverTest;
    [SerializeField] private GameObject gameClearTest;
    [SerializeField] private GameObject pauseCanvas;


    private void Start()
    {
       // gameOverTest.SetActive(false);
        gameClearTest.SetActive(false);
        pauseCanvas.SetActive(false);

    }
    private void Update()
    {
        switch (GameManager.Instance.state)
        {
            case GameManager.States.GamePlaying:
                //�|�[�Y�E�B���h�E���\��
                if (pauseCanvas.activeSelf)
                {
                    pauseCanvas.SetActive(false);
                }
                break;
            case GameManager.States.GamePause:
                //�|�[�Y�E�B���h�E��\��
                if (!pauseCanvas.activeSelf)
                {
                    pauseCanvas.SetActive(true);
                }
                break;
            //case GameManager.States.GameOver:
            //    //�Q�[���I�[�o�[UI �e�L�X�g��\��
            //    if (gameOverTest.activeSelf)
            //    {
            //        gameOverTest.SetActive(true);
            //    }
            //    break;
            //case GameManager.States.GameClear:
            //    //�Q�[���N���AUI �e�L�X�g��\��
            //    if (!gameOverTest.activeSelf)
            //    {
            //        gameClearTest.SetActive(true);
            //    }
                break;
        }
    }

    //�|�[�Y���(�Q�[���ĊJ�{�^���p)

    public void OnResumeButton()
    {
        GameManager.Instance.GameRrsume();
    }

    //�|�[�Y���(�^�C�g���ɖ߂�{�^���p)
    public void OnPauseButton()
    {
        GameManager.Instance.ChangeTitleScene();

    }

    //�|�[�Y���(�Q�[���I���{�^���p)
    public void OnQuitButton()
    {
        GameManager.Instance.GameQuit();
    }
}
