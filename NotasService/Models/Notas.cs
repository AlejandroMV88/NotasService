using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasService.Models
{
    public class Notas
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Content { get; set; }
        public DateTime Creation_date { get; set; }

        [JsonProperty("update_date")]
        public DateTime Update_date { get; set; }
    }
}
