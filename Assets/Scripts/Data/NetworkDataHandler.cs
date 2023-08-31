using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace CMG.BallMazeGame.Data
{
    public class NetworkDataHandler : MonoBehaviour
    {
        public static NetworkDataHandler Instance;
        [SerializeField] private string _url;

        private Coroutine requestRoutine;
        
        private void Awake()
        {
            Instance = this;
        }

        public void HandleWebrequest(SessionData sessionData)
        {
            var json = JsonUtility.ToJson(sessionData);
            if (requestRoutine != null)
                StopCoroutine(requestRoutine);

            //File.WriteAllText("D:\\Projects\\data.txt", json);

            requestRoutine = StartCoroutine(HandleWebrequestRoutine(json));
        }

        IEnumerator HandleWebrequestRoutine(string data)
        {
            var url = new Uri(_url);
            var request = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.downloadHandler.error);
            }
            else
            {
                Debug.Log("Data send!, Catch it Victor!");
            }
        }
    }
}