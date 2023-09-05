using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //プレイヤー０のところにプレイヤー１に以降
    //ゲームステート
    public enum States//列挙型
    {
        Title,
        GamePlaying,
        GamePause,
        GameOver,
        GameClear,
    }
    public States state = States.Title;
    //インスペクタ表示用
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip gamePlayBGM;
    [SerializeField] private AudioClip gameOverSE;
    [SerializeField] private AudioClip ganeClearSE;
    [SerializeField] private float retaertIntervalTime;
    [SerializeField] private float cleartIntervalTime;
    #region　シングルトン記述
    public static GameManager Instance;//自分自身のクラスあるメモリにロードされる

    private void Awake()//最初に一回だけ呼ばれる、スタートより先に呼ばれる、オブジェクトを非アクティブ状態
    {
        if (Instance == null)
        {
            //staticインスタンスに自分自身のゲームオブジェクトを代入
            Instance = this;

            //このゲームオブジェクトを破壊しない、、どのシーンを作ろうがオブジェクトが残る
            DontDestroyOnLoad(gameObject);
        }
        else

        {
            //すでに存在しているなら削除（１つのみ存在）
            Destroy(gameObject);
        }

    }
    #endregion

    //コンポーネント取得用
    private AudioSource audioSource;
    private void Start()
    {
        //コンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        //デバック用：シーン別初期化処理
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
                //pボタンが押されたときに
                if (Input.GetKeyDown(KeyCode.P))
                {
                    //ポーズ画面を表示
                    GamePause();
                }

                break;
        }
        //Debug.Log(Time.timeScale);

        //Escキーを押したらゲームを終了        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameQuit();
        }
    }
    //ゲームオーバー処理
    public void GameOver()
    {
        Debug.Log("ゲームオーバー");
        state = States.GameOver;

        //ゲームオーバーSEを再生
        audioSource.PlayOneShot(gameOverSE);

        //ゲームをリスタート
        Invoke(nameof(Restart), retaertIntervalTime);
    }
    public void GameClear()
    {
        state = States.GameClear;
        //ゲームクリアSEを再生
        audioSource.PlayOneShot(ganeClearSE);

        //ゲームをリスタート
        Invoke(nameof(ChangSceneGamePlaying), retaertIntervalTime);
    }
    //<summary>
    //リスタート処理
    //</summary>
    public void Restart()
    {
        //ステートをゲームプレイ中に変更
        state = States.GamePlaying;

        //現在のシーンを取得してロードする
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }
    //タイトルシーンに
    public void ChangeTitleScene()
    {
        //ステートの変更
        state = States.GamePlaying;

        //タイムスケールを戻す
        Time.timeScale = 1f;
        //ステートを変更
        state = States.Title;

        //BGM変更
        audioSource.clip = gamePlayBGM;
        audioSource.Play();

        //ゲームプレイシーンをロード
        SceneManager.LoadScene("TitleScene");
    }


    public void ChangSceneGamePlaying()
    {

        //ステートを変更
        state = States.GamePlaying;

        //BGM変更
        audioSource.clip = gamePlayBGM;
        audioSource.Play();

        //ゲームプレイシーンをロード
        SceneManager.LoadScene("GameScene");
    }

    //<summary>
    //ゲーム処理
    //</summary>
    public void GamePause()
    {
        //ステートの変更
        state = States.GamePause;

        //タイムスケールの変更
        Time.timeScale = 0f;
    }

    //<summary>
    //ゲーム再開処理
    //</summary>
    public void GameRrsume()
    {
        //ステートの変更
        state = States.GamePlaying;

        //タイムスケールの変更
        Time.timeScale = 1f;
    }

    //ゲーム終了処理
    public void GameQuit()
    {
        Application.Quit();
    }
}
