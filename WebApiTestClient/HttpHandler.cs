﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;

namespace WebApiTestClient
{
    public class HttpHandler:IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            object response = null;
            if (context.Request.QueryString["ApiName"] != null)
            {
                response = ApiExplorerHelper.GetApi(context.Request.QueryString["ApiName"]);
            }
            else
            {
                response =  ApiExplorerHelper.GetAPIs();
            }
            RespondJson(context,response );
        }


        private void RespondJson(HttpContext context, object content)
        {
            context.Response.ContentType = "application/json";

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            
            context.Response.Write(JsonConvert.SerializeObject(content,settings));
        }

        public bool IsReusable { get { return false; } }
    }
}