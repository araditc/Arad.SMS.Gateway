using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CPS
{
    /// <summary>
    /// Various utility functions, that can be usefull to user. All functions are static, so no need to create class.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Encodes HTML/XML most important chars.
        /// </summary>
        /// <param name="input">String to encode.</param>
        /// <returns>Encoded string.</returns>
        public static string HtmlSpecialChars(string input)
        {
            input = input.Replace("&", "&amp;");
            input = input.Replace("<", "&lt;");
            input = input.Replace(">", "&gt;");
            return input;
        }

        /// <summary>
        /// Escapes &lt;, &gt;, and &amp; characters in given term for inclusion into XML (like search query). Also wraps the term in XML tags if xpath is specified.
        /// Note that this function does not escape @, $, " and other symbols, that are meaningfull in a search query. In you want to escape input that comes directly from user and that isn't supposed to contain any search operators at all, it's probably better to use <see cref="Utils.CPS_QueryTerm"/> function.
        /// </summary>
        /// <param name="term">Term to be escaped.</param>
        /// <param name="xpath">Optional term xpath.</param>
        /// <param name="escape">Whether to escape term's XML.</param>
        /// <returns>Escaped and wrapped term.</returns>
        public static string CPS_Term(string term, string xpath = "", bool escape = true)
        {
            string prefix = "", postfix = "";

            if (xpath.Length > 0)
            {
                string[] xpathx = xpath.Split('/');
                foreach (string tag in xpathx)
                {
                    if (tag.Length > 0)
                    {
                        prefix = prefix + "<" + tag + ">";
                        postfix = "</" + tag + ">" + postfix;
                    }
                }
            }

            return prefix + (escape ? HtmlSpecialChars(term) : term) + postfix;
        }

        /// <summary>
        /// Escapes &lt;, &gt;, &amp; characters as well as @"{}()=$~+[]* (search query operators) in the given term for inclusion into search query. Also wraps term in XML tags if xpath is specified.
        /// </summary>
        /// <param name="term">Term to be escaped.</param>
        /// <param name="xpath">Optional term xpath.</param>
        /// <param name="allowed_symbols">String of operator symbols user is allowed to use.</param>
        /// <returns>Escaped and wrapped term.</returns>
        public static string CPS_QueryTerm(string term, string xpath = "", string allowed_symbols = "")
        {
            string newTerm = "";
            int x = 0;

            for (x = 0; x < term.Length; x++)
            {
                switch (term[x])
                {
                    case '@':
                    case '$':
                    case '"':
                    case '=':
                    case '<':
                    case '>':
                    case '(':
                    case ')':
                    case '{':
                    case '}':
                    case '~':
                    case '+':
                    case '*':
                    case '[':
                    case ']':
                        if (!Array.Exists<char>(allowed_symbols.ToCharArray(), al => al == term[x]))
                            newTerm += "\\";
                        break;
                }

                newTerm += term[x];
            }

            return CPS_Term(newTerm, xpath);
        }

        /// <summary>
        /// Converts given query dictionary to query string.
        /// </summary>
        /// <param name="dict">Query in Dictionary&lt;string, Object&gt; form, where dictionary key is xpath and value is string or Dictionary&lt;string, Object&gt;.</param>
        /// <returns>Query as string.</returns>
        public static string CPS_QueryDictionary(Dictionary<string, Object> dict)
        {
            string ret = "";
            foreach (KeyValuePair<string, Object> pair in dict)
            {
                try
                {
                    Dictionary<string, Object> subdict = (Dictionary<string, Object>)pair.Value;
                    ret += CPS_Term(CPS_QueryDictionary(subdict), pair.Key, false);
                    continue;
                }
                catch (Exception)
                {
                }

                try
                {
                    string value = (string)pair.Value;
                    ret += CPS_Term(value, pair.Key);
                }
                catch (Exception)
                {
                }
            }
            return ret;
        }

        /// <summary>
        /// Generates ordering string for sorting by relevance for <see cref="CPS_SearchRequest.setOrdering(string)"/>.
        /// </summary>
        /// <param name="ascdesc">Specifies 'ascending' or 'descending' order.</param>
        /// <returns>Ordering string.</returns>
        public static string CPS_RelevanceOrdering(string ascdesc = "")
        {
            return "<relevance>" + HtmlSpecialChars(ascdesc) + "</relevance>";
        }

        /// <summary>
        /// Generates ordering string for numeric ordering for <see cref="CPS_SearchRequest.setOrdering(string)"/>.
        /// </summary>
        /// <param name="tag">Numeric's tag xpath.</param>
        /// <param name="ascdesc">Specifies 'ascending' or 'descending' order.</param>
        /// <returns>Ordering string.</returns>
        public static string CPS_NumericOrdering(string tag, string ascdesc = "ascending")
        {
            return "<numeric>" + CPS_Term(ascdesc, tag) + "</numeric>";
        }

        /// <summary>
        /// Generates ordering string for date ordering for <see cref="CPS_SearchRequest.setOrdering(string)"/>.
        /// </summary>
        /// <param name="tag">Date's tag xpath.</param>
        /// <param name="ascdesc">Specifies 'ascending' or 'descending' order.</param>
        /// <returns>Ordering string.</returns>
        public static string CPS_DateOrdering(string tag, string ascdesc = "ascending")
        {
            return "<date>" + CPS_Term(ascdesc, tag) + "</date>";
        }

        /// <summary>
        /// Generates ordering string for string ordering for <see cref="CPS_SearchRequest.setOrdering(string)"/>.
        /// </summary>
        /// <param name="tag">Tag's xpath by which to perform ordering.</param>
        /// <param name="lang">Language (collation) used for ordering, for example 'en'.</param>
        /// <param name="ascdesc">Specifies 'ascending' or 'descending' order.</param>
        /// <returns>Ordering string.</returns>
        public static string CPS_StringOrdering(string tag, string lang, string ascdesc = "ascending")
        {
            return "<string>" + CPS_Term(ascdesc + "," + lang, tag) + "</string>";
        }

        /// <summary>
        /// Generates ordering string for generic distance ordering for <see cref="CPS_SearchRequest.setOrdering(string)"/>.
        /// </summary>
        /// <param name="type">Ordering type.</param>
        /// <param name="array">Dictionary of xpaths and coordinates.</param>
        /// <param name="ascdesc">Specifies 'ascending' or 'descending' order.</param>
        /// <returns>Ordering string.</returns>
        private static string CPS_GenericDistanceOrdering(string type, Dictionary<string, string> array, string ascdesc)
        {
            string res = "<distance type=\"" + HtmlSpecialChars(type) + "\" order=\"" + HtmlSpecialChars(ascdesc) + "\">";
            foreach (KeyValuePair<string, string> pair in array)
                res += CPS_Term(pair.Value, pair.Key);
            res += "</distance>";
            return res;
        }

        /// <summary>
        /// Generates ordering string for latitude/longitude (GPS) distance ordering for <see cref="CPS_SearchRequest.setOrdering(string)"/>.
        /// </summary>
        /// <param name="array">Dictionary with xpaths and coordinates for center point. Dictionary must contain exactly two elements, latitude first and longitude second.</param>
        /// <param name="ascdesc">Specifies 'ascending' or 'descending' order.</param>
        /// <returns>Ordering string.</returns>
        public static string CPS_LatLongDistanceOrdering(Dictionary<string, string> array, string ascdesc = "ascending")
        {
            return CPS_GenericDistanceOrdering("latlong", array, ascdesc);
        }

        /// <summary>
        /// Generates ordering string for plain distance ordering for <see cref="CPS_SearchRequest.setOrdering(string)"/>.
        /// </summary>
        /// <param name="array">Dictionary for xpaths and coordinates for center point.</param>
        /// <param name="ascdesc">Specifies 'ascending' or 'descending' order.</param>
        /// <returns>Ordering string.</returns>
        public static string CPS_PlaneDistanceOrdering(Dictionary<string, string> array, string ascdesc = "ascending")
        {
            return CPS_GenericDistanceOrdering("plane", array, ascdesc);
        }

        /// <summary>
        /// Converts string containing Windows float to string containing Unix float - fixes decimal seperator.
        /// </summary>
        /// <param name="value">String to convert.</param>
        /// <returns>Converted string.</returns>
        public static string CPS_FloatString(string value)
        {
            return value.Replace(',', '.');
        }
    }

    /// <summary>
    /// The CPS_PRX_Operation class represents a particular operation for the partial-xreplace command.
    /// An operation consists of the xpath that identifies which nodes the operation should be performed on,
    /// the type of operation and new content that should be used in the operation.
    /// <seealso cref="CPS_PartialXRequest"/>
    /// </summary>
    public class CPS_PRX_Operation
    {
        /// <summary>
        /// Constructs an instance of the CPS_PRX_Operation class.
        /// </summary>
        /// <param name="xpath">XPath 1.0 expression denoting which nodes the operation should be performed on.</param>
        /// <param name="operation">Type of the operation e.g. "append_children" or "remove".</param>
        /// <param name="document">Content of the operation - required for anything except the remove operation. Can be specified using <see cref="CPS_SimpleXML"/>, <see cref="XmlElement"/> or <see cref="string"/>.</param>
        public CPS_PRX_Operation(string xpath, string operation, Object document = null)
        {
            this.p_xpath = xpath;
            this.p_operation = operation;
            this.p_document = document;
        }

        /// <summary>
        /// Renders the resulting XML. Used for internal purposes and should not be used from API.
        /// </summary>
        /// <returns>Resulting XML as string.</returns>
        public string renderXML()
        {
            string ret = "<cps:xpath_match>" + Utils.HtmlSpecialChars(this.p_xpath) + "</cps:xpath_match>";
            ret += "<cps:" + Utils.HtmlSpecialChars(this.p_operation) + ">";
            if (this.p_operation != null)
            {
                bool done = false;

                if (!done)
                    try
                    {
                        string document_type1 = (string)this.p_document;
                        if (document_type1 != null)
                        {
                            ret += document_type1;
                            done = true;
                        }
                    }
                    catch (Exception)
                    {
                    }

                if (!done)
                    try
                    {
                        XmlElement document_type2 = (XmlElement)this.p_document;
                        if (document_type2 != null)
                        {
                            ret += document_type2.OuterXml;
                            done = true;
                        }
                    }
                    catch (Exception)
                    {
                    }

                if (!done)
                    try
                    {
                        CPS_SimpleXML document_type3 = (CPS_SimpleXML)this.p_document;
                        if (document_type3 != null)
                        {
                            ret += document_type3.GetString();
                            done = true;
                        }
                    }
                    catch (Exception)
                    {
                    }

                if (!done)
                    throw new CPS_Exception("Invalid passed document type");
            }
            ret += "</cps:" + Utils.HtmlSpecialChars(this.p_operation) + ">";

            return ret;
        }

        /// <summary>
        /// Operation XPath.
        /// </summary>
        private string p_xpath;
        /// <summary>
        /// Operation name.
        /// </summary>
        private string p_operation;
        /// <summary>
        /// Operation content document as <see cref="CPS_SimpleXML"/>, <see cref="XmlElement"/> or <see cref="string"/>.
        /// </summary>
        private Object p_document;
    }

    /// <summary>
    /// The CPS_PRX_Changeset class represents a changeset for the partial-xreplace command.
    /// A changeset is one or more document IDs and one or more operations to be performed on documents with these IDs.
    /// <seealso cref="CPS_PartialXRequest"/>
    /// </summary>
    public class CPS_PRX_Changeset
    {
        /// <summary>
        /// Constructs an instance of the CPS_PRX_Changeset class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="operation">Operation to perform as <see cref="CPS_PRX_Operation"/>.</param>
        public CPS_PRX_Changeset(string id, CPS_PRX_Operation operation)
        {
            this.p_ids = new List<string>();
            this.p_ids.Add(id);
            this.p_operations = new List<CPS_PRX_Operation>();
            this.p_operations.Add(operation);
        }

        /// <summary>
        /// Constructs an instance of the CPS_PRX_Changeset class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <param name="operation">Operation to perform as <see cref="CPS_PRX_Operation"/>.</param>
        public CPS_PRX_Changeset(List<string> ids, CPS_PRX_Operation operation)
        {
            this.p_ids = ids;
            this.p_operations = new List<CPS_PRX_Operation>();
            this.p_operations.Add(operation);
        }

        /// <summary>
        /// Constructs an instance of the CPS_PRX_Changeset class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="operations">List of operations to perform as <see cref="CPS_PRX_Operation"/>.</param>
        public CPS_PRX_Changeset(string id, List<CPS_PRX_Operation> operations)
        {
            this.p_ids = new List<string>();
            this.p_ids.Add(id);
            this.p_operations = operations;
        }

        /// <summary>
        /// Constructs an instance of the CPS_PRX_Changeset class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <param name="operations">List of operations to perform as <see cref="CPS_PRX_Operation"/>.</param>
        public CPS_PRX_Changeset(List<string> ids, List<CPS_PRX_Operation> operations)
        {
            this.p_ids = ids;
            this.p_operations = operations;
        }

        /// <summary>
        /// Renders the resulting XML. Used for internal purposes and should not be used from API.
        /// </summary>
        /// <param name="docIdXpath">Document ID XPath.</param>
        /// <returns>Resulting XML as string.</returns>
        public string renderXML(List<string> docIdXpath)
        {
            string ret = "";
            string prefix = "", postfix = "";
            int i = 0;

            for (i = 0; i < docIdXpath.Count; i++)
            {
                prefix = prefix + "<" + Utils.HtmlSpecialChars(docIdXpath[i]) + ">";
                postfix = "</" + Utils.HtmlSpecialChars(docIdXpath[i]) + ">" + postfix;
            }

            for (i = 0; i < this.p_ids.Count; i++)
            {
                ret += prefix;
                ret += Utils.HtmlSpecialChars(this.p_ids[i]);
                ret += postfix;
            }

            bool isolate_changes = (this.p_operations.Count > 1);

            for (i = 0; i < this.p_operations.Count; i++)
            {
                if (isolate_changes)
                    ret += "<cps:change>";
                ret += this.p_operations[i].renderXML();
                if (isolate_changes)
                    ret += "</cps:change>";
            }

            return ret;
        }

        /// <summary>
        /// List of document IDs.
        /// </summary>
        private List<string> p_ids;
        /// <summary>
        /// List of operations.
        /// </summary>
        private List<CPS_PRX_Operation> p_operations;
    }

    /// <summary>
    /// Partial-XReplace command request class, wrapper for <see cref="CPS_ModifyRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_ModifyResponse"/>.
    /// <seealso cref="CPS_ModifyResponse"/>
    /// </summary>
    public class CPS_PartialXRequest : CPS_ModifyRequest
    {
        /// <summary>
        /// Constructs an instance of the CPS_PartialXRequest class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// <seealso cref="CPS_PRX_Changeset"/>
        /// </summary>
        /// <param name="changeset"><see cref="CPS_PRX_Changeset"/> to perform.</param>
        /// <param name="requestId">Specifies request ID.</param>
        public CPS_PartialXRequest(CPS_PRX_Changeset changeset, string requestId = "")
            : base("partial-xreplace", null, requestId)
        {
            this.p_changesets = new List<CPS_PRX_Changeset>();
            this.p_changesets.Add(changeset);
        }

        /// <summary>
        /// Constructs an instance of the CPS_PartialXRequest class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// <seealso cref="CPS_PRX_Changeset"/>
        /// </summary>
        /// <param name="changesets">List of <see cref="CPS_PRX_Changeset"/>s to perform.</param>
        /// <param name="requestId">Specifies request ID.</param>
        public CPS_PartialXRequest(List<CPS_PRX_Changeset> changesets, string requestId = "")
            : base("partial-xreplace", null, requestId)
        {
            this.p_changesets = changesets;
        }

        /// <summary>
        /// Constructs an instance of the CPS_PartialXRequest class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// <seealso cref="CPS_PRX_Changeset"/>
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="operation">Operation as <see cref="CPS_PRX_Operation"/> to perform.</param>
        /// <param name="requestId">Specifies request ID.</param>
        public CPS_PartialXRequest(string id, CPS_PRX_Operation operation, string requestId = "")
            : base("partial-xrequest", null, requestId)
        {
            this.p_changesets = new List<CPS_PRX_Changeset>();
            this.p_changesets.Add(new CPS_PRX_Changeset(id, operation));
        }

        /// <summary>
        /// Constructs an instance of the CPS_PartialXRequest class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// <seealso cref="CPS_PRX_Changeset"/>
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <param name="operation">Operation as <see cref="CPS_PRX_Operation"/> to perform.</param>
        /// <param name="requestId">Specifies request ID.</param>
        public CPS_PartialXRequest(List<string> ids, CPS_PRX_Operation operation, string requestId = "")
            : base("partial-xrequest", null, requestId)
        {
            this.p_changesets = new List<CPS_PRX_Changeset>();
            this.p_changesets.Add(new CPS_PRX_Changeset(ids, operation));
        }

        /// <summary>
        /// Constructs an instance of the CPS_PartialXRequest class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// <seealso cref="CPS_PRX_Changeset"/>
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="operations">List of operations as <see cref="CPS_PRX_Operation"/> to perform.</param>
        /// <param name="requestId">Specifies request ID.</param>
        public CPS_PartialXRequest(string id, List<CPS_PRX_Operation> operations, string requestId = "")
            : base("partial-xrequest", null, requestId)
        {
            this.p_changesets = new List<CPS_PRX_Changeset>();
            this.p_changesets.Add(new CPS_PRX_Changeset(id, operations));
        }

        /// <summary>
        /// Constructs an instance of the CPS_PartialXRequest class.
        /// <seealso cref="CPS_PRX_Operation"/>
        /// <seealso cref="CPS_PRX_Changeset"/>
        /// </summary>
        /// <param name="ids">List of document IDs.</param>
        /// <param name="operations">List of operations as <see cref="CPS_PRX_Operation"/> to perform.</param>
        /// <param name="requestId">Specifies request ID.</param>
        public CPS_PartialXRequest(List<string> ids, List<CPS_PRX_Operation> operations, string requestId = "")
            : base("partial-xrequest", null, requestId)
        {
            this.p_changesets = new List<CPS_PRX_Changeset>();
            this.p_changesets.Add(new CPS_PRX_Changeset(ids, operations));
        }
    }

    /// <summary>
    /// CPS_EmptyResponse is used in cases where no reply from the server is expected.
    /// </summary>
    public class CPS_EmptyResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_EmptyResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        public CPS_EmptyResponse(CPS_Connection connection, CPS_Request request)
            : base(connection, request)
        {
        }
    }

    /// <summary>
    /// Status command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_StatusResponse"/>.
    /// <seealso cref="CPS_StatusResponse"/>
    /// </summary>
    public class CPS_StatusRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_StatusRequest class instance.
        /// </summary>
        /// <param name="requestId">Specifies request ID.</param>
        public CPS_StatusRequest(string requestId = "")
            : base("status", requestId)
        {
        }
    }

    /// <summary>
    /// Status command response class, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request class is <see cref="CPS_StatusRequest"/>.
    /// <seealso cref="CPS_StatusRequest"/>
    /// </summary>
    public class CPS_StatusResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_StatusResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_StatusResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
        }

        /// <summary>
        /// Returns status response content as CPS_SimpleXML object.
        /// </summary>
        /// <returns>Status content.</returns>
        public CPS_SimpleXML getStatus()
        {
            return (CPS_SimpleXML)base.getContentArray(DOC_TYPE.DOC_TYPE_SIMPLEXML);
        }
    }

    /// <summary>
    /// Search command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_SearchResponse"/>.
    /// <seealso cref="CPS_SearchResponse"/>
    /// </summary>
    public class CPS_SearchRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_SearchRequest class instance.
        /// </summary>
        /// <param name="query">Search query string. See <see cref="CPS_SearchRequest.setQuery(string)"/> for more info.</param>
        /// <param name="offset">Defines number of document to skip before including in search results.</param>
        /// <param name="docs">Defines number of documents to return.</param>
        /// <param name="list">Dictionary with keys as xpaths and values as listing options (yes | no | snippet | highlight).</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_SearchRequest(string query, int offset = -1, int docs = -1, Dictionary<string, string> list = null, string requestId = "")
            : base("search", requestId)
        {
            this.setQuery(query);
            if (offset >= 0)
                this.setOffset(offset);
            if (docs >= 0)
                this.setDocs(docs);
            if (list != null)
                this.setList(list);
        }

        /// <summary>
        /// Sets search query. Use can use <see cref="Utils"/> class functions for generating query.
        /// <example>
        /// This example show how to use <see cref="Utils"/> function to generate search query.
        /// <code>
        /// r.setQuery("(" + Utils.CPS_Term("predefined_term", "generated_fields/some_field") + Utils.CPS_Term(user_supplied_term, "searchable_fields/text") + ")");
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="value">Query string. Please note, that all &lt;, &gt;, and &amp; characters, that are not supposed to be XML tags, must be escaped using <see cref="Utils.CPS_Term"/>, <see cref="Utils.CPS_QueryTerm"/> or <see cref="Utils.HtmlSpecialChars"/>.</param>
        public void setQuery(string value)
        {
            base.setParam("query", value);
        }

        /// <summary>
        /// Sets search query. Query should be in Dictionary&lt;string, Object&gt; form where Object is string or Dictionary&lt;string, Object&gt;.
        /// </summary>
        /// <param name="value">Query dictionary.</param>
        public void setQuery(Dictionary<string, Object> value)
        {
            base.setParam("query", Utils.CPS_QueryDictionary(value));
        }

        /// <summary>
        /// Sets returned document count.
        /// </summary>
        /// <param name="value">Document count.</param>
        public void setDocs(int value)
        {
            base.setParam("docs", value.ToString());
        }

        /// <summary>
        /// Sets returned document offset (documents to skip, before including them in response).
        /// </summary>
        /// <param name="value">Document offset.</param>
        public void setOffset(int value)
        {
            base.setParam("offset", value.ToString());
        }

        /// <summary>
        /// Sets single facet path.
        /// </summary>
        /// <param name="value">Facet path.</param>
        public void setFacet(string value)
        {
            base.setParam("facet", value);
        }

        /// <summary>
        /// Sets multiple facet paths.
        /// </summary>
        /// <param name="value">List of facet paths.</param>
        public void setFacet(List<string> value)
        {
            base.setParam("facet", value);
        }

        /// <summary>
        /// Sets stemming language.
        /// </summary>
        /// <param name="value">Stemming language code.</param>
        public void setStemLang(string value)
        {
            base.setParam("stem-lang", value);
        }

        /// <summary>
        /// Sets exatch matching.
        /// </summary>
        /// <param name="value">Exatch match rule.</param>
        public void setExactMatch(string value)
        {
            base.setParam("exact-match", value);
        }

        /// <summary>
        /// Sets grouping option.
        /// </summary>
        /// <param name="tagName">Tag name for grouping.</param>
        /// <param name="count">Maximum number of document to return per group.</param>
        public void setGroup(string tagName, int count)
        {
            base.setParam("group", tagName);
            base.setParam("group_size", count.ToString());
        }

        /// <summary>
        /// Sets list option.
        /// </summary>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        public void setList(Dictionary<string, string> list)
        {
            if (list == null)
                return;

            string listString = "";

            foreach (KeyValuePair<string, string> pair in list)
                listString += Utils.CPS_Term(pair.Value, pair.Key);

            base.setParam("list", listString);
        }

        /// <summary>
        /// Sets single ordering rule. You can use <see cref="Utils"/> class function to generate this rule.
        /// </summary>
        /// <param name="order">Ordering rule.</param>
        public void setOrdering(string order)
        {
            base.setParam("ordering", order);
        }

        /// <summary>
        /// Sets multiple ordering rules. You can use <see cref="Utils"/> class function to generate these rules.
        /// </summary>
        /// <param name="order">List of ordering rules.</param>
        public void setOrdering(List<string> order)
        {
            if (order == null)
                return;

            string orderingString = "";
            foreach (string ordering in order)
                orderingString += ordering;

            base.setParam("ordering", orderingString);
        }

        /// <summary>
        /// Defines aggregation queries for the search request.
        /// </summary>
        /// <param name="aggregate">Single aggregation query.</param>
        public void setAggregate(string aggregate)
        {
            base.setParam("aggregate", aggregate);
        }

        /// <summary>
        /// Defines aggregation queries for the search request.
        /// </summary>
        /// <param name="aggregate">List of aggregation queries.</param>
        public void setAggregate(List<string> aggregate)
        {
            base.setParam("aggregate", aggregate);
        }
    }

    /// <summary>
    /// The CPS_SQLSearchRequest class is virtually identical to the <see cref="CPS_SearchRequest"/> class, but is used for querying the database through the Basic SQL interface.
    /// Corresponding response class is <see cref="CPS_SearchResponse"/>.
    /// <seealso cref="CPS_SearchResponse"/>
    /// </summary>
    public class CPS_SQLSearchRequest : CPS_SearchRequest
    {
        /// <summary>
        /// Constructs an instance of the CPS_SQLSearchRequest class.
        /// </summary>
        /// <param name="sql_query">SQL query.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_SQLSearchRequest(string sql_query, string requestId = "")
            : base("", -1, -1, null, requestId)
        {
            this.setParam("sql", sql_query);
        }
    }

    /// <summary>
    /// Search command response class, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request clases are <see cref="CPS_SearchRequest"/>, <see cref="CPS_SimilarDocumentRequest"/> and <see cref="CPS_SimilarTextRequest"/>.
    /// <seealso cref="CPS_SearchRequest"/>
    /// <seealso cref="CPS_SimilarDocumentRequest"/>
    /// <seealso cref="CPS_SimilarTextRequest"/>.
    /// </summary>
    public class CPS_SearchResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_SearchResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_SearchResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
        }

        /// <summary>
        /// Gets documents from response, returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="type">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object getDocuments(CPS_Response.DOC_TYPE type = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            return base.getRawDocuments(type);
        }

        /// <summary>
        /// Gets returned facets.
        /// </summary>
        /// <returns>Dictionary of facets.</returns>
        public Dictionary<string, Dictionary<string, int>> getFacets()
        {
            return base.getRawFacets();
        }

        /// <summary>
        /// Returns aggregated data from the response, returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, List&lt;CPS_SimpleXML&gt;&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, List&gt;XmlElement&gt;&gt;.
        /// </summary>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Aggregate data as Dictionary&lt;string, List&lt;CPS_SimpleXML&gt;&gt; or Dictionary&lt;string, List&lt;XmlElement&gt;&gt;.</returns>
        public Object getAggregate(CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            return base.getRawAggregate(returnType);
        }

        /// <summary>
        /// Gets returned document count.
        /// </summary>
        /// <returns>Document count.</returns>
        public int getFound()
        {
            return base.getParamInt("found");
        }

        /// <summary>
        /// Gets total document count, that mathed search query.
        /// </summary>
        /// <returns>Document count.</returns>
        public int getHits()
        {
            return base.getParamInt("hits");
        }

        /// <summary>
        /// Gets returned documents starting offset (position).
        /// </summary>
        /// <returns>Starting offset.</returns>
        public int getFrom()
        {
            return base.getParamInt("from");
        }

        /// <summary>
        /// Gets returned documents ending offset (position).
        /// </summary>
        /// <returns>Ending offset.</returns>
        public int getTo()
        {
            return base.getParamInt("to");
        }

        /*/// <summary>
        /// Creates cursor to iterate over large resultset if it was asked during request.
        /// </summary>
        /// <param name="returnType">Defines used document return type.</param>
        /// <returns>CPS_Cursor class object.</returns>
        public CPS_Cursor getCursor(CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_SIMPLEXML)
        {
            return new CPS_Cursor(this.p_connection, this.getParam("cursor_id"), this.getParam("cursor_data"), this.p_documents, returnType);
        }*/
    }

    /*/// <summary>
    /// Class that allows iterating over large resultset.
    /// </summary>
    public class CPS_Cursor
    {
        /// <summary>
        /// Creates new CPS_Cursor class instance. Should not be used directly, instead use <see cref="CPS_SearchResponse.getCursor"/> method.
        /// </summary>
        /// <param name="connection">CPS_Connection object.</param>
        /// <param name="cursor_id">Cursor ID.</param>
        /// <param name="cursor_data">Cursor data.</param>
        /// <param name="data">Raw documents.</param>
        /// <param name="returnType">Document return type.</param>
        public CPS_Cursor(CPS_Connection connection, string cursor_id, string cursor_data, Object data, CPS_Response.DOC_TYPE returnType = CPS_Response.DOC_TYPE.DOC_TYPE_SIMPLEXML)
        {
            this.p_connection = connection;
            this.p_cursor_id = cursor_id;
            this.p_cursor_data = cursor_data;
            this.p_data = data;
        }

        private CPS_Connection p_connection;
        private string p_cursor_id;
        private string p_cursor_data;
        private Object p_data;
    }*/

    /// <summary>
    /// The CPS_ShowHistoryRequest class is a wrapper for the <see cref="CPS_Request"/> class for the show-history command.
    /// Corresponding response class is <see cref="CPS_LookupResponse"/>.
    /// <seealso cref="CPS_LookupResponse"/>
    /// </summary>
    public class CPS_ShowHistoryRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_ShowHistoryRequest class instance.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="returnDocs">Set this to true if you want historical document content returned as well.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ShowHistoryRequest(string id, bool returnDocs = false, string requestId = "")
            : base("show-history", requestId)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            base.setRawDocuments(ids);
            if (returnDocs)
                base.setParam("return_doc", "yes");
        }
    }

    /// <summary>
    /// Generic modify command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_ModifyResponse"/>.
    /// <seealso cref="CPS_ModifyResponse"/>
    /// </summary>
    public class CPS_ModifyRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_ModifyRequest class instance.
        /// </summary>
        /// <param name="command">Modify command name.</param>
        /// <param name="documents">Documents, to modify. Can be of type Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ModifyRequest(string command, Object documents, string requestId = "")
            : base(command, requestId)
        {
            if (documents != null)
                base.setRawDocuments(documents);
        }

        /// <summary>
        /// Creates new CPS_ModifyRequest class instance.
        /// </summary>
        /// <param name="command">Modify command name.</param>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document to modify. Can be of type CPS_SimpleXML or XmlElement.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ModifyRequest(string command, string id, Object document, string requestId = "")
            : base(command, requestId)
        {
            Dictionary<string, Object> documents = new Dictionary<string, Object>();
            documents[id] = document;
            base.setRawDocuments(documents);
        }
    }

    /// <summary>
    /// Insert command request class, wrapper for <see cref="CPS_ModifyRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_ModifyResponse"/>.
    /// <seealso cref="CPS_ModifyResponse"/>
    /// </summary>
    public class CPS_InsertRequest : CPS_ModifyRequest
    {
        /// <summary>
        /// Creates new CPS_InsertRequest class instance.
        /// </summary>
        /// <param name="documents">Documents, to modify. Can be of type Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_InsertRequest(Object documents, string requestId = "")
            : base("insert", documents, requestId)
        {
        }

        /// <summary>
        /// Creates new CPS_InsertRequest class instance.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document to modify. Can be of type CPS_SimpleXML or XmlElement.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_InsertRequest(string id, Object document, string requestId = "")
            : base("insert", id, document, requestId)
        {
        }
    }

    /// <summary>
    /// Update command request class, wrapper for <see cref="CPS_ModifyRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_ModifyResponse"/>.
    /// <seealso cref="CPS_ModifyResponse"/>
    /// </summary>
    public class CPS_UpdateRequest : CPS_ModifyRequest
    {
        /// <summary>
        /// Creates new CPS_UpdateRequest class instance.
        /// </summary>
        /// <param name="documents">Documents, to modify. Can be of type Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_UpdateRequest(Object documents, string requestId = "")
            : base("update", documents, requestId)
        {
        }

        /// <summary>
        /// Creates new CPS_Update request class instance.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document to modify. Can be of type CPS_SimpleXML or XmlElement.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_UpdateRequest(string id, Object document, string requestId = "")
            : base("update", id, document, requestId)
        {
        }
    }

    /// <summary>
    /// Replace command request class, wrapped for <see cref="CPS_ModifyRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_ModifyResponse"/>.
    /// <seealso cref="CPS_ModifyResponse"/>
    /// </summary>
    public class CPS_ReplaceRequest : CPS_ModifyRequest
    {
        /// <summary>
        /// Creates new CPS_ReplaceRequest class instance.
        /// </summary>
        /// <param name="documents">Documents, to modify. Can be of type Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ReplaceRequest(Object documents, string requestId = "")
            : base("replace", documents, requestId)
        {
        }

        /// <summary>
        /// Creates new CPS_ReplaceRequest class instance.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document to modify. Can be of type CPS_SimpleXML or XmlElement.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ReplaceRequest(string id, Object document, string requestId = "")
            : base("replace", id, document, requestId)
        {
        }
    }

    /// <summary>
    /// Partial-Replace command request class, wrapper for <see cref="CPS_ModifyRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_ModifyResponse"/>.
    /// <seealso cref="CPS_ModifyResponse"/>
    /// </summary>
    public class CPS_PartialReplaceRequest : CPS_ModifyRequest
    {
        /// <summary>
        /// Creates new CPS_PartialReplaceRequest class instance.
        /// </summary>
        /// <param name="documents">Documents, to modify. Can be of type Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_PartialReplaceRequest(Object documents, string requestId = "")
            : base("partial-replace", documents, requestId)
        {
        }

        /// <summary>
        /// Creates new CPS_PartialReplaceRequest class instance.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="document">Document to modify. Can be of type CPS_SimpleXML or XmlElement.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_PartialReplaceRequest(string id, Object document, string requestId = "")
            : base("partial-replace", id, document, requestId)
        {
        }
    }

    /// <summary>
    /// Delete command request class, wrapper for <see cref="CPS_ModifyRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_ModifyResponse"/>.
    /// <seealso cref="CPS_ModifyResponse"/>
    /// </summary>
    public class CPS_DeleteRequest : CPS_ModifyRequest
    {
        /// <summary>
        /// Creates new CPS_DeleteRequest class instance.
        /// </summary>
        /// <param name="id">Document ID to delete.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_DeleteRequest(string id, string requestId = "")
            : base("delete", null, requestId)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            base.setRawDocuments(ids);
        }

        /// <summary>
        /// Creates new CPS_DeleteRequest class instance.
        /// </summary>
        /// <param name="id">List of document IDs to delete.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_DeleteRequest(List<string> id, string requestId = "")
            : base("delete", null, requestId)
        {
            base.setRawDocuments(id);
        }
    }

    /// <summary>
    /// Generic modify command response, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request classes are <see cref="CPS_UpdateRequest"/>, <see cref="CPS_DeleteRequest"/>, <see cref="CPS_ReplaceRequest"/>, <see cref="CPS_PartialReplaceRequest"/> and <see cref="CPS_InsertRequest"/>.
    /// <seealso cref="CPS_UpdateRequest"/>
    /// <seealso cref="CPS_DeleteRequest"/>
    /// <seealso cref="CPS_ReplaceRequest"/>
    /// <seealso cref="CPS_PartialReplaceRequest"/>
    /// <seealso cref="CPS_InsertRequest"/>.
    /// </summary>
    public class CPS_ModifyResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_ModifyResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_ModifyResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
        }

        /// <summary>
        /// Gets modified document IDs.
        /// </summary>
        /// <returns>List od modified document IDs.</returns>
        public List<string> getModifiedIds()
        {
            List<string> ret = new List<string>();
            Dictionary<string, XmlElement> docs = (Dictionary<string, XmlElement>)(base.getRawDocuments(DOC_TYPE.DOC_TYPE_STDCLASS));
            foreach (KeyValuePair<string, XmlElement> pair in docs)
                ret.Add(pair.Key);
            return ret;
        }
    }

    /// <summary>
    /// List-Words command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_ListWordsResponse"/>.
    /// <seealso cref="CPS_ListWordsResponse"/>
    /// </summary>
    public class CPS_ListWordsRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_ListWordsRequest class instance.
        /// </summary>
        /// <param name="query">List-Words query.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ListWordsRequest(string query, string requestId = "")
            : base("list-words", requestId)
        {
            this.setQuery(query);
        }

        /// <summary>
        /// Sets list-words query.
        /// </summary>
        /// <param name="value">List-Words query.</param>
        public void setQuery(string value)
        {
            base.setParam("query", value);
        }
    }

    /// <summary>
    /// List-Words command response class, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request class if <see cref="CPS_ListWordsRequest"/>.
    /// <seealso cref="CPS_ListWordsRequest"/>
    /// </summary>
    public class CPS_ListWordsResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_ListWordsResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_ListWordsResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
        }

        /// <summary>
        /// Gets listed word information.
        /// </summary>
        /// <returns>Dictionary of word information.</returns>
        public Dictionary<string, Dictionary<string, CPS_ListWordsResponse.WordInfo>> getWords()
        {
            return (Dictionary<string, Dictionary<string, CPS_ListWordsResponse.WordInfo>>)(base.getRawWords());
        }

        /// <summary>
        /// Word information structure.
        /// </summary>
        public struct WordInfo
        {
            /// <summary>
            /// Word.
            /// </summary>
            public string word;
            /// <summary>
            /// Number of times, word appears in storage.
            /// </summary>
            public int count;
        }
    }

    /// <summary>
    /// Alternatives command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_AlternativesResponse"/>.
    /// <seealso cref="CPS_AlternativesResponse"/>
    /// <seealso href="http://www.clusterpoint.com/wiki/XML_Alternatives">Alternatives command description in Clusterpoint Wiki</seealso>.
    /// </summary>
    public class CPS_AlternativesRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_AlternativesRequest class instance.
        /// </summary>
        /// <param name="query">Alternatives query.</param>
        /// <param name="cr">Parameter 'cr'. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.</param>
        /// <param name="idif">Parameter 'idif'. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.</param>
        /// <param name="h">Parameter 'cr'. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_AlternativesRequest(string query, float cr = -1, float idif = -1, float h = -1, string requestId = "")
            : base("alternatives", requestId)
        {
            this.setQuery(query);
            this.setCr(cr);
            this.setIdif(idif);
            this.setH(h);
        }

        /// <summary>
        /// Sets alternatives query.
        /// </summary>
        /// <param name="value">Alternatives query.</param>
        public void setQuery(string value)
        {
            base.setParam("query", value);
        }

        /// <summary>
        /// Sets 'cr' parameter. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.
        /// </summary>
        /// <param name="value">Parameter value.</param>
        public void setCr(float value)
        {
            if (value > 0)
                base.setParam("cr", Utils.CPS_FloatString(value.ToString()));
        }

        /// <summary>
        /// Sets 'idif' parameter. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.
        /// </summary>
        /// <param name="value">Parameter value.</param>
        public void setIdif(float value)
        {
            if (value > 0)
                base.setParam("idif", Utils.CPS_FloatString(value.ToString()));
        }

        /// <summary>
        /// Sets 'h' parameter. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.
        /// </summary>
        /// <param name="value">Parameter value.</param>
        public void setH(float value)
        {
            if (value > 0)
                base.setParam("h", Utils.CPS_FloatString(value.ToString()));
        }
    }

    /// <summary>
    /// Alternatives command response class, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request class is <see cref="CPS_AlternativesRequest"/>.
    /// <seealso cref="CPS_AlternativesRequest"/>
    /// </summary>
    public class CPS_AlternativesResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_AlternativesResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_AlternativesResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
            this.p_localWords = null;
        }

        /// <summary>
        /// Gets alternatives word information.
        /// </summary>
        /// <returns>Dictionary of word information.</returns>
        public Dictionary<string, Dictionary<string, CPS_AlternativesResponse.WordInfo>> getWords()
        {
            return (Dictionary<string, Dictionary<string, CPS_AlternativesResponse.WordInfo>>)(base.getRawWords());
        }

        /// <summary>
        /// Gets given word counts.
        /// </summary>
        /// <param name="word">Word to get count for.</param>
        /// <returns>Word count.</returns>
        public int getWordCount(string word)
        {
            if (this.p_localWords == null)
                this.p_localWords = base.getRawWordCounts();

            try
            {
                return this.p_localWords[word];
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Alternatives word information.
        /// </summary>
        public struct WordInfo
        {
            /// <summary>
            /// Word.
            /// </summary>
            public string word;
            /// <summary>
            /// Number of times, word appears in storage.
            /// </summary>
            public int count;
            /// <summary>
            /// 'h' value. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.
            /// </summary>
            public float h;
            /// <summary>
            /// 'idif' value. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.
            /// </summary>
            public float idif;
            /// <summary>
            /// 'cr' value. Please see <see href="http://www.clusterpoint.com/wiki/XML_Alternatives">Clusterpoint Wiki</see> for detailed description.
            /// </summary>
            public float cr;
        }

        /// <summary>
        /// Local word count cache.
        /// </summary>
        private Dictionary<string, int> p_localWords;
    }

    /// <summary>
    /// Retrieve command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_LookupResponse"/>.
    /// <seealso cref="CPS_LookupResponse"/>.
    /// </summary>
    public class CPS_RetrieveRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_RetrieveRequest class instance.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_RetrieveRequest(string id, string requestId = "")
            : base("retrieve", requestId)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            base.setRawDocuments(ids);
        }

        /// <summary>
        /// Creates new CPS_RetrieveRequest class instance.
        /// </summary>
        /// <param name="id">List of document IDs.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_RetrieveRequest(List<string> id, string requestId = "")
            : base("retrieve", requestId)
        {
            base.setRawDocuments(id);
        }
    }

    /// <summary>
    /// Lookup command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_LookupResponse"/>.
    /// <seealso cref="CPS_LookupResponse"/>
    /// </summary>
    public class CPS_LookupRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_LookupRequest class instance.
        /// </summary>
        /// <param name="id">Document ID.</param>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_LookupRequest(string id, Dictionary<string, string> list = null, string requestId = "")
            : base("lookup", requestId)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            base.setRawDocuments(ids);
            this.setList(list);
        }

        /// <summary>
        /// Creates new CPS_LookupRequest class instance.
        /// </summary>
        /// <param name="id">List of document IDs.</param>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_LookupRequest(List<string> id, Dictionary<string, string> list = null, string requestId = "")
            : base("lookup", requestId)
        {
            base.setRawDocuments(id);
            this.setList(list);
        }

        /// <summary>
        /// Sets listing options.
        /// </summary>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        public void setList(Dictionary<string, string> list)
        {
            if (list == null)
                return;

            string listString = "";

            foreach (KeyValuePair<string, string> pair in list)
                listString += Utils.CPS_Term(pair.Value, pair.Key);

            base.setParam("list", listString);
        }
    }

    /// <summary>
    /// Lookup command response, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request classes are <see cref="CPS_RetrieveRequest"/>, <see cref="CPS_ListLastRequest"/>, <see cref="CPS_ListFirstRequest"/>, <see cref="CPS_RetrieveLastRequest"/>, <see cref="CPS_RetrieveFirstRequest"/> and <see cref="CPS_LookupRequest"/>.
    /// <seealso cref="CPS_RetrieveRequest"/>
    /// <seealso cref="CPS_ListLastRequest"/>
    /// <seealso cref="CPS_ListFirstRequest"/>
    /// <seealso cref="CPS_RetrieveLastRequest"/>
    /// <seealso cref="CPS_RetrieveFirstRequest"/>
    /// <seealso cref="CPS_LookupRequest"/>
    /// </summary>
    public class CPS_LookupResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_LookupResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_LookupResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
        }

        /// <summary>
        /// Gets returned documents.
        /// </summary>
        /// <param name="type">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object getDocuments(CPS_Response.DOC_TYPE type = CPS_Response.DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            return base.getRawDocuments(type);
        }

        /// <summary>
        /// Gets returned document count.
        /// </summary>
        /// <returns>Document count.</returns>
        public int getFound()
        {
            return base.getParamInt("found");
        }

        /// <summary>
        /// Gets returned documents starting offset (position).
        /// </summary>
        /// <returns>Starting offset.</returns>
        public int getFrom()
        {
            return base.getParamInt("from");
        }

        /// <summary>
        /// Gets returned documents ending offset (position).
        /// </summary>
        /// <returns>Ending offset.</returns>
        public int getTo()
        {
            return base.getParamInt("to");
        }
    }

    /// <summary>
    /// Generic list-* and retrieve-* command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_LookupResponse"/>.
    /// <seealso cref="CPS_LookupResponse"/>
    /// </summary>
    public class CPS_ListLastRetrieveFirstRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_ListLastRetrieveFirst class instance.
        /// </summary>
        /// <param name="command">Command name.</param>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ListLastRetrieveFirstRequest(string command, int offset, int docs, Dictionary<string, string> list, string requestId = "")
            : base(command, requestId)
        {
            if (offset >= 0)
                base.setParam("offset", offset.ToString());

            if (docs >= 0)
                base.setParam("docs", docs.ToString());

            if (list != null)
                this.setList(list);
        }

        /// <summary>
        /// Sets listing option.
        /// </summary>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        public void setList(Dictionary<string, string> list)
        {
            if (list == null)
                return;

            string listString = "";

            foreach (KeyValuePair<string, string> pair in list)
                listString += Utils.CPS_Term(pair.Value, pair.Key);

            base.setParam("list", listString);
        }
    }

    /// <summary>
    /// List-Last command request class, wrapper for <see cref="CPS_ListLastRetrieveFirstRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_LookupResponse"/>.
    /// <seealso cref="CPS_LookupResponse"/>
    /// </summary>
    public class CPS_ListLastRequest : CPS_ListLastRetrieveFirstRequest
    {
        /// <summary>
        /// Creates new CPS_ListLastRequest class instance.
        /// </summary>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ListLastRequest(Dictionary<string, string> list, int offset = -1, int docs = -1, string requestId = "")
            : base("list-last", offset, docs, list, requestId)
        {
        }
    }

    /// <summary>
    /// List-First command request class, wrapper for <see cref="CPS_ListLastRetrieveFirstRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_LookupResponse"/>.
    /// <seealso cref="CPS_LookupResponse"/>
    /// </summary>
    public class CPS_ListFirstRequest : CPS_ListLastRetrieveFirstRequest
    {
        /// <summary>
        /// Creates new CPS_ListFirstRequest class instance.
        /// </summary>
        /// <param name="list">Dictionary of xpaths as keys and list options (yes | no | snippet | highlight) as values.</param>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ListFirstRequest(Dictionary<string, string> list, int offset = -1, int docs = -1, string requestId = "")
            : base("list-first", offset, docs, list, requestId)
        {
        }
    }

    /// <summary>
    /// Retrieve-Last command request class, wrapper for <see cref="CPS_ListLastRetrieveFirstRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_LookupResponse"/>.
    /// <seealso cref="CPS_LookupResponse"/>
    /// </summary>
    public class CPS_RetrieveLastRequest : CPS_ListLastRetrieveFirstRequest
    {
        /// <summary>
        /// Creates new CPS_RetrieveLastRequest class instance.
        /// </summary>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_RetrieveLastRequest(int offset = -1, int docs = -1, string requestId = "")
            : base("retrieve-last", offset, docs, null, requestId)
        {
        }
    }

    /// <summary>
    /// Retrieve-First command request class, wrapper for <see cref="CPS_ListLastRetrieveFirstRequest"/> and <see cref="CPS_Request"/> classes.
    /// Corresponding response class is <see cref="CPS_LookupResponse"/>.
    /// <seealso cref="CPS_LookupResponse"/>
    /// </summary>
    public class CPS_RetrieveFirstRequest : CPS_ListLastRetrieveFirstRequest
    {
        /// <summary>
        /// Creates new CPS_RetrieveFirstRequest class instance.
        /// </summary>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_RetrieveFirstRequest(int offset = -1, int docs = -1, string requestId = "")
            : base("retrieve-first", offset, docs, null, requestId)
        {
        }
    }

    /// <summary>
    /// Search-Delete command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_SearchDeleteResponse"/>.
    /// <seealso cref="CPS_SearchDeleteResponse"/>
    /// </summary>
    public class CPS_SearchDeleteRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_SearchDeleteRequest class instance.
        /// </summary>
        /// <param name="query">Search query. <see cref="CPS_SearchRequest.setQuery(string)"/> for more information.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_SearchDeleteRequest(string query, string requestId = "")
            : base("search-delete", requestId)
        {
            this.setQuery(query);
        }

        /// <summary>
        /// Sets search query.
        /// </summary>
        /// <param name="value">Search query.</param>
        public void setQuery(string value)
        {
            base.setParam("query", value);
        }

        /// <summary>
        /// Sets stemming language.
        /// </summary>
        /// <param name="value">Language code.</param>
        public void setStemLang(string value)
        {
            base.setParam("stem-lang", value);
        }

        /// <summary>
        /// Sets exatch matching option.
        /// </summary>
        /// <param name="value">Option value.</param>
        public void setExactMatch(string value)
        {
            base.setParam("exact-match", value);
        }
    }

    /// <summary>
    /// Search-Delete command response class, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request class is <see cref="CPS_SearchDeleteRequest"/>.
    /// <seealso cref="CPS_SearchDeleteRequest"/>.
    /// </summary>
    public class CPS_SearchDeleteResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_SearchDeleteResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_SearchDeleteResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
        }

        /// <summary>
        /// Returned number of document, that matched search query and were deleted.
        /// </summary>
        /// <returns>Number of documents.</returns>
        public int getHits()
        {
            return base.getParamInt("hits");
        }
    }

    /// <summary>
    /// List-Paths command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_ListPathsResponse"/>.
    /// <seealso cref="CPS_ListPathsResponse"/>
    /// </summary>
    public class CPS_ListPathsRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_ListPathsRequest class instance.
        /// </summary>
        /// <param name="requestId">Request ID.</param>
        public CPS_ListPathsRequest(string requestId = "")
            : base("list-paths", requestId)
        {
        }
    }

    /// <summary>
    /// List-Paths command response class, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request class is <see cref="CPS_ListPathsRequest"/>.
    /// <seealso cref="CPS_ListPathsRequest"/>
    /// </summary>
    public class CPS_ListPathsResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_ListPathsResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_ListPathsResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
        }

        /// <summary>
        /// Gets returned paths list.
        /// </summary>
        /// <returns>Paths list.</returns>
        public List<string> getPaths()
        {
            List<string> ret = new List<string>();
            CPS_SimpleXML content = (CPS_SimpleXML)base.getContentArray(DOC_TYPE.DOC_TYPE_SIMPLEXML);

            if (content != null && content["paths"] != null)
            {
                CPS_SimpleXML list = content["paths"];
                for (int i = 0;; i++)
                {
                    if (list["path", i] == null)
                        break;
                    ret.Add(list["path", i]);
                }
            }
            else
                return null;

            return ret;
        }
    }

    /// <summary>
    /// List-Facets command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding response class is <see cref="CPS_ListFacetsResponse"/>.
    /// <seealso cref="CPS_ListFacetsResponse"/>
    /// </summary>
    public class CPS_ListFacetsRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_ListFacetsRequest class instance.
        /// </summary>
        /// <param name="path">Single path to list facets from.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ListFacetsRequest(string path, string requestId = "")
            : base("list-facets", requestId)
        {
            base.setParam("path", path);
        }

        /// <summary>
        /// Creates new CPS_ListFacetsRequest class instance.
        /// </summary>
        /// <param name="paths">List of paths to list facets from.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_ListFacetsRequest(List<string> paths, string requestId = "")
            : base("list-facets", requestId)
        {
            base.setParam("path", paths);
        }
    }

    /// <summary>
    /// List-Facets command response class, wrapper for <see cref="CPS_Response"/> class.
    /// Corresponding request class is <see cref="CPS_ListFacetsRequest"/>.
    /// <seealso cref="CPS_ListFacetsRequest"/>
    /// </summary>
    public class CPS_ListFacetsResponse : CPS_Response
    {
        /// <summary>
        /// Creates new CPS_ListFacetsResponse class instance.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_ListFacetsResponse(CPS_Connection connection, CPS_Request request, string responseXml)
            : base(connection, request, responseXml)
        {
        }

        /// <summary>
        /// Gets returned facets.
        /// </summary>
        /// <returns>Dictionary of facets.</returns>
        public Dictionary<string, List<string>> getFacets()
        {
            Dictionary<string, Dictionary<string, int>> rawFacets = base.getRawFacets();
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();

            foreach (KeyValuePair<string, Dictionary<string, int>> pair1 in rawFacets)
            {
                ret[pair1.Key] = new List<string>();

                foreach (KeyValuePair<string, int> pair2 in pair1.Value)
                    ret[pair1.Key].Add(pair2.Key);
            }

            return ret;
        }
    }

    /// <summary>
    /// Similar command (by document ID) request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding request class is <see cref="CPS_SearchResponse"/>.
    /// <see cref="CPS_SearchResponse"/>
    /// </summary>
    public class CPS_SimilarDocumentRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_SimilarDocumentRequest class instance.
        /// </summary>
        /// <param name="docid">Document ID to search similar documents to.</param>
        /// <param name="len">Number of keywords to extract.</param>
        /// <param name="quota">Minimum number of keywords to match.</param>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="query">Additional filtering query.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_SimilarDocumentRequest(string docid, int len, int quota, int offset = -1, int docs = -1, string query = "", string requestId = "")
            : base("similar", requestId)
        {
            base.setParam("id", docid);
            base.setParam("len", len.ToString());
            base.setParam("quota", quota.ToString());
            if (docs >= 0)
                base.setParam("docs", docs.ToString());
            if (offset >= 0)
                base.setParam("offset", offset.ToString());
            if (query.Length > 0)
                base.setParam("query", query);
        }
    }

    /// <summary>
    /// Similar command (by passed text) request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding request class is <see cref="CPS_SearchResponse"/>.
    /// <see cref="CPS_SearchResponse"/>
    /// </summary>
    public class CPS_SimilarTextRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_SimilarTextRequest class instance.
        /// </summary>
        /// <param name="text">Text to search similar documets to.</param>
        /// <param name="len">Number of keywords to extract.</param>
        /// <param name="quota">Minimum number of keywords to match.</param>
        /// <param name="offset">Number of document to skip before including them in response.</param>
        /// <param name="docs">Number of document to return.</param>
        /// <param name="query">Additional filtering query.</param>
        /// <param name="requestId">Request ID.</param>
        public CPS_SimilarTextRequest(string text, int len, int quota, int offset = -1, int docs = -1, string query = "", string requestId = "")
            : base("similar", requestId)
        {
            base.setParam("text", text);
            base.setParam("len", len.ToString());
            base.setParam("quota", quota.ToString());
            if (docs >= 0)
                base.setParam("docs", docs.ToString());
            if (offset >= 0)
                base.setParam("offset", offset.ToString());
            if (query.Length > 0)
                base.setParam("query", query);
        }
    }

    // TODO: alerts

    /// <summary>
    /// Begin-Transaction command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding request class is <see cref="CPS_Response"/>.
    /// <see cref="CPS_Response"/>
    /// </summary>
    public class CPS_BeginTransactionRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_BeginTransactionRequest class instance.
        /// </summary>
        /// <param name="requestId">Request ID.</param>
        public CPS_BeginTransactionRequest(string requestId = "")
            : base("begin-transaction", requestId)
        {
        }
    }

    /// <summary>
    /// Commit-Transaction command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding request class is <see cref="CPS_Response"/>.
    /// <see cref="CPS_Response"/>
    /// </summary>
    public class CPS_CommitTransactionRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_BeginTransactionRequest class instance.
        /// </summary>
        /// <param name="requestId">Request ID.</param>
        public CPS_CommitTransactionRequest(string requestId = "")
            : base("commit-transaction", requestId)
        {
        }
    }

    /// <summary>
    /// Rollback-Transaction command request class, wrapper for <see cref="CPS_Request"/> class.
    /// Corresponding request class is <see cref="CPS_Response"/>.
    /// <see cref="CPS_Response"/>
    /// </summary>
    public class CPS_RollbackTransactionRequest : CPS_Request
    {
        /// <summary>
        /// Creates new CPS_BeginTransactionRequest class instance.
        /// </summary>
        /// <param name="requestId">Request ID.</param>
        public CPS_RollbackTransactionRequest(string requestId = "")
            : base("rollback-transaction", requestId)
        {
        }
    }
}
