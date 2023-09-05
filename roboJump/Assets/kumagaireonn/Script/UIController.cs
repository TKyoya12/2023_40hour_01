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
                //ポーズウィンドウを非表示
                if (pauseCanvas.activeSelf)
                {
                    pauseCanvas.SetActive(false);
                }
                break;
            case GameManager.States.GamePause:
                //ポーズウィンドウを表示
                if (!pauseCanvas.activeSelf)
                {
                    pauseCanvas.SetActive(true);
                }
                break;
            //case GameManager.States.GameOver:
            //    //ゲームオーバーUI テキストを表示
            //    if (gameOverTest.activeSelf)
            //    {
            //        gameOverTest.SetActive(true);
            //    }
            //    break;
            //case GameManager.States.GameClear:
            //    //ゲームクリアUI テキストを表示
            //    if (!gameOverTest.activeSelf)
            //    {
            //        gameClearTest.SetActive(true);
            //    }
                break;
        }
    }

    //ポーズ画面(ゲーム再開ボタン用)

    public void OnResumeButton()
    {
        GameManager.Instance.GameRrsume();
    }

    //ポーズ画面(タイトルに戻るボタン用)
    public void OnPauseButton()
    {
        GameManager.Instance.ChangeTitleScene();

    }

    //ポーズ画面(ゲーム終了ボタン用)
    public void OnQuitButton()
    {
        GameManager.Instance.GameQuit();
    }
}
