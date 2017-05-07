using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieHubUI.Models
{
    public class ViewModelMovie
    {       
            public int Id { get; set; }
            public string Name { get; set; }       
    }

    public class MovieIndexModel
    {
        public IEnumerable<tbl_Movie> tblMovieSet { get; set; }
        public IEnumerable<UploadedFile> upFile { get; set; }
    }
}