using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間

//ブロックの定義スクリプト
public class Block : MonoBehaviour
{
    ////////////////
    //ブロック情報//
    ////////////////
    
    //ブロックの種類を識別するためのID
    public int id;

    //爆発アニメーションオブジェクト
    [SerializeField] GameObject explosionPrehub = default;

    ////////////////////////////////////
    //ブロックを消した時に呼ばれる関数//
    ////////////////////////////////////
    
    public void Explosion() {
        //ブロックを破壊
        Destroy(gameObject);

        //アニメーションを再生
        GameObject explosion = Instantiate(explosionPrehub, transform.position, transform.rotation);

        //0.2秒後に破壊
        Destroy(explosion, 0.2f);
    }

}