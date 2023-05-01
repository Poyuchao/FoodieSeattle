using System.IO;

using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Test fixture set up class to pair with TestHelper.cs
    /// </summary>
    [SetUpFixture]
    public class TestFixture
    {
        // Holds path to the Web Root
        public static string DataWebRootPath = "./wwwroot";

        // Holds path to the data folder for the content
        public static string DataContentRootPath = "./data/";

        /// <summary>
        /// Pre-test setup function that makes copies of current data on hand
        /// of the datastore for use by TestHelper
        /// </summary>
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            
            // Copy over latest version of datastore files
            var DataWebPath = "../../../../src/bin/Debug/net6.0/wwwroot/data";
            var DataUTDirectory = "wwwroot";
            var DataUTPath = DataUTDirectory + "/data";

            // Delete the Detination folder
            if (Directory.Exists(DataUTDirectory))
            {
                Directory.Delete(DataUTDirectory, true);
            }

            // Make the data directory
            Directory.CreateDirectory(DataUTPath);

            // Copy over all data files
            var filePaths = Directory.GetFiles(DataWebPath);
            foreach (var filename in filePaths)
            {
                string OriginalFilePathName = filename.ToString();
                var newFilePathName = OriginalFilePathName.Replace(DataWebPath, DataUTPath);

                File.Copy(OriginalFilePathName, newFilePathName);
            }
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}

