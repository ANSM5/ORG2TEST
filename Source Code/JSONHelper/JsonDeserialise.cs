using System;
using Models;
using Newtonsoft.Json;

namespace JsonHelper
{
    public class JsonDeserialise
    {
        public ImportFile Deserialise(string content)
        {
            try
            {
                var deserializedObject = JsonConvert.DeserializeObject<ImportFile>(content);

                return deserializedObject;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when deserialising file content. Error: {e.Message}");
            }
        }
    }
}
