using System.Collections.Generic;   //オブジェクトのリストを扱うための名前空間
using DG.Tweening;
using UnityEngine;                  //Unityのゲームエンジンの機能を使うための名前空間
using UnityEngine.SceneManagement;  //Unityエンジンのシーン管理プログラムの名前空間
using UnityEngine.UI;               //UnityのUI機能の名前空間

//パズルゲームの管理を行うスクリプト
public class PazzleGameManager : MonoBehaviour
{
    ////////////////////////////////////
    //ブロックの生成や操作に必要な情報//
    ////////////////////////////////////

    //ブロックを生成するスクリプト
    [SerializeField] BlockGenerator blockGenerator = default;

    //クリック最中かを判定する変数
    bool isClicking;

    //削除するブロックの格納を行うリスト
    [SerializeField] List<Block> removeBlocks = new List<Block>();

    //現在クリックしたブロック
    Block currentClickingBlock;

    //////////////////////////////
    //プレイヤーと敵のパラメータ//
    //////////////////////////////
    
    int enemyHP; //敵のHP
    [SerializeField] Text enemyHPText = default;        //スクリーン表示用
    [SerializeField] Slider enemyHPGauge = default;     //HPゲージ

    int playerHP; //プレイヤーのHP
    [SerializeField] Text playerHPText = default;       //スクリーン表示用
    [SerializeField] Slider playerHPGauge = default;    //HPゲージ

    //////////////////////////////////////////
    // Clear時に表示したいパネルオブジェクト//
    //////////////////////////////////////////
    
    public GameObject panelObject;

    ////////////////
    //スタート関数//
    ////////////////
    
    void Start()
    {
        //クリアパネルは非表示
        panelObject.SetActive(false);

        //バトルBGM設定
        SoundManager.instance.playBGM(SoundManager.BGM.BattleBGM);

        //////////////////////
        //敵パラメータの設定//
        //////////////////////

        //敵HPの初期化
        enemyHP = ParamsSO.Entity.initEnemyHP;
        //テキストの初期化
        enemyHPText.text = enemyHP.ToString() + "/" + enemyHP.ToString();
        //最大HPの設定
        enemyHPGauge.maxValue = ParamsSO.Entity.initEnemyHPGauge;
        //ゲージの初期化
        enemyHPGauge.value = ParamsSO.Entity.initEnemyHPGauge;

        //////////////////////////////
        //プレイヤーパラメータの設定//
        //////////////////////////////

        //プレイヤーHPの初期化
        playerHP = ParamsSO.Entity.initPlayerHP;
        //テキストの初期化
        playerHPText.text = playerHP.ToString() + "/" + playerHP.ToString();
        //最大HPの設定
        playerHPGauge.maxValue = ParamsSO.Entity.initPlayerHPGauge;
        //ゲージの初期化
        playerHPGauge.value = ParamsSO.Entity.initPlayerHPGauge;

        ////////////////
        //盤面の初期化//
        ////////////////
        
        StartCoroutine(blockGenerator.Spawns(ParamsSO.Entity.initBlockCount));
    }

    ////////////////////
    //アップデート関数//
    ////////////////////
    
    void Update()
    {
        //右クリックを押した時
        if (Input.GetMouseButtonDown(0))
        {
            //クリック開始関数
            Click_Start();
        }
        //右クリックを離した時
        else if (Input.GetMouseButtonUp(0))
        {
            //クリック終了関数
            Click_End();
        }
        //右クリックをしている時
        else if (isClicking) {
            //クリック中関数
            Click_Now();
        }
    }

    //////////////////////////////////
    //クリックしたら呼ばれるメソッド//
    //////////////////////////////////

    void Click_Start() {

        //マウスのクリック位置を取得
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //２Dに設定
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //////////////////////////
        //ブロックを選択する処理//
        //////////////////////////
        
        //ブロックに当たった場合
        if (hit && hit.collider.GetComponent<Block>()) {
            //クリックしたブロックを削除リストに加える
            Block block = hit.collider.GetComponent<Block>();
            AddRemoveBlock(block);
            isClicking = true;
        }
    }

    //////////////////////////////////////////
    //クリックしている最中に呼ばれるメソッド//
    //////////////////////////////////////////
    
    void Click_Now() {

        //マウスのクリック位置を取得
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //２Dに設定
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        ////////////////////////
        //ブロックを繋げる処理//
        ////////////////////////
        
        //ブロックに当たった場合
        if (hit && hit.collider.GetComponent<Block>())
        {
            Block block = hit.collider.GetComponent<Block>();
            
            //１個前ににクリックしたブロックと同じ種類か判定
            if (block.id == currentClickingBlock.id) 
            { 
                //１個前にクリックしたブロックからの距離が近ければ
                float distance = Vector2.Distance(block.transform.position, currentClickingBlock.transform.position);
                if (distance < ParamsSO.Entity.BlockDistance) {
                    //削除リストに追加する
                    AddRemoveBlock(block);
                }
            }
        }
    }

    //////////////////////////////////////
    //クリックを離したら呼ばれるメソッド//
    //////////////////////////////////////
    
