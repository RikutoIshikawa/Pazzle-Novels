using System.Collections.Generic;   //�I�u�W�F�N�g�̃��X�g���������߂̖��O���
using UnityEngine;                  //Unity�̃Q�[���G���W���̋@�\���g�����߂̖��O���
using UnityEngine.UI;               //Unity��UI�@�\�̖��O���

//�`�����N�^�[�摜���Ǘ�����X�N���v�g
public class Character_Operation : MonoBehaviour
{
    //////////////////////////
    //�L�����N�^�[�摜�f�[�^//
    //////////////////////////
    
    [SerializeField] Sprite _character1;
    [SerializeField] Sprite _character2;
    [SerializeField] Sprite _character3;
    [SerializeField] Sprite _character4;
    [SerializeField] Sprite _character5;
    [SerializeField] Sprite _character6;

    ////////////////////////////////////////////
    //�L�����N�^�[�摜���}�������I�u�W�F�N�g//
    ////////////////////////////////////////////
    
    [SerializeField] GameObject _characterObject;

    ////////////////////////
    //�L�����N�^�[�v���n�u//
    ////////////////////////
    
    [SerializeField] GameObject _characterPrefab;

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

    //�e�L�X�g�̃L�[���[�h�ƃI�u�W�F�N�g��R�Â��鎫��
    Dictionary<string, GameObject> _textToSpriteObject;

    ////////////////////////
    //�����̏��������\�b�h//
    ////////////////////////
    
    void Awake()
    {
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
        Vector3 position = new Vector3(0, -1f, parentZ);
        Quaternion rotation = Quaternion.identity;

        // �e�I�u�W�F�N�g��Transform���擾
        Transform parent = parentObject.transform;

        GameObject item = null;
        item = Instantiate(_characterPrefab, position, rotation, parent);
        
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