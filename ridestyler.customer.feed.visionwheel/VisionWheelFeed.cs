using System.Collections.Generic;
using System.IO;
using ridestyler.core.feed;
using System.Threading.Tasks;

using ridestyler.core.feed.Models;
using ridestyler.core.feed.Providers.Interfaces;
using ridestyler.core.feed.Providers.RideStyler;
using ridestyler.core.feed.Providers.RideStyler.Response;
using System.Reflection;
using System.Linq;

namespace ridestyler.customer.feed.visionwheel
{
    class VisionWheelFeed : IFeedTransform
    {
        private readonly ILogProvider _logProvider;
        private readonly IFTPProvider _ftpProvider;
        private readonly ICsvProvider _csvProvider;
        private readonly IOutputProvider _outputProvider;
        private readonly ISettingsProvider _settings;

        public VisionWheelFeed(
            ILogProvider logProvider,
            IFTPProvider ftpProvider,
            ICsvProvider csvProvider,
            IOutputProvider outputProvider,
            ISettingsProvider settings)
        {
            _logProvider = logProvider;
            _ftpProvider = ftpProvider;
            _csvProvider = csvProvider;
            _outputProvider = outputProvider;
            _settings = settings;
        }

        public async Task<RideStylerFeedResult> Run()
        {
            return await RunFTP();
        }


        public async Task<RideStylerFeedResult> RunFTP()
        {
  
            _ftpProvider.Username = _settings.Settings.Ftp.Username;
            _ftpProvider.Password = _settings.Settings.Ftp.Password;
            _ftpProvider.Server = _settings.Settings.Ftp.Server;

            _logProvider.Log("Connecting...");
            await _ftpProvider.Connect();

            _logProvider.Log("Getting newest file");

            using (MemoryStream ms = new MemoryStream())
            {
                 _outputProvider.SetOutputCsv(ms, new OuputRowMap()); 

                if (_settings.Settings.Ftp.RootDir != null)
                    await getFileFTP(_settings.Settings.Ftp.fileRegex);
                else
                    await getFileFTP("MIL.csv");

                await _outputProvider.FinalizeOutput();

                _logProvider.Log($"Imported {_outputProvider.RecordCount} records.");
                _logProvider.Log("Finished!");

                return new RideStylerFeedResult() { Files = new IRidestylerResponse[] { new CsvResponse("supplier-data.csv", ms) }, Settings = null };
            }

        }

        

        private async Task<bool> getFileFTP(string regex)
        {
            //string regex = ".*\\.csv$";
           
            FTPFile newestFile = await _ftpProvider.GetNewestFile("/", new System.Text.RegularExpressions.Regex(regex));

            _logProvider.Log($"Downloading {newestFile.Path}...");
            using (Stream inputStream = await _ftpProvider.Download(newestFile))
            {

                // Parse the file
                _logProvider.Log("Parsing...");
                proccessStream(newestFile.Path, inputStream);   
            }

            return true;
        }
       

