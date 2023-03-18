using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private readonly Dictionary<string, System.Action> keywords = new();
    
    // Start is called before the first frame update
    void Start()
    {
        keywords.Add("close", Camera.main.GetComponent<MoveCamera>().MoveUp);
        keywords.Add("down", Camera.main.GetComponent<MoveCamera>().MoveDown);
        keywords.Add("left", Camera.main.GetComponent<MoveCamera>().MoveLeft);

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (keywords.TryGetValue(args.text, out System.Action keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
