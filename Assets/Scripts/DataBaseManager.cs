using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    static public DataBaseManager instance;

    private PlayerStat theStat;

    public GameObject prefebs_Floating_Text;
    public GameObject parent;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);

            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // 1. 씬 이동. A (이벤트 true false) <-> B (전역변수)
    // 2. 세이브와 로드
    // 3. 미리 만들어두면 편하다. 아이템. 

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches; // 25번재 쓰위치 (Boss_Appear) true. // 세이브.

    public List<Item> itemList = new List<Item>();

    private void FloatText(int number, string color)
    {
        Vector3 vector = theStat.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefebs_Floating_Text, vector, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().text.text = number.ToString();
        if(color == "GREEN")
            clone.GetComponent<FloatingText>().text.color = Color.green;
        else if(color == "BLUE")
            clone.GetComponent<FloatingText>().text.color = Color.blue;
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
    }

    public void UseItem(int _itemID)
    {
        switch (_itemID)
        {
            case 10001:
                if (theStat.hp >= theStat.currentHP + 50)
                    theStat.currentHP += 50;
                else
                    theStat.currentHP = theStat.hp;
                FloatText(50, "GREEN");
                break;
            case 10002:
                if (theStat.mp >= theStat.currentMP + 50)
                    theStat.currentMP += 15;
                else
                    theStat.currentMP = theStat.mp;
                FloatText(50, "BLUE");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theStat = FindObjectOfType<PlayerStat>();
        itemList.Add(new Item(10001, "빨간 포션", "체력을 50 채워주는 마법의 물약", Item.ItemType.Use));
        itemList.Add(new Item(10002, "파란 포션", "마나를 15 채워주는 마법의 물약", Item.ItemType.Use));
        itemList.Add(new Item(10003, "농축 빨간 포션", "체력을 350 채워주는 마법의 물약", Item.ItemType.Use));
        itemList.Add(new Item(10004, "농축 파란 포션", "마나을 80 채워주는 마법의 물약", Item.ItemType.Use));
        itemList.Add(new Item(11001, "랜덤 상자", "랜덤으로 포션이 나온다. 낮은 확률로 꽝", Item.ItemType.Use));
        itemList.Add(new Item(20001, "짧은 검", "기본적인 용사의 검.", Item.ItemType.Equip, 3));
        itemList.Add(new Item(20301, "사파이어 반지", "1초에 체력1을 회복시켜주는 마법 반지", Item.ItemType.Equip, 0, 0, 1));
        itemList.Add(new Item(30001, "고대 유물의 조각 1", "반으로 쪼개진 고대 유물의 파편", Item.ItemType.Quest));
        itemList.Add(new Item(30002, "고대 유물의 조각 2", "반으로 쪼개진 고대 유물의 파편", Item.ItemType.Quest));
        itemList.Add(new Item(30003, "고대 유물", "고대 유적에 잠들어있던 고대의 유물", Item.ItemType.Quest));
        itemList.Add(new Item(90000, "지수", "블랙핑크 짱짱 걸", Item.ItemType.ETC));
    }
}
