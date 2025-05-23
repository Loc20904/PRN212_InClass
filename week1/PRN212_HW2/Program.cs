
// A utility to analyze text files and provide statistics
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileAnalyzer
{
    class Program
    {
        public static int CountWord(string content)
        {
            int wordCount = content.Split(" ").Length;
            return wordCount;
        }

        public static int CountCharacter(string content,bool whitespace)
        {
            int characterCount = 0;
            if (whitespace)
            {
                content = content.Replace("\n", "").Replace("\r", "").Replace("\t", ""); // Remove newlines and tabs
                characterCount = content.Length;
            }               
            else
            {
                // Remove whitespace characters
                string[] words = content.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    characterCount += word.Length;
                }
            }
            return characterCount;
        }

        public static int CountSentence(string content)
        {
            int sentenceCount = 0;
            char[] sentenceDelimiters = { '.', '!', '?' };
            string[] sentences = content.Split(sentenceDelimiters, StringSplitOptions.RemoveEmptyEntries);
            sentenceCount = sentences.Length;
            return sentenceCount;
        }
        public static Dictionary<string, int> CountWordFrequency(string content)
        {
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
            string[] words = content.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                string cleanedWord = word.ToLower().Trim(new char[] { '.', ',', '!', '?' });
                if (wordFrequency.ContainsKey(cleanedWord))
                {
                    wordFrequency[cleanedWord]++;
                }
                else
                {
                    wordFrequency[cleanedWord] = 1;
                }
            }
            return wordFrequency;
        }

        public static string GetMostCommonWords(Dictionary<string, int> wordFrequency)
        {
            var mostCommonWords = wordFrequency.OrderByDescending(x => x.Value).Take(5);
            string result = string.Join(", ", mostCommonWords.Select(x => $"{x.Key} ({x.Value})"));
            return result;
        }

        public static double AvgWords(string contents)
        {
            int characters=CountCharacter(contents, false);
            int words = CountWord(contents);
            double avg = (double)characters / words;
            return avg;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");

            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }
            
            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");

                // Read the file content
                string content = File.ReadAllText(filePath);
                // TODO: Implement analysis functionality
                // 1. Count words
                // 2. Count characters (with and without whitespace)
                // 3. Count sentences
                // 4. Identify most common words
                // 5. Average word length
                // Example implementation for counting lines:
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines: {lineCount}");

                // TODO: Additional analysis to be implemented
                Console.WriteLine($"Number of words: {CountWord(content)}");
                Console.WriteLine($"Number of characters with whitespace: {CountCharacter(content,true)}");
                Console.WriteLine($"Number of characters without whitespace: {CountCharacter(content, false)}");
                Console.WriteLine($"Number of sentences: {CountSentence(content)}");
                Console.WriteLine($"Most common words: {GetMostCommonWords(CountWordFrequency(content))}");
                Console.WriteLine($"Average word length: {AvgWords(content):F2} characters");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}