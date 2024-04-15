using UnityEngine.SceneManagement;  //Unityエンジンのシーン管理プログラムの名前空間
using UnityEngine;                  //Unityのゲームエンジンの機能を使うための名前空間
using DG.Tweening;                  //DOTweenのDOVirtual.DelayedCall()を利用するための名前空間

//シーン変更関数を管理するスクリプト
public class ChangeScene : MonoBehaviour
{
    ///////////////////////////////////////////////////
    //Startボタンが押された際にMain_Sceneへ切り替える//
    ///////////////////////////////////////////////////
    
    public void Button_changesStart()
    {
        //スタートボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.StartButton);

        //３秒後に実行
        DOVirtual.DelayedCall(3.0f, () => {
            //Main_Sceneへ移動
            SceneManager.LoadScene("Main_Scene", LoadSceneMode.Single);
        });
    }

    ///////////////////////////////////////////////////
    //Yesボタンが押された際にMiddle_Sceneへ切り替える//
    ///////////////////////////////////////////////////
    
    public void Button_changeYes()
    {
        //ボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //２秒後に実行
        DOVirtual.DelayedCall(2.0f, () => {
            //Middle_Sceneへ移動
            SceneManager.LoadScene("Middle_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////
    //Noボタンが押された際にGame_Over１へ切り替える//
    /////////////////////////////////////////////////
    
    public void Button_changeNo()
    {
        //ボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //２秒後に実行
        DOVirtual.DelayedCall(2.0f, () => {
            //Game_Over1へ移動
            SceneManager.LoadScene("Game_Over1", LoadSceneMode.Single);
        });
    }

    ///////////////////////////////////////////////////
    //Retryボタンが押された際にMain_Sceneへ切り替える//
    ///////////////////////////////////////////////////
    
    public void Button_changeRetry1()
    {
        //ボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //２秒後に実行
        DOVirtual.DelayedCall(2.0f, () => {
            //Main_Sceneへ移動
            SceneManager.LoadScene("Main_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////////
    //Readyボタンが押された際にPazzle_Sceneへ切り替える//
    /////////////////////////////////////////////////////
    
    public void Button_changeReady()
    {
        //ボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //２秒後に実行
        DOVirtual.DelayedCall(2.0f, () => {
            //Pazzle_Sceneへ移動
            SceneManager.LoadScene("Pazzle_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////////
    //Retryボタンが押された際にPazzle_Sceneへ切り替える//
    /////////////////////////////////////////////////////

    public void Button_changeRetry2()
    {
        //ボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //２秒後に実行
        DOVirtual.DelayedCall(2.0f, () => {
            //Pazzle_Sceneへ移動
            SceneManager.LoadScene("Pazzle_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////////////////
    //タイトルへ戻るボタンが押された際にTitle_Sceneへ切り替える//
    /////////////////////////////////////////////////////////////
    
    public void Button_changeTitle()
    {
        //ボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //２秒後に実行
        DOVirtual.DelayedCall(2.0f, () => {

            //BGMを流す
            SoundManager.instance.playBGM(SoundManager.BGM.StartBGM);

            //Title_Sceneへ移動
            SceneManager.LoadScene("Title_Scene", LoadSceneMode.Single);
        });
    }

    ////////////////////////////////////////////////////
    //進むyボタンが押された際にFinal_Sceneへ切り替える//
    ////////////////////////////////////////////////////
    
    public void Button_changeNext()
    {
        //ボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //２秒後に実行
        DOVirtual.DelayedCall(2.0f, () => {
            //Final_Sceneへ移動
            SceneManager.LoadScene("Final_Scene", LoadSceneMode.Single);
        });
    }

    /////////////////////////////////////////////////////////////
    //タイトルへ戻るボタンが押された際にTitle_Sceneへ切り替える//
    /////////////////////////////////////////////////////////////
    
    public void Button_changeTitle2()
    {
        //ボタンSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Button);

        //２秒後に実行
        DOVirtual.DelayedCall(2.0f, () => {
            //BGMを消す
            SoundManager.instance.stopBGM(SoundManager.BGM.FinalBGM);

            //BGMを流す
            SoundManager.instance.playBGM(SoundManager.BGM.StartBGM);

            //Title_Sceneへ移動
            SceneManager.LoadScene("Title_Scene", LoadSceneMode.Single);
        });
    }
}