    void Click_End() {

        //////////////////////////
        //ブロックを削除する処理//
        //////////////////////////
        
        //削除リストに入っている数を取得
        int removeCount = removeBlocks.Count;

        //同じブロック３個以上繋がっているか判定
        if (removeCount >= 3) {

            ////////////////////////////////////////
            //消したブロックの種類による処理の変更//
            ////////////////////////////////////////

            //剣ブロックの場合：消した数×３０だけ敵のHPを減らす
            if (removeBlocks[0].id == 0)
            {
                //敵のパラメータの変更
                enemyHP -= ParamsSO.Entity.SwordPoint * removeCount;
                enemyHPText.text = enemyHP.ToString() + "/" + enemyHPGauge.maxValue;
                
                //攻撃音を鳴らす
                SoundManager.instance.playSE(SoundManager.SE.Sword);

                //敵のHPゲージを減らす
                enemyHPGauge.value = enemyHP;

                //敵のHPが０未満になったら、クリアメソッドを実行
                if (enemyHP < 0) {
                    Clear();
                }
            }
            //お玉ブロックの場合：消した数×10だけ敵のHPを減らす
            else if (removeBlocks[0].id == 1)
            {
                //敵のパラメータの変更
                enemyHP -= ParamsSO.Entity.LadlePoint * removeCount;
                enemyHPText.text = enemyHP.ToString() + "/" + enemyHPGauge.maxValue;

                //攻撃音を鳴らす
                SoundManager.instance.playSE(SoundManager.SE.Ladle);

                //敵のHPゲージを減らす
                enemyHPGauge.value = enemyHP;

                //敵のHPが０未満になったら、クリアメソッドを実行
                if (enemyHP < 0)
                {
                    Clear();
                }
            }

            //ハートブロックの場合：消した数×2０だけプレイヤーHPが回復する
            else if (removeBlocks[0].id == 2)
            {
                //プレイヤーパラメータの変更
                playerHP += ParamsSO.Entity.HeartPoint * removeCount;
                playerHPText.text = playerHP.ToString() + "/" + playerHPGauge.maxValue;

                //回復音を鳴らす
                SoundManager.instance.playSE(SoundManager.SE.Heart);

                //プレイヤーHPのゲージを増やす
                playerHPGauge.value = playerHP;
            }

            //ドクロブロックの場合：消した数×１５だけプレイヤーHPが減る
            else if (removeBlocks[0].id == 3)
            {
                //プレイヤーパラメータの変更
                playerHP -= ParamsSO.Entity.SkullPoint * removeCount; ;
                playerHPText.text = playerHP.ToString() + "/" + playerHPGauge.maxValue;

                //ダメージ音を鳴らす
                SoundManager.instance.playSE(SoundManager.SE.Skull);

                //プレイヤーのHPゲージを減らす
                playerHPGauge.value = playerHP;

                //プレイヤーのHPが０未満になったら、ゲームオーバーを実行
                if (playerHP < 0)
                {
                    GameOver();
                }
            }
            else 
            {
                Debug.Log("エラー");
            }

            //////////////////////
            //ブロックの削除処理//
            //////////////////////
            
            //リストに追加されたブロックを削除する
            for (int i = 0; i < removeCount; i++)
            {
                //破裂アニメーションを再生
                removeBlocks[i].Explosion();
            }

            //消したブロックの数だけBlockGeneratorで生成する
            StartCoroutine(blockGenerator.Spawns(removeCount));
        }
        
        ////////////////////////////////////////////
        //選んだブロックの大きさと色を元に戻す処理//
        ////////////////////////////////////////////
        
        for (int i = 0; i < removeCount; i++) {
            removeBlocks[i].transform.localScale = Vector3.one * 5.0f;
            removeBlocks[i].GetComponent<SpriteRenderer>().color = Color.white;
        }

        //////////////////////////////////////////////////////
        //削除リストを空にして、クリック状態の解除をする処理//
        //////////////////////////////////////////////////////
        
        removeBlocks.Clear();
        isClicking = false;
    }

    ////////////////////////////////////////////////////
    //指定されたブロックを削除リストに追加するメソッド//
    ////////////////////////////////////////////////////
    
    void AddRemoveBlock(Block block) 
    {
        //現在クリックしているブロックを更新する
        currentClickingBlock = block;
        
        //
        if (removeBlocks.Contains(block) == false) {
            //選んだブロックを大きくする
            block.transform.localScale = Vector3.one * 6.0f;

            //選んだブロックの色を変える
            block.GetComponent<SpriteRenderer>().color = Color.cyan;

            //削除リストに追加
            removeBlocks.Add(block);

            //ブロックに触れた時に、BlockSEを鳴らす
            SoundManager.instance.playSE(SoundManager.SE.Block);
        }
    }
    
    //////////////////
    //クリアメソッド//
    //////////////////
    
    private void Clear()
    {
        //とどめSEを流す
        SoundManager.instance.playSE(SoundManager.SE.LastAttack);

        //バトルBGMをストップ
        SoundManager.instance.stopBGM(SoundManager.BGM.BattleBGM);

        //クリア画面表示
        panelObject.SetActive(true);

        //クリアSEを流す
        SoundManager.instance.playSE(SoundManager.SE.Victory);
    }

    //////////////////////////
    //ゲームオーバーメソッド//
    //////////////////////////
    
    private void GameOver()
    {
        //１秒後に実行
        DOVirtual.DelayedCall(1.0f, () => {
            //GameOver2シーンへ移動
            SceneManager.LoadScene("Game_Over2", LoadSceneMode.Single);
        });
    }
}