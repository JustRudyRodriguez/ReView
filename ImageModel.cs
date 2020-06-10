using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReView
{
   public class ImageModel//holds Json data.
    {
        public ImageModelChildren Data { get; set; }//Grabs data from "data" tag in json. Not case sensitive due to nuget extension.
    }
}
