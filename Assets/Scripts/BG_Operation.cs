using System.Collections.Generic;   //�I�u�W�F�N�g�̃��X�g���������߂̖��O���
using UnityEngine;                  //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���
using UnityEngine.UI;               //Unity��UI�@�\�̖��O���

//�w�i�摜�Ȃǂ��Ǘ�����X�N���v�g
public class BG_Operation : MonoBehaviour
{
    //////////////
    //�摜�f�[�^//
    //////////////
    
    [SerializeField] Sprite _background1;   //�w�i�p�̉摜
    [SerializeField] Sprite _gallery1;      //�M�������[�摜�P
    [SerializeField] Sprite _gallery2;      //�M�������[�摜�Q

    ////////////////////////////////
    //�摜���}�������I�u�W�F�N�g//
    ////////////////////////////////
    
    [SerializeField] GameObject _backgroundObject;  //�w�i�I�u�W�F�N�g
    [SerializeField] GameObject _galleryObject;     //�M�������[�I�u�W�F�N�g
    
    ////////////////
    //�摜�v���n�u//
    ////////////////
    
    [SerializeField] GameObject _imagePrefab; //�摜�v���n�u

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
        GameObject item = Instantiate(_imagePrefab, position, rotation, parent);
        
        // ���������I�u�W�F�N�g�ɁA��������擾�����摜��ݒ�
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