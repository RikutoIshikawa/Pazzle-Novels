using System.Collections.Generic; //オブジェクトのリストを扱うための名前空間
using UnityEngine; //Unityのゲームエンジンの機能を使うための名前空間
using UnityEngine.UI; //UnityのUI機能の名前空間

//チャラクターを管理するスクリプト
public class Character_Operation : MonoBehaviour
{
    //キャラクター用の画像
    [SerializeField] Sprite _character1;
    [SerializeField] Sprite _character2;
    [SerializeField] Sprite _character3;
    [SerializeField] Sprite _character4;
    [SerializeField] Sprite _character5;
    [SerializeField] Sprite _character6;

    //キャラクターオブジェクト
    [SerializeField] GameObject _characterObject;

    //プレハブ用の変数
    [SerializeField] GameObject _characterPrefab;

    Dictionary<string, Sprite> _textToSprite; //テキストのキーワードと画像を紐づける辞書
    Dictionary<string, GameObject> _textToParentObject; //テキストのキーワードと親オブジェクトを紐づける辞書

    Dictionary<string, GameObject> _textToSpriteObject; //テキストのキーワードとオブジェクトを紐づける辞書

    //辞書の初期化メソッド
    void Awake()
    {
        //それぞれの辞書の設定
        //_textToSprite
        _textToSprite = new Dictionary<string, Sprite>();
        _textToSprite.Add("character1", _character1);
        _textToSprite.Add("character2", _character2);
        _textToSprite.Add("character3", _character3);
        _textToSprite.Add("character4", _character4);
        _textToSprite.Add("character5", _character5);
        _textToSprite.Add("character6", _character6);

        //_textToParentObject
        _textToParentObject = new Dictionary<string, GameObject>();
        _textToParentObject.Add("characterObject", _characterObject);

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
        Vector3 position = new Vector3(0, -1f, parentZ);
        Quaternion rotation = Quaternion.identity;

        // 親オブジェクトのTransformを取得
        Transform parent = parentObject.transform;

        GameObject item = null;
        item = Instantiate(_characterPrefab, position, rotation, parent);
        
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