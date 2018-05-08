using System.IO;
using LitJson;
using UnityEngine;

namespace Assets.Scripts.DataAccess.Repository.JsonRepository
{
    public abstract class JsonRepository
    {
        private JsonData data;

        //Opens and reads the file
        public JsonRepository OpenFileAndReadData(string path)
        {
            data = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + path));
            return this;
        }

        //Gets the collection
        public JsonData GetCollection()
        {
            if (data == null)
                Debug.Log("Local Data Not Found");
            return data;
        }

    }
}
