using JsonMigrator.Tests.TestMigrations;
using FluentAssertions;
using NUnit.Framework.Internal;

namespace JsonMigrator.Tests
{
    public class JsonMigratorShould
    {
        private const string EmptyFile = "../../../TestFiles/empty.json";
        private const string ResultFile = "../../../TestFiles/result.json";
        [Test]
        public void SuccessfulExecuteMigrations()
        {
            JsonMigrations.JsonMigrator.AddJsonMigrations(typeof(TestMigrationForAdding).Assembly);
            JsonMigrations.JsonMigrator.Migrate("Test", EmptyFile);

            var result = File.ReadAllText(EmptyFile).Trim();
            var expected = File.ReadAllText(ResultFile).Replace("\n","\r\n").Trim();
            result.Should().BeEquivalentTo(expected);
        }

        [TearDown]
        public void CleanUp()
        {
            File.WriteAllText(EmptyFile, "{}");
        }
    }
}
