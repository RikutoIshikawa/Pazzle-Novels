using System.Collections.Generic;   //オブジェクトのリストを扱うための名前空間
using UnityEngine;                  //Unityのゲームエンジンの機能を使うための名前空間
using UnityEngine.UI;               //UnityのUI機能の名前空間

//背景画像などを管理するスクリプト
public class BG_Operation : MonoBehaviour
{
    //////////////
    //画像データ//
    //////////////
    
    [SerializeField] Sprite _background1;   //背景用の画像
    [SerializeField] Sprite _gallery1;      //ギャラリー画像１
    [SerializeField] Sprite _gallery2;      //ギャラリー画像２

    ////////////////////////////////
    //画像が挿入されるオブジェクト//
    ////////////////////////////////
    
    [SerializeField] GameObject _backgroundObject;  //背景オブジェクト
    [SerializeField] GameObject _galleryObject;     //ギャラリーオブジェクト
    
    ////////////////
    //画像プレハブ//
    ////////////////
    
    [SerializeField] GameObject _imagePrefab; //画像プレハブ

    ////////////////////////////
    //画像表示に使用するデータ//
    ////////////////////////////

    //テキストのキーワードと画像を紐づける辞書
    Dictionary<string, Sprite> _textToSprite;

    //テキストのキーワードと親オブジェクトを紐づける辞書
    Dictionary<string, GameObject> _textToParentObject;

    ////////////////////////////
    //画像削除に使用するデータ//
    ////////////////////////////

    //テキストのキーワードと画像が挿入されたオブジェクトを紐づける辞書
    Dictionary<string, GameObject> _textToSpriteObject;

    ////////////////////////
    //辞書の初期化メソッド//
    ////////////////////////
    
    void Awake()
    {        
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
    
    ////////////////////////////////////////
    //キーワードに応じて画像を挿入する関数//
    ////////////////////////////////////////

    public void PutImage(string imageName, string parentObjectName)
    {
        //////////////////////////////////////
        //指定された画像とオブジェクトの取得//
        //////////////////////////////////////
        
        // 辞書から指定された名前の画像を取得
        Sprite image = _textToSprite[imageName];

        //辞書から指定された名前の親オブジェクトを取得
        GameObject parentObject = _textToParentObject[parentObjectName];

        //////////////////////////
        //画像オブジェクトの表示//
        //////////////////////////
        
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
        
        ////////////////////////////////////
        //削除できるように削除用辞書に保存//
        ////////////////////////////////////
        
        // 画像に対する辞書に、画像の名前と生成したオブジェクトを追加
        _textToSpriteObject.Add(imageName, item);
    }
    
    ////////////////////////////////////////
    //キーワードに応じて画像を削除する関数//
    ////////////////////////////////////////
    
    public void RemoveImage(string imageName)
    {
        //辞書から取得した画像オブジェクトを破棄
        Destroy(_textToSpriteObject[imageName]);
    }
}