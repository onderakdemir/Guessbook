using Microsoft.AspNetCore.Mvc;
using System;
using GuessBook.Web.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GuessBook.Web.Controllers
{
    public class ImageCallerController : BaseAPIController
    {

        private IHostingEnvironment _env;
        public ImageCallerController(IHostingEnvironment env)
        {
            _env = env;
        }
        [HttpPost]
        public string UploadFile(TransferDataDto transferdata)
        {
            if (transferdata == null) throw new Exception("Model can not be null");

            byte[] filebytess = Convert.FromBase64String(transferdata.Fileb64);
            try
            {
                string module_path = "";
                //if (transferdata.ModuleNo == "1")
                    module_path = "/images/questions";

                var webRoot = _env.WebRootPath + module_path;
                var file = Path.Combine(webRoot, transferdata.FileName);

                if (System.IO.File.Exists(file))
                {
                    return "AlreadyExist";
                }
                else
                {
                    System.IO.File.WriteAllBytes(file, filebytess);
                    return "OK";
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        [HttpPost]
        public string UploadFileOptions(TransferDataDto transferdata)
        {
            if (transferdata == null) throw new Exception("Model can not be null");

            byte[] filebytess = Convert.FromBase64String(transferdata.Fileb64);
            try
            {
                string module_path = "";             
                module_path = "/images/options";

                var webRoot = _env.WebRootPath + module_path;
                var file = Path.Combine(webRoot, transferdata.FileName);

                if (System.IO.File.Exists(file))
                {
                    throw new Exception("AlreadyExist");
                }
                else
                {
                    System.IO.File.WriteAllBytes(file, filebytess);
                    return "OK";
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
