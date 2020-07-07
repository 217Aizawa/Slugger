using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public static GameObject bat;
    public GameObject emptyBat;
    GameObject model;

    Rigidbody batRb;

    public bool isBat = false;//バットの軌道表示
    //bool swing = false;

    //自動スイング用
    //Vector3 startPos = new Vector3(-0.4f ,0.8f ,19.8f);
    //Vector3 endPos;

    //ローカルな現在位置
    Vector3 batGripLocal;
    Vector3 batHeadLocal;
    Vector3 batGripLocal2;
    Vector3 batHeadLocal2;
    Vector3 batDirLocal;

    //現在位置
    public static Vector3 batHead;
    public static Vector3 batGrip;
    public static Vector3 batHead2;
    public static Vector3 batGrip2;
    public static Vector3 batDir;
    Vector3 nowPos;

    //過去位置
    Vector3 prevBatHead;
    Vector3 prevBatGrip;
    Vector3 prevBatDir;
    Vector3 latestPos;

    public static Vector3 swingSpeed;

    
    private Mesh mesh;//　メッシュ
    public List<Vector3> verticesLists = new List<Vector3>();//　頂点リスト
    public List<int> tempTriangles = new List<int>();//　三角形のリスト
    private bool isSwordCollider = false;//　軌跡の表示のオン・オフフラグ
    private Vector3 oldStartPos;//　前フレームの剣元の位置
    private Vector3 oldEndPos;//　前フレームの剣先の位置

    private MeshCollider meshCollider;
    private MeshFilter meshFilter;

    void Start()
    {
        batRb = GetComponent<Rigidbody>();
        batDirLocal = new Vector3(0, 0.9f, 0);//バットの長さ=90cm
        batGripLocal = new Vector3(0, -0.45f, 0);
        batHeadLocal = new Vector3(0, 0.45f, 0);
        batGripLocal2 = new Vector3(0, -0.45f, 0);
        batHeadLocal2 = new Vector3(0, 0.45f, 0);
        swingSpeed = new Vector3(0, 0, 0);

        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.convex = true;//　他の凸コライダと衝突有効にする
        meshCollider.isTrigger = true;//　物理的に当たらないようにする
        meshCollider.sharedMesh = mesh;//　メッシュコライダに自作メッシュを設定

    }

    void FixedUpdate()
    {
        nowPos = this.gameObject.transform.position;
        swingSpeed = ((nowPos - latestPos) / Time.deltaTime);

        batDir = transform.TransformDirection(batDirLocal);//向きだけ変換する
        batGrip = transform.TransformPoint(batGripLocal);//グリップ位置
        batHead = transform.TransformPoint(batHeadLocal);//ヘッド位置

        //Debug.Log("World :" + batDir + " Local :" + batDirLocal);
        //Debug.Log("World :" + batGrip + " Local :" + batGripLocal);
        //Debug.Log("magnitude" + swingSpeed.magnitude);

        if(2 < swingSpeed.magnitude)
        {
            CreateMesh();
            Debug.Log("CreateMesh called");
        }
        if (5 < swingSpeed.magnitude && isBat)
        {
            model = Instantiate(emptyBat, transform.position,transform.rotation);
        }
        Destroy(model, 2f);
        /*
        if (Input.GetKey(KeyCode.S))
        {
            swing = true;
            Debug.Log("swing start");
        }

        if (swing)
        {
            transform.position -= new Vector3(0, 0, 0.02f);
        }
        */

        /**********************過去位置**************************/
        prevBatDir = batDir;
        prevBatGrip = batGrip;
        prevBatHead = batHead;
        latestPos = transform.position;
        /********************************************************/
    }

    void CreateMesh()
    {
        mesh.Clear();

        //　頂点以外のリストのクリア
        verticesLists.Clear();
        tempTriangles.Clear();

        verticesLists.AddRange(new Vector3[] {
        prevBatHead, prevBatGrip,
        batHead, batGrip,
        batHead2, batGrip2
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

        //oldStartPos = startPosition.position;
        //oldEndPos = endPosition.position;

    }
}
