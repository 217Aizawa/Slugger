using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComplement : MonoBehaviour
{

    //グリップ
    public Transform startPosition;
    public Transform startPosition2;
    //ヘッド
    public Transform endPosition;
    public Transform endPosition2;

    //　メッシュ
    private Mesh mesh;
    //　頂点リスト
    public List<Vector3> verticesLists = new List<Vector3>();
    //　三角形のリスト
    public List<int> tempTriangles = new List<int>();
    //　軌跡の表示のオン・オフフラグ
    private bool isSwordCollider = false;
    //　前フレームのグリップの位置
    private Vector3 oldStartPos;
    //　前フレームのヘッドの位置
    private Vector3 oldEndPos;

    private MeshCollider meshCollider;
    private MeshFilter meshFilter;

    float speed;
    Vector3 prevPos;

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
    void FixedUpdate()
    {
        speed = ((transform.position - prevPos) / Time.deltaTime).magnitude;

        if(2 < speed)
        {
            CreateMesh();
        }

        prevPos = transform.position;
    }

    //バットの軌跡作成メソッド
    void CreateMesh()
    {
        //mesh.Clear();

        //　頂点以外のリストのクリア
        verticesLists.Clear();
        tempTriangles.Clear();

        verticesLists.AddRange(new Vector3[] {
        oldStartPos, oldEndPos,
        startPosition.position, endPosition.position,
        startPosition2.position, endPosition2.position
    });

        //　本当なら全面作らなければいけないがCovexにチェックを入れると勝手に作ってくれる
        tempTriangles.AddRange(new int[]{
        0, 1, 2,
        2, 1, 3,
        2, 3, 4,
        4, 3, 5,
		/*
		0, 2, 4,
		5, 1, 3
		*/
	});

        mesh.vertices = verticesLists.ToArray();
        mesh.triangles = tempTriangles.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        //　メッシュコライダの再有効化
        meshCollider.enabled = false;
        meshCollider.enabled = true;

        oldStartPos = startPosition.position;
        oldEndPos = endPosition.position;

    }
}
