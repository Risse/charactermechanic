using System.IO;
using System.Xml;

namespace Armory
{
    /// <summary>
    /// A Document Provider to retreive documents using the provided protocol
    /// </summary>
    internal class ArmoryDocumentProvider
    {
        #region Read Only & Static Fields

        /// <summary>
        /// The protocol that we will use to retrieve the documents
        /// </summary>
        private readonly IArmoryProtocol m_protocol;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmoryDocumentProvider"/> class.
        /// </summary>
        /// <param name="protocol">The protocol that is to be used to retrieve the documents</param>
        public ArmoryDocumentProvider(IArmoryProtocol protocol)
        {
            m_protocol = protocol;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the document from the Armory
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetDocument(string uri)
        {
            Stream stream = m_protocol.GetResponse(uri);

            XmlDocument document = new XmlDocument();
            document.Load(stream);

            stream.Close();

            return document;
        }

        #endregion
    }
}