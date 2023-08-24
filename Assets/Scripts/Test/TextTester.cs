using TMPro;
using UnityEngine;

namespace CMG.BallMazeGame.Tester
{
    public class TextTester : MonoBehaviour
    {
        public static TextTester Instance;
        private string _newText;
        [SerializeField] private TextMeshProUGUI _text;
        
        
        private void Awake()
        {
            Instance = this;
        }

        public void SetText(string text)
        {
            _newText += $"\n {text}";
            _text.text = _newText;
        }
    }
}