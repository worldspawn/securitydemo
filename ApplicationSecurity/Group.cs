using System.Collections.Generic;

namespace ApplicationSecurity
{
    public class Group
    {
        public Group()
        {
            Sets = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Sets { get; set; } 
    }
}