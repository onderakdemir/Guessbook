using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessBook.Web.Models
{  
    public class TransferDataDto
    {
        [JsonProperty("fileb64")]
        public string Fileb64 { get; set; }

        [JsonProperty("fimeName")]
        public string FileName { get; set; }

        [JsonProperty("modelNo")]
        public string ModuleNo { get; set; }

        [JsonProperty("photoAlt")]
        public string PhotoAlt { get; set; }

    }
}
