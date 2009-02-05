using System.IO;
using System.Xml;
using NUnit.Framework;
using Rhino.Mocks;

namespace Armory.Test
{
    [TestFixture]
    public class ArmoryDocumentProviderTest
    {
        private MockRepository m_mock;
        private string m_validUri;
        private Stream m_validCharacterSheetStream;
        private FileStream m_noResultsStream;
        private FileStream m_notFoundStream;

        [TestFixtureSetUp]
        public void Setup()
        {
            m_mock = new MockRepository();

            m_validUri = "http://www.wowarmory.com/character-sheet.xml?realm=Test&name=Test";

            m_validCharacterSheetStream = OpenDocument("ValidCharacterSheet");
            m_noResultsStream = OpenDocument("NoResults");
            m_notFoundStream = OpenDocument("NotFound");
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            m_validCharacterSheetStream.Close();
            m_noResultsStream.Close();
        }

        [Test]
        public void GetDocument_ReturnsNotNull()
        {
            IArmoryProtocol protocol = m_mock.DynamicMock<IArmoryProtocol>();
            ArmoryDocumentProvider provider = new ArmoryDocumentProvider(protocol);

            Expect.Call(protocol.GetResponse("NotFound")).Return(m_notFoundStream);
            m_mock.Replay(protocol);

            XmlDocument document = provider.GetDocument("NotFound");

            Assert.IsTrue(document != null);
        }

        [Test]
        public void GetDocument_ShouldCall_Protocol_GetStream()
        {
            IArmoryProtocol protocol = m_mock.DynamicMock<IArmoryProtocol>();

            ArmoryDocumentProvider provider = new ArmoryDocumentProvider(protocol);

            Expect.Call(protocol.GetResponse(m_validUri)).Return(m_validCharacterSheetStream);
            m_mock.Replay(protocol);

            provider.GetDocument(m_validUri);

            m_mock.Verify(protocol);
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private static FileStream OpenDocument(string fileName)
        {
            return File.Open("Documents\\" + fileName + ".xml", FileMode.Open, FileAccess.Read);
        }
    }
}