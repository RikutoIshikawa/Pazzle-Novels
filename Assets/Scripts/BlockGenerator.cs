using System.Collections;   //オブジェクトのリストを扱うための名前空間
using UnityEngine;          //Unityのゲームエンジンの機能を使うための名前空間

//ブロックの生成を行うスクリプト
public class BlockGenerator : MonoBehaviour
{
    //////////////////
    //ブロックデータ//
    //////////////////
    
    [SerializeField] GameObject blockPrefab = default;  //ブロックプレハブの設定

    [SerializeField] Sprite[] blockSprites = default;   //ブロックの画像を設定

    //////////////////////////////////////////////
    //指定された数だけブロックを生成するメソッド//
    //////////////////////////////////////////////
    
    public IEnumerator Spawns(int count) 
    {
       //Countの数だけ繰り返す
        for(int i = 0; i < count; i++)
        {
            //ブロックプレハブの座標を取得
            float pos_x = blockPrefab.transform.position.x;
            float pos_y = blockPrefab.transform.position.y;
            float pos_z = blockPrefab.transform.position.z;

            //x座標をランダムに変更
            Vector3 pos = new Vector3(pos_x + Random.Range(-1f, 1f), pos_y, pos_z);
            GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity);

            //ブロックのIDをランダムに設定
            int blockID = Random.Range(0, blockSprites.Length);

            //画像変更のコンポーネントを用いて画像を挿入
            block.GetComponent<SpriteRenderer>().sprite = blockSprites[blockID];

            //ブロックスクリプトのidを取得
            block.GetComponent<Block>().id = blockID; 

            //生成にラグを持たせる
            yield return new WaitForSeconds(0.04f);
        }
    }
}