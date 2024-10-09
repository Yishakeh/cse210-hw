using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    // Class to represent a journal entry
    public class JournalEntry
    {
        public string Date { get; }
        public string Prompt { get; }
        public string Response { get; }

        public JournalEntry(string date, string prompt, string response)
        {
            Date = date;
            Prompt = prompt;
            Response = response;
        }

        public override string ToString()
        {
            return $"{Date} | {Prompt} | {Response}";
        }
    }

    // Class to manage the journal
    public class Journal
    {
        private List<JournalEntry> entries;
        private Random random;

        public Journal()
        {
            entries = new List<JournalEntry>();
            random = new Random();
        }

        public void AddEntry(string prompt, string response)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            entries.Add(new JournalEntry(date, prompt, response));
        }

        public void DisplayEntries()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
                }
            }
        }

        public void LoadFromFile(string filename)
        {
            entries.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(new[] { "~|~" }, StringSplitOptions.None);
                    if (parts.Length == 3)
                    {
                        entries.Add(new JournalEntry(parts[0], parts[1], parts[2]));
                    }
                }
            }
        }

        public string GetRandomPrompt()
        {
            var prompts = new List<string>
            {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?"
            };
            return prompts[random.Next(prompts.Count)];
        }
    }

    // Main program class
    public class Program
    {
        private static Journal journal = new Journal();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display journal");
                Console.WriteLine("3. Save journal to file");
                Console.WriteLine("4. Load journal from file");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        WriteEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        SaveJournal();
                        break;
                    case "4":
                        LoadJournal();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void WriteEntry()
        {
            string prompt = journal.GetRandomPrompt();
            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            journal.AddEntry(prompt, response);
        }

        private static void SaveJournal()
        {
            Console.Write("Enter filename to save: ");
            string filename = Console.ReadLine();
            journal.SaveToFile(filename);
            Console.WriteLine("Journal saved.");
        }

        private static void LoadJournal()
        {
            Console.Write("Enter filename to load: ");
            string filename = Console.ReadLine();
            journal.LoadFromFile(filename);
            Console.WriteLine("Journal loaded.");
        }
    }
}