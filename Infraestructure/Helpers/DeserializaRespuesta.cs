using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Helpers
{
    public class DeserializaRespuesta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="peticion"></param>
        /// <returns></returns>
        public async Task<string> DeserializaCuerpo(string peticion)
        {
            string request = "";

            if (peticion != null && peticion.Length > 0)
            {
                request = JsonConvert.DeserializeObject(peticion).ToString();
            }

            return request;
        }        
    }
}