        private void proccessStream(string path, Stream inputStream)
        {
           
            _csvProvider.Start(inputStream);

            Dictionary<string, string> row = null;
            while ((row = _csvProvider.GetNextRow()) != null)
            {
                decimal? CostTotal = null;
                decimal? MSRP = null;

                switch (_settings.Settings.Feed.PriceLevel)
                {
                    case "CDMIL":
                        CostTotal = decimal.Parse(row["Price"]);
                        MSRP = decimal.Parse(row["CAD MSRP"]);
                        break;
                    case "CDP6":
                        CostTotal = decimal.Parse(row["Jobber"]);
                        MSRP = decimal.Parse(row["CAD MSRP"]);
                        break;
                    default:
                        CostTotal = decimal.Parse(row["Price"]);
                        MSRP = decimal.Parse(row["MSRP"]);
                        break;
                }

                decimal? ShipHeight = decimal.Parse(row["Height"]);
                decimal? ShipWidth = decimal.Parse(row["Width"]);
                decimal? ShipDepth = decimal.Parse(row["Length"]);
                decimal? ShipWeight = decimal.Parse(row["Gross Weight"]);


                _outputProvider.SaveRecord(new OutputRow()
                {
                    PartNumber = row["PartNo"],
                    SupplierNumber = row["Barcode"],
                    Model = row["Description"],
                    Brand = row["Brand"],
                    Discontinued = row["Discontinued"] == "Yes",

                    ShipHeight = ShipHeight,
                    ShipWidth = ShipWidth,
                    ShipDepth = ShipDepth,
                    ShipWeight = ShipWeight,

                    CostTotal = CostTotal,
                    MSRP = MSRP,
                    StockAvailable = int.Parse("Qty AL"),
                    SourceName = "AL"
                }) ;

                _outputProvider.SaveRecord(new OutputRow()
                {
                    PartNumber = row["PartNo"],
                    SupplierNumber = row["Barcode"],
                    Model = row["Description"],
                    Brand = row["Brand"],
                    Discontinued = row["Discontinued"] == "Yes",

                    ShipHeight = ShipHeight,
                    ShipWidth = ShipWidth,
                    ShipDepth = ShipDepth,
                    ShipWeight = ShipWeight,

                    CostTotal = CostTotal,
                    MSRP = MSRP,
                    StockAvailable = int.Parse("Qty CA"),
                    SourceName = "CA"
                });

                _outputProvider.SaveRecord(new OutputRow()
                {
                    PartNumber = row["PartNo"],
                    SupplierNumber = row["Barcode"],
                    Model = row["Description"],
                    Brand = row["Brand"],
                    Discontinued = row["Discontinued"] == "Yes",

                    ShipHeight = ShipHeight,
                    ShipWidth = ShipWidth,
                    ShipDepth = ShipDepth,
                    ShipWeight = ShipWeight,

                    CostTotal = CostTotal,
                    MSRP = MSRP,
                    StockAvailable = int.Parse("Qty IN"),
                    SourceName = "IN"
                });

                _outputProvider.SaveRecord(new OutputRow()
                {
                    PartNumber = row["PartNo"],
                    SupplierNumber = row["Barcode"],
                    Model = row["Description"],
                    Brand = row["Brand"],
                    Discontinued = row["Discontinued"] == "Yes",

                    ShipHeight = ShipHeight,
                    ShipWidth = ShipWidth,
                    ShipDepth = ShipDepth,
                    ShipWeight = ShipWeight,

                    CostTotal = CostTotal,
                    MSRP = MSRP,
                    StockAvailable = int.Parse("Qty TX"),
                    SourceName = "TX"
                });

                _outputProvider.SaveRecord(new OutputRow()
                {
                    PartNumber = row["PartNo"],
                    SupplierNumber = row["Barcode"],
                    Model = row["Description"],
                    Brand = row["Brand"],
                    Discontinued = row["Discontinued"] == "Yes",

                    ShipHeight = ShipHeight,
                    ShipWidth = ShipWidth,
                    ShipDepth = ShipDepth,
                    ShipWeight = ShipWeight,

                    CostTotal = CostTotal,
                    MSRP = MSRP,
                    StockAvailable = int.Parse("Qty NC"),
                    SourceName = "NC"
                });

                _outputProvider.SaveRecord(new OutputRow()
                {
                    PartNumber = row["PartNo"],
                    SupplierNumber = row["Barcode"],
                    Model = row["Description"],
                    Brand = row["Brand"],
                    Discontinued = row["Discontinued"] == "Yes",

                    ShipHeight = ShipHeight,
                    ShipWidth = ShipWidth,
                    ShipDepth = ShipDepth,
                    ShipWeight = ShipWeight,

                    CostTotal = CostTotal,
                    MSRP = MSRP,
                    StockAvailable = int.Parse("Qty ON"),
                    SourceName = "ON"
                });


                //CAD MSRP -- Visionwheel_CDMIL.csv, CDP6.csv
                //MSRP -- p6.csv, p6-CC.csv, MIL.csv, CONT.csv, MIL-CC.csv
                //Jobber -- Visionwheel_CDMIL.csv

                //Price


                //Qty AL
                //Qty CA
                //Qty IN
                //Qty TX
                //Qty NC
                //Qty ON


            }
            
        }
    }
}
