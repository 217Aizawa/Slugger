using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComplement : MonoBehaviour
{
    //衝突判定補完用プログラム
    //バットの過去の１フレートと現在の１フレームから三角形の平面メッシュを作成し、その平面をボールが通過した場合衝突したものとする
    //その交点から仮想のバットを作成し、衝突した際に発生する撃力をボールに与える

    //グリップ位置
    public Transform gripPosition;
    public Transform gripPosition2;
    //ヘッド位置
    public Transform headPosition;
    public Transform headPosition2;

    private Mesh mesh;
    private Mesh planeMesh;//衝突判定用メッシュ
    //　頂点リスト
    public List<Vector3> verticesLists = new List<Vector3>();
    //　三角形のリスト
    public List<int> tempTriangles = new List<int>();
    //　前フレームのグリップの位置
    private Vector3 oldGripPos;
    //　前フレームのヘッドの位置
    private Vector3 oldheadPos;

    private MeshCollider meshCollider;
    private MeshFilter meshFilter;
    float speed;
    Vector3 prevPos;

    //　軌跡の表示のオン・オフフラグ
    private bool isBatCollider = false;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        //衝突判定用メッシュの作成
        planeMesh = new Mesh();
        meshCollider = GetComponent<MeshCollider>();
        //　他の凸コライダと衝突有効にする
        //meshCollider.convex = true;
        //　物理的に当たらないようにする
        //meshCollider.isTrigger = true;
        //　メッシュコライダに自作メッシュを設定
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = ((transform.position - prevPos) / Time.deltaTime).magnitude;
        Debug.Log("prevPos" + prevPos);
        //Debug.Log("speed" + speed);
        //start時はprevPosが未定義なので0になるので、ガードをかける
        if (2 < speed && prevPos != Vector3.zero)
        {
            Debug.Log("create mesh called");
            CreateMesh();
        }
        /*
        if (planeMesh)
            Debug.Log("planeMesh true");
        else
            Debug.Log("planeMesh false");

        prevPos = transform.position;
    }

    /*void LateUpdate()
    {
        if (2 < speed && prevPos != Vector3.zero)
        {
            Debug.Log("create mesh called");
            CreateMesh();
        }
    }*/

        //バットの軌跡作成メソッド
        void CreateMesh()
        {
            //mesh.Clear();//問題の根本ではない
            planeMesh.Clear();
            //meshCollider.sharedMesh.Clear();
            //　リストのクリア
            verticesLists.Clear();//バットの座標系に影響している
            tempTriangles.Clear();

            verticesLists.AddRange(new Vector3[] {                  //頂点リストはこの順番で良いのか？
        oldGripPos, oldheadPos,
        gripPosition.position, headPosition.position,
        gripPosition2.position, headPosition2.position
    });

            //　頂点を結ぶ順番
            tempTriangles.AddRange(new int[]{
        0, 1, 2,
        2, 1, 3,
        2, 3, 4,
        4, 3, 5,
        //追加分
        0, 1, 4,
        4, 1, 5,
        0, 2, 4,
        5, 1, 3,
		/*//足りない分を追加する　convexは使わない
        2,3,1
        4,5,1
        
		0, 2, 4,//新旧grip
		5, 1, 3*///新旧head
		
	});
            /*
            mesh.vertices = verticesLists.ToArray();
            mesh.triangles = tempTriangles.ToArray();

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();*/

            //衝突判定用のプレーンを作成
            planeMesh.vertices = verticesLists.ToArray();
            planeMesh.triangles = tempTriangles.ToArray();
            //領域、法線の再計算
            planeMesh.RecalculateBounds();
            planeMesh.RecalculateNormals();

            //　メッシュコライダの再有効化
            meshCollider.enabled = false;
            meshCollider.enabled = true;

            oldGripPos = gripPosition.position;
            oldheadPos = headPosition.position;

        }
    }
}
