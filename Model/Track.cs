using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

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
                Sections.AddLast(new Section()
                {
                    SectionType = section
                });
            }

            return Sections;
        }

        public Section GetStartSection()
        {
            return Sections.FirstOrDefault(s => s.SectionType == Section.SectionTypes.StartGrid);
        }

        public Section GetPReviousSection(Section s)
        {
            LinkedListNode<Section> result = Sections.Find(s);
            LinkedListNode<Section> previousResult = result.Previous;
            return previousResult.Value;
        }
    }
}