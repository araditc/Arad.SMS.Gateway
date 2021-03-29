using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CPS
{
    /// <summary>
    /// The CPS_Simple class contains methods suitable for most requests that don't require advanced parameters to be specified.
    /// </summary>
    public class CPS_Simple
    {
        /// <summary>
        /// Creates new CPS_Simple class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        public CPS_Simple(CPS_Connection connection)
        {
            this.p_connection = connection;
            this.p_lastResponse = null;
        }

        /// <summary>
        /// Performs a search command. Returns the documents found in a Dictionary with document IDs as keys and document contents as values. Returned document type depends on returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="query">Query string. See <see cref="CPS.CPS_SearchRequest.setQuery(string)"/> for more info on best practices.</param>
        /// <param name="offset">Number of documents to skip before including them in results.</param>
        /// <param name="docs">Maximum document count to return.</param>
        /// <param name="list">Dictionary with tag xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="ordering">Single ordering rule. You can use <see cref="Utils"/> class function to generate this rule.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <param name="stemLang">Defines temming language.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object search(string query, int offset = -1, int docs = -1, Dictionary<string, string> list = null, string ordering = "", CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS, string stemLang = "")
        {
            CPS_SearchRequest request = new CPS_SearchRequest(query, offset, docs, list);
            if (ordering.Length > 0)
                request.setOrdering(ordering);
            if (stemLang.Length > 0)
                request.setStemLang(stemLang);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_SearchResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Performs a search command. Returns the documents found in a Dictionary with document IDs as keys and document contents as values. Returned document type depends on returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="query">Query string. See <see cref="CPS.CPS_SearchRequest.setQuery(string)"/> for more info on best practices.</param>
        /// <param name="offset">Number of documents to skip before including them in results.</param>
        /// <param name="docs">Maximum document count to return.</param>
        /// <param name="list">Dictionary with tag xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="ordering">Multiple ordering rules. You can use <see cref="Utils"/> class function to generate these rules.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <param name="stemLang">Defines temming language.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object search(string query, int offset, int docs, Dictionary<string, string> list, List<string> ordering, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS, string stemLang = "")
        {
            CPS_SearchRequest request = new CPS_SearchRequest(query, offset, docs, list);
            if (ordering != null)
                request.setOrdering(ordering);
            if (stemLang.Length > 0)
                request.setStemLang(stemLang);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_SearchResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Performs a search command with SQL query. Returns the documents found in a Dictionary with document IDs as keys and document contents as values. Returned document type depends on returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="sql_query">The SQL query string.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object sql_search(string sql_query, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_SQLSearchRequest request = new CPS_SQLSearchRequest(sql_query);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_SearchResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Inserts single document into Clusterpoint Server storage.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document as CPS_SimpleXML or XmlElement object.</param>
        /// <returns>List of document IDs inserted.</returns>
        public List<string> insertSingle(string id, Object document)
        {
            CPS_InsertRequest request = new CPS_InsertRequest(id, document);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Inserts multiple documents into Clusterpoint Server storage.
        /// </summary>
        /// <param name="documents">Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt; where key is document ID.</param>
        /// <returns>List of document IDs inserted.</returns>
        public List<string> insertMultiple(Object documents)
        {
            CPS_InsertRequest request = new CPS_InsertRequest(documents);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Updates single document in Clusterpoint Server storage.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document as CPS_SimpleXML or XmlElement object.</param>
        /// <returns>List of document IDs updated.</returns>
        public List<string> updateSingle(string id, Object document)
        {
            CPS_UpdateRequest request = new CPS_UpdateRequest(id, document);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Updates multiple documents in Clusterpoint Server storage.
        /// </summary>
        /// <param name="documents">Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt; where key is document ID.</param>
        /// <returns>List of document IDs updated.</returns>
        public List<string> updateMultiple(Object documents)
        {
            CPS_UpdateRequest request = new CPS_UpdateRequest(documents);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Replaces single document in Clusterpoint Server storage.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document as CPS_SimpleXML or XmlElement object.</param>
        /// <returns>List of document IDs replaced.</returns>
        public List<string> replaceSingle(string id, Object document)
        {
            CPS_ReplaceRequest request = new CPS_ReplaceRequest(id, document);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Replaces multiple documents in Clusterpoint Server storage.
        /// </summary>
        /// <param name="documents">Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt; where key is document ID.</param>
        /// <returns>List of document IDs replaced.</returns>
        public List<string> replaceMultiple(Object documents)
        {
            CPS_ReplaceRequest request = new CPS_ReplaceRequest(documents);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Partially replaces single document in Clusterpoint Server storage.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document as CPS_SimpleXML or XmlElement object.</param>
        /// <returns>List of document IDs partial-replace'd.</returns>
        public List<string> partialReplaceSingle(string id, Object document)
        {
            CPS_PartialReplaceRequest request = new CPS_PartialReplaceRequest(id, document);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Partially replaces multiple document in Clusterpoint Server storage.
        /// </summary>
        /// <param name="documents">Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt; where key is document ID.</param>
        /// <returns>List of document IDs partial-replace'd.</returns>
        public List<string> partialReplaceMultiple(Object documents)
        {
            CPS_PartialReplaceRequest request = new CPS_PartialReplaceRequest(documents);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Performs a partial-xreplace command in Clusterpoint Server storage.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="operation">Operation as <see cref="CPS_PRX_Operation"/> to perform.</param>
        /// <returns>List of document IDs partial-xreplace'd.</returns>
        public List<string> partialXReplace(string id, CPS_PRX_Operation operation)
        {
            CPS_PartialXRequest request = new CPS_PartialXRequest(id, operation);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Performs a partial-xreplace command in Clusterpoint Server storage.
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <param name="operation">Operation as <see cref="CPS_PRX_Operation"/> to perform.</param>
        /// <returns>List of document IDs partial-xreplace'd.</returns>
        public List<string> partialXReplace(List<string> ids, CPS_PRX_Operation operation)
        {
            CPS_PartialXRequest request = new CPS_PartialXRequest(ids, operation);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Performs a partial-xreplace command in Clusterpoint Server storage.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="operations">List of operations as <see cref="CPS_PRX_Operation"/> to perform.</param>
        /// <returns>List of document IDs partial-xreplace'd.</returns>
        public List<string> partialXReplace(string id, List<CPS_PRX_Operation> operations)
        {
            CPS_PartialXRequest request = new CPS_PartialXRequest(id, operations);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Performs a partial-xreplace command in Clusterpoint Server storage.
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <param name="operations">List of operations as <see cref="CPS_PRX_Operation"/> to perform.</param>
        /// <returns>List of document IDs partial-xreplace'd.</returns>
        public List<string> partialXReplace(List<string> ids, List<CPS_PRX_Operation> operations)
        {
            CPS_PartialXRequest request = new CPS_PartialXRequest(ids, operations);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Deletes single document from Clusterpoint Server storage.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <returns>List of document IDs deleted.</returns>
        public List<string> delete(string id)
        {
            CPS_DeleteRequest request = new CPS_DeleteRequest(id);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Deletes multiple documents from Clusterpoint Server storage.
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <returns>List of document IDs deleted.</returns>
        public List<string> delete(List<string> ids)
        {
            CPS_DeleteRequest request = new CPS_DeleteRequest(ids);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ModifyResponse)this.p_lastResponse).getModifiedIds();
        }

        /// <summary>
        /// Performs list-last command. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="offset">Defines number of document to skip before including in search results.</param>
        /// <param name="docs">Defines number of documents to return.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object listLast(Dictionary<string, string> list = null, int offset = -1, int docs = -1, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_ListLastRequest request = new CPS_ListLastRequest(list, offset, docs);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_LookupResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Performs list-first command. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="offset">Defines number of document to skip before including in search results.</param>
        /// <param name="docs">Defines number of documents to return.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object listFirst(Dictionary<string, string> list = null, int offset = -1, int docs = -1, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_ListFirstRequest request = new CPS_ListFirstRequest(list, offset, docs);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_LookupResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Performs retrieve-last command. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="offset">Defines number of document to skip before including in search results.</param>
        /// <param name="docs">Defines number of documents to return.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object retrieveLast(int offset = -1, int docs = -1, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_RetrieveLastRequest request = new CPS_RetrieveLastRequest(offset, docs);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_LookupResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Performs retrieve-first command. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="offset">Defines number of document to skip before including in search results.</param>
        /// <param name="docs">Defines number of documents to return.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object retrieveFirst(int offset = -1, int docs = -1, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_RetrieveFirstRequest request = new CPS_RetrieveFirstRequest(offset, docs);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_LookupResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Retrieves single document from Clusterpoint Server storage. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns CPS_SimpleXML.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns XmlElement.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Document as CPS_SimpleXml or XmlElement.</returns>
        public Object retrieveSingle(string id, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_RetrieveRequest request = new CPS_RetrieveRequest(id);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            CPS_LookupResponse response = (CPS_LookupResponse)this.p_lastResponse;
            if (response.getDocuments() == null)
                return null;
            if (returnType == CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
            {
                Dictionary<string, XmlElement> ret1 = (Dictionary<string, XmlElement>)(response.getDocuments(returnType));
                foreach (KeyValuePair<string, XmlElement> pair in ret1)
                    return pair.Value;
            }
            if (returnType == CPS_Response.DOC_TYPE.DOC_TYPE_ARRAY)
            {
                return null;
            }
            if (returnType == CPS_Response.DOC_TYPE.DOC_TYPE_SIMPLEXML)
            {
                Dictionary<string, CPS_SimpleXML> ret3 = (Dictionary<string, CPS_SimpleXML>)(response.getDocuments(returnType));
                foreach (KeyValuePair<string, CPS_SimpleXML> pair in ret3)
                    return pair.Value;
            }
            return null;
        }

        /// <summary>
        /// Retrieves multiple documents from Clusterpoint Server storage. returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object retrieveMultiple(List<string> ids, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_RetrieveRequest request = new CPS_RetrieveRequest(ids);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_LookupResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Lookups single document in Clusterpoint Server storage. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns CPS_SimpleXML.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns XmlElement.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="list">Dictionary with keys as xpaths and values as listing options (yes | no | snippet | highlight).</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Document as CPS_SimpleXml or XmlElement.</returns>
        public Object lookupSingle(string id, Dictionary<string, string> list = null, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_LookupRequest request = new CPS_LookupRequest(id, list);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            CPS_LookupResponse response = (CPS_LookupResponse)this.p_lastResponse;
            if (response.getDocuments() == null)
                return null;
            if (returnType == CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
            {
                Dictionary<string, XmlElement> ret1 = (Dictionary<string, XmlElement>)(response.getDocuments(returnType));
                foreach (KeyValuePair<string, XmlElement> pair in ret1)
                    return pair.Value;
            }
            if (returnType == CPS_Response.DOC_TYPE.DOC_TYPE_ARRAY)
            {
                return null;
            }
            if (returnType == CPS_Response.DOC_TYPE.DOC_TYPE_SIMPLEXML)
            {
                Dictionary<string, CPS_SimpleXML> ret3 = (Dictionary<string, CPS_SimpleXML>)(response.getDocuments(returnType));
                foreach (KeyValuePair<string, CPS_SimpleXML> pair in ret3)
                    return pair.Value;
            }
            return null;
        }

        /// <summary>
        /// Lookups multiple documents in Clusterpoint Server storage. returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <param name="list">Dictionary with keys as xpaths and values as listing options (yes | no | snippet | highlight).</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object lookupMultiple(List<string> ids, Dictionary<string, string> list = null, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_LookupRequest request = new CPS_LookupRequest(ids, list);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_LookupResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Performs list-words command by a given wildcard.
        /// </summary>
        /// <param name="wildcard">Words wildcard.</param>
        /// <returns>Dictionary of listed words.</returns>
        public Dictionary<string, Dictionary<string, CPS_ListWordsResponse.WordInfo>> listWords(string wildcard)
        {
            CPS_ListWordsRequest request = new CPS_ListWordsRequest(wildcard);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ListWordsResponse)this.p_lastResponse).getWords();
        }

        /// <summary>
        /// Performs alternatives command for given query. Returns most probable query (could be identical to given one).
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="cr">Parameter 'cr'. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.</param>
        /// <param name="idif">Parameter 'idif'. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.</param>
        /// <param name="h">Parameter 'cr'. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.</param>
        /// <returns>Alternative query.</returns>
        public string alternatives(string query, float cr = -1, float idif = -1, float h = -1)
        {
            CPS_AlternativesRequest request = new CPS_AlternativesRequest(query, cr, idif, h);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            CPS_AlternativesResponse response = (CPS_AlternativesResponse)this.p_lastResponse;
            Dictionary<string, Dictionary<string, CPS_AlternativesResponse.WordInfo>> words = response.getWords();
            string res = "";
            string xp_accum = "";
            string prevxpath = null;
            foreach (KeyValuePair<string, Dictionary<string, CPS_AlternativesResponse.WordInfo>> pair in words)
            {
                string original = pair.Key;
                string xpath = "";
                int pos = 0;
                if ((pos = original.IndexOf('/')) >= 0)
                {
                    xpath = original.Substring(0, pos);
                    original = original.Substring(pos + 1);
                }
                if (prevxpath != null && xpath != prevxpath)
                {
                    res += Utils.CPS_Term(xp_accum, prevxpath);
                    xp_accum = "";
                }
                prevxpath = xpath;
                if (pair.Value.Count > 0)
                {
                    foreach (KeyValuePair<string, CPS_AlternativesResponse.WordInfo> apair in pair.Value)
                    {
                        xp_accum += (xp_accum == "" ? "" : " ") + apair.Key;
                        break;
                    }
                }
                else
                {
                    xp_accum += (xp_accum == "" ? "" : " ") + original;
                }
            }
            if (xp_accum.Length > 0)
                res += (res == "" ? "" : " ") + Utils.CPS_Term(xp_accum, prevxpath == null ? "" : prevxpath);
            return res;
        }

        /// <summary>
        /// Performs status command.
        /// </summary>
        /// <returns>Status information.</returns>
        public CPS_SimpleXML status()
        {
            CPS_StatusRequest request = new CPS_StatusRequest();
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_StatusResponse)this.p_lastResponse).getStatus();
        }

        /// <summary>
        /// Performs search-delete command.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <returns>Number of documents deleted.</returns>
        public int searchDelete(string query)
        {
            CPS_SearchDeleteRequest request = new CPS_SearchDeleteRequest(query);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_SearchDeleteResponse)this.p_lastResponse).getHits();
        }

        /// <summary>
        /// Performs list-paths command.
        /// </summary>
        /// <returns>List of available paths.</returns>
        public List<string> listPaths()
        {
            CPS_ListPathsRequest request = new CPS_ListPathsRequest();
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ListPathsResponse)this.p_lastResponse).getPaths();
        }

        /// <summary>
        /// Performs list-facets command for single facet path.
        /// </summary>
        /// <param name="path">Facet path.</param>
        /// <returns>List of available facets.</returns>
        public Dictionary<string, List<string>> listFacets(string path)
        {
            CPS_ListFacetsRequest request = new CPS_ListFacetsRequest(path);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ListFacetsResponse)this.p_lastResponse).getFacets();
        }

        /// <summary>
        /// Performs list-facets command for multiple facet paths.
        /// </summary>
        /// <param name="paths">Facet paths.</param>
        /// <returns>List of available facets.</returns>
        public Dictionary<string, List<string>> listFacets(List<string> paths)
        {
            CPS_ListFacetsRequest request = new CPS_ListFacetsRequest(paths);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_ListFacetsResponse)this.p_lastResponse).getFacets();
        }

        /// <summary>
        /// Searches for similar documents by given document ID. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="docid">Document ID to search similar documents to.</param>
        /// <param name="len">Number of keywords to extract.</param>
        /// <param name="quota">Minimum number of keywords to match.</param>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <param name="query">Additional filtering query.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object similarDocument(string docid, int len, int quota, int offset = -1, int docs = -1, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS, string query = "")
        {
            CPS_SimilarDocumentRequest request = new CPS_SimilarDocumentRequest(docid, len, quota, offset, docs, query);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_SearchResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Searches for similar documents by given text. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="text">Text to search similar documents to.</param>
        /// <param name="len">Number of keywords to extract.</param>
        /// <param name="quota">Minimum number of keywords to match.</param>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <param name="query">Additional filtering query.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object similarText(string text, int len, int quota, int offset = -1, int docs = -1, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS, string query = "")
        {
            CPS_SimilarTextRequest request = new CPS_SimilarTextRequest(text, len, quota, offset, docs, query);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_SearchResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Retrieves modification history of the document by revision ID. Returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="returnDocs">Set to true to return document content (subject to license conditions).</param>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Revisions as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object showHistory(string id, bool returnDocs = false, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            CPS_ShowHistoryRequest request = new CPS_ShowHistoryRequest(id, returnDocs);
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return ((CPS_LookupResponse)this.p_lastResponse).getDocuments(returnType);
        }

        /// <summary>
        /// Begins new transaction.
        /// </summary>
        /// <returns>Always true, in case of error throws exception.</returns>
        public bool beginTransaction()
        {
            CPS_BeginTransactionRequest request = new CPS_BeginTransactionRequest();
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return true;
        }

        /// <summary>
        /// Commits last transaction.
        /// </summary>
        /// <returns>Always true, in case of error throws exception.</returns>
        public bool commitTransaction()
        {
            CPS_CommitTransactionRequest request = new CPS_CommitTransactionRequest();
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return true;
        }

        /// <summary>
        /// Rollbacks last transaction.
        /// </summary>
        /// <returns>Always true, in case of error throws exception.</returns>
        public bool rollbackTransaction()
        {
            CPS_RollbackTransactionRequest request = new CPS_RollbackTransactionRequest();
            this.p_lastResponse = this.p_connection.sendRequest(request);
            return true;
        }

        /// <summary>
        /// Gets last received response. This could be useful if you need extra information from the response object.
        /// <seealso cref="CPS_Response"/>
        /// </summary>
        /// <returns>Last received response.</returns>
        public CPS_Response getLastResponse()
        {
            return this.p_lastResponse;
        }

        /// <summary>
        /// Clusterpoint Server connection.
        /// </summary>
        private CPS_Connection p_connection;
        /// <summary>
        /// Last received response.
        /// </summary>
        private CPS_Response p_lastResponse;
    }
}
