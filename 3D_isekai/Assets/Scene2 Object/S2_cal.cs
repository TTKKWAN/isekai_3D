using System.Data.SqlTypes;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace InteractiveCalculator
{
    public class S2_cal : MonoBehaviour
    {
        [SerializeField]
        private Text _displayText;

        [SerializeField]
        private int _maxDisplayLength = 12;

        public enum DisplayMode { value, input };
        public DisplayMode CurrentDisplayMode { get; private set; }

        public decimal Input { get; private set; }
        public decimal Value { get; private set; }
        public int InputDecimals { get; private set; }
        public bool UsingDecimals { get; private set; }
        private bool lastPressWasEquals;
        private string currentOperator; // 기존 private 연산자

        public bool isOn;

        [SerializeField] private GameObject keyObject; // 열쇠 오브젝트
        [SerializeField] private Transform keySpawnPoint; // 열쇠가 나타날 위치


        // 연산자에 접근할 수 있는 public 속성 추가
        public string CurrentOperator
        {
            get { return currentOperator; }
        }

        void Start()
        {
            OnPressedClearAll();
        }

        public void OnPressedClearEntry()
        {
            Input = 0;
            InputDecimals = 0;
            UsingDecimals = false;
            UpdateDisplay();
        }

        public void OnPressedClearAll()
        {
            isOn = true;
            Value = 0;
            currentOperator = "+";
            CurrentDisplayMode = DisplayMode.value;
            OnPressedClearEntry();
        }

        public void OnPressedOff()
        {
            OnPressedClearAll();
            isOn = false;
            UpdateDisplay();
        }

        public void OnPressedOn()
        {
            isOn = true;
            UpdateDisplay();
        }

        public void OnPressedOperator(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                case "÷":
                case "/":
                case "*":
                case "x":
                case "X":
                    if (CurrentDisplayMode == DisplayMode.input)
                        OnPressedEquals();
                    currentOperator = op;

                    // 연산자 선택 시 디스플레이 업데이트
                    UpdateDisplay();

                    CurrentDisplayMode = DisplayMode.value;
                    lastPressWasEquals = false;
                    break;

                case "√":
                case "sqrt":
                    if (CurrentDisplayMode == DisplayMode.input)
                        OnPressedEquals();
                    currentOperator = op;
                    CurrentDisplayMode = DisplayMode.value;
                    lastPressWasEquals = false;
                    OnPressedEquals();
                    break;

                default:
                    Debug.LogError("Unknown operator '" + op + "'");
                    break;
            }
        }


        public void OnPressedNumber(int number)
        {
            if (CurrentDisplayMode == DisplayMode.value)
                Input = 0;
            if (lastPressWasEquals)
                Value = 0;

            if (!UsingDecimals)
            {
                Input *= 10;
                Input += number;
            }
            else
            {
                Input += (decimal)number / (decimal)Mathf.Pow(10, ++InputDecimals);
            }

            CurrentDisplayMode = DisplayMode.input;
            lastPressWasEquals = false;
            UpdateDisplay();
        }

        public void OnPressedDecimal()
        {
            UsingDecimals = true;
            lastPressWasEquals = false;

            if (CurrentDisplayMode != DisplayMode.input)
            {
                Input = 0;
                InputDecimals = 0;
                CurrentDisplayMode = DisplayMode.input;
            }

            UpdateDisplay();
        }

        public void OnPressedPercentage()
        {
            Input /= 100;
            OnPressedEquals();
        }

        public void OnPressedPi()
        {
            Input = 3.14159265358979323m;
            UsingDecimals = true;
            InputDecimals = 12;
            lastPressWasEquals = false;
            CurrentDisplayMode = DisplayMode.input;
            UpdateDisplay();
        }

        public void OnPressedSwapSign()
        {
            if (CurrentDisplayMode == DisplayMode.input)
                Input *= -1;
            else
                Value *= -1;

            UpdateDisplay();
        }

        public void OnPressedEquals()
        {
            try
            {
                if (Input == 220200)
                {
                    Debug.Log("4221 입력 후 = 버튼 눌림, 계산기 및 오브젝트 숨김, 문 열림!");
                    if (keyObject != null)
                    {
                        // 열쇠를 활성화
                        keyObject.SetActive(true);

                        // Rigidbody 활성화 및 물리 적용
                        Rigidbody keyRigidbody = keyObject.GetComponent<Rigidbody>();
                        if (keyRigidbody != null)
                        {
                            keyRigidbody.isKinematic = false; // 물리 활성화
                            keyRigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse); // 아래로 힘 추가
                        }
                        AudioSource keyAudio = keyObject.GetComponent<AudioSource>();
                        if (keyAudio != null)
                        {
                            keyAudio.PlayDelayed((float) 0.4);
                        }
                    }
                    return; // 이후 로직 실행 방지
                }
                // var triggerController = FindObjectOfType<MinusController>();

                // triggerController.SetMinusPressed(); // MinusTriggerController에 - 버튼 누름 알림

                switch (currentOperator)
                {
                    case "-":
                        Value -= Input;
                        break;
                    case "+":
                        Value += Input;
                        break;
                    case "÷":
                    case "/":
                        Value /= Input;
                        break;
                    case "*":
                    case "x":
                    case "X":
                        Value *= Input;
                        break;
                    case "√":
                    case "sqrt":
                        Value = (decimal)Mathf.Sqrt((float)Value);
                        break;
                    default:
                        Debug.LogError("Unknown operator '" + currentOperator + "'");
                        break;
                }
                lastPressWasEquals = true;
                UsingDecimals = false;
                InputDecimals = 0;
                CurrentDisplayMode = DisplayMode.value;
                UpdateDisplay();
            }
            catch
            {
                _displayText.text = "Error";
            }
        }

        private void UpdateDisplay()
        {
            if (_displayText == null)
            {
                Debug.LogError("Error: _displayText가 null입니다. Unity 에디터에서 Text 컴포넌트를 연결하세요.");
                return;
            }

            _displayText.gameObject.SetActive(isOn);

            if (!isOn) return;

            // 연산자가 "-"일 때 "-" 기호만 표시
            if (currentOperator == "-")
            {
                _displayText.text = "-";
                return;
            }

            if (CurrentDisplayMode == DisplayMode.input)
            {
                _displayText.text = FormatDecimal(Input, InputDecimals);
                return;
            }

            _displayText.text = FormatDecimal(Normalize(Value), DigitsAfterDecimal(Value));
        }


        string FormatDecimal(decimal val, int decimalDigits)
        {
            string str = val.ToString($"N{decimalDigits}", new CultureInfo("en-US"));

            if (str.Length <= _maxDisplayLength)
                return str;

            if (val < (decimal)Mathf.Pow(10, _maxDisplayLength - 3) && val > -(decimal)Mathf.Pow(10, _maxDisplayLength - 4))
                return str.Substring(0, _maxDisplayLength);

            return val.ToString($"G7", new CultureInfo("en-US"));
        }

        static int DigitsAfterDecimal(decimal value)
        {
            return ((SqlDecimal)value).Scale;
        }

        public static decimal Normalize(decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }

        public bool IsEqualsPressed()
        {
            return lastPressWasEquals;
        }
    }
}
