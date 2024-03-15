using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間

//ブロックの定義スクリプト
public class Block : MonoBehaviour
{
    public int id; //ブロックの種類を識別するためのID

    [SerializeField] GameObject explosionPrehub = default; //爆発アニメーションオブジェクト

    public void Explosion() {
        //ブロックを破壊
        Destroy(gameObject);

        //アニメーションを再生
        GameObject explosion = Instantiate(explosionPrehub, transform.position, transform.rotation);

        //0.2秒後に破壊
        Destroy(explosion, 0.2f);
    }

}
