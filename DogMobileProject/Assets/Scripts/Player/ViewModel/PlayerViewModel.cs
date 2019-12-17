using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization; // XmlSerializer

[System.Serializable]
[XmlRoot(ElementName = "text")]
public class PlayerViewModel 
{
    System.Action OnPropertyChange;

  //  [XmlElement(ElementName = "PrefabName")]
    private string _PrefabName;
  //  [XmlElement(ElementName = "ModelName")]
    private string _ModelName;
  //  [XmlElement(ElementName = "PlayerName")]
    private string _PlayerName;

  //  [XmlElement(ElementName = "rotaionSpeed")]
    private float _rotaionSpeed;
   // [XmlElement(ElementName = "maxRotationSpeed")]
    private float _maxRotationSpeed;
   // [XmlElement(ElementName = "maxSpeed")]
    private float _maxSpeed;
  //  [XmlElement(ElementName = "currentSpeed")]
    private float _currentSpeed;
   // [XmlElement(ElementName = "accelateFactor")]
    private float _accelateFactor;

   // [XmlElement(ElementName = "maxHP")]
    private float _maxHP;
    private float _curHp;
  //  [XmlElement(ElementName = "attack")]
    private float _attack;
  //  [XmlElement(ElementName = "defend")]
    private float _defend;
  //  [XmlElement(ElementName = "agility")]
    private float _agility;
  //  [XmlElement(ElementName = "resistance")]
    private float _resistance;

  //  [XmlElement(ElementName = "classId")]
    private int _classId;

    private Vector3 _currentPosition;
    private Vector3 _currentDirection;
    private Quaternion _currentRotation;

    public float CurHP
    {
        get => _curHp;
        set
        {
            _curHp = value;
            CheckDead(_curHp);
            OnPropertyChange?.Invoke();
        }
    }
    public float MaxHP
    {
        get => _maxHP;
        set
        {
            _maxHP = value;
            OnPropertyChange?.Invoke();
        }
    }
    public float Attack
    {
        get => _attack;
        set
        {
            _attack = value;
            OnPropertyChange?.Invoke();
        }
    }
    public float Defend
    {
        get => _defend;
        set
        {
            _defend = value;
            OnPropertyChange?.Invoke();
        }
    }
    public float Agility
    {
        get => _agility;
        set
        {
            _agility = value;
            OnPropertyChange?.Invoke();
        }
    }
    public float Resistance
    {
        get => _resistance;
        set
        {
            _resistance = value;
            OnPropertyChange?.Invoke();
        }
    }


    public float MaxRotation
    {
        get => _maxRotationSpeed;
        set
        {
            _maxRotationSpeed = value;
            CheckMaxRotationSpeed();
            OnPropertyChange?.Invoke();
        }
    }
    public float RotationSpeed
    {
        get => _rotaionSpeed;
        set
        {
            _rotaionSpeed = value;
            OnPropertyChange?.Invoke();
        }
    }
    public float MaxSpeed
    {
        get => _maxSpeed;
        set
        {
            _maxSpeed = value;
            OnPropertyChange?.Invoke();
        }
    }
    public float CurrentSpeed
    {
        get
        {
            return _currentSpeed;
        }
        set
        {
            _currentSpeed = value;
            CheckMaxVelocity();
            OnPropertyChange?.Invoke();
        }
    }
    public float AccelateFactor
    {
        get => _accelateFactor;
        set
        {
            _accelateFactor = value;
            OnPropertyChange?.Invoke();
        }
    }

    public Vector3 CurrentPosition
    {
        get { return _currentPosition; }
        set
        {
            _currentPosition = value;
        }
    }
    public Vector3 CurrentDirection
    {
        get => _currentDirection;
        set
        {
            _currentDirection = value;
            OnPropertyChange?.Invoke();
        }
    }
    public Quaternion CurrentRotation
    {
        get => _currentRotation;
        set
        {
             _currentRotation = value;
        }
    }

    public string PlayerName
    {
        get => _PlayerName;
        set
        {
            _PlayerName = value;
            OnPropertyChange?.Invoke();
        }
    }
    public string ModelName
    {
        get => _ModelName;
        set
        {
            _ModelName = value;
            OnPropertyChange?.Invoke();
        }
    }
    public string PrefabName
    {
        get => _PrefabName;
        set
        {
            _PrefabName = value;
            OnPropertyChange?.Invoke();
        }
    }

    public int ClassId
    {
        get => _classId;
        set
        {
            _classId = value;
            OnPropertyChange?.Invoke();
        }
    }

    void CheckDead(float _HP)
    {
        if(_HP <= 0)
        {

        }
    }

    public bool CheckMaxVelocity()
    {
        if (_currentSpeed >= MaxSpeed - 0.01f)
        {
            _currentSpeed = MaxSpeed;
            return true;
        }

        return false;
    }

    public void CheckMaxRotationSpeed()
    {
        _maxRotationSpeed = Mathf.Clamp(_maxRotationSpeed, 0.1f, 5.0f);
    }

    public void UpdateSpeed()
    {
        _currentSpeed += Time.deltaTime * 1.0f;

        CheckMaxVelocity();
    }

    public void resetSpeed()
    {
        _currentSpeed = 0.0f;

    }



}
