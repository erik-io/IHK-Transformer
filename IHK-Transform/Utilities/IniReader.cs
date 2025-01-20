using System.Collections.Generic;
using System.IO;

namespace IHK_Transform.Utilities
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

            foreach (var line in File.ReadAllLines(filePath))
            {
                var trimmedLine = line.Trim();

                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith(";"))
                    continue;

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Trim('[', ']');
                    if (!sections.ContainsKey(currentSection))
                        sections[currentSection] = new Dictionary<string, string>();
                }
                else if (currentSection != null)
                {
                    var keyValue = trimmedLine.Split(new[] { '=' }, 2);
                    if (keyValue.Length == 2)
                    {
                        var key = keyValue[0].Trim();
                        var value = keyValue[1].Trim();
                        sections[currentSection][key] = value;
                    }
                }
            }
        }

        public string GetValue(string section, string key)
        {
            return sections.ContainsKey(section) && sections[section].ContainsKey(key)
                ? sections[section][key]
                : null;
        }
    }
}
