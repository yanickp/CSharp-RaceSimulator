using System.Collections.Generic;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, Section.SectionTypes[] sections)
        {
            this.Name = name;
            this.Sections = ArrayToLinkedList(sections);
        }

        private LinkedList<Section> ArrayToLinkedList(Section.SectionTypes[] s)
        {
            LinkedList<Section> Sections = new LinkedList<Section>();

            foreach (Section.SectionTypes section in s)
            {
                Sections.AddLast(new Section(section));
            }

            return Sections;
        }
    }
}