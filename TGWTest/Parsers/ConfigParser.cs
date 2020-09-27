using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TGWTest.Models;
using TGWTest.Validators;

namespace TGWTest.Parsers
{
    class ConfigParser
    {
        private string _baseConfigFile;
        private string _projectConfigFile;
        private string _experimentConfigFile;


        private List<Section> _parameters = new List<Section>();

        public ConfigParser(string baseConfigFile, string projectConfigFile = null, string experimentConfigFile = null)
        {
            _baseConfigFile = baseConfigFile;
            _projectConfigFile = projectConfigFile;
            _experimentConfigFile = experimentConfigFile;
        }

        public List<Section> LoadAllParameters()
        {
            //_parameters.Add(new Parameter("Key", "source", "Value"));
            //_parameters.Add(new Parameter("Key1", "source", 1));
            //_parameters.Add(new Parameter("Key2", "source", DateTime.Now));
            LoadConfigFile(_baseConfigFile);
            if (_projectConfigFile != null) LoadConfigFile(_projectConfigFile);
            if (_experimentConfigFile != null) LoadConfigFile(_experimentConfigFile);
            return _parameters;
        }
        public dynamic GetParamValue(string paramId)
        {
            var param = _parameters.FirstOrDefault(n => n.Key == paramId);
            return param.Value;
        }
        private void LoadConfigFile(string configFile)
        {
            var validator = new Validator();

            var currentDir = AppDomain.CurrentDomain.BaseDirectory;

            var lines = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), configFile));
            Section currentSection = null;
            SubSection currentSubSection = null;

            foreach (string line in lines)
            {
                //parsing sections
                string sectionIndicator = "=== Section: ";
                string sectionEndIndicator = " ===";
                if (line.Contains(sectionIndicator) && line.Contains(sectionEndIndicator))
                {
                    var section = line.Substring(line.IndexOf(sectionIndicator) + sectionIndicator.Length);
                    section = section.Substring(0, section.IndexOf(sectionEndIndicator));
                    if (validator.ValidateSection(section))
                    {
                        if (!_parameters.Any(p => p.Key == section))
                        {
                            var tempSection = new Section(section, new List<SubSection>());
                            _parameters.Add(tempSection);
                            currentSection = tempSection;
                        }
                        else currentSection = _parameters.First(p => p.Key == section);
                    }
                    continue;
                }
                //parsing SubSections
                string subSectionIndicator = "- ";
                string subSectionEndIndicator = ":";
                if (line.Contains(subSectionIndicator) && line.Contains(subSectionEndIndicator))
                {
                    var subSection = line.Substring(line.IndexOf(subSectionIndicator) + subSectionIndicator.Length);
                    subSection = subSection.Substring(0, subSection.IndexOf(subSectionEndIndicator));
                    if (validator.ValidateSubSection(subSection))
                    {
                        if (currentSection?.Value?.FirstOrDefault(s => s.Key == subSection) == null)
                        {
                            var tempSubsection = new SubSection(subSection, new List<Parameter>());
                            currentSection.Value.Add(tempSubsection);
                            currentSubSection = tempSubsection;
                        }
                        else
                            currentSubSection = currentSection.Value.First(s => s.Key == subSection);
                    }
                    continue;
                }
                //parsing parameters
                string parameterIndicator = ":";
                string parameterEndIndicator = "//";
                if (line.Contains(parameterIndicator))
                {
                    var key = line.Substring(0, line.IndexOf(parameterIndicator));
                    var value = line.Substring(line.IndexOf(parameterIndicator) + parameterIndicator.Length);
                    value = value.Substring(0, value.IndexOf(parameterEndIndicator));
                    value = value.Replace("\t", "");
                    if(validator.ValidateParameter(key, value))
                    {
                        var currentParam = currentSubSection.Value.FirstOrDefault(c => c.Key == key);
                        if (currentParam == null)
                        {
                            currentSubSection.Value.Add(new Parameter(key, configFile, value));
                        }
                        else
                        {
                            currentParam.Value = value;
                            currentParam.Source = configFile;
                        }
                    }
                    continue;
                }
            }
        }
    }
}
