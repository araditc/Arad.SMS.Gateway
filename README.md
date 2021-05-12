# Arad SMS Platform is a Carrier neutral SMS Platform for SMS Wholesale
Open source SMS gateway and Cloud-based SMS software built only for SMS trading.

**Arad SMS Platform** is a fully-equipped business SMS platform for SMS aggregators, Mobile operators, OTTs, and enterprises. The platform is a hosted, turn-key SMS trading solution. It is completely carrier neutral, intuitive to use and it doesn't require any hardware or software. With our SMS Platform, you are able to run your SMS wholesale business from A-Z, including detailed message testing, effective routing, and pricing options and built-in finances and billing system.

Run the entire SMS wholesale business and resell SMS traffic all over the world from a single place with minimal manual work! 
The pricing packages are very attractive, purposely created to enable profitable SMS trading business for our wholesale clients. Monetize wholesale messaging avoiding unnecessary costs using platform’s testing and monitoring systems.

Routing by Network, Pricing, Bulk Rerouting, SMS Firewall, Filters, HLR/MNP, DLR check.

Real-time Margin check, Reporting, CDR, Statistics. Finance & Billing, 24/7 Support.

Arad SMS Platform
----------------------

The Arad SMS Platform is represented by the following set of projects. This is a community version and open source. 


