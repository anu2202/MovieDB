using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieHubUI.Models
{
    public class UploadedFile
    {
       
            public int ID { get; set; }
            public string Title { get; set; }
            public long Size { get; set; }
            [AllowHtml]
            public string Name { get; set; }
            public string Path { get; set; }
       
    }
}