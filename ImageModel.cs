using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReView
{
   public class ImageModel//holds Json data.
    {   //There is a tree of classes that start from here, They are used to dive into the deaper layers of the json structure.

 
        public ImageModelChildren Data { get; set; }//Grabs data from "data" tag in json. Not case sensitive due to nuget extension.
<<<<<<< HEAD
        //There is a tree of classes that start from here, They are used to dive into the deaper layers of the json structure.

        public ImageModel message { get; set; }
=======

        public ErrorCode Error { get; set; }// may not be needed.

>>>>>>> d4b1e6009d9e57353b395527eeb9589010e24370
    }
}
