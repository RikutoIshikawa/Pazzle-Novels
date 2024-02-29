using System.Collections.Generic; //ジェネリック（汎用的な）コレクションクラスやインターフェースを操作するための名前空間
using DG.Tweening; //Unityでのアニメーションの作成や管理を容易にするためのライブラリ
using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間
using UnityEngine.SceneManagement; //Unityでシーンの読み込みや切り替え、シーン間のデータのやり取りなどを行うための名前空間
using UnityEngine.UI; //UnityのUI要素を作成・操作するための名前空間

//パズルゲームの管理を行うスクリプト
public class PazzleGameSystem : MonoBehaviour
{
    [SerializeField] BlockGenerator blockGenerator = default; //ブロックを生成するオブジェクト
    bool isClicking; //クリック最中かを判定する変数
    [SerializeField] List<Block> removeBlocks = new List<Block>(); //削除するブロックの格納を行うリスト
    Block currentClickingBlock; //現在クリックしたブロック

    int enemyHP; //敵のHP
    [SerializeField] Text enemyHPText = default; //スクリーン表示用
    [SerializeField] Slider enemyHPGauge = default; //HPゲージ

    int playerHP; //プレイヤーのHP
    [SerializeField] Text playerHPText = default; //スクリーン表示用
    [SerializeField] Slider playerHPGauge = default; //HPゲージ
    
    public GameObject panelObject; // Clear時のパネルオブジェクト

    void Start()
    {
        panelObject.SetActive(false); //パネルは非表示

        SoundManager.instance.playBGM(SoundManager.BGM.BattleBGM); //BGM設定

        enemyHP = ParamsSO.Entity.initEnemyHP; //敵HPの初期化
        enemyHPText.text = enemyHP.ToString() + "/" + enemyHP.ToString(); //テキストの初期化
        enemyHPGauge.maxValue = ParamsSO.Entity.initEnemyHPGauge; //最大HPの設定
        enemyHPGauge.value = ParamsSO.Entity.initEnemyHPGauge; //ゲージの初期化

        playerHP = ParamsSO.Entity.initPlayerHP; //プレイヤーHPの初期化
        playerHPText.text = playerHP.ToString() + "/" + playerHP.ToString(); //テキストの初期化
        playerHPGauge.maxValue = ParamsSO.Entity.initPlayerHPGauge; //最大HPの設定
        playerHPGauge.value = ParamsSO.Entity.initPlayerHPGauge; //ゲージの初期化

        StartCoroutine(blockGenerator.Spawns(ParamsSO.Entity.initBlockCount)); //盤面の初期化
    }

    void Update()
    {
        //右クリックを押した時
        if (Input.GetMouseButtonDown(0))
        {
            Click_Start();
        }
        //右クリックを離した時
        else if (Input.GetMouseButtonUp(0))
        {
            Click_End();
        }
        //右クリックをしている時
        else if (isClicking) {
            Click_Now();
        }
    }

    //クリックしたら呼ばれるメソッド
    void Click_Start() {
        //マウスのクリック位置を特定
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //ブロックに当たった場合
        if (hit && hit.collider.GetComponent<Block>()) {
            //クリックしたブロックを削除リストに加える
            Block block = hit.collider.GetComponent<Block>();
            AddRemoveBlock(block);
            isClicking = true;
        }
    }

