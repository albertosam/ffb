namespace MultiAgentes.Lib.Requests
{
    using System.Text;

    /// <summary>
    /// Defines the <see cref="JsonContent" />.
    /// </summary>
    public class JsonContent : System.Net.Http.StringContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonContent"/> class.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        public JsonContent(object value)
            : base(Newtonsoft.Json.JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonContent"/> class.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="mediaType">The mediaType<see cref="string"/>.</param>
        public JsonContent(object value, string mediaType)
            : base(Newtonsoft.Json.JsonConvert.SerializeObject(value), Encoding.UTF8, mediaType)
        {
        }
    }
}
