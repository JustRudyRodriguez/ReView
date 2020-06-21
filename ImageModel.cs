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
        //There is a tree of classes that start from here, They are used to dive into the deaper layers of the json structure.

        public ImageModel message { get; set; }
    }
}
