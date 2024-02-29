using System.Collections.Generic; //ジェネリック（汎用的な）コレクションクラスやインターフェースを操作するための名前空間
using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間
using UnityEngine.UI; //UnityのUI要素を作成・操作するための名前空間

//アイテムを管理するスクリプト
public class Item_Operation : MonoBehaviour
{
    //アイテム用の画像
    [SerializeField] Sprite _item1;
    [SerializeField] Sprite _item2;
    [SerializeField] Sprite _item3;

    //アイテムオブジェクト
    [SerializeField] GameObject _itemObject;

    //プレハブ用の変数
    [SerializeField] GameObject _itemPrefab;

    Dictionary<string, Sprite> _textToSprite; //テキストのキーワードと画像を紐づける辞書
    Dictionary<string, GameObject> _textToParentObject; //テキストのキーワードと親オブジェクトを紐づける辞書

    Dictionary<string, GameObject> _textToSpriteObject; //テキストのキーワードとオブジェクトを紐づける辞書

    //辞書の初期化メソッド
    void Awake()
    {
        //それぞれの辞書の設定
        //_textToSprite
        _textToSprite = new Dictionary<string, Sprite>();
        _textToSprite.Add("item1", _item1);
        _textToSprite.Add("item2", _item2);
        _textToSprite.Add("item3", _item3);

        //_textToParentObject
        _textToParentObject = new Dictionary<string, GameObject>();
        _textToParentObject.Add("itemObject", _itemObject);

        //_textToSpriteObject
        _textToSpriteObject = new Dictionary<string, GameObject>();
    }
    
    //指定された画像を指定された親オブジェクトに配置するメソッド
    public void PutImage(string imageName, string parentObjectName)
    {
        Sprite image = _textToSprite[imageName]; // 辞書から指定された名前の画像を取得
        GameObject parentObject = _textToParentObject[parentObjectName]; //辞書から指定された名前の親オブジェクトを取得

        //親オブジェクトのZ座標を取得
        float parentZ = parentObject.transform.position.z;
        // 画像を配置する位置と回転を指定
        Vector3 position = new Vector3(0, 0, parentZ);
        Quaternion rotation = Quaternion.identity;

        // 親オブジェクトのTransformを取得
        Transform parent = parentObject.transform;

        //オブジェクトを生成
        GameObject item = null;
        item = Instantiate(_itemPrefab, position, rotation, parent);
        
        // 生成した画像に、辞書から取得した画像を設定
        item.GetComponent<Image>().sprite = image;
        
        // 画像に対する辞書に、画像の名前と生成したオブジェクトを追加
        _textToSpriteObject.Add(imageName, item);
    }
    
    //指定された画像を削除するメソッド
    public void RemoveImage(string imageName)
    {
        Destroy(_textToSpriteObject[imageName]); //辞書から取得した画像オブジェクトを破棄
    }
}