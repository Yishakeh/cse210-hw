using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorization
{
    // Class to represent a scripture reference
    public class Reference
    {
        public string Book { get; }
        public string Verse { get; }

        public Reference(string book, string verse)
        {
            Book = book;
            Verse = verse;
        }

        public Reference(string book, int verseStart, int verseEnd)
        {
            Book = book;
            Verse = $"{verseStart}-{verseEnd}";
        }

        public override string ToString()
        {
            return $"{Book} {Verse}";
        }
    }

    // Class to represent a word in the scripture
    public class Word
    {
        public string Text { get; private set; }
        public bool IsHidden { get; private set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public void Hide()
        {
            IsHidden = true;
        }

        public override string ToString()
        {
            return IsHidden ? "_____" : Text;
        }
    }

    // Class to represent the scripture itself
    public class Scripture
    {
        public Reference Reference { get; }
        private List<Word> Words { get; }

        public Scripture(Reference reference, string text)
        {
            Reference = reference;
            Words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        public void HideRandomWord()
        {
            var unhiddenWords = Words.Where(w => !w.IsHidden).ToList();
            if (unhiddenWords.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(unhiddenWords.Count);
                unhiddenWords[index].Hide();
            }
        }

        public string GetDisplayText()
        {
            return $"{Reference}\n" + string.Join(" ", Words);
        }

        public bool AllWordsHidden()
        {
            return Words.All(w => w.IsHidden);
        }
    }

    // Main program class
    public class Program
    {
        public static void Main(string[] args)
        {
            var reference = new Reference("John", 3, 16);
            var scripture = new Scripture(reference, "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit.");
                string input = Console.ReadLine();
                
                if (input?.ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWord();

                if (scripture.AllWordsHidden())
                {
                    Console.Clear();
                    Console.WriteLine("All words are now hidden!");
                    break;
                }
            }
        }
    }
}
