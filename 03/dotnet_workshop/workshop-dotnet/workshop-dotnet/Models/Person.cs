using Newtonsoft.Json;

namespace workshop_dotnet.Models
{
    public class Person
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        // TODO: enter the all lower-cased name in the JsonProperty attribute
        // so that this property matches up correctly to the Json property name
        [JsonProperty("TODO")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("type")]
        public string Type => typeof(Person).Name.ToLower();

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(FirstName) &&
                   !string.IsNullOrEmpty(LastName) &&
                   !string.IsNullOrEmpty(Email);
        }
    }
}