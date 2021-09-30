using System.Collections.Generic;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        Track(string name, Section.SectionTypes[] sections)
        {
            
        }
    }
}