using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitectureExample.Domain.Enums
{
    public class BookSituationEnum
    {
        public static BookSituationEnum Awaiting { get; } = new BookSituationEnum(1, "Awaiting");
        public static BookSituationEnum Lent { get; } = new BookSituationEnum(2, "Lent");

        private BookSituationEnum(int val, string name)
        {
            Value = val;
            Name = name;
        }

        private BookSituationEnum() { } //required for EF

        public int Value { get; private set; }  
        public string Name { get; private set; }

        public static IEnumerable<BookSituationEnum> List()
        {
            return new[] { Awaiting, Lent };
        }

        public static BookSituationEnum FromString(string enumString)
        {
            return List().FirstOrDefault(r => String.Equals(r.Name, enumString, StringComparison.OrdinalIgnoreCase));
        }

        public static BookSituationEnum FromValue(int value)
        {
            return List().FirstOrDefault(r => r.Value == value);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static bool operator == (BookSituationEnum first, BookSituationEnum second)
        {
            return first.Value == second.Value;
        }

        public static bool operator != (BookSituationEnum first, BookSituationEnum second)
        {
            return first.Value != second.Value;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this.Value != ((BookSituationEnum)obj).Value;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
