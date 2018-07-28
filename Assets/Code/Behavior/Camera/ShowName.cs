using UnityEngine;
using System.Collections;

/// <summary>
/// Draw GUI label to show name which is always forward to camera;
/// </summary>
public class ShowName : MonoBehaviour
{
    //主角对象
    //private GameObject player;
    //主摄像机对象
    private Camera camera;
    //NPC名称
    public string mName;// = "我是出租车";
    public Color mColor;//设置显示颜色为黄色
    
    //NPC模型高度
    private float npcHeight = 2;

    void Start()
    {
        //根据Tag得到主角对象
       // player = GameObject.FindGameObjectWithTag("MainCamera");
        camera = Camera.main;
        mColor = new Color(0.5f, 0.2f, 0.6f);//Color.yellow;
    }

    void Update()
    {
        //保持NPC一直面朝主角
        //transform.LookAt(player.transform);
    }

    void OnGUI()
    {
        if (name.Length <= 0)
            return;

        //得到NPC头顶在3D世界中的坐标
        //默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + npcHeight, transform.position.z);
        //根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
        Vector2 position = camera.WorldToScreenPoint(worldPosition);
        //得到真实NPC头顶的2D坐标
        position = new Vector2(position.x, Screen.height - position.y);
        //计算NPC名称的宽高
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(name));

        GUI.color = mColor;//Color.yellow;
        //绘制NPC名称
        GUI.Label(new Rect(position.x - (nameSize.x / 2), position.y - nameSize.y, nameSize.x, nameSize.y), name);

    }

    void SetText(string str, Color color)
    {
        mName = str;
        mColor = color;
    }
}