    //クリックしている最中に呼ばれるメソッド
    void Click_Now() {
        //マウスのクリック位置を特定
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //ブロックに当たった場合
        if (hit && hit.collider.GetComponent<Block>())
        {
            Block block = hit.collider.GetComponent<Block>();
            //もし、１個前ににクリックしたブロックと同じ種類
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

    //クリックを離したら呼ばれるメソッド
    void Click_End() {
        //同じブロック３個以上繋がっていれば
        int removeCount = removeBlocks.Count;
        if (removeCount >= 3) {
            //剣ブロックを消し数×３０だけ敵のHPを減らす
            if (removeBlocks[0].id == 0)
            {
                enemyHP -= ParamsSO.Entity.SwordPoint * removeCount;
                enemyHPText.text = enemyHP.ToString() + "/" + enemyHPGauge.maxValue;
                
                //攻撃音を鳴らす
                SoundManager.instance.playSE(SoundManager.SE.Sword);
                //敵のHPゲージを減らす
                enemyHPGauge.value = enemyHP;

                //敵のHPが０未満になったら、Clearへ移動
                if (enemyHP < 0) {
                    Clear();
                }
            }
            //お玉ブロックを消し数×10だけ敵のHPを減らす
            else if (removeBlocks[0].id == 1)
            {
                enemyHP -= ParamsSO.Entity.LadlePoint * removeCount;
                enemyHPText.text = enemyHP.ToString() + "/" + enemyHPGauge.maxValue;

                //攻撃音を鳴らす
                SoundManager.instance.playSE(SoundManager.SE.Ladle);
                //敵のHPゲージを減らす
                enemyHPGauge.value = enemyHP;

                //敵のHPが０未満になったら、Clearへ移動
                if (enemyHP < 0)
                {
                    Clear();
                }
            }
            //Heartブロックを消したら消した数×2０だけHPが回復する
            else if (removeBlocks[0].id == 2)
            {
                playerHP += ParamsSO.Entity.HeartPoint * removeCount;
                playerHPText.text = playerHP.ToString() + "/" + playerHPGauge.maxValue;

                //回復音を鳴らす
                SoundManager.instance.playSE(SoundManager.SE.Heart);
                //プレイヤーHPのゲージを増やす
                playerHPGauge.value = playerHP;
            }
            //Skullブロックを消したら消した数×１５だけHPが減る
            else if (removeBlocks[0].id == 3)
            {
                playerHP -= ParamsSO.Entity.SkullPoint * removeCount; ;
                playerHPText.text = playerHP.ToString() + "/" + playerHPGauge.maxValue;

                //ダメージ音を鳴らす
                SoundManager.instance.playSE(SoundManager.SE.Skull);
                //プレイヤーのHPゲージを減らす
                playerHPGauge.value = playerHP;

                //プレイヤーのHPが０未満になったら、GameOverへ移動
                if (playerHP < 0)
                {
                    GameOver();
                }
            }
            else 
            {
                Debug.Log("エラー");
            }

            //リストに追加されたブロックを削除する
            for (int i = 0; i < removeCount; i++)
            {
                //破裂アニメーションを再生
                removeBlocks[i].Explosion();
            }
            //消したブロックの数だけBlockGeneratorで生成する
            StartCoroutine(blockGenerator.Spawns(removeCount));
        }
        //選んだブロックの大きさと色を元に戻す
        for (int i = 0; i < removeCount; i++) {
            removeBlocks[i].transform.localScale = Vector3.one * 5.0f;
            removeBlocks[i].GetComponent<SpriteRenderer>().color = Color.white;
        }

        //削除リストを空にする
        removeBlocks.Clear();
        isClicking = false;
    }

    //指定されたブロックを削除リストに追加するメソッド
    void AddRemoveBlock(Block block) 
    {
        //現在クリックしているブロックを更新する
        currentClickingBlock = block;
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
    
    //クリアした場合
    private void Clear()
    {
        //SE設定
        SoundManager.instance.playSE(SoundManager.SE.LastAttack);

        //BGMストップ
        SoundManager.instance.stopBGM(SoundManager.BGM.BattleBGM);

        //クリア画面表示
        panelObject.SetActive(true);

        //SE設定
        SoundManager.instance.playSE(SoundManager.SE.Victory);
    }

    //ゲームオーバーの場合
    private void GameOver()
    {
        //１秒後に実行
        DOVirtual.DelayedCall(1.0f, () => {
            //GameOver2シーンへ移動
            SceneManager.LoadScene("Game_Over2", LoadSceneMode.Single);
        });
    }
}
