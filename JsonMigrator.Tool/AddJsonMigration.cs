using Cocona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonMigrator.Tool
{
    [HasSubCommands(typeof(AddCommands))]
    public class JsonMigration
    {
        [Command("add")]
        public void Command() 
        {
        }

    }
    

    public class AddCommands
    {
        [Command("add json migration")]
        public void Command()
        {
            if ()
            {

            }
            else
            {

            }
        }

        

    }
}
