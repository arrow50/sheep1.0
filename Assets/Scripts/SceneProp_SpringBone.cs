using System;
using UnityEngine;
using System.Collections;

public class SceneProp_SpringBone : MonoBehaviour
{
    private void Awake()
    {
        _Trs = transform;
        _LocalRotation = transform.localRotation;
        Chid.localPosition = BoneAxis;
    }


    private void Start()
    {
        _SpringLength = Vector3.Distance(_Trs.position, Chid.position);
        _CurrTipPos = Chid.position;
        _PrevTipPos = Chid.position;
    }

    private void LateUpdate()
    {
        UpdateSpring();
    }

    public void UpdateSpring()
    {
        _Org = _Trs;
        //重置旋转
        _Trs.localRotation = _LocalRotation;
        //force 为力 = 加速度，因此需要除以时间的平方
        float sqrDt = Time.deltaTime * Time.deltaTime;
        //stiffness刚性:虚拟骨骼方向绕自身四元数旋转，boneAxis如果是（0，-1,0）那么就是说始终会受到向下的类似重力的力。force越小，
        Vector3 force = _Trs.rotation * (BoneAxis * StiffnessForce) / sqrDt;
        //drag阻力 类似damping 上两帧的末端位置 - 上一帧的末端位置 = 阻力
        force += (_PrevTipPos - _CurrTipPos) * DampingForce / sqrDt;
        //  force += springForce / sqrDt;
        //储存上一帧的末端位置
        Vector3 lastFrameTipPos = _CurrTipPos;
        //前向移动的距离加上当前位置，+ 类似重力和阻力 = 当前尖端应该的位置
        _CurrTipPos = (_CurrTipPos - _PrevTipPos) + _CurrTipPos + (force * sqrDt);
        //把已有的尖端位置用骨骼长度约束进一步规范
        _CurrTipPos = ((_CurrTipPos - _Trs.position).normalized * _SpringLength) + _Trs.position;
        _MoveDir = _CurrTipPos - transform.TransformPoint(BoneAxis);
        
        //储存上一帧的末端位置
        _PrevTipPos = lastFrameTipPos;

        //适用旋转
        Vector3 aimVector = _Trs.TransformDirection(BoneAxis);
        //根据两帧之间的位移值，构建一个旋转四元数（因为前面假定了骨骼长度不变）。
        Quaternion aimRotation = Quaternion.FromToRotation(aimVector, _CurrTipPos - _Trs.position);
        //original
        //_Trs.rotation = aimRotation * _Trs.rotation;
        //Kobayahsi:Lerp with mixWeight
        Quaternion secondaryRotation = aimRotation * _Trs.rotation;
        //第一根骨骼要多大程度上跟随第二根骨骼的旋转？1代表完全像第二根骨骼一样
        _Trs.rotation = Quaternion.Lerp(_Org.rotation, secondaryRotation, _DynamicRatio);
        Chid.rotation = Quaternion.Lerp(_Org.rotation, secondaryRotation, _DynamicRatio);
        Chid.position = _CurrTipPos;
        _MoveDir = Vector3.SmoothDamp(CurrentMoveDir, _MoveDir, ref Velocity, 0.1f);
        CurrentMoveDir = _MoveDir;
        if(_MoveDir.magnitude > MaxMoveDirLength) _MoveDir = Vector3.Normalize(_MoveDir) * MaxMoveDirLength;
        Renderers.sharedMaterial.SetVector("_SpringBoneDir", _MoveDir);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_CurrTipPos, Radius);
            Gizmos.DrawLine(transform.position, Chid.position);
            Gizmos.DrawLine(Chid.position, Chid.position + _MoveDir);
            //UpdateSpring();
        }
    }
#endif
    
    [Tooltip("受影响的renderer")] public Renderer Renderers;
    [Tooltip("第二级骨骼，本脚本挂载的物体为第一级骨骼")] public Transform Chid;
    [Tooltip("骨骼的方向，无论何时骨骼都会尝试回到这个方向")] public Vector3 BoneAxis = new Vector3(0.0f, -1.0f, 0.0f);
    [Tooltip("骨骼ui的半径")] [Range(0, 1f)] public float Radius = 0.1f;
    [Tooltip("骨骼方向力的大小，越小骨骼越柔软")] [Range(0.001f, 0.01f)]
    public float StiffnessForce = 0.004f;
    [Tooltip("空气阻力大小")] [Range(0, 0.2f)] public float DampingForce = 0.08f;
    [Tooltip("二级骨骼的旋转有多大程度上受一级骨骼的影响？")] [Range(0, 1f)]
    public float _DynamicRatio = 0.1f;
    public float MaxMoveDirLength = 1.0f;
    [Tooltip("是否显示ui")] public bool debug = true;

    //public float threshold = 0.01f;
    private float _SpringLength;
    private Quaternion _LocalRotation;
    private Transform _Trs;
    private Vector3 _CurrTipPos;
    private Vector3 _PrevTipPos;
    private Vector3 _MoveDir;
    private Transform _Org;
    private Vector3 Velocity;
    private Vector3 CurrentMoveDir;
}