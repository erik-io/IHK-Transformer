using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IHK_Transform.Infrastructure.Configuration
{
    internal class IniReader
    {
        private readonly Dictionary<string, Dictionary<string, string>> sections = new Dictionary<string, Dictionary<string, string>>();

        public IniReader(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Die angegebene .ini-Datei wurde nicht gefunden.", filePath);

            LoadData(filePath);
        }

        private void LoadData(string filePath)
        {
            string currentSection = null;

            var lines = File.ReadAllLines(filePath).Select(l => l.Trim());

            foreach (var line in lines)
            {
                // Leere Zeilen überspringen
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Kommentare überspringen (beginnen mit ; oder #)
                if (line.StartsWith(";") || line.StartsWith("#"))
                    continue;

                // Sektion erkennen [SectionName]
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line.Substring(1, line.Length - 2);
                    if (!sections.ContainsKey(currentSection))
                        sections[currentSection] = new Dictionary<string, string>();
                    continue;
                }

                // Schlüssel-Wert-Paare verarbeiten
                if (currentSection != null && line.Contains("="))
                {
                    // Teile die Zeile am ersten =-Zeichen
                    var parts = line.Split(new [] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();

                        // Inline-Kommentare entfernen
                        var commentIndex = value.IndexOfAny(new[] { ';', '#' });
                        if (commentIndex >= 0)
                            value = value.Substring(0, commentIndex).Trim();

                        // Leere Werte als null speichern
                        if (string.IsNullOrWhiteSpace(value))
                            value = null;

                        sections[currentSection][key] = value;
                    }
                }
            }
        }

        public string GetValue(string section, string key)
        {
            if (sections.ContainsKey(section) && sections[section].ContainsKey(key))
                return sections[section][key];
            return null;
        }

        public bool HasSection(string section)
        {
            return sections.ContainsKey(section);
        }

        public IEnumerable<string> GetSectionNames()
        {
            return sections.Keys;
        }
    }
}
