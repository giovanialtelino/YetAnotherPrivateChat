using System;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using YetAnotherPrivateChat.Shared;
using YetAnotherPrivateChat.Shared.DTO;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;


namespace YetAnotherPrivateChat.Shared
{
    public static class ToJsonTest
    {
        public static StringContent ObjectToJson(object o)
        {
           return new StringContent(
              JsonConvert.SerializeObject(o),
              Encoding.UTF8,
              "application/json");
        }
    }    
}

 