using System.Collections.Generic;   //�I�u�W�F�N�g�̃��X�g���������߂̖��O���
using UnityEngine;                  //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���
using UnityEngine.UI;               //Unity��UI�@�\�̖��O���

//�A�C�e���摜���Ǘ�����X�N���v�g
public class Item_Operation : MonoBehaviour
{
    //////////////////////
    //�A�C�e���摜�f�[�^//
    //////////////////////
    
    [SerializeField] Sprite _item1;
    [SerializeField] Sprite _item2;
    [SerializeField] Sprite _item3;

    ////////////////////////////////
    //�摜���}�������I�u�W�F�N�g//
    ////////////////////////////////
    
    [SerializeField] GameObject _itemObject;
    
    ////////////////////
    //�A�C�e���v���n�u//
    ////////////////////
    
    [SerializeField] GameObject _itemPrefab;

    ////////////////////////////
    //�摜�\���Ɏg�p����f�[�^//
    ////////////////////////////

    //�e�L�X�g�̃L�[���[�h�Ɖ摜��R�Â��鎫��
    Dictionary<string, Sprite> _textToSprite;

    //�e�L�X�g�̃L�[���[�h�Ɛe�I�u�W�F�N�g��R�Â��鎫��
    Dictionary<string, GameObject> _textToParentObject;

    ////////////////////////////
    //�摜�폜�Ɏg�p����f�[�^//
    ////////////////////////////

    //�e�L�X�g�̃L�[���[�h�Ɖ摜���}�����ꂽ�I�u�W�F�N�g��R�Â��鎫��
    Dictionary<string, GameObject> _textToSpriteObject;

    ////////////////////////
    //�����̏��������\�b�h//
    ////////////////////////

    ////////////////////////
    //�����̏��������\�b�h//
    ////////////////////////
   
    void Awake()
    {
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

    ////////////////////////////////////////
    //�L�[���[�h�ɉ����ĉ摜��}������֐�//
    ////////////////////////////////////////
    
    public void PutImage(string imageName, string parentObjectName)
    {
        //////////////////////////////////////
        //�w�肳�ꂽ�摜�ƃI�u�W�F�N�g�̎擾//
        //////////////////////////////////////

        // ��������w�肳�ꂽ���O�̉摜���擾
        Sprite image = _textToSprite[imageName];

        //��������w�肳�ꂽ���O�̐e�I�u�W�F�N�g���擾
        GameObject parentObject = _textToParentObject[parentObjectName];

        //////////////////////////
        //�摜�I�u�W�F�N�g�̕\��//
        //////////////////////////
        
        //�e�I�u�W�F�N�g��Z���W���擾
        float parentZ = parentObject.transform.position.z;

        // �摜��z�u����ʒu�Ɖ�]���w��
        Vector3 position = new Vector3(0, 0, parentZ);
        Quaternion rotation = Quaternion.identity;

        // �e�I�u�W�F�N�g��Transform���擾
        Transform parent = parentObject.transform;

        //�I�u�W�F�N�g�𐶐�
        GameObject item = null;
        item = Instantiate(_itemPrefab, position, rotation, parent);
        
        // ���������摜�ɁA��������擾�����摜��ݒ�
        item.GetComponent<Image>().sprite = image;

        ////////////////////////////////////
        //�폜�ł���悤�ɍ폜�p�����ɕۑ�//
        ////////////////////////////////////

        // �摜�ɑ΂��鎫���ɁA�摜�̖��O�Ɛ��������I�u�W�F�N�g��ǉ�
        _textToSpriteObject.Add(imageName, item);
    }

    ////////////////////////////////////////
    //�L�[���[�h�ɉ����ĉ摜���폜����֐�//
    ////////////////////////////////////////
    
    public void RemoveImage(string imageName)
    {
        //��������擾�����摜�I�u�W�F�N�g��j��
        Destroy(_textToSpriteObject[imageName]);
    }
}