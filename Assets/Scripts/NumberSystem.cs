using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem : MonoBehaviour
{

    private AudioManager theAudio;

    public string key_sound; // 방향키 사운드
    public string enter_sound; // 결정키
    public string cancel_sound; // 취소 & 오답
    public string correct_sound; // 정답

    private int count; // 배열의 크기, 자릿수 1000 -> 3
    private int selectTextBox; // 선택된 자릿수
    private int result; // 플레이어가 도출해낸 값
    private int correctNumber; // 정답

    private string tempNumber;
    
    public GameObject superObject; // 화면 가운데 정렬
    public GameObject[] panel;
    public Text[] Number_Text;
    public int asd;

    public Animator anim;

    public bool activated; // return new waitUntil
    private bool keyinput; // 키처리 활성화, 비활성화
    private bool correctFlag; //정답인지 아닌지 여부


    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    public void ShowNumber(int _correctNumber)
    {
        correctNumber = _correctNumber;
        activated = true;
        correctFlag = false;

        string temp = correctNumber.ToString(); // "143451" 이유 -> length.(자릿수 반환)

        for (int i = 0; i < temp.Length; i++)
        {
            count = i;
            panel[i].SetActive(true);
            Number_Text[i].text = "0";
        }

        superObject.transform.position = new Vector3(superObject.transform.position.x + (asd * count), superObject.transform.position.y, superObject.transform.position.z);

        selectTextBox = 0;
        result = 0;
        SetColor();
        anim.SetBool("Appear", true);
        keyinput = true;
    }

    public bool GetResult()
    {
        return correctFlag;
    }

    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_Text[selectTextBox].text); // 선택된 자리수의 텍스트를 Integer 숫자 형식으로 강제 형변환

        if (_arrow == "DOWN")
        {
            if (temp == 0)
                temp = 9;
            else
                temp--;
        }
        else if (_arrow == "UP")
        {
            if (temp == 9)
                temp = 0;
            else
                temp++;
        }

        Number_Text[selectTextBox].text = temp.ToString();
    }

    public void SetColor()
    {
        Color color = Number_Text[0].color;
        color.a = 0.3f;
        for (int i = 0; i <= count; i++)
        {
            Number_Text[i].color = color;
        }

        color.a = 1f;
        Number_Text[selectTextBox].color = color;

    }

    // Update is called once per frame
    void Update()
    {
        if (keyinput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("DOWN");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("UP");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(key_sound);
                if (selectTextBox < count)
                    selectTextBox++;
                else
                    selectTextBox = 0;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(key_sound);
                if (selectTextBox > 0)
                    selectTextBox--;
                else
                    selectTextBox = count;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.Z)) // 결정키
            {
                theAudio.Play(key_sound);
                keyinput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X)) // 취소키
            {
                theAudio.Play(key_sound);
                keyinput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
    }

    IEnumerator OXCoroutine()
    {
        Color color = Number_Text[0].color;
        color.a = 1f;

        for (int i = count; i >= 0; i--) // 반대로 한 이유
        {
            Number_Text[i].color = color;
            tempNumber += Number_Text[i].text; // 5000... -> 1356 // 6531 끝번호부터 의도대로 넣기 위함 
        }

        yield return new WaitForSeconds(1f);

        result = int.Parse(tempNumber);

        if (result == correctNumber)
        {
            theAudio.Play(correct_sound);
            correctFlag = true;
        }
        else
        {
            theAudio.Play(cancel_sound);
            correctFlag = false;
        }

        Debug.Log("우리가 낸 답 = " + result + "정답 = " + correctNumber);
        StartCoroutine(ExitCoroutine());
    }

    IEnumerator ExitCoroutine()
    {
        result = 0;
        tempNumber = "";
        anim.SetBool("Appear", false);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i <= count; i++)
        {
            panel[i].SetActive(false);
        }
        superObject.transform.position = new Vector3(superObject.transform.position.x - (asd * count), superObject.transform.position.y, superObject.transform.position.z);

        activated = false;
    }
}
