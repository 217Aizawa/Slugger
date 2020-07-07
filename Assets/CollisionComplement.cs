using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComplement : MonoBehaviour
{

    //　剣元1
    public Transform startPosition;
    //　剣元2
    public Transform startPosition2;
    //　剣先1
    public Transform endPosition;
    //　剣先2
    public Transform endPosition2;

    //　メッシュ
    private Mesh mesh;
    //　頂点リスト
    public List<Vector3> verticesLists = new List<Vector3>();
    //　三角形のリスト
    public List<int> tempTriangles = new List<int>();
    //　軌跡の表示のオン・オフフラグ
    private bool isSwordCollider = false;
    //　前フレームの剣元の位置
    private Vector3 oldStartPos;
    //　前フレームの剣先の位置
    private Vector3 oldEndPos;

    private MeshCollider meshCollider;
    private MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        //　他の凸コライダと衝突有効にする
        meshCollider.convex = true;
        //　物理的に当たらないようにする
        meshCollider.isTrigger = true;
        //　メッシュコライダに自作メッシュを設定
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
