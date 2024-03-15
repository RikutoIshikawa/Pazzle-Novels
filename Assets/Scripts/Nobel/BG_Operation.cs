using System.Collections.Generic; //ジェネリック（汎用的な）コレクションクラスやインターフェースを操作するための名前空間
using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間
using UnityEngine.UI; //UnityのUI要素を作成・操作するための名前空間

//背景画像やギャラリー管理するスクリプト
public class BG_Operation : MonoBehaviour
{
    [SerializeField] Sprite _background1; //背景用の画像
    [SerializeField] Sprite _gallery1; //ギャラリー画像１
    [SerializeField] Sprite _gallery2; //ギャラリー画像２
                                       
    [SerializeField] GameObject _backgroundObject; //背景オブジェクト
    [SerializeField] GameObject _galleryObject; //ギャラリーオブジェクト
    
    [SerializeField] GameObject _imagePrefab; //画像プレハブ
    
    Dictionary<string, Sprite> _textToSprite; //テキストのキーワードと画像を紐づける辞書
    Dictionary<string, GameObject> _textToParentObject; //テキストのキーワードと親オブジェクトを紐づける辞書

    Dictionary<string, GameObject> _textToSpriteObject; //テキストのキーワードとオブジェクトを紐づける辞書

    //辞書の初期化メソッド
    void Awake()
    {
        //それぞれの辞書の設定
        //_textToSprite
        _textToSprite = new Dictionary<string, Sprite>();
        _textToSprite.Add("background1", _background1);
        _textToSprite.Add("gallery1", _gallery1);
        _textToSprite.Add("gallery2", _gallery2);

        //_textToParentObject
        _textToParentObject = new Dictionary<string, GameObject>();
        _textToParentObject.Add("backgroundObject", _backgroundObject);
        _textToParentObject.Add("galleryObject", _galleryObject);

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
        GameObject item = Instantiate(_imagePrefab, position, rotation, parent);
        
        // 生成したオブジェクトに、辞書から取得した画像を設定
        item.GetComponent<Image>().sprite = image;
        
        // 画像に対する辞書に、画像の名前と生成したオブジェクトを追加
        _textToSpriteObject.Add(imageName, item);
    }
    
    //指定されたオブジェクトを削除するメソッド
    public void RemoveImage(string imageName)
    {
        Destroy(_textToSpriteObject[imageName]); //辞書から取得した画像オブジェクトを破棄
    }
}