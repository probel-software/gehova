using System;
using System.IO;
using System.Reflection;

namespace Probel.Gehova.Business.Helpers
{
    public class AssetManager
    {
        #region Fields

        private readonly Assembly SourceAssembly;

        #endregion Fields

        #region Constructors

        public AssetManager(Type sourceType)
        {
            SourceAssembly = Assembly.GetAssembly(sourceType);
        }

        public AssetManager(object source)
        {
            if (source != null) { SourceAssembly = Assembly.GetAssembly(source.GetType()); }
            else { throw new ArgumentException($"The source is null."); }
        }

        #endregion Constructors

        #region Methods

        public string GetScript(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName)) { throw new InvalidDataException("The resource name is null or empty."); }
            else if (resourceName.EndsWith(".sql") == false)
            {
                throw new InvalidDataException(
                    "The resource does not seem to be an sql file. The expected extension is '.sql'");
            }
            else
            {
                using (var stream = SourceAssembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new ArgumentNullException(
                            $"'{nameof(resourceName)}' is null. " +
                            $"Are you sure the resource '{resourceName}' exists in the assembly '{SourceAssembly.FullName}'");
                    }
                    using (var reader = new StreamReader(stream))
                    {
                        var text = reader.ReadToEnd();
                        return text;
                    }
                }
            }
        }

        #endregion Methods
    }
}