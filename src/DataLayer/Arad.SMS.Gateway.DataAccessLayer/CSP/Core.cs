using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace CPS
{
    /// <summary>
    /// CPS Exception class, that can be thrown in various cases.
    /// </summary>
    public class CPS_Exception : System.Exception
    {
        /// <summary>
        /// Creates new CPS_Exception class instance.
        /// </summary>
        public CPS_Exception()
            : base()
        {
            this.p_code = (int)ERROR_CODE.OK;
        }

        /// <summary>
        /// Creates new CPS_Exception class instance.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public CPS_Exception(string message)
            : base(message)
        {
            this.p_code = (int)ERROR_CODE.OK;
        }

        /// <summary>
        /// Creates new CPS_Exception class instance.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Another exception.</param>
        public CPS_Exception(string message, System.Exception inner)
            : base(message, inner)
        {
            this.p_code = (int)ERROR_CODE.OK;
        }

        /// <summary>
        /// Creates new CPS_Exception class instance. Constructor not available for user.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected CPS_Exception(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            this.p_code = (int)ERROR_CODE.OK;
        }

        /// <summary>
        /// Converts CPS_Exception to string message.
        /// </summary>
        /// <returns>String message.</returns>
        public override string ToString()
        {
            if (this.p_code != 0)
                return "[" + this.p_code + "] " + this.Message;
            else
                return this.Message;
        }

        /// <summary>
        /// Sets CPS_Exception error code
        /// </summary>
        /// <param name="code">Error code</param>
        /// <returns>Self (CPS_Exception).</returns>
        public CPS_Exception SetCode(ERROR_CODE code)
        {
            this.p_code = (int)code;
            return this;
        }

        /// <summary>
        /// Sets CPS_Exception error code
        /// </summary>
        /// <param name="code">Error code</param>
        /// <returns>Self (CPS_Exception)</returns>
        public CPS_Exception SetCode(int code)
        {
            this.p_code = code;
            return this;
        }

        /// <summary>
        /// Error code.
        /// </summary>
        private int p_code;

        /// <summary>
        /// Various error codes.
        /// </summary>
        public enum ERROR_CODE
        {
            /// <summary>
            /// OK, no error - 0.
            /// </summary>
            OK = 0,
            /// <summary>
            /// Invalid response - 9001.
            /// </summary>
            INVALID_RESPONSE = 9001,
            /// <summary>
            /// Invalid parameter - 9002.
            /// </summary>
            INVALID_PARAMETER = 9002,
            /// <summary>
            /// Invalid xpath(s) - 9003.
            /// </summary>
            INVALID_XPATHS = 9003,
            /// <summary>
            /// Invalid connection string - 9004.
            /// </summary>
            INVALID_CONNECTION_STRING = 9004,
            /// <summary>
            /// Protobuf-related error - 9005.
            /// </summary>
            PROTOBUF_ERROR = 9005,
            /// <summary>
            /// Socket-related error - 9006.
            /// </summary>
            SOCKET_ERROR = 9006,
            /// <summary>
            /// Timeout - 9007.
            /// </summary>
            TIMEOUT = 9007,
            /// <summary>
            /// No working servers remaining - 9008.
            /// </summary>
            NO_WORKING_SERVERS = 9008,
            /// <summary>
            /// SSL handshake error, that includes certificate validation - 9009.
            /// </summary>
            SSL_HANDSHAKE = 9009
        }
    }

    /// <summary>
    /// Hash wrapper for easy access to hashing methods.
    /// </summary>
    class CPS_Hasher
    {
        /// <summary>
        /// MD5 hash.
        /// </summary>
        /// <param name="input">Data to hash.</param>
        /// <returns>Hash string.</returns>
        public static string MD5(string input)
        {
            System.Security.Cryptography.MD5 hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        /// <summary>
        /// SHA1 hash.
        /// </summary>
        /// <param name="input">Data to hash.</param>
        /// <returns>Hash string</returns>
        public static string SHA1(string input)
        {
            System.Security.Cryptography.SHA1 hasher = System.Security.Cryptography.SHA1.Create();
            byte[] data = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        /// <summary>
        /// SHA1-HMAC hash.
        /// </summary>
        /// <param name="key">HMAC key.</param>
        /// <param name="message">Data to hash.</param>
        /// <returns>HMAC hash string.</returns>
        public static string SHA1_HMAC(string key, string message)
        {
            System.Security.Cryptography.HMACSHA1 hasher = new System.Security.Cryptography.HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] data = hasher.ComputeHash(Encoding.UTF8.GetBytes(message));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
    }

    /// <summary>
    /// The load balancer class - connects to different nodes randomly unless one of the nodes is down.
    /// </summary>
    public class CPS_LoadBalancer
    {
        /// <summary>
        /// Constructs an instance of the Load Balancer class.
        /// </summary>
        /// <param name="connectionStrings">List of connection strings.</param>
        /// <param name="storageNames">List of storage names corresponding to each of the connection strings.</param>
        public CPS_LoadBalancer(List<string> connectionStrings, List<string> storageNames = null)
        {
            this.p_connectionStrings = connectionStrings;
            this.p_storageNames = storageNames;
            this.p_usedConnections = new List<int>();
            this.p_numUsed = 0;
            this.p_lastReturnedIndex = -1;
            this.p_lastSuccess = false;
            this.p_exclusionTime = 30;
            this.p_statusFilePrefix = System.IO.Path.GetTempPath() + "cps-api-node-status-";
            this.p_sendWhenAllFailed = true;
            this.p_debug = false;
            this.p_retryRequests = false;
            this.p_random = new Random();
        }

        /// <summary>
        /// Sets the debugging mode.
        /// </summary>
        /// <param name="debugMode">Debugging mode.</param>
        public void setDebug(bool debugMode)
        {
            this.p_debug = debugMode;
        }

        /// <summary>
        /// This is called by CPS_Connection whenever there is a new request to be sent. This method should not be used directly.
        /// </summary>
        /// <param name="request">The request before being rendered.</param>
        public void newRequest(CPS_Request request)
        {
            if (request.getCommand() == "search")
                this.p_retryRequests = true;
            else
                this.p_retryRequests = false;
            if (this.p_numUsed == this.p_connectionStrings.Count)
            {
                this.p_usedConnections.Clear();
                this.p_numUsed = 0;
                this.p_lastReturnedIndex = -1;
                this.p_lastSuccess = false;
            }
        }

        /// <summary>
        /// This function is called by CPS_Connection before sending the request to find out where to send it to. This method should not be used directly.
        /// </summary>
        /// <param name="storageName">If request on different connections should be sent to different storage names, this parameter should be set before returning.</param>
        /// <returns>Connection string as result and storageName by reference.</returns>
        public string getConnectionString(ref string storageName)
        {
            if (this.p_numUsed == this.p_connectionStrings.Count)
                return null;
            if (this.p_lastSuccess)
            {
                this.p_lastSuccess = false;
                return this.p_connectionStrings[this.p_lastReturnedIndex];
            }

            int i = 0;
            do
            {
                i = this.p_random.Next(0, this.p_connectionStrings.Count);
                if (this.p_usedConnections.Exists(x => x == i))
                    continue;
                string source = this.p_connectionStrings[i] + (this.p_storageNames != null && this.p_storageNames.Count > i && this.p_storageNames[i] != null ? "|" + this.p_storageNames[i] : "");
                string hash = CPS_Hasher.MD5(source);
                if (System.IO.File.Exists(this.p_statusFilePrefix + hash))
                {
                    System.DateTime mtime = System.IO.File.GetLastWriteTime(this.p_statusFilePrefix + hash);
                    mtime.AddSeconds(this.p_exclusionTime);
                    if (mtime < System.DateTime.Now)
                        System.IO.File.Delete(this.p_statusFilePrefix + hash);
                    else
                    {
                        if (this.p_connectionStrings.Count - this.p_numUsed > 1 || !this.p_sendWhenAllFailed)
                        {
                            if (this.p_debug)
                                System.IO.File.AppendAllText("debug.txt", "[LoadBalancer] Skipping " + this.p_connectionStrings[i] + (this.p_storageNames != null && this.p_storageNames.Count > i && this.p_storageNames[i] != null ? "|" + this.p_storageNames[i] : "") + " because it's marked as failed on " + System.IO.File.GetLastWriteTime(this.p_statusFilePrefix + hash).ToString("dd-MM-yyyy HH:mm:ss") + ".\n");

                            this.p_usedConnections.Add(i);
                            this.p_numUsed += 1;

                            if (this.p_connectionStrings.Count == this.p_numUsed && !this.p_sendWhenAllFailed)
                            {
                                if (this.p_debug)
                                    System.IO.File.AppendAllText("debug.txt", "[LoadBalancer] All servers are marked as failed.\n");

                                throw new CPS_Exception("No servers to send to").SetCode(CPS_Exception.ERROR_CODE.NO_WORKING_SERVERS);
                            }
                        }
                    }
                }
            }
            while (this.p_usedConnections.Exists(x => x == i));

            this.p_usedConnections.Add(i);
            this.p_numUsed += 1;
            if (this.p_storageNames != null && this.p_storageNames.Count > i && this.p_storageNames[i] != null)
                storageName = this.p_storageNames[i];
            this.p_lastReturnedIndex = i;
            this.p_lastSuccess = true;
            if (this.p_debug)
                System.IO.File.AppendAllText("debug.txt", "[LoadBalancer] Connecting to " + this.p_connectionStrings[i] + (this.p_storageNames != null && this.p_storageNames.Count > i && this.p_storageNames[i] != null ? "|" + this.p_storageNames[i] : "") + ".\n");

            return this.p_connectionStrings[i];
        }

        /// <summary>
        /// This function is called whenever there was a sending failure - an error received or an exception raised.
        /// </summary>
        private void logFailure()
        {
            if (this.p_debug)
                System.IO.File.AppendAllText("debug.txt", "[LoadBalancer] Failed for " + this.p_connectionStrings[this.p_lastReturnedIndex] + (this.p_storageNames != null && this.p_storageNames.Count > this.p_lastReturnedIndex && this.p_storageNames[this.p_lastReturnedIndex] != null ? "|" + this.p_storageNames[this.p_lastReturnedIndex] : "") + ".\n");

            string source = this.p_connectionStrings[this.p_lastReturnedIndex] + (this.p_storageNames != null && this.p_storageNames.Count > this.p_lastReturnedIndex && this.p_storageNames[this.p_lastReturnedIndex] != null ? "|" + this.p_storageNames[this.p_lastReturnedIndex] : "");
            string hash = CPS_Hasher.MD5(source);
            System.IO.FileStream f = System.IO.File.Create(this.p_statusFilePrefix + hash);
            f.Close();
        }

        /// <summary>
        /// This function is called whenever there was a sending success - no error received and no exception raised.
        /// </summary>
        private void logSuccess()
        {
            this.p_lastSuccess = true;
            if (this.p_debug)
                System.IO.File.AppendAllText("debug.txt", "[LoadBalancer] Success for " + this.p_connectionStrings[this.p_lastReturnedIndex] + (this.p_storageNames != null && this.p_storageNames.Count > this.p_lastReturnedIndex && this.p_storageNames[this.p_lastReturnedIndex] != null ? "|" + this.p_storageNames[this.p_lastReturnedIndex] : "") + ".\n");

            string source = this.p_connectionStrings[this.p_lastReturnedIndex] + (this.p_storageNames != null && this.p_storageNames.Count > this.p_lastReturnedIndex && this.p_storageNames[this.p_lastReturnedIndex] != null ? "|" + this.p_storageNames[this.p_lastReturnedIndex] : "");
            string hash = CPS_Hasher.MD5(source);

            if (System.IO.File.Exists(this.p_statusFilePrefix + hash))
                System.IO.File.Delete(this.p_statusFilePrefix + hash);
        }

        /// <summary>
        /// This function should return whether a request should be retried depending on response.
        /// </summary>
        /// <param name="responseString">Raw response string.</param>
        /// <param name="exception">Received exception.</param>
        /// <returns>Boolean value showing if request should be retried.</returns>
        public bool shouldRetry(string responseString, CPS_Exception exception = null)
        {
            if (!this.p_retryRequests)
                return false;
            if (this.p_numUsed == this.p_connectionStrings.Count)
                return false;
            if (exception != null)
            {
                this.logFailure();
                return true;
            }
            int sp = responseString.IndexOf("<cps:error>");
            if (sp == -1)
            {
                this.logSuccess();
                return false;
            }
            int ep = responseString.LastIndexOf("</cps:error>");
            if (ep == -1)
            {
                this.logFailure();
                return true;
            }

            try
            {
                XmlDocument errorXml = new XmlDocument();
                errorXml.LoadXml("<root xmlns:cps=\"www.clusterpoint.com\">" + responseString.Substring(sp, ep - sp + 12) + "</root>");

                foreach (XmlElement e in errorXml.DocumentElement.ChildNodes)
                {
                    string code = e["code"].InnerXml;
                    string level = e["level"].InnerXml;

                    if (level == "FAILED" || level == "ERROR" || level == "FATAL" || code == "2344")
                    {
                        this.logFailure();
                        return true;
                    }
                }              
            }
            catch (Exception)
            {
                return true;
            }

            this.logSuccess();
            return false;
        }

        /// <summary>
        /// This function defines how long should a node should be excluded from the list of valid nodes after a failure. Default is 30 seconds.
        /// </summary>
        /// <param name="seconds">Number of seconds that the node will be excluded for.</param>
        public void setExclusionTime(int seconds)
        {
            this.p_exclusionTime = seconds;
        }

        /// <summary>
        /// This function lets you set the prefix for status files. Default prefix is System.IO.Path.GetTempPath() + "cps-api-node-status-".
        /// </summary>
        /// <param name="prefix">New prefix.</param>
        public void setStatusFilePrefix(string prefix)
        {
            this.p_statusFilePrefix = prefix;
        }

        /// <summary>
        /// This function defines whether the request should be sent to one of the nodes anyway, when all the nodes in the list have failed.
        /// </summary>
        /// <param name="value">Boolean value.</param>
        public void setSendWhenAllFailed(bool value)
        {
            this.p_sendWhenAllFailed = value;
        }

        /// <summary>
        /// List of passed connection strings.
        /// </summary>
        private List<string> p_connectionStrings;
        /// <summary>
        /// List of passed storage names.
        /// </summary>
        private List<string> p_storageNames;
        /// <summary>
        /// Debug mode flag.
        /// </summary>
        private bool p_debug;
        /*/// <summary>
        /// Unknown variable, found in PHP API
        /// </summary>
        private bool p_loadBalance;*/
        /// <summary>
        /// List of used connection identifiers.
        /// </summary>
        private List<int> p_usedConnections;
        /// <summary>
        /// Count of used connections.
        /// </summary>
        private int p_numUsed;
        /// <summary>
        /// Index of last returned connection.
        /// </summary>
        private int p_lastReturnedIndex;
        /// <summary>
        /// Flag if last request was successfull.
        /// </summary>
        private bool p_lastSuccess;
        /// <summary>
        /// Connection exclusion time in case of failure.
        /// </summary>
        private int p_exclusionTime;
        /// <summary>
        /// Status file prefix.
        /// </summary>
        private string p_statusFilePrefix;
        /// <summary>
        /// Flag to send request even when all connections have failed.
        /// </summary>
        private bool p_sendWhenAllFailed;
        /// <summary>
        /// Flag if requests should be retried.
        /// </summary>
        private bool p_retryRequests;
        /// <summary>
        /// Random object.
        /// </summary>
        private Random p_random;
    }

    /// <summary>
    /// Wildcard matching class.
    /// </summary>
    class CPS_Wildcard
    {
        /// <summary>
        /// Checks if given wildcard string matches given text.
        /// </summary>
        /// <param name="wildcard">Wildcard string.</param>
        /// <param name="text">Given text.</param>
        /// <returns>Matching result as boolean value.</returns>
        public static bool Match(string wildcard, string text)
        {
            wildcard = wildcard.Replace(@"\", @"\\");
            wildcard = wildcard.Replace(".", @"\.");
            wildcard = wildcard.Replace("?", ".");
            wildcard = wildcard.Replace("*", ".*?");
            wildcard = wildcard.Replace(" ", @"\s");

            return new System.Text.RegularExpressions.Regex("^" + wildcard + "$", System.Text.RegularExpressions.RegexOptions.IgnoreCase).IsMatch(text);
        }
    }

    /// <summary>
    /// CPS Connection class, that represents connection to CPS storage.
    /// </summary>
    public class CPS_Connection
    {
        /// <summary>
        /// Creates new CPS Connection class instance. Note. Constructor does not connect to storage.
        /// </summary>
        /// <param name="connectionSpecification">Connection string (URL) or load balancing object. .NET API supports tcp://..., tcps://... and http://... protocols for connection strings.</param>
        /// <param name="storageName">Storage new you want to connect to.</param>
        /// <param name="username">Username for storage.</param>
        /// <param name="password">Password for storage.</param>
        /// <param name="documentRootXpath">Document root tag name, for example 'document'.</param>
        /// <param name="documentIdXpath">Document ID xpath, for example '//document/id'.</param>
        /// <param name="customEnvelopeParams">Additional request envelope parameters</param>
        public CPS_Connection(Object connectionSpecification, string storageName, string username, string password, string documentRootXpath = "document", string documentIdXpath = "//document/id", Dictionary<string, string> customEnvelopeParams = null)
        {
            this.p_storageName = storageName;
            this.p_customEnvelopeParams = customEnvelopeParams;
            this.p_hmacUserKey = null;
            this.p_hmacSignKey = null;
            this.p_username = username;
            this.p_password = password;
            this.p_documentRootXpath = documentRootXpath;
            this.p_documentIdXpath = new List<string>();
            string[] xid = documentIdXpath.Split('/');
            int i = 0;
            for (i = 0; i < xid.Length; i++)
                if (xid[i].Length > 0)
                    this.p_documentIdXpath.Add(xid[i]);

            this.p_connectionString = null;
            this.p_connectionSwitcher = null;

            try
            {
                string connection_type1 = (string)connectionSpecification;
                if (connection_type1 != null)
                    this.p_connectionString = this.parseConnectionString(connection_type1);
            }
            catch (CPS_Exception e) // thrown by parseConnectionString
            {
                throw e; // rethrow
            }
            catch (Exception)
            {
                // do nothing
            }

            try
            {
                CPS_LoadBalancer connection_type2 = (CPS_LoadBalancer)connectionSpecification;
                if (connection_type2 != null)
                    this.p_connectionSwitcher = connection_type2;
            }
            catch (Exception)
            {
                // do nothing
            }

            if (this.p_connectionString == null && this.p_connectionSwitcher == null)
                throw new CPS_Exception("Invalid connection string").SetCode(CPS_Exception.ERROR_CODE.INVALID_CONNECTION_STRING);

            this.p_applicationId = "CPS_dotNET_API";
            this.p_debug = false;
            this.p_docid_resetting = true;

            this.p_lastNetworkDuration = 0;
            this.p_lastRequestDuration = 0;
            this.p_lastRequestSize = 0;
            this.p_lastResponseSize = 0;

            this.p_transactionId = null;
            this.p_random = new Random();

            this.p_sslCustomCA = null;
            this.p_sslCustomCN = null;
        }

        /// <summary>
        /// Sets custom CA and Common Name for SSL certificate validation for cases when system validation fails.
        /// </summary>
        /// <param name="sslCustomCA">PEM file with server's CA certificate or null if CA should not be verified.</param>
        /// <param name="sslCustomCN">String specifying server'a Common Name of null if Common Name should not be verified.</param>
        public void setCustomSSLValidation(string sslCustomCA = null, string sslCustomCN = null)
        {
            this.p_sslCustomCA = sslCustomCA;
            this.p_sslCustomCN = sslCustomCN;
        }

        /// <summary>
        /// Verifies the remote Secure Sockets Layer (SSL) certificate used for authentication.
        /// </summary>
        /// <param name="sender">An object that contains state information for this validation.</param>
        /// <param name="certificate">The certificate used to authenticate the remote party.</param>
        /// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>A Boolean value that determines whether the specified certificate is accepted for authentication.</returns>
        private bool ConnectionServerValidationCallback(
              Object sender,
              System.Security.Cryptography.X509Certificates.X509Certificate certificate,
              System.Security.Cryptography.X509Certificates.X509Chain chain,
              System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None) // system validation succeeded
                return true;

            System.Net.Security.SslPolicyErrors fixedPolicyErrors = sslPolicyErrors;

            if ((fixedPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch) == System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch)
            {
                if (this.p_sslCustomCN != null) // have specified custom common name
                {
                    try
                    {
                        System.Security.Cryptography.X509Certificates.X509Certificate2 certificate2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(certificate);
                        string common_name = certificate2.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false);

                        if (CPS_Wildcard.Match(common_name, this.p_sslCustomCN) || CPS_Wildcard.Match(this.p_sslCustomCN, common_name))
                            fixedPolicyErrors ^= System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch;
                    }
                    catch (Exception)
                    {
                        // do nothing
                    }
                }
                else // have no custom common name, will accept any common name
                    fixedPolicyErrors ^= System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch;
            }

            if ((fixedPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) == System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors)
            {
                if (this.p_sslCustomCA != null) // have specified custom common name
                {
                    try
                    {
                        System.Security.Cryptography.X509Certificates.X509Certificate2 certificate2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(this.p_sslCustomCA);

                        if (chain.ChainElements.Count > 0 && chain.ChainElements[0].Certificate.Thumbprint == certificate2.Thumbprint && chain.ChainElements[0].Certificate.SerialNumber == certificate2.SerialNumber)
                            fixedPolicyErrors ^= System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors;
                    }
                    catch (Exception)
                    {
                        // do nothing
                    }
                }
                else // have no custom CA, will accept any CA
                    fixedPolicyErrors ^= System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors;
            }

            return (fixedPolicyErrors == System.Net.Security.SslPolicyErrors.None);
        }

        /// <summary>
        /// Sends request to Clusterpoint Server. Returned CPS_Response should be casted to command-specific response class.
        /// </summary>
        /// <param name="request">Request to send.</param>
        /// <returns>Command-specific CPS_Response object instance.</returns>
        public CPS_Response sendRequest(CPS_Request request)
        {
            bool firstSend = true;
            string previousRenderedStorage = "";
            string requestXml = "";
            byte[] requestBytes = null;
            string rawResponse = "";
            bool quit = true;

            if (this.p_connectionSwitcher != null)
                this.p_connectionSwitcher.newRequest(request);

            do
            {
                CPS_Exception e = null;

                if (this.p_connectionSwitcher != null)
                    this.p_connectionString = this.parseConnectionString(this.p_connectionSwitcher.getConnectionString(ref this.p_storageName));

                try
                {
                    if (this.p_transactionId != null)
                        request.setParam("transaction_id", this.p_transactionId);
                    if (firstSend || previousRenderedStorage != this.p_storageName)
                    {
                        requestXml = this.renderRequest(request);
                        requestBytes = Encoding.UTF8.GetBytes(requestXml);
                        previousRenderedStorage = this.p_storageName;

                        if (this.p_debug)
                        {
                            FileStream fs = new FileStream("request.xml", FileMode.Create);
                            fs.Write(requestBytes, 0, requestBytes.Length);
                            fs.Close();
                        }
                    }
                    firstSend = false;

                    this.p_lastRequestSize = requestXml.Length;
                    this.p_lastNetworkDuration = 0;

                    Stopwatch totTimer = new Stopwatch();
                    Stopwatch netTimer = new Stopwatch();

                    totTimer.Start();
                    if (this.p_connectionString.Scheme.ToLower() == "http")
                    {
                        // TODO: implement HMAC support when server side supports it
                        HttpWebRequest webreq = (HttpWebRequest)HttpWebRequest.Create(this.p_connectionString.OriginalString);
                        webreq.UserAgent = this.p_applicationId;
                        webreq.Method = "POST";
                        webreq.ContentType = "application/x-www-form-urlencoded";
                        webreq.ContentLength = requestBytes.Length;
                        webreq.Headers["Recipient"] = this.p_storageName;
                        webreq.Proxy = null;

                        Stream webreq_data;
                        try
                        {
                            webreq_data = webreq.GetRequestStream();
                        }
                        catch (Exception)
                        {
                            throw new CPS_Exception("Invalid connection string").SetCode(CPS_Exception.ERROR_CODE.INVALID_CONNECTION_STRING);
                        }

                        netTimer.Start();
                        webreq_data.Write(requestBytes, 0, requestBytes.Length);
                        webreq_data.Close();
                        netTimer.Stop();

                        HttpWebResponse webrsp;
                        try
                        {
                            webrsp = (HttpWebResponse)webreq.GetResponse();
                        }
                        catch (Exception)
                        {
                            throw new CPS_Exception("Invalid connection string").SetCode(CPS_Exception.ERROR_CODE.INVALID_CONNECTION_STRING);
                        }
                        Stream webrsp_data = webrsp.GetResponseStream();
                        StreamReader webrsp_reader = new StreamReader(webrsp_data);

                        netTimer.Start();
                        rawResponse = webrsp_reader.ReadToEnd();
                        webrsp_reader.Close();
                        netTimer.Stop();
                    }

                    if (this.p_connectionString.Scheme.ToLower() == "tcp" || this.p_connectionString.Scheme.ToLower() == "tcps")
                    {
                        int port = this.p_connectionString.Port;
                        if (port <= 0)
                            port = 5550;
                        TcpClient tcp;
                        try
                        {
                            netTimer.Start();
                            tcp = new TcpClient(this.p_connectionString.Host, port);
                            netTimer.Stop();
                        }
                        catch (SocketException)
                        {
                            netTimer.Stop();
                            throw new CPS_Exception("Cannot connect to specified server").SetCode(CPS_Exception.ERROR_CODE.SOCKET_ERROR);
                        }
                        catch (Exception) // all other cases
                        {
                            netTimer.Stop();
                            throw new CPS_Exception("Invalid connection string").SetCode(CPS_Exception.ERROR_CODE.INVALID_CONNECTION_STRING);
                        }

                        NetworkStream net = tcp.GetStream();
                        System.IO.Stream strm = net;

                        if (this.p_connectionString.Scheme.ToLower() == "tcps")
                        {
                            System.Net.Security.SslStream ssl = new System.Net.Security.SslStream(strm, false, new System.Net.Security.RemoteCertificateValidationCallback(ConnectionServerValidationCallback), null);

                            try
                            {
                                ssl.AuthenticateAsClient(this.p_connectionString.Host);
                            }
                            catch (Exception)
                            {
                                throw new CPS_Exception("Error establishing SSL connection").SetCode(CPS_Exception.ERROR_CODE.SSL_HANDSHAKE);
                            }

                            strm = ssl;
                        }

                        Protobuf pb = new Protobuf();
                        pb.CreateField(1, Protobuf.WireType.LengthDelimited, requestBytes);
                        pb.CreateStringField(2, this.p_storageName);

                        if (this.p_hmacUserKey != null && this.p_hmacSignKey != null)
                        {
                            string characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                            char[] tokenchars = new char[16];
                            for (int i = 0; i < 16; i++)
                                tokenchars[i] = characters[this.p_random.Next(characters.Length)];
                            string token = new string(tokenchars);

                            System.DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();
                            System.DateTime validity = System.DateTime.Now.ToUniversalTime();
                            validity.AddSeconds(10);
                            System.TimeSpan dtdiff = validity - epoch;
                            UInt64 unixvalidity = (UInt64)dtdiff.TotalSeconds;

                            pb.CreateStringField(14, token);
                            pb.CreateFixed64Field(15, unixvalidity);
                            pb.CreateStringField(16, this.p_hmacUserKey);
                            pb.CreateStringField(17, CPS_Hasher.SHA1(CPS_Hasher.SHA1(requestXml) + token + unixvalidity + this.p_hmacSignKey));
                            pb.CreateStringField(18, CPS_Hasher.SHA1_HMAC(this.p_hmacSignKey, requestXml + token + unixvalidity));
                        }

                        MemoryStream ms = new MemoryStream();
                        Protobuf_Streamer pbs = new Protobuf_Streamer(ms);
                        pb.WriteToStream(pbs);

                        byte[] header = CPS_Length2Header((int)(ms.Length));

                        netTimer.Start();
                        try
                        {
                            strm.Write(header, 0, 8);
                            strm.Write(ms.GetBuffer(), 0, (int)(ms.Length));
                        }
                        catch (Exception)
                        {
                            netTimer.Stop();
                            throw new CPS_Exception("Error sending request").SetCode(CPS_Exception.ERROR_CODE.SOCKET_ERROR);
                        }
                        strm.Flush();

                        try
                        {
                            strm.Read(header, 0, 8);
                            netTimer.Stop();
                        }
                        catch (Exception)
                        {
                            netTimer.Stop();
                            throw new CPS_Exception("Error receiving response").SetCode(CPS_Exception.ERROR_CODE.SOCKET_ERROR);
                        }

                        int len = CPS_Header2Length(header);
                        if (len <= 0)
                            throw new CPS_Exception("Invalid response from server").SetCode(CPS_Exception.ERROR_CODE.INVALID_RESPONSE);

                        byte[] recv = new byte[len];
                        int got = 0;
                        netTimer.Start();
                        while (got < len)
                        {
                            int br = 0;
                            try
                            {
                                br = strm.Read(recv, got, len - got);
                                if (br == 0)
                                {
                                    netTimer.Stop();
                                    throw new CPS_Exception("Server unexpectedly closed connection").SetCode(CPS_Exception.ERROR_CODE.SOCKET_ERROR);
                                }
                            }
                            catch (Exception)
                            {
                                netTimer.Stop();
                                throw new CPS_Exception("Error receiving response").SetCode(CPS_Exception.ERROR_CODE.SOCKET_ERROR);
                            }
                            got += br;
                        }
                        strm.Close();
                        netTimer.Stop();

                        ms = new MemoryStream(recv);
                        pbs = new Protobuf_Streamer(ms);
                        pb = new Protobuf(pbs);

                        rawResponse = pb.GetStringField(1);
                    }
                    totTimer.Stop();

                    this.p_lastRequestDuration = totTimer.ElapsedMilliseconds;
                    this.p_lastRequestDuration = this.p_lastRequestDuration / 1000.0;

                    this.p_lastNetworkDuration = netTimer.ElapsedMilliseconds;
                    this.p_lastNetworkDuration = this.p_lastNetworkDuration / 1000.0;

                    this.p_lastResponseSize = rawResponse.Length;

                    if (this.p_debug)
                    {
                        FileStream fs = new FileStream("response.xml", FileMode.Create);
                        byte[] responseBytes = Encoding.UTF8.GetBytes(rawResponse);
                        fs.Write(responseBytes, 0, responseBytes.Length);
                        fs.Close();
                    }
                }
                catch (CPS_Exception e_)
                {
                    e = e_;
                }
                
                if (this.p_connectionSwitcher != null)
                    quit = !this.p_connectionSwitcher.shouldRetry(rawResponse, e);
                else
                    quit = true;

                if (quit && e != null)
                    throw e;
            }
            while (!quit);

            switch (request.getCommand())
            {
                case "search":
                case "similar":
                    return new CPS_SearchResponse(this, request, rawResponse);
                case "update":
                case "delete":
                case "replace":
                case "partial-replace":
                case "partial-xreplace":
                case "insert":
                case "create-alert":
                case "update-alerts":
                case "delete-alerts":
                    return new CPS_ModifyResponse(this, request, rawResponse);
                case "alternatives":
                    return new CPS_AlternativesResponse(this, request, rawResponse);
                case "list-words":
                    return new CPS_ListWordsResponse(this, request, rawResponse);
                case "status":
                    return new CPS_StatusResponse(this, request, rawResponse);
                case "retrieve":
                case "list-last":
                case "list-first":
                case "retrieve-last":
                case "retrive-first":
                case "lookup":
                case "show-history":
                    return new CPS_LookupResponse(this, request, rawResponse);
                case "search-delete":
                    return new CPS_SearchDeleteResponse(this, request, rawResponse);
                case "list-paths":
                    return new CPS_ListPathsResponse(this, request, rawResponse);
                case "list-facets":
                    return new CPS_ListFacetsResponse(this, request, rawResponse);
                case "list-alerts":
                    return new CPS_Response(this, request, rawResponse);
                    // TODO: change this !!!
                default:
                    CPS_Response ret = new CPS_Response(this, request, rawResponse);
                    // This is explicitly processed here, because of .NET limitations. PHP API changes this directly from CPS_Response constructor.
                    if (request.getCommand() == "begin-transaction" || request.getCommand() == "commit-transaction" || request.getCommand() == "rollback-transaction")
                        this.p_transactionId = ret.getTransactionId();
                    return ret;
            }
        }

        /// <summary>
        /// Sets new application ID or name to use in requests. This can be usefull to identify requests in log file.
        /// </summary>
        /// <param name="applicationId">Application ID or name.</param>
        public void setApplicationId(string applicationId)
        {
            this.p_applicationId = applicationId;
        }

        /// <summary>
        /// Enables or disables debug mode.
        /// </summary>
        /// <param name="debugMode">True or False to enable or disable debug mode.</param>
        public void setDebug(bool debugMode)
        {
            this.p_debug = debugMode;
            if (this.p_connectionSwitcher != null)
                this.p_connectionSwitcher.setDebug(debugMode);
        }

        /// <summary>
        /// Enables or disables resetting document IDs when modifying. On by default. You should change this setting if You plan to insert documents with auto-incremented IDs or with IDs already integrated into the document.
        /// </summary>
        /// <param name="resetIds">True or False to enable or disable document IDs resetting.</param>
        public void setDocIdResetting(bool resetIds)
        {
            this.p_docid_resetting = resetIds;
        }

        /// <summary>
        /// Sets HMAC keys used for this connection, this overrides username/password passed in constructor.
        /// </summary>
        /// <param name="userKey">User's key (or sometimes called public key), used for identifying user. This is transmitted over network.</param>
        /// <param name="signKey">Signature key (or sometimes called private key), used for signing requests. This is NOT transmitted over network.</param>
        public void setHMACKeys(string userKey, string signKey)
        {
            this.p_hmacUserKey = userKey;
            this.p_hmacSignKey = signKey;
            this.p_username = "";
            this.p_password = "";
        }

        /// <summary>
        /// Renders request XML for sending to storage.
        /// </summary>
        /// <param name="request">Request instance to render.</param>
        /// <returns>XML request as string.</returns>
        private string renderRequest(CPS_Request request)
        {
            Dictionary<string, string> envelopeFields = new Dictionary<string, string>();
            envelopeFields["storage"] = this.p_storageName;
            envelopeFields["user"] = this.p_username;
            envelopeFields["password"] = this.p_password;
            envelopeFields["command"] = request.getCommand();
            if (this.p_customEnvelopeParams != null)
                foreach (KeyValuePair<string, string> cep in this.p_customEnvelopeParams)
                    envelopeFields[cep.Key] = cep.Value;

            if (request.getRequestId().Length > 0)
                envelopeFields["requestid"] = request.getRequestId();
            if (this.p_applicationId.Length > 0)
                envelopeFields["application"] = this.p_applicationId;
            if (request.getRequestType().Length > 0)
                envelopeFields["type"] = request.getRequestType();
            if (request.getClusterLabel().Length > 0)
                envelopeFields["label"] = request.getClusterLabel();

            return request.getRequestXML(this.p_documentRootXpath, this.p_documentIdXpath, envelopeFields, this.p_docid_resetting);
        }

        /// <summary>
        /// Gets document root tag name.
        /// </summary>
        /// <returns>Tag name string.</returns>
        public string getDocumentRootXpath()
        {
            return this.p_documentRootXpath;
        }

        /// <summary>
        /// Gets parsed document ID xpath.
        /// </summary>
        /// <returns>List of strings with tag names.</returns>
        public List<string> getDocumentIdXpath(int fallback = 0)
        {
            if (fallback == 0)
                return this.p_documentIdXpath;
            else
            {
                List<string> ret = new List<string>(this.p_documentIdXpath);
                if (fallback >= 1 && ret.Count >= 1)
                    ret[0] = "document";
                if (fallback >= 2 && ret.Count >= 2)
                    ret[ret.Count - 1] = "id";
                return ret;
            }
        }

        /// <summary>
        /// Gets last request total duration in seconds.
        /// </summary>
        /// <returns>Duration in seconds as double.</returns>
        public double getLastRequestDuration()
        {
            return this.p_lastRequestDuration;
        }

        /// <summary>
        /// Gets last request network (request processing + request/response transmit time) duration in seconds, could be imprecise in case of HTTP connection.
        /// </summary>
        /// <returns>Duration in seconds as double.</returns>
        public double getLastNetworkDuration()
        {
            return this.p_lastNetworkDuration;
        }

        /// <summary>
        /// Gets last request size in UTF8 characters.
        /// </summary>
        /// <returns>Request UTF8 character count.</returns>
        public int getLastRequestSize()
        {
            return this.p_lastRequestSize;
        }

        /// <summary>
        /// Last response size in UTF8 characters.
        /// </summary>
        /// <returns>Response UTF8 character count.</returns>
        public int getLastResponseSize()
        {
            return this.p_lastResponseSize;
        }

        /// <summary>
        /// Returns the info on where the last connection was made.
        /// </summary>
        /// <returns>Last connection string.</returns>
        public string getLastConnectionInfo()
        {
            // TODO: check if this return something valid
            if (this.p_connectionString != null)
                return this.p_connectionString.ToString();
            else
                return "";
        }

        /// <summary>
        /// Clears transaction context from connection object.
        /// </summary>
        public void clearTransactionId()
        {
            this.p_transactionId = null;
        }

        /// <summary>
        /// Parses connection string to Uri class
        /// </summary>
        /// <param name="connstr">Connection string as string</param>
        /// <returns>Connection string as Uri</returns>
        private Uri parseConnectionString(string connstr)
        {
            Uri ret = null;
            try
            {
                ret = new Uri(connstr);
            }
            catch (Exception)
            {
                throw new CPS_Exception("Invalid connection string").SetCode(CPS_Exception.ERROR_CODE.INVALID_CONNECTION_STRING);
            }

            if (ret.Scheme.ToLower() != "http" && ret.Scheme.ToLower() != "tcp" && ret.Scheme.ToLower() != "tcps")
                throw new CPS_Exception("Invalid connection string").SetCode(CPS_Exception.ERROR_CODE.INVALID_CONNECTION_STRING);

            if (ret.Host.Length <= 0)
                throw new CPS_Exception("Invalid connection string").SetCode(CPS_Exception.ERROR_CODE.INVALID_CONNECTION_STRING);

            return ret;
        }

        /// <summary>
        /// Generates byte array with CPS TCP header.
        /// </summary>
        /// <param name="length">Request length.</param>
        /// <returns>Header as 8 byte array.</returns>
        private byte[] CPS_Length2Header(int length)
        {
            byte[] ret = new byte[8];
            ret[0] = 0x09;
            ret[1] = 0x09;
            ret[2] = 0x00;
            ret[3] = 0x00;
            ret[4] = (byte)((length & 0x000000FF));
            ret[5] = (byte)((length & 0x0000FF00) >> 8);
            ret[6] = (byte)((length & 0x00FF0000) >> 16);
            ret[7] = (byte)((length & 0xFF000000) >> 24);
            return ret;
        }

        /// <summary>
        /// Extracts response length from received header.
        /// </summary>
        /// <param name="data">8 byte long header.</param>
        /// <returns>Response length.</returns>
        private int CPS_Header2Length(byte[] data)
        {
            if (data.Length >= 8 && data[0] == 0x09 && data[1] == 0x09 && data[2] == 0x00 && data[3] == 0x00)
            {
                int int4 = data[4], int5 = data[5], int6 = data[6], int7 = data[7];
                return int4 | int5 << 8 | int6 << 16 | int7 << 24;
            }
            else
                return 0;
        }

        /// <summary>
        /// Connection string.
        /// </summary>
        private Uri p_connectionString;
        /// <summary>
        /// LoadBalancer object.
        /// </summary>
        private CPS_LoadBalancer p_connectionSwitcher;
        /// <summary>
        /// Storage name.
        /// </summary>
        private string p_storageName;
        /// <summary>
        /// User name.
        /// </summary>
        private string p_username;
        /// <summary>
        /// Password.
        /// </summary>
        private string p_password;
        /// <summary>
        /// Document root tag name.
        /// </summary>
        private string p_documentRootXpath;
        /// <summary>
        /// Parsed document ID xpath.
        /// </summary>
        private List<string> p_documentIdXpath;
        /// <summary>
        /// Application ID or name.
        /// </summary>
        private string p_applicationId;
        /// <summary>
        /// Debug mode.
        /// </summary>
        private bool p_debug;
        /// <summary>
        /// Flag for document ids resetting.
        /// </summary>
        private bool p_docid_resetting;
        /// <summary>
        /// Last request total duration in seconds.
        /// </summary>
        private double p_lastRequestDuration;
        /// <summary>
        /// Last request network (request processing + request/response transmit time) duration in seconds, could be imprecise in case of HTTP connection.
        /// </summary>
        private double p_lastNetworkDuration;
        /// <summary>
        /// Last request size in UTF8 characters.
        /// </summary>
        private int p_lastRequestSize;
        /// <summary>
        /// Last response size in UTF8 characters.
        /// </summary>
        private int p_lastResponseSize;
        /// <summary>
        /// User's key (or sometimes called public key), used for identifying user. This is transmitted over network.
        /// </summary>
        private string p_hmacUserKey;
        /// <summary>
        /// Signature key (or sometimes called private key), used for signing requests. This is NOT transmitted over network.
        /// </summary>
        private string p_hmacSignKey;
        /// <summary>
        /// Transaction ID.
        /// </summary>
        public string p_transactionId;
        /// <summary>
        /// Additional request envelope parameters.
        /// </summary>
        private Dictionary<string, string> p_customEnvelopeParams;
        /// <summary>
        /// Random object.
        /// </summary>
        private Random p_random;
        /// <summary>
        /// Specifies user-specified SSL CA used for connection certificate validation.
        /// </summary>
        private string p_sslCustomCA;
        /// <summary>
        /// Specifies user-specified SSL Common Name for connection certification validation.
        /// </summary>
        private string p_sslCustomCN;
    }

    /// <summary>
    /// CPS Request class defines single request for CPS storage.
    /// </summary>
    public class CPS_Request
    {
        /// <summary>
        /// Creates new CPS Request class instance.
        /// </summary>
        /// <param name="command">API command for this request.</param>
        /// <param name="requestId">Request ID to identify it in log files.</param>
        public CPS_Request(string command, string requestId = "")
        {
            this.p_command = command;
            this.p_requestId = requestId;
            this.p_label = "";
            this.p_requestType = "";
            this.p_textParams = new Dictionary<string, List<string>>();
            this.p_rawParams = new Dictionary<string, List<string>>();
            this.p_documents = null;// new Dictionary<string, string>();
            this.p_requestDom = null;
            this.p_extraXmlParam = null;
            this.p_changesets = null;
        }

        /// <summary>
        /// Converts current request to XML string.
        /// </summary>
        /// <param name="docRootXpath">Document root tag name.</param>
        /// <param name="docIdXpath">Parsed document ID xpath.</param>
        /// <param name="envelopeParams">Additional CPS envelope parameters.</param>
        /// <param name="resetDocIds">Flag to reset document IDs.</param>
        /// <returns>Request as XML string.</returns>
        virtual public string getRequestXML(string docRootXpath, List<string> docIdXpath, Dictionary<string, string> envelopeParams, bool resetDocIds)
        {
            this.p_requestDom = new XmlDocument();
            this.p_requestDom.AppendChild(this.p_requestDom.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            XmlElement root = this.p_requestDom.CreateElement("cps:request", "www.clusterpoint.com");
            this.p_requestDom.AppendChild(root);

            foreach (KeyValuePair<string, string> param in envelopeParams)
            {
                root.AppendChild(this.p_requestDom.CreateElement("cps:" + param.Key, "www.clusterpoint.com")).InnerXml = param.Value;
            }

            XmlElement contentTag = (XmlElement)root.AppendChild(this.p_requestDom.CreateElement("cps:content", "www.clusterpoint.com"));

            foreach (KeyValuePair<string, List<string>> param in this.p_textParams)
            {
                foreach (string param_value in param.Value)
                    contentTag.AppendChild(this.p_requestDom.CreateElement(param.Key)).InnerXml = param_value;
            }

            foreach (KeyValuePair<string, List<string>> param in this.p_rawParams)
            {
                foreach (string param_value in param.Value)
                {
                    XmlElement tag = this.p_requestDom.CreateElement(param.Key);
                    XmlDocumentFragment fragment = this.p_requestDom.CreateDocumentFragment();
                    fragment.InnerXml = param_value;
                    tag.AppendChild(fragment);
                    contentTag.AppendChild(tag);
                }
            }

            if (this.p_changesets != null && this.p_changesets.Count > 0)
            {
                string xmlContent = "";
                if (this.p_changesets.Count > 1)
                {
                    int i = 0;
                    for (i = 0; i < this.p_changesets.Count; i++)
                    {
                        xmlContent += "<cps:changeset>";
                        xmlContent += this.p_changesets[i].renderXML(docIdXpath);
                        xmlContent += "</cps:changeset>";
                    }
                }
                else
                    xmlContent += this.p_changesets[0].renderXML(docIdXpath);

                if (xmlContent.Length > 0)
                {
                    XmlDocument tmpDoc = new XmlDocument();
                    tmpDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<fakedoc xmlns:cps=\"www.clusterpoint.com\">" + xmlContent + "</fakedoc>");
                    foreach (XmlNode tmpNode in tmpDoc.DocumentElement.ChildNodes)
                        contentTag.AppendChild(this.p_requestDom.ImportNode(tmpNode, true));
                }
            }

            if (this.p_extraXmlParam != null)
            {
                XmlDocumentFragment fragment = this.p_requestDom.CreateDocumentFragment();
                fragment.InnerXml = this.p_extraXmlParam;
                contentTag.AppendChild(fragment);
            }

            if (this.p_documents != null)
                foreach (KeyValuePair<string, XmlElement> pair in this.p_documents)
                {
                    int i = 0;
                    XmlElement xid = null;

                    if (pair.Value == null)
                    {
                        xid = contentTag;

                        for (i = 0; i < docIdXpath.Count; i++)
                            xid = (XmlElement)(xid.AppendChild(xid.OwnerDocument.CreateElement(docIdXpath[i])));

                        xid.AppendChild(xid.OwnerDocument.CreateTextNode(pair.Key));

                        continue;
                    }

                    XmlElement xdoc = (XmlElement)(contentTag.AppendChild(contentTag.OwnerDocument.ImportNode(pair.Value, true)));
                    if (!resetDocIds)
                        continue;
                    xid = xdoc;
                    for (i = 1; i < docIdXpath.Count; i++)
                    {
                        XmlElement xchild = xid[docIdXpath[i]];
                        if (xchild == null)
                            xchild = (XmlElement)(xid.AppendChild(xid.OwnerDocument.CreateElement(docIdXpath[i])));

                        if (i + 1 == docIdXpath.Count)
                            if (xchild.InnerXml != pair.Key)
                                xchild.AppendChild(xchild.OwnerDocument.CreateTextNode(pair.Key));

                        xid = xchild;
                    }
                }

            return this.p_requestDom.OuterXml;
        }

        /// <summary>
        /// Returns the string with control characters stripped.
        /// </summary>
        /// <param name="src">Source string.</param>
        /// <returns>String with control characters stripped.</returns>
        public static string getValidXmlValue(string src)
        {
            string ret = src;
            for (char c = (char)0x00; c <= 0x1f; c++)
                if (c != 0x09 && c != 0x0a && c != 0x0d)
                    ret = ret.Replace(c, ' ');
            return ret;
        }

        /// <summary>
        /// Gets current command.
        /// </summary>
        /// <returns>Command name.</returns>
        public string getCommand()
        {
            return this.p_command;
        }

        /// <summary>
        /// Sets request parameter as single string value.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        public void setParam(string name, string value)
        {
            if (Array.Exists<string>(CPS_Request.p_textParamNames, pn => pn == name))
                this.setTextParam(name, value);
            else if (Array.Exists<string>(CPS_Request.p_rawParamNames, pn => pn == name))
                this.setRawParam(name, value);
            else
            {
                // PHP throws exception, should we do the same?
            }
        }

        /// <summary>
        /// Sets request parameter as multiple string values.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">List of parameter values.</param>
        public void setParam(string name, List<string> value)
        {
            if (Array.Exists<string>(CPS_Request.p_textParamNames, pn => pn == name))
                this.setTextParam(name, value);
            else if (Array.Exists<string>(CPS_Request.p_rawParamNames, pn => pn == name))
                this.setRawParam(name, value);
            else
            {
                // PHP throws exception, should we do the same?
            }
        }

        /// <summary>
        /// Sets extra XML to send in the content tag of the request. Only use this if you know what you are doing.
        /// </summary>
        /// <param name="value">Extra XML as a string.</param>
        public void setExtraXmlParam(string value)
        {
            this.p_extraXmlParam = value;
        }

        /// <summary>
        /// Gets current request ID.
        /// </summary>
        /// <returns>Request ID.</returns>
        public string getRequestId()
        {
            return this.p_requestId;
        }

        /// <summary>
        /// Sets current request ID.
        /// </summary>
        /// <param name="requestId">Request ID.</param>
        public void setRequestId(string requestId)
        {
            this.p_requestId = requestId;
        }

        /// <summary>
        /// Sets label for cluster nodeset.
        /// </summary>
        /// <param name="label">Label value.</param>
        public void setClusterLabel(string label)
        {
            this.p_label = label;
        }

        /// <summary>
        /// Gets label for cluster nodeset.
        /// </summary>
        /// <returns>Label value.</returns>
        public string getClusterLabel()
        {
            return this.p_label;
        }

        /// <summary>
        /// Gets current request type.
        /// </summary>
        /// <returns>Request type.</returns>
        public string getRequestType()
        {
            return this.p_requestType;
        }

        /// <summary>
        /// Sets current request type. Do not do this unless 100% sure what you are doing!
        /// </summary>
        /// <param name="requestType">Request type.</param>
        public void setRequestType(string requestType)
        {
            this.p_requestType = requestType;
        }

        /// <summary>
        /// Sets documents for this request.
        /// Currently supported types are:
        /// * Dictionary&lt;string, CPS_SimpleXML&gt;;
        /// * Dictionary&lt;string, XmlElement&gt;;
        /// * List&lt;string&gt;.
        /// </summary>
        /// <param name="documents">Documents.</param>
        protected void setRawDocuments(Object documents)
        {
            try
            {
                Dictionary<string, XmlElement> documents_type1 = (Dictionary<string, XmlElement>)documents;
                if (documents_type1 != null)
                {
                    this.p_documents = documents_type1;
                    return;
                }
            }
            catch (Exception)
            {
                // do nothing
            }

            try
            {
                Dictionary<string, CPS_SimpleXML> documents_type2 = (Dictionary<string, CPS_SimpleXML>)documents;
                if (documents_type2 != null)
                {
                    this.p_documents = new Dictionary<string, XmlElement>();
                    foreach (KeyValuePair<string, CPS_SimpleXML> pair in documents_type2)
                        this.p_documents[pair.Key] = pair.Value.ConvertToXML();
                    return;
                }
            }
            catch (Exception)
            {
                // do nothing
            }

            try
            {
                Dictionary<string, Object> documents_type3 = (Dictionary<string, Object>)documents;
                if (documents_type3 != null)
                {
                    this.p_documents = new Dictionary<string, XmlElement>();
                    foreach (KeyValuePair<string, Object> pair in documents_type3)
                    {
                        try
                        {
                            XmlElement xval = (XmlElement)(pair.Value);
                            if (xval != null)
                            {
                                this.p_documents[pair.Key] = xval;
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            // do nothing
                        }

                        try
                        {
                            CPS_SimpleXML sval = (CPS_SimpleXML)(pair.Value);
                            if (sval != null)
                            {
                                this.p_documents[pair.Key] = sval.ConvertToXML();
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                            // do nothing
                        }

                        throw new CPS_Exception("Invalid passed document dictionary element type");
                    }
                    return;
                }
            }
            catch (Exception)
            {
                // do nothing
            }

            try
            {
                List<string> documents_type4 = (List<string>)documents;
                if (documents_type4 != null)
                {
                    this.p_documents = new Dictionary<string, XmlElement>();
                    foreach (string docid in documents_type4)
                        this.p_documents[docid] = null;
                    return;
                }
            }
            catch (Exception)
            {
                // do nothing
            }

            throw new CPS_Exception("Invalid passed document type");
        }

        /// <summary>
        /// Sets text request parameter as single value.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        private void setTextParam(string name, string value)
        {
            List<string> value_array = new List<string>();
            value_array.Add(value);
            this.setTextParam(name, value_array);
        }

        /// <summary>
        /// Sets text request parameter as multiple values.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">List of parameter values.</param>
        private void setTextParam(string name, List<string> value)
        {
            this.p_textParams[name] = value;
        }

        /// <summary>
        /// Sets raw request parameter as single value.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        private void setRawParam(string name, string value)
        {
            List<string> value_array = new List<string>();
            value_array.Add(value);
            this.setRawParam(name, value_array);
        }

        /// <summary>
        /// Sets raw request parameter as multiple values.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">List of parameter values.</param>
        private void setRawParam(string name, List<string> value)
        {
            this.p_rawParams[name] = value;
        }

        /// <summary>
        /// Command name.
        /// </summary>
        private string p_command;
        /// <summary>
        /// Request ID.
        /// </summary>
        private string p_requestId;
        /// <summary>
        /// Request type.
        /// </summary>
        private string p_requestType;
        /// <summary>
        /// Dictionary of text parameters.
        /// </summary>
        private Dictionary<string, List<string>> p_textParams;
        /// <summary>
        /// Dictionary of raw parameters.
        /// </summary>
        private Dictionary<string, List<string>> p_rawParams;
        /// <summary>
        /// Request documents.
        /// </summary>
        private Dictionary<string, XmlElement> p_documents;
        /// <summary>
        /// Request XML DOM object.
        /// </summary>
        private XmlDocument p_requestDom;
        /// <summary>
        /// List of changesets for partial-xreplace command.
        /// </summary>
        protected List<CPS_PRX_Changeset> p_changesets;

        /// <summary>
        /// List of available text parameter names.
        /// </summary>
        private static string[] p_textParamNames = {
            "added_external_id",
		    "added_id",
            "aggregate",
            "alert_id",
		    "case_sensitive",
            "count",
		    "cr",
            "create_cursor",
            "cursor_id",
            "cursor_data",
		    "deleted_external_id",
		    "deleted_id",
		    "description",
		    "docs",
		    "exact-match",
		    "facet",
            "facet_size",
		    "fail_if_exists",
		    "file",
		    "finalize",
		    "for",
		    "force",
            "force_precise_results",
            "force_segment",
		    "from",
		    "full",
		    "group",
		    "group_size",
		    "h",
		    "id",
		    "idif",
		    "iterator_id",
		    "len",
		    "message",
		    "offset",
            "optimize_to",
		    "path",
		    "persistent",
		    "position",
		    "quota",
		    "rate2_ordering",
		    "rate_from",
		    "rate_to",
		    "relevance",
            "return_doc",
		    "return_internal",
		    "sequence_check",
            "sql",
		    "stem-lang",
		    "step_size",
		    "text",
            "transaction_id",
		    "type"
        };

        /// <summary>
        /// List of available raw parameter names.
        /// </summary>
        private static string[] p_rawParamNames = {
            "alert",
            "query",
		    "list",
		    "ordering"
        };
        /// <summary>
        /// Extra XML parameter for request content.
        /// </summary>
        private string p_extraXmlParam;
        /// <summary>
        /// Label for cluster nodeset.
        /// </summary>
        private string p_label;
    }

    /// <summary>
    /// This class, which is derived from the CPS_Request class, can be used to send pre-formed XML requests to the storage.
    /// If you already have the XML request, you can send it to the API by using this class. Note that parameters from the CPS_Connection
    /// object such as storage name, username and password will not be used to form the request.
    /// </summary>
    public class CPS_StaticRequest : CPS_Request
    {
        /// <summary>
        /// Constructs an instance of the CPS_StaticRequest class.
        /// </summary>
        /// <param name="xml">Full XML string to send.</param>
        /// <param name="command">Specifies which command is being sent. This is not mandatory for sending
        /// the request per se, but how the response will be parsed will depend on this setting, so if you
        /// want full response parsing from the API, you should specify the command here. Note that setting this
        /// parameter will not change the XML being sent to the database</param>
        /// <param name="requestId">Request ID to identify it in log files.</param>
        public CPS_StaticRequest(string xml, string command = "", string requestId = "")
            : base(command, requestId)
        {
            this.p_renderedXml = xml;
        }

        /// <summary>
        /// Gets current request to XML string.
        /// </summary>
        /// <param name="docRootXpath">Ignored parameter, just for method signature purposes.</param>
        /// <param name="docIdXpath">Ignored parameter, just for method signature purposes.</param>
        /// <param name="envelopeParams">Ignored parameter, just for method signature purposes.</param>
        /// <param name="resetDocIds">Ignored parameter, just for method signature purposes.</param>
        /// <returns>Request as XML string.</returns>
        override public string getRequestXML(string docRootXpath, List<string> docIdXpath, Dictionary<string, string> envelopeParams, bool resetDocIds)
        {
            return this.p_renderedXml;
        }

        /// <summary>
        /// Full XML request.
        /// </summary>
        private string p_renderedXml;
    }

    /// <summary>
    /// CPS Response class contains single response from CPS storage.
    /// </summary>
    public class CPS_Response
    {
        /// <summary>
        /// Creates new instance of CPS Response class.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        /// <param name="responseXml">Raw response XML as string.</param>
        public CPS_Response(CPS_Connection connection, CPS_Request request, string responseXml)
        {
            this.p_connection = connection;
            this.p_transactionId = null;
            this.p_simpleXml = new XmlDocument();
            try
            {
                this.p_simpleXml.LoadXml(responseXml);
            }
            catch (Exception)
            {
                throw new CPS_Exception("Received invalid XML response").SetCode(CPS_Exception.ERROR_CODE.INVALID_RESPONSE);
            }

            XmlElement content = null;
            this.p_errors = new List<CPS_SimpleXML>();
            foreach (XmlElement xmle in this.p_simpleXml.DocumentElement.ChildNodes)
            {
                if (xmle.Name == "cps:storage")
                    this.p_storageName = xmle.InnerXml;
                else if (xmle.Name == "cps:command")
                    this.p_command = xmle.InnerXml;
                else if (xmle.Name == "cps:seconds")
                {
                    try
                    {
                        this.p_seconds = float.Parse(xmle.InnerXml, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch (Exception)
                    {
                        this.p_seconds = 0;
                    }
                }
                else if (xmle.Name == "cps:error")
                {
                    this.p_errors.Add(new CPS_SimpleXML(xmle));
                }
                else if (xmle.Name == "cps:content")
                    content = xmle;
            }

            foreach (CPS_SimpleXML sxer in this.p_errors)
            {
                string elvl = sxer["level"];
                if (elvl == "REJECTED" || elvl == "FAILED" || elvl == "ERROR" || elvl == "FATAL")
                    throw new CPS_Exception(sxer["message"]).SetCode(sxer["code"]);
            }

            this.p_textParams = new Dictionary<string, string>();
            this.p_contentArray = null;
            this.p_documents = new Dictionary<string, XmlElement>();
            this.p_facets = new Dictionary<string, Dictionary<string, int>>();
            bool saveDocs = true;
            this.p_words = null;
            this.p_wordCounts = new Dictionary<string, int>();
            this.p_aggregate = new Dictionary<string, List<XmlElement>>();

            if (content != null)
            {
                switch (this.p_command)
                {
                    case "update":
                    case "delete":
                    case "insert":
                    case "partial-replace":
                    case "partial-xreplace":
                    case "replace":
                    case "lookup":
                        if (this.p_command != "lookup")
                            saveDocs = false;
                        break;
                    case "search":
                    case "retrieve":
                    case "retrieve-last":
                    case "retrieve-first":
                    case "list-last":
                    case "list-first":
                    case "similar":
                    case "show-history":
                    case "cursor-next-batch":
                        // In PHP here comes document extraction. In C# document extraction is later, so nothing to do here
                        break;
                    case "alternatives":
                        Dictionary<string, Dictionary<string, CPS_AlternativesResponse.WordInfo>> alt_words = new Dictionary<string, Dictionary<string, CPS_AlternativesResponse.WordInfo>>();
                        foreach (XmlElement xaltlist in content.ChildNodes)
                            foreach (XmlElement xalt in xaltlist.ChildNodes)
                            {
                                if (xalt.Name == "alternatives")
                                {
                                    Dictionary<string, CPS_AlternativesResponse.WordInfo> winfo = new Dictionary<string, CPS_AlternativesResponse.WordInfo>();
                                    XmlElement xto = xalt["to"];
                                    if (xto == null)
                                        continue;
                                    XmlElement xto_count = xalt["count"];
                                    if (xto_count == null)
                                        continue;

                                    int ito_count = 0;
                                    try
                                    {
                                        ito_count = int.Parse(xto_count.InnerXml);
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }

                                    foreach (XmlElement xword in xalt.ChildNodes)
                                    {
                                        if (xword.Name == "word")
                                        {
                                            XmlAttribute xcount = xword.Attributes["count"];
                                            XmlAttribute xcr = xword.Attributes["cr"];
                                            XmlAttribute xidif = xword.Attributes["idif"];
                                            XmlAttribute xh = xword.Attributes["h"];
                                            if (xcount == null || xcr == null || xidif == null || xh == null)
                                                continue;

                                            CPS_AlternativesResponse.WordInfo info = new CPS_AlternativesResponse.WordInfo();
                                            info.word = xword.InnerXml;

                                            try
                                            {
                                                info.count = int.Parse(xcount.Value);
                                                info.cr = float.Parse(xcr.Value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                                info.idif = float.Parse(xidif.Value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                                info.h = float.Parse(xh.Value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }

                                            winfo[info.word] = info;
                                        }
                                    }

                                    alt_words[xto.InnerXml] = winfo;
                                    this.p_wordCounts[xto.InnerXml] = ito_count;
                                }
                            }
                        this.p_words = alt_words;
                        break;
                    case "list-words":
                        Dictionary<string, Dictionary<string, CPS_ListWordsResponse.WordInfo>> list_words = new Dictionary<string, Dictionary<string, CPS_ListWordsResponse.WordInfo>>();
                        foreach (XmlElement xlist in content.ChildNodes)
                        {
                            if (xlist.Name == "list")
                            {
                                Dictionary<string, CPS_ListWordsResponse.WordInfo> winfo = new Dictionary<string, CPS_ListWordsResponse.WordInfo>();
                                XmlAttribute xto = xlist.Attributes["to"];
                                if (xto == null)
                                    continue;

                                foreach (XmlElement xword in xlist.ChildNodes)
                                {
                                    if (xword.Name == "word")
                                    {
                                        XmlAttribute xcount = xword.Attributes["count"];
                                        if (xcount == null)
                                            continue;

                                        CPS_ListWordsResponse.WordInfo info = new CPS_ListWordsResponse.WordInfo();
                                        info.word = xword.InnerXml;

                                        try
                                        {
                                            info.count = int.Parse(xcount.Value);
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }

                                        winfo[info.word] = info;
                                    }
                                }

                                list_words[xto.Value] = winfo;
                            }
                        }
                        this.p_words = list_words;
                        break;
                    case "status":
                    case "list-paths":
                    case "list-databases":
                    case "create-database":
                    case "edit-database":
                    case "rename-database":
                    case "drop-database":
                    case "describe-database":
                    case "list-nodes":
                    case "list-hubs":
                    case "list-hosts":
                    case "set-offline":
                    case "set-online":
                    case "list-alerts":
                        this.p_contentArray = content;
                        break;
                    case "begin-transaction":
                        try
                        {
                            this.p_transactionId = content["transaction_id"].InnerXml;
                        }
                        catch (Exception)
                        {
                            this.p_transactionId = null;
                        }
                        break;
                    case "commit-transaction":
                    case "rollback-transaction":
                        this.p_transactionId = null;
                        break;
                    default:
                        // nothing special
                        break;
                }

                if (this.p_command == "search" || this.p_command == "list-facets")
                {
                    foreach (XmlElement xmle in content.ChildNodes)
                    {
                        if (xmle.Name == "facet")
                        {
                            XmlAttribute xattr = xmle.Attributes["path"];
                            if (xattr == null || xattr.Value.Length <= 0)
                                continue;

                            string path = xattr.Value;
                            this.p_facets[path] = new Dictionary<string, int>();

                            foreach (XmlElement xmlt in xmle.ChildNodes)
                            {
                                if (xmlt.Name == "term")
                                {
                                    this.p_facets[path][xmlt.InnerXml] = 0;

                                    if (this.p_command == "search")
                                    {
                                        xattr = xmlt.Attributes["hits"];
                                        if (xattr != null && xattr.Value.Length > 0)
                                        {
                                            try
                                            {
                                                this.p_facets[path][xmlt.InnerXml] = int.Parse(xattr.Value);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (xmle.Name == "aggregate")
                        {
                            string query = "";
                            try
                            {
                                XmlElement qel = xmle["query"];
                                if (qel != null)
                                    query = qel.InnerXml;
                            }
                            catch (Exception)
                            {
                            }

                            if (query.Length > 0)
                            {
                                this.p_aggregate[query] = new List<XmlElement>();
                                foreach (XmlElement xmld in xmle.ChildNodes)
                                    if (xmld.Name == "data")
                                        this.p_aggregate[query].Add(xmld);
                            }
                        }
                    }
                }

                foreach (XmlElement xmle in content.ChildNodes)
                {
                    if (Array.Exists<string>(CPS_Response.p_textParamNames, pn => pn == xmle.Name))
                        this.p_textParams[xmle.Name] = xmle.InnerXml;
                    else if (xmle.Name == "results")
                    {
                        foreach (XmlElement xmld in xmle.ChildNodes)
                            this.ProcessRawDocument(connection, xmld, saveDocs);
                    }
                    else if (xmle.Name == connection.getDocumentRootXpath() ||
                        (xmle.Name == "document" && (this.p_command == "insert" || this.p_command == "update" || this.p_command == "replace" || this.p_command == "partial-replace" || this.p_command == "partial-xreplace" || this.p_command == "delete"))
                        )
                        this.ProcessRawDocument(connection, xmle, saveDocs);
                }
            }
        }

        /// <summary>
        /// Creates new empty instance of CPS Response class without parsing response XML.
        /// </summary>
        /// <param name="connection">CPS_Connection class instance.</param>
        /// <param name="request">CPS_Request class instance.</param>
        protected CPS_Response(CPS_Connection connection, CPS_Request request)
        {
            this.p_connection = connection;
            this.p_transactionId = null;
            this.p_simpleXml = null;
            this.p_errors = new List<CPS_SimpleXML>();
            this.p_textParams = new Dictionary<string, string>();
            this.p_contentArray = null;
            this.p_documents = new Dictionary<string, XmlElement>();
            this.p_facets = new Dictionary<string, Dictionary<string, int>>();
            this.p_words = null;
            this.p_wordCounts = new Dictionary<string, int>();
            this.p_aggregate = new Dictionary<string, List<XmlElement>>();
        }

        private void ProcessRawDocument(CPS_Connection connection, XmlElement xmld, bool saveDocs)
        {
            if (xmld.Name == connection.getDocumentRootXpath() ||
                (xmld.Name == "document" && (this.p_command == "insert" || this.p_command == "update" || this.p_command == "replace" || this.p_command == "partial-replace" || this.p_command == "partial-xreplace" || this.p_command == "delete"))
                )
            {
                string id = "";

                if (this.p_command == "show-history")
                {
                    try
                    {
                        XmlElement xmlid = xmld["revision_id", "www.clusterpoint.com"];
                        id = xmlid.InnerXml;
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    for (int f = 0; f < 3; f++)
                    {
                        List<string> idPath = connection.getDocumentIdXpath(f);
                        XmlElement xmlid = xmld;
                        int i = 0;
                        for (i = 1; i < idPath.Count; i++)
                        {
                            if (xmlid[idPath[i]] != null)
                                xmlid = xmlid[idPath[i]];
                            else
                                break;

                            if (i + 1 == idPath.Count)
                                id = xmlid.InnerXml;
                        }

                        if (id.Length > 0)
                            break;
                    }
                }

                if (id.Length > 0)
                {
                    if (saveDocs)
                        this.p_documents[id] = xmld;
                    else
                        this.p_documents[id] = null;
                }
            }
        }

        /// <summary>
        /// Gets documents from response, returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, CPS_SimpleXML&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, XmlElement&gt;.
        /// </summary>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Documents as Dictionary&lt;string, CPS_SimpleXML&gt; or Dictionary&lt;string, XmlElement&gt;.</returns>
        public Object getRawDocuments(DOC_TYPE returnType)
        {
            if (returnType == DOC_TYPE.DOC_TYPE_SIMPLEXML)
            {
                Dictionary<string, CPS_SimpleXML> ret = new Dictionary<string, CPS_SimpleXML>();

                foreach (KeyValuePair<string, XmlElement> pair in this.p_documents)
                    ret[pair.Key] = new CPS_SimpleXML(pair.Value);

                return ret;
            }
            else if (returnType == DOC_TYPE.DOC_TYPE_ARRAY)
            {
                return null; // not supported yet
            }
            else // DOC_TYPE.DOC_TYPE_STDCLASS
                return this.p_documents;
        }

        /// <summary>
        /// Gets raw facets.
        /// </summary>
        /// <returns>Dictionary with facets.</returns>
        public Dictionary<string, Dictionary<string, int>> getRawFacets()
        {
            return this.p_facets;
        }

        /// <summary>
        /// Gets aggregate data from response, returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns Dictionary&lt;string, List&lt;CPS_SimpleXML&gt;&gt;.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns Dictionary&lt;string, List&gt;XmlElement&gt;&gt;.
        /// </summary>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Aggregate data as Dictionary&lt;string, List&lt;CPS_SimpleXML&gt;&gt; or Dictionary&lt;string, List&lt;XmlElement&gt;&gt;.</returns>
        public Object getRawAggregate(DOC_TYPE returnType)
        {
            if (returnType == DOC_TYPE.DOC_TYPE_SIMPLEXML)
            {
                Dictionary<string, List<CPS_SimpleXML>> ret = new Dictionary<string, List<CPS_SimpleXML>>();

                foreach (KeyValuePair<string, List<XmlElement>> pair in this.p_aggregate)
                {
                    ret[pair.Key] = new List<CPS_SimpleXML>();
                    foreach (XmlElement xmle in pair.Value)
                        ret[pair.Key].Add(new CPS_SimpleXML(xmle));
                }

                return ret;
            }
            else if (returnType == DOC_TYPE.DOC_TYPE_ARRAY)
            {
                return null; // not supported yet
            }
            else // DOC_TYPE.DOC_TYPE_STDCLASS
                return this.p_aggregate;
        }

        /// <summary>
        /// Returns word information for 'alternatives' and 'list-words' commands.
        /// </summary>
        /// <returns>Word information.</returns>
        public Object getRawWords()
        {
            return this.p_words;
        }

        /// <summary>
        /// Returns word counts for 'alternatives' command.
        /// </summary>
        /// <returns>Dictionary&lt;string, int&gt; with words and count pairs.</returns>
        public Dictionary<string, int> getRawWordCounts()
        {
            return this.p_wordCounts;
        }

        /// <summary>
        /// Gets response parameter value.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <returns>Parameter value.</returns>
        public string getParam(string name)
        {
            if (Array.Exists<string>(CPS_Response.p_textParamNames, pn => pn == name))
                return this.p_textParams[name];
            else
                throw new CPS_Exception("Invalid response parameter").SetCode(CPS_Exception.ERROR_CODE.INVALID_PARAMETER);
        }

        /// <summary>
        /// Gets response parameter value ar integer.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <returns>Parameter value as integer.</returns>
        public int getParamInt(string name)
        {
            try
            {
                return int.Parse(this.getParam(name));
            }
            catch (CPS_Exception ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets CPS storage processing time for this response.
        /// </summary>
        /// <returns>Seconds.</returns>
        public float getSeconds()
        {
            return this.p_seconds;
        }

        /// <summary>
        /// Gets response content for simple commands, like 'status', returned type depends on passed returnType parameter.
        /// If returnType is DOC_TYPE_SIMPLEXML, returns CPS_SimpleXML.
        /// If returnType is DOC_TYPE_ARRAY, returns null - not supported yet.
        /// If returnType is DOC_TYPE_STDCLASS, returns XmlElement.
        /// </summary>
        /// <param name="returnType">Defines used return type.</param>
        /// <returns>Content as CPS_SimpleXML or XmlElement.</returns>
        public Object getContentArray(DOC_TYPE returnType = DOC_TYPE.DOC_TYPE_STDCLASS)
        {
            if (returnType == DOC_TYPE.DOC_TYPE_SIMPLEXML)
                return new CPS_SimpleXML(this.p_contentArray);
            else if (returnType == DOC_TYPE.DOC_TYPE_ARRAY)
                return null; // not supported yet
            else // DOC_TYPE.DOC_TYPE_STDCLASS
                return this.p_contentArray;
        }

        /// <summary>
        /// Gets response errors.
        /// </summary>
        /// <returns>List of errors.</returns>
        public List<CPS_SimpleXML> getErrors()
        {
            return this.p_errors;
        }

        /// <summary>
        /// Response XML DOM object.
        /// </summary>
        private XmlDocument p_simpleXml;
        /// <summary>
        /// Storage name.
        /// </summary>
        private string p_storageName;
        /// <summary>
        /// Command name.
        /// </summary>
        private string p_command;
        /// <summary>
        /// Response processing time in seconds.
        /// </summary>
        private float p_seconds;
        /*/// <summary>
        /// PHP API contains unused variable hits.
        /// </summary>
        private int p_hits;
        /// <summary>
        /// PHP API contains unused variable found.
        /// </summary>
        private int p_found;
        /// <summary>
        /// PHP API contains unused variable from.
        /// </summary>
        private int p_from;
        /// <summary>
        /// PHP API contains unused variable to.
        /// </summary>
        private int p_to;*/
        /// <summary>
        /// List of errors
        /// </summary>
        private List<CPS_SimpleXML> p_errors;
        /// <summary>
        /// Response documents.
        /// </summary>
        protected Dictionary<string, XmlElement> p_documents;
        /// <summary>
        /// Response facets.
        /// </summary>
        protected Dictionary<string, Dictionary<string, int>> p_facets;
        /// <summary>
        /// Response text parameters.
        /// </summary>
        protected Dictionary<string, string> p_textParams;
        /// <summary>
        /// Response word information.
        /// </summary>
        protected Object p_words;
        /// <summary>
        /// Response word counts.
        /// </summary>
        protected Dictionary<string, int> p_wordCounts;
        /*/// <summary>
        /// PHP API contains unused variable paths.
        /// </summary>
        protected List<string> p_paths;*/
        /// <summary>
        /// Response content, not an array, naming is simply the same as for PHP API.
        /// </summary>
        protected XmlElement p_contentArray;
        /// <summary>
        /// Connection object.
        /// </summary>
        protected CPS_Connection p_connection;

        /// <summary>
        /// List of available response text parameter names.
        /// </summary>
        private static string[] p_textParamNames = {
            "hits",
		    "from",
		    "to",
		    "found",
		    "real_query",
		    "iterator_id",
            "more",
            "cursor_id",
            "cursor_data"
        };

        /// <summary>
        /// Available return document types.
        /// </summary>
        public enum DOC_TYPE
        {
            /// <summary>
            /// PHP's simpleXML-like <see cref="CPS.CPS_SimpleXML"/> class.
            /// </summary>
            DOC_TYPE_SIMPLEXML,
            /// <summary>
            /// Array, not supported yet.
            /// </summary>
            DOC_TYPE_ARRAY,
            /// <summary>
            /// .NET native XML object - <see cref="XmlElement"/>.
            /// </summary>
            DOC_TYPE_STDCLASS
        };

        /// <summary>
        /// Aggregate data.
        /// </summary>
        private Dictionary<string, List<XmlElement>> p_aggregate;

        /// <summary>
        /// Transaction ID, that is extracted from response. This differs from PHP API, because of .NET limitations.
        /// </summary>
        private string p_transactionId;

        /// <summary>
        /// Gets transaction ID from response. This differs from PHP API, because of .NET limitations.
        /// </summary>
        /// <returns>Transaction ID.</returns>
        public string getTransactionId()
        {
            return this.p_transactionId;
        }
    }

    /// <summary>
    /// CPS SimpleXML class provides simple way to get XML values, very similar to PHP's SimpleXML class.
    /// </summary>
    public class CPS_SimpleXML
    {
        /// <summary>
        /// Creates new CPS SimpleXML class instance from passed XmlElement instance.
        /// </summary>
        /// <param name="element">XmlElement class instance.</param>
        public CPS_SimpleXML(XmlElement element)
        {
            this.p_name = element.Name;
            this.p_values = new List<string>();
            this.p_childs = new Dictionary<string, List<CPS_SimpleXML>>();
            this.p_cdata = new List<string>();

            foreach (XmlNode child in element.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Text)
                    this.p_values.Add(child.Value);
                else if (child.NodeType == XmlNodeType.CDATA)
                    this.p_cdata.Add(child.Value);
                else if (child.NodeType == XmlNodeType.Element)
                {
                    if (!this.p_childs.ContainsKey(child.Name))
                        this.p_childs[child.Name] = new List<CPS_SimpleXML>();
                    this.p_childs[child.Name].Add(new CPS_SimpleXML((XmlElement)(child)));
                }
            }
        }

        /// <summary>
        /// Gets current tag name.
        /// </summary>
        /// <returns>Tag name.</returns>
        public string GetName()
        {
            return this.p_name;
        }

        /// <summary>
        /// Gets current tag value as string.
        /// </summary>
        /// <param name="seqno">For multiple tags only, tag number.</param>
        /// <returns>String value.</returns>
        public string GetString(int seqno = 0)
        {
            try
            {
                return this.p_values[seqno];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get current tag's CDATA contents.
        /// </summary>
        /// <param name="seqno">For multiple tags only, tag number.</param>
        /// <returns>String value.</returns>
        public string GetCData(int seqno = 0)
        {
            try
            {
                return this.p_cdata[seqno];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets current tag value as string.
        /// </summary>
        /// <param name="sxml">CPS_SimpleXML class instance.</param>
        /// <returns>String value.</returns>
        public static implicit operator string(CPS_SimpleXML sxml)
        {
            return sxml.GetString();
        }

        /// <summary>
        /// Gets current tag value as integer.
        /// </summary>
        /// <param name="seqno">For multiple tags only, tag number.</param>
        /// <returns>Integer value.</returns>
        public int GetInt(int seqno = 0)
        {
            try
            {
                return int.Parse(this.GetString(seqno));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets current tag value as integer.
        /// </summary>
        /// <param name="sxml">CPS_SimpleXML class instance.</param>
        /// <returns>Integer value.</returns>
        public static implicit operator int(CPS_SimpleXML sxml)
        {
            return sxml.GetInt();
        }

        /// <summary>
        /// Gets current tag value as float.
        /// </summary>
        /// <param name="seqno">For multiple tags only, tag number.</param>
        /// <returns>Float value.</returns>
        public float GetFloat(int seqno = 0)
        {
            try
            {
                return float.Parse(this.GetString(seqno), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets current tag value as float.
        /// </summary>
        /// <param name="sxml">CPS_SimpleXML class instance.</param>
        /// <returns>Float value.</returns>
        public static implicit operator float(CPS_SimpleXML sxml)
        {
            return sxml.GetFloat();
        }

        /// <summary>
        /// Gets child element by tag name and (in case of multiple tags) tag number.
        /// </summary>
        /// <param name="name">Child name.</param>
        /// <param name="seqno">Tag number.</param>
        /// <returns>Child CPS_SimpleXML element.</returns>
        public CPS_SimpleXML GetChild(string name, int seqno = 0)
        {
            try
            {
                return this.p_childs[name][seqno];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets child element by tag name and (in case of multiple tags) tag number.
        /// </summary>
        /// <param name="name">Child name.</param>
        /// <param name="seqno">Tag number.</param>
        /// <returns>Child CPS_SimpleXML element.</returns>
        public CPS_SimpleXML this[string name, int seqno = 0]
        {
            get
            {
                try
                {
                    return this.p_childs[name][seqno];
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Converts current CPS_SimpleXML instance to XmlElement instance.
        /// </summary>
        /// <param name="parent">Parent XmlElement instance.</param>
        /// <returns>New XmlElement instance.</returns>
        private XmlElement ConvertToXML(XmlElement parent)
        {
            XmlElement ret = (XmlElement)(parent.AppendChild(parent.OwnerDocument.CreateElement(this.p_name)));

            foreach (string value in this.p_values)
                ret.AppendChild(ret.OwnerDocument.CreateTextNode(value));

            foreach (string value in this.p_cdata)
                ret.AppendChild(ret.OwnerDocument.CreateCDataSection(value));

            foreach (KeyValuePair<string, List<CPS_SimpleXML>> pair in this.p_childs)
                foreach (CPS_SimpleXML child in pair.Value)
                    child.ConvertToXML(ret);

            return ret;
        }

        /// <summary>
        /// Converts current CPS_SimpleXML instance to XmlElement instance.
        /// </summary>
        /// <returns>New XmlElement instance.</returns>
        public XmlElement ConvertToXML()
        {
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            
            return this.ConvertToXML(root);
        }

        /// <summary>
        /// Generates string with spaces.
        /// </summary>
        /// <param name="spaces">Number of spaces to generate.</param>
        /// <returns>String with spaces.</returns>
        private string Spaces(int spaces)
        {
            int i = 0;
            string ret = "";

            for (i = 0; i < spaces; i++)
                ret += " ";

            return ret;
        }

        /// <summary>
        /// Dumps CPS_SimpleXML content and structure, very similar to PHP's print_r() function, very usefull for debugging purposes.
        /// Output is recomended to view using fixed-width font, for example Courier New.
        /// </summary>
        /// <param name="spaces">Number of identing spaces.</param>
        /// <returns>Dumped structure.</returns>
        private string Dump(int spaces)
        {
            string ret = "";
            int elems = 0;

            ret += "CPS_SimpleXML(" + this.p_name + ")\r\n";
            ret += this.Spaces(spaces) + "{\r\n";

            foreach (string value in this.p_values)
            {
                if (elems > 0)
                    ret += ",\r\n";
                ret += this.Spaces(spaces + 2) + "\"" + value + "\"";
                elems++;
            }

            foreach (string value in this.p_cdata)
            {
                if (elems > 0)
                    ret += ",\r\n";
                ret += this.Spaces(spaces + 2) + "CDATA: \"" + value + "\"";
                elems++;
            }

            foreach (KeyValuePair<string, List<CPS_SimpleXML>> pair in this.p_childs)
                foreach (CPS_SimpleXML child in pair.Value)
                {
                    if (elems > 0)
                        ret += ",\r\n";
                    ret += this.Spaces(spaces + 2) + "[\"" + pair.Key + "\"] => " + child.Dump(spaces + 2);
                    elems++;
                }

            ret += "\r\n" + this.Spaces(spaces) + "}";

            return ret;
        }

        /// <summary>
        /// Dumps CPS_SimpleXML content and structure, very similar to PHP's print_r() function, very usefull for debugging purposes.
        /// Output is recomended to view using fixed-width font, for example Courier New.
        /// </summary>
        /// <returns>Dumped structure.</returns>
        public string Dump()
        {
            return Dump(0);
        }

        /// <summary>
        /// Tag name.
        /// </summary>
        private string p_name;
        /// <summary>
        /// Tag values.
        /// </summary>
        private List<string> p_values;
        /// <summary>
        /// Tag child elements.
        /// </summary>
        private Dictionary<string, List<CPS_SimpleXML>> p_childs;
        /// <summary>
        /// CDATA values.
        /// </summary>
        private List<string> p_cdata;
    }
}