| Project                         | Type          | Link                                                                                   | Project description |
|---------------------------------|---------------|----------------------------------------------------------------------------------------|---------------------|
| **Arad.SMS.Gateway.WebApi**     | API           |     | The WebAPI for incoming traffic. |
| **Arad.SMS.Gateway.ApiProcessRequest**       | Worker           |     | The ApiProcessRequest, get message from queue and process them and add to schedule table for storing in DB. |
| **Arad.SMS.Gateway.ExportData**            | Worker  |      | The ExportData used for giving excel report and archiving data after 48H. |
| **Arad.SMS.Gateway.GetSmsDelivery**        | Worker           |  | The GetSmsDelivery used for getting delivery after sending message. |
| **Arad.SMS.Gateway.GiveBackCredit**      | Worker |     | The GiveBackCredit, refund all failed message cost to customer. |
| **Arad.SMS.Gateway.MessageParser**    | Worker |        | The MessageParser used for parsing incoming message. |
| **Arad.SMS.Gateway.RegularContent** | Worker    |         | The RegularContent used for Regular Content service. |
| **Arad.SMS.Gateway.SaveLog**  | Worker  |           | The SaveLog, save all log to file. |
| **Arad.SMS.Gateway.SaveSentSms**      | Worker           |    | The SaveSentSms, save message to DB after sending. |
| **Arad.SMS.Gateway.SaveSmsDelivery**        | Worker |     | The SaveSmsDelivery, save delivery to DB. |
| **Arad.SMS.Gateway.TerafficRelay**      | Worker           |           | The TerafficRelay used for relay MO and DLR traffic to user URL. |
| **Arad.SMS.Gateway.Business**      | DataLayer           |   | Data layer. |
| **Arad.SMS.Gateway.Common**      | DataLayer           |    | Data layer. |
| **Arad.SMS.Gateway.DataAccessLayer**      | DataLayer           |       | Data layer. |
| **Arad.SMS.Gateway.Facade**      | DataLayer           |       | Data layer. |
| **Arad.SMS.Gateway.GeneralLibrary**      | Library           |      | General library |
| **Arad.SMS.Gateway.GeneralTools**      | Tools           |   | General tools. |
| **Arad.SMS.Gateway.SqlLibrary**      | SqlLibrary           |    | This library used for sending message from SQL to MSMQ and contain many useful function. |
| **Arad.SMS.Gateway.ScheduledBulkSms**      | Worker Read DB           |     | The ScheduledBulkSms, read bulk message from DB and send them to MSMQ. |
| **Arad.SMS.Gateway.ScheduledSms**      | Worker  Read DB          |      |  The ScheduledSms, read message from DB and send them to MSMQ. |
| **Arad.SMS.Gateway.URLRewriter**      | Library           |     | The URLRewriter Library. |
| **Arad.SMS.Gateway.Web**      | UI           |    | The GUI for administrators and users. |
| **Arad.SMS.Gateway.AradSmsSender**      | Worker           |      | The AradSmsSender used for getting message from MSMQ and route it with [Arad](https://arad-itc.com). |
| **Arad.SMS.Gateway.MagfaSmsSender**      | Worker           |       | The MagfaSmsSender used for getting message from MSMQ and route it with [MAGFA](http://www.magfa.com/). |
| **Arad.SMS.Gateway.ManageThread**      | Worker           |       | The ManageThread implement interfaces for sending message and manage thread for workers. |
| **Arad.SMS.Gateway.RahyabPGSmsSender**      | Worker           |  | The RahyabPGSmsSender used for getting message from MSMQ and route it with [Rahyab Payam Gostaran](http://rahyabcp.ir/). |
| **Arad.SMS.Gateway.RahyabRGSmsSender**      | Worker           |   | The RahyabRGSmsSender used for getting message from MSMQ and route it with [Rahyab Rayaneh Gostar](http://sms.rahyab.ir/). |



# Hosted SMS Platform
- Cloud-based SMS trading platform tailored to SMS wholesale business
- Fully managed platform: no engineering support required
- Turnkey web-based solution including Switching, Pricing, Routing, Billing and Fault management
- No Capex: no hardware to buy
- Unlimited licence: no limitations in the number of supplier connections, customers, resellers, users, rate plans, etc.
- Multitenant support of resellers and customers

# Web Cockpit
- State of the art and user-friendly web portal to manage your complete wholesale business
- All you need is a browser (no maintenance required)
- Designed to be used from any device, e.g. tablets
- Smart reseller, customer and user management
- Definable access permissions
- Unlimited number of resellers
- White-label support (brand the portal with your corporate design)

# Rate Plans
- Rate plans manager with negative margin indicator
- Fast and simple price management
- Create standard rate plans (Products) or individual rate plans tailored to you customers' requirements
- Create multiple rate plans for the same customer
- The system automatically issues the rate changes out to your customers
- Auto import of your providers' updates

# Routing
- Powerful and feature rich routing editor
- Routing changes are activated in less than 10 seconds
- MNP/HLR based routing to maximize delivery
- Load balancing over several suppliers
- Conditional routing allows total control: route based on sender ID, CLI, content, destination, etc.
- Set routing priority conditions for time-critical traffic
- Enhanced least cost routing (LCR) with strong features

# Statistics
- Real-time and detailed reporting
- Comprehensive presentation of commercial and quality data
- Graphical presentation of results with interactive charts
- Sophisticated filter function with high granularity
- Sophisticated drill down possibilities, down to individual messages

# Reporting
- Fast and individual export of detailed CDRs
- Browse your CDRs using the powerful message log viewer
- Analyse your traffic with flexible data filtering features
- Simple data export to Excel/CSV for post-processing purposes
- Data export via download or FTP to customers FTP account
- Scheduled delivery of reports to your email

# Billing
- Real-time billing for postpaid and prepaid connections
- Multiple balances and billing accounts per Business Partner
- Credit management and alerts for balance and overdraft
- Support of multiple currencies, taxes, languages, and time zones
- Easy integration with your existing company billing system by exporting CDRs
- Delivery of CDRs via Email or scheduled uploads to FTP server
- Possibility to use your existing billing platform

# Invoicing & Finance
- Automatic invoicing to your customers
- Flexible definition of recipients (Email, Hardcopy)
- Fast and easy credit top-up with auto-invoicing
- Define individual payment terms and discounts types
- Support of multiple currencies and taxes
- Multi-languages/culture and time zones
- Customizable template for generating invoice

# SMS Testing System
- DLR testing: Test any number «on the fly» to check DLR results
- HLR/MNP testing: test any number «on the fly» to check HLR/MNP results
- Create your own SIM Test suite: create your own complete 360° testing system by setting up your own SIM modems, we empower you
- Open: any 3rd Party Testing Solution can be connected

# SMS Firewall / Filter
- SPAM filter: intelligent SMS Firewall which detects and blocks SPAM messages
- Destination filter: SMS are filtered based on recipient lists
- Originating filter: SMS are filtered based on Sender ID and CLI list
- Content filter: SMS are filtered based on message text
- Individual filter: SMS can be filtered based on your own rules and business needs

# High Security Standards
- Highly protected and hacker safe platform
- Data is hosted on servers in Iran
- Systems are collocated in bank certified data centres
- Connections are restricted by IP address, only trusted IP’s can connect to the platform
- HTTP(s) and VPN connectivity are available on request
- Fully redundant system

# High Availability
- Robust and redundant cloud based platform (billions of messages sent in the last years)
- Redundant SMS platform: 99.99% availability
- High throughput capacity & extremely low delivery latency
- Actual system throughput has ca. 50,000 SMS/second (ca. 36 Mio SMS/hour)
- System throughput can be extended easily by adding more routing servers
- Carrier-grade availability
- The System itself is fully redundant
- Auto-rebinding if a connection is down
- Internal watchdog monitors and repairs any system issues

# Additional Features
- All SMS features supported
- SMS Protocols for Suppliers: SMPP, SMPP over SSL
- SMS Protocols for Customers: SMPP, SMPP over SSL, http, https
- Support of Binary SMS, like WAP Push, vCard, etc.
- Support for all character sets: GSM 03.38, 16 Bit Unicode (Arabic, Asian, Cyrillic)
- MNP Support: Number portability support to maximize delivery
- Fast and competent support: 24/7 availability of our professional support team
- High throughput capacity and extremely low delivery latency
- Redundant and robust SMS platform: carrier-grade availability (99.999%)
- Reliable SMS platform: billions of messages sent in the last years


# Install
To use, you must first install the latest version of Visual Studio. You also need to install the SQL Server 2019. After creating the database, you can create tables, etc. through the .sql file.
After installing DB, add **Arad.SMS.Gateway.SqlLibrary.dll** to SQL assembly.

```sql
EXEC sp_changedbowner 'sa'
ALTER DATABASE [Arad.SMS.Gateway.DB] SET trustworthy ON
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO

sp_configure 'clr enabled', 1;
GO
RECONFIGURE;
GO

sp_configure 'show advanced options', 0;
GO
RECONFIGURE;
GO

CREATE ASSEMBLY [Messaging]
FROM 'C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Messaging.dll'
WITH PERMISSION_SET = UNSAFE;


CREATE ASSEMBLY [Arad.SMS.Gateway.SqlLibrary]
FROM 'PATH\Arad.SMS.Gateway.SqlLibrary.dll'
WITH PERMISSION_SET = UNSAFE;

```

## Super admin

- Username: administrator
- Password: Arad@1112

## Main admin
- Username: admin
- Password: Arad@1112

Super admin can create unlimited main admin. Each main admin can have own setting.

# All Arad SMS Platform version
We have three version. 


| Features                         | Community          | SMS Hub               | SMSC |
|---------------------|---------------------|---------------------|---------------------|
| **Hosted SMS Platform**     | &#x2611;           |   &#x2611;  | &#x2611; |
| **Web Cockpit**     | &#x2611;           |  &#x2611;   | &#x2611; |
| **Rate Plans**     | &#x2611;           |  &#x2611;   | &#x2611; |
| **Routing**     | &#x2611;           |   &#x2611;  | &#x2611; |
| **Statistics**     | &#x2611;           |  &#x2611;   | &#x2611; |
| **Reporting**     | &#x2611;           |   &#x2611;  | &#x2611; |
| **Billing**     | &#x2611;           |   &#x2611;  | &#x2611; |
| **Invoicing & Finance**     | &#x2611;           |   &#x2611;  | &#x2611; |
| **SMS Testing System**     | &#x2611;           |   &#x2611;  | &#x2611; |
| **SMS Firewall / Filter**     | &#x2611;           |  &#x2611;   | &#x2611; |
| **Push notification**     | &#x2612;           |  &#x2611;   | &#x2611; |
| **SS7/SIGTRAN support**     | &#x2612;           |   &#x2612;  | &#x2611; |
| **SMPP and SMPP over SSL**     | &#x2612;           |  &#x2611;   | &#x2611; |
| **Message queue**     | MSMQ           |   Arad.Q  | Arad.Q |
| **Data base**     | SQL Server           |   NoSQL  | NoSQL |
| **TPS**     | 100           |   10K per node  | 20K per node |
| **Price**     | Free and Open Source           |  [Contact](mailto:info@arad-itc.org)   | [Contact](mailto:info@arad-itc.org) |


## Contributing
Pull requests are welcome! If you are planning a larger feature, please open an issue first for community input.

## Donate

If you want to support Arad ITC development you can [contact us](mailto:info@arad-itc.org)

[![Donate with PayPal](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://paypal.me/araditcco?locale.x=en_US)



## Feedback

info [at] arad-itc.org

<a href="https://twitter.com/araditc">Twitter</a>
