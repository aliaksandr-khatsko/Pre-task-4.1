using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Security;
using System.Runtime.Serialization.Json;

namespace Home_Appliances
{
    class Program
    {
        static void Main(string[] args)
        {

            //Televisor televisor = new Televisor("Sony", "HRMN-100", "Red", 1000, 42, "1280x960", "CLD");
            //MultimediaAcousticSystem audioSystem = new MultimediaAcousticSystem("SVEN", "CDO-50", "Black", 20, 5.1);
            //Notebook notebook = new Notebook("ASUS", "SX50", "Black", 325, "2x2000GHr", 1000000, 8000, "GeForce NX500", "800x600", 15, true);
            //PC pc = new PC("Pentium", "5", "Black", 550, "5x2000GHr", 1000000, 4000, "GeForce sX8000", "1920x1080", 24, false);
            //Fridge fridge = new Fridge("Atlant", "SX200", "White", 500, 1.9, 0.6, 0.7, true);
            //Washer washer = new Washer("LG", "L5", "Silver", 800, 1.2, 1.0, 0.45, 800, 5);
            //Ketle ketle = new Ketle("Bosh", "S5", "Silver", 550, 1.2);
            //VacuumCleaner vacuumCleaner = new VacuumCleaner("Samsung", "GCRE1800", "Purple", 600, 1800);


            List<ElectricalAppliances> appliences = new List<ElectricalAppliances>();
            var serializer = new XmlSerializer();
            JSONSerializer binSerializer = new JSONSerializer();

            //appliences.Add(audioSystem);
            //appliences.Add(televisor);
            //appliences.Add(notebook);
            //appliences.Add(pc);
            //appliences.Add(washer);

            bool appliactionNeverStop = false;

            do
            {
                Console.WriteLine("Home Appliances System is ready to use");
                Console.WriteLine();
                Console.WriteLine("To see a list of Home Appliances please enter LIST and press Enter");
                Console.WriteLine();
                Console.WriteLine("To upload Home Appliances from xml file please enter XML and press Enter");
                Console.WriteLine();
                Console.WriteLine("To upload Home Appliances from data file please enter JSON and press Enter");
                Console.WriteLine();
                Console.WriteLine("To save Home Appliances to xml file please enter SAVEXML and press Enter");
                Console.WriteLine();
                Console.WriteLine("To save Home Appliances to data file please enter SAVEJSON and press Enter");
                Console.WriteLine();


                bool correctCommand = false;
                string userChoiceFirstMenu = Console.ReadLine();
                do
                {
                    if (String.Equals(userChoiceFirstMenu, "LIST"))
                    {
                        correctCommand = true;
                    }

                    else if (String.Equals(userChoiceFirstMenu, "XML"))
                    {
                        correctCommand = true;
                    }

                    else if (String.Equals(userChoiceFirstMenu, "JSON"))
                    {
                        correctCommand = true;
                    }

                    else if (String.Equals(userChoiceFirstMenu, "SAVEXML"))
                    {
                        correctCommand = true;
                    }
                    else if (String.Equals(userChoiceFirstMenu, "SAVEJSON"))
                    {
                        correctCommand = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong command, choose from the following options (LIST, XML, JSON, SAVEXML, SAVEJSON)");

                        userChoiceFirstMenu = Console.ReadLine();
                    }
                } while (correctCommand == false);

                switch (userChoiceFirstMenu)
                {
                    case "LIST":
                        try
                        {
                            if (appliences.Count > 0)
                            {
                                for (int i = 1; i < appliences.Count + 1; i++)
                                {
                                    Console.WriteLine("ID: {0}", i);
                                    appliences[i - 1].PrintSummary();
                                }

                            }
                            else
                            {
                                throw (new CatalogIsEpmtyException("Appliances Catalog is empty. Please, upload appliances first."));
                            }
                        }
                        catch (CatalogIsEpmtyException ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                            Console.WriteLine();
                        }
                        break;
                    case "XML":
                        try
                        {
                            appliences = serializer.Deserialize();
                        }
                        catch (DirectoryNotFoundException directoryNotFound)
                        {
                            Console.WriteLine(directoryNotFound.Message);
                        }
                        catch (FileNotFoundException fileNotFound)
                        {
                            Console.WriteLine(fileNotFound.Message);
                        }
                        catch (PathTooLongException longPath)
                        {
                            Console.WriteLine(longPath.Message);
                        }

                        catch (SecurityException securityException)
                        {
                            Console.WriteLine(securityException.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "JSON":
                        try
                        {
                            appliences = binSerializer.Deserialize();
                        }
                        catch (DirectoryNotFoundException directoryNotFound)
                        {
                            Console.WriteLine(directoryNotFound.Message);
                        }
                        catch (FileNotFoundException fileNotFound)
                        {
                            Console.WriteLine(fileNotFound.Message);
                        }
                        catch (PathTooLongException longPath)
                        {
                            Console.WriteLine(longPath.Message);
                        }

                        catch (SecurityException securityException)
                        {
                            Console.WriteLine(securityException.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case "SAVEXML":
                        serializer.Serialize(appliences);
                        break;
                    case "SAVEJSON":
                        binSerializer.Serialize(appliences);
                        break;
                   
                    default:
                        break;
                }
            } while (appliactionNeverStop == false);

        }

    }

    //Exception for handling users choice of an appliance
    public class ApplianceDoNotExistException : Exception
    {
        public ApplianceDoNotExistException(string message)
            : base(message)
        {
        }
    }

    //Exception for handling availability of appliances in catalog
    public class CatalogIsEpmtyException : Exception
    {
        public CatalogIsEpmtyException(string message)
            : base(message)
        {
        }
    }

    //Exeption for handling entered by user range of appliances power.
    public class OutOfRangeException : Exception
    {
        public OutOfRangeException(string message)
            : base(message)
        {
        }
    }



    public class JSONSerializer
    {
        public void Serialize(List<ElectricalAppliances> applianceses)
        {
            Type[] knownTypes = new Type[] { typeof(Televisor), typeof(MultimediaAcousticSystem), typeof(Notebook), typeof(PC), typeof(Fridge), typeof(Washer), typeof(Ketle), typeof(VacuumCleaner) };
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<ElectricalAppliances>), knownTypes);


            using (FileStream fs = new FileStream("ElectricalAppliances.json", FileMode.Create))
            {
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs))
                {
                    serializer.WriteObject(writer, applianceses);
                }
            }
            Console.WriteLine("Appliances Saved");
            Console.ReadLine();

        }

        public List<ElectricalAppliances> Deserialize()
        {
            Type[] knownTypes = new Type[] { typeof(Televisor), typeof(MultimediaAcousticSystem), typeof(Notebook), typeof(PC), typeof(Fridge), typeof(Washer), typeof(Ketle), typeof(VacuumCleaner) };
            DataContractJsonSerializer dsr = new DataContractJsonSerializer(typeof(List<ElectricalAppliances>), knownTypes);
            List<ElectricalAppliances> electricalApp;

            using (FileStream fs = new FileStream("ElectricalAppliances.json", FileMode.Open))
            {
                using (var reader = JsonReaderWriterFactory.CreateJsonReader(fs, XmlDictionaryReaderQuotas.Max))
                {
                    electricalApp = (List<ElectricalAppliances>)dsr.ReadObject(reader);
                }

                return electricalApp;
            }

            Console.WriteLine("Appliances Uploaded");
            Console.ReadLine();

        }
    }

    public class XmlSerializer
    {
        public void Serialize(List<ElectricalAppliances> electricalAppliances)
        {
            Type[] knownTypes = new Type[] { typeof(Televisor), typeof(MultimediaAcousticSystem), typeof(Notebook), typeof(PC), typeof(Fridge), typeof(Washer), typeof(Ketle), typeof(VacuumCleaner) };

            DataContractSerializer ser = new DataContractSerializer(typeof(List<ElectricalAppliances>), knownTypes);

            using (FileStream fs = new FileStream("ElectricalAppliances.xml", FileMode.OpenOrCreate))
            {
                ser.WriteObject(fs, electricalAppliances);
            }

            Console.WriteLine("Appliances Saved");
            Console.ReadLine();
        }

        public List<ElectricalAppliances> Deserialize()
        {
            Type[] knownTypes = new Type[] { typeof(Televisor), typeof(MultimediaAcousticSystem), typeof(Notebook), typeof(PC), typeof(Fridge), typeof(Washer), typeof(Ketle), typeof(VacuumCleaner) };
            DataContractSerializer dsr = new DataContractSerializer(typeof(List<ElectricalAppliances>), knownTypes);

            List<ElectricalAppliances> electricalAppliences;

            using (FileStream fs = new FileStream("ElectricalAppliances.xml", FileMode.Open))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                electricalAppliences = (List<ElectricalAppliances>)dsr.ReadObject(reader);
            }

            return electricalAppliences;
        }

    }

    [DataContract(Name = "ElectricalAppliances")]
    public abstract class ElectricalAppliances
    {
        [DataMember]
        public string Producer { get; private set; }

        [DataMember]
        public string Model { get; private set; }

        [DataMember]
        public string Color { get; private set; }

        [DataMember]
        public int PowerW { get; private set; }

        [DataMember]
        public bool SwitchStatus { get; private set; }

        public ElectricalAppliances(string producer, string model, string color, int powerW)
        {
            this.Producer = producer;
            this.Model = model;
            this.Color = color;
            this.PowerW = powerW;
            this.SwitchStatus = false;
        }

        abstract public void PrintSummary();

        public void SwitchOn()
        {
            this.SwitchStatus = true;
        }

        public void SwitchOff()
        {
            this.SwitchStatus = false;
        }
    }

    [DataContract(Name = "VideoElectronics")]
    public abstract class VideoElectronics : ElectricalAppliances
    {
        [DataMember]
        public string DataType { get; private set; }

        public VideoElectronics(string producer, string model, string color, int powerW)
            : base(producer, model, color, powerW)
        {
            DataType = "Video";
        }
    }

    [DataContract(Name = "AudioElectronics")]
    public abstract class AudioElectronics : ElectricalAppliances
    {
        [DataMember]
        public string DataType { get; private set; }

        public AudioElectronics(string producer, string model, string color, int powerW)
            : base(producer, model, color, powerW)
        {
            DataType = "Audio";
        }
    }

    [DataContract(Name = "Televisor")]
    public class Televisor : VideoElectronics
    {
        [DataMember]
        public double DiagonalInches { get; private set; }
        [DataMember]
        public string ScreenResolution { get; private set; }
        [DataMember]
        public string ScreenTechnology { get; private set; }



        public Televisor(string producer, string model, string color, int powerW, double diagonalInches, string screenResolution, string screenTechnology)
            : base(producer, model, color, powerW)
        {
            this.DiagonalInches = diagonalInches;
            this.ScreenResolution = screenResolution;
            this.ScreenTechnology = screenTechnology;
        }

        public override void PrintSummary()
        {
            string dispalaySwitchStatus = "No";

            if (SwitchStatus == true)
            {
                dispalaySwitchStatus = "Yes";
            }

            Console.WriteLine("Class: {0}", this.GetType().Name);
            Console.WriteLine("In work: {0}", dispalaySwitchStatus);
            Console.WriteLine("producer: {0}", Producer);
            Console.WriteLine("model: {0}", Model);
            Console.WriteLine("color: {0}", Color);
            Console.WriteLine("Power Consumption (W): {0}", PowerW);
            Console.WriteLine("diagonalInches: {0}", DiagonalInches);
            Console.WriteLine("screenResolution: {0}", ScreenResolution);
            Console.WriteLine("screenTechnology: {0}", ScreenTechnology);
            Console.WriteLine();
        }
    }

    [DataContract(Name = "MultimediaAcousticsSystem")]
    public class MultimediaAcousticSystem : AudioElectronics
    {
        [DataMember]
        public double AudioSystemType;

        public MultimediaAcousticSystem(string producer, string model, string color, int powerW, double audioSystemType)
            : base(producer, model, color, powerW)
        {
            this.AudioSystemType = audioSystemType;
        }

        public override void PrintSummary()
        {
            string dispalaySwitchStatus = "No";

            if (SwitchStatus == true)
            {
                dispalaySwitchStatus = "Yes";
            }

            Console.WriteLine("Class: {0}", this.GetType().Name);
            Console.WriteLine("In work: {0}", dispalaySwitchStatus);
            Console.WriteLine("Producer: {0}", Producer);
            Console.WriteLine("Model: {0}", Model);
            Console.WriteLine("Color: {0}", Color);
            Console.WriteLine("Power Consumption (W): {0}", PowerW);
            Console.WriteLine("AudioSystemType: {0}", AudioSystemType);
            Console.WriteLine();
        }
    }

    [DataContract(Name = "Computers")]
    public abstract class Computers : ElectricalAppliances
    {
        [DataMember]
        public string CPU { get; private set; }
        [DataMember]
        public int HDDVolumeMb { get; private set; }
        [DataMember]
        public int RAMVolumeMb { get; private set; }
        [DataMember]
        public string VideoCardModel { get; private set; }
        [DataMember]
        public string ScreenResolution { get; private set; }
        [DataMember]
        public double DiagonalInches { get; private set; }
        [DataMember]
        public bool Portative { get; private set; }

        public Computers(string producer, string model, string color, int powerW, string cpu, int hddVolumeMb, int ramVolumeMb, string videoCardModel, string screenResolution, double diagonalInches, bool portative)
            : base(producer, model, color, powerW)
        {
            this.CPU = cpu;
            this.DiagonalInches = diagonalInches;
            this.HDDVolumeMb = hddVolumeMb;
            this.RAMVolumeMb = ramVolumeMb;
            this.VideoCardModel = videoCardModel;
            this.ScreenResolution = screenResolution;
            this.Portative = portative;
        }

    }

    [DataContract(Name = "Notebook")]
    public class Notebook : Computers
    {
        public Notebook(string producer, string model, string color, int powerW, string cpu, int hddVolumeMb, int ramVolumeMb, string videoCardModel, string screenResolution, double diagonalInches, bool portative)
            : base(producer, model, color, powerW, cpu, hddVolumeMb, ramVolumeMb, videoCardModel, screenResolution, diagonalInches, true)
        {

        }

        public override void PrintSummary()
        {
            string dispalaySwitchStatus = "No";

            if (SwitchStatus == true)
            {
                dispalaySwitchStatus = "Yes";
            }

            string displayPortativeValue = "No";

            if (Portative == true)
            {
                displayPortativeValue = "Yes";
            }

            Console.WriteLine("Class: {0}", this.GetType().Name);
            Console.WriteLine("In work: {0}", dispalaySwitchStatus);
            Console.WriteLine("Portative: {0}", displayPortativeValue);
            Console.WriteLine("Producer: {0}", Producer);
            Console.WriteLine("Model: {0}", Model);
            Console.WriteLine("Color: {0}", Color);
            Console.WriteLine("Power Consumption (W): {0}", PowerW);
            Console.WriteLine("CPU: {0}", CPU);
            Console.WriteLine("HHD Volume (Mb): {0}", HDDVolumeMb);
            Console.WriteLine("RAM Volume (Mb): {0}", RAMVolumeMb);
            Console.WriteLine("Video Card: {0}", VideoCardModel);
            Console.WriteLine("Screen Resolution: {0}", ScreenResolution);
            Console.WriteLine("Diagonal (Inches): {0}", DiagonalInches);
            Console.WriteLine();
        }
    }


    [DataContract(Name = "PC")]
    public class PC : Computers
    {
        public PC(string producer, string model, string color, int powerW, string cpu, int hddVolumeMb, int ramVolumeMb, string videoCardModel, string screenResolution, double diagonalInches, bool portative)
            : base(producer, model, color, powerW, cpu, hddVolumeMb, ramVolumeMb, videoCardModel, screenResolution, diagonalInches, false)
        {

        }

        public override void PrintSummary()
        {
            string dispalaySwitchStatus = "No";

            if (SwitchStatus == true)
            {
                dispalaySwitchStatus = "Yes";
            }

            string displayPortativeValue = "Yes";

            if (Portative == false)
            {
                displayPortativeValue = "No";
            }

            Console.WriteLine("Class: {0}", this.GetType().Name);
            Console.WriteLine("In work: {0}", dispalaySwitchStatus);
            Console.WriteLine("Portative: {0}", displayPortativeValue);
            Console.WriteLine("Producer: {0}", Producer);
            Console.WriteLine("Model: {0}", Model);
            Console.WriteLine("Color: {0}", Color);
            Console.WriteLine("Power Consumption (W): {0}", PowerW);
            Console.WriteLine("CPU: {0}", CPU);
            Console.WriteLine("HHD Volume (Mb): {0}", HDDVolumeMb);
            Console.WriteLine("RAM Volume (Mb): {0}", RAMVolumeMb);
            Console.WriteLine("Video Card: {0}", VideoCardModel);
            Console.WriteLine("Screen Resolution: {0}", ScreenResolution);
            Console.WriteLine("Diagonal (Inches): {0}", DiagonalInches);
            Console.WriteLine();
        }
    }

    [DataContract(Name = "LargeAppliances")]
    public abstract class LargeAppliances : ElectricalAppliances
    {
        [DataMember]
        public string ApplianceSize { get; private set; }
        [DataMember]
        public double Heigh { get; private set; }
        [DataMember]
        public double Width { get; private set; }
        [DataMember]
        public double Depth { get; private set; }

        public LargeAppliances(string producer, string model, string color, int powerW, double heigh, double width, double depth)
            : base(producer, model, color, powerW)
        {
            ApplianceSize = "Large";
            this.Heigh = heigh;
            this.Width = width;
            this.Depth = depth;
        }
    }

    [DataContract(Name = "Fridge")]
    public class Fridge : LargeAppliances
    {
        [DataMember]
        public bool Frezer { get; private set; }

        public Fridge(string producer, string model, string color, int powerW, double heigh, double width, double depth, bool freezer)
            : base(producer, model, color, powerW, heigh, width, depth)
        {
            this.Frezer = freezer;
        }

        public override void PrintSummary()
        {
            string dispalaySwitchStatus = "No";

            if (SwitchStatus == true)
            {
                dispalaySwitchStatus = "Yes";
            }

            string FreezerPresence = "No";
            if (Frezer == true)
            {
                FreezerPresence = "Yes";
            }

            Console.WriteLine("Class: {0}", this.GetType().Name);
            Console.WriteLine("In work: {0}", dispalaySwitchStatus);
            Console.WriteLine("Producer: {0}", Producer);
            Console.WriteLine("Model: {0}", Model);
            Console.WriteLine("Color: {0}", Color);
            Console.WriteLine("Power Consumption (W): {0}", PowerW);
            Console.WriteLine("Freezer: {0}", FreezerPresence);
            Console.WriteLine("Heigh: {0}", Heigh);
            Console.WriteLine("Width: {0}", Width);
            Console.WriteLine("Depth: {0}", Depth);
            Console.WriteLine();
        }

    }

    [DataContract(Name = "Washer")]
    public class Washer : LargeAppliances
    {
        [DataMember]
        public int SpinSpeedRPM { get; private set; }
        [DataMember]
        public double VolumeLiters { get; private set; }

        public Washer(string producer, string model, string color, int powerW, double heigh, double width, double depth, int spinSpeedRPM, double volumeLiters)
            : base(producer, model, color, powerW, heigh, width, depth)
        {
            this.SpinSpeedRPM = spinSpeedRPM;
            this.VolumeLiters = volumeLiters;
        }

        public override void PrintSummary()
        {
            string dispalaySwitchStatus = "No";

            if (SwitchStatus == true)
            {
                dispalaySwitchStatus = "Yes";
            }

            Console.WriteLine("Class: {0}", this.GetType().Name);
            Console.WriteLine("In work: {0}", dispalaySwitchStatus);
            Console.WriteLine("Producer: {0}", Producer);
            Console.WriteLine("Model: {0}", Model);
            Console.WriteLine("Color: {0}", Color);
            Console.WriteLine("Power Consumption (W): {0}", PowerW);
            Console.WriteLine("Pin Speed (RPM): {0}", SpinSpeedRPM);
            Console.WriteLine("Volume (L): {0}", VolumeLiters);
            Console.WriteLine("Heigh: {0}", Heigh);
            Console.WriteLine("Width: {0}", Width);
            Console.WriteLine("Depth: {0}", Depth);
            Console.WriteLine();
        }

    }

    [DataContract(Name = "CookingAppliances")]
    public abstract class CookingAppliances : ElectricalAppliances
    {

        [DataMember]
        public string ApplianceType { get; private set; }

        public CookingAppliances(string producer, string model, string color, int powerW)
            : base(producer, model, color, powerW)
        {
            ApplianceType = "Cooking";
        }
    }

    [DataContract(Name = "Ketle")]
    public class Ketle : CookingAppliances
    {
        [DataMember]
        public double VolumeLiters { get; private set; }

        public Ketle(string producer, string model, string color, int powerW, double volumeLiters)
            : base(producer, model, color, powerW)
        {
            this.VolumeLiters = volumeLiters;
        }

        public override void PrintSummary()
        {
            string dispalaySwitchStatus = "No";

            if (SwitchStatus == true)
            {
                dispalaySwitchStatus = "Yes";
            }

            Console.WriteLine("Class: {0}", this.GetType().Name);
            Console.WriteLine("In work: {0}", dispalaySwitchStatus);
            Console.WriteLine("Producer: {0}", Producer);
            Console.WriteLine("Model: {0}", Model);
            Console.WriteLine("Color: {0}", Color);
            Console.WriteLine("Power Consumption (W): {0}", PowerW);
            Console.WriteLine("Volume (L): {0}", VolumeLiters);
            Console.WriteLine();
        }

    }

    [DataContract(Name = "CleaningAppliances")]
    public abstract class CleaningAppliances : ElectricalAppliances
    {
        [DataMember]
        public string ApplianceType { get; private set; }

        public CleaningAppliances(string producer, string model, string color, int powerW)
            : base(producer, model, color, powerW)
        {
            ApplianceType = "Cleaning";
        }
    }

    [DataContract(Name = "VacuumCleaner")]
    public class VacuumCleaner : CleaningAppliances
    {
        [DataMember]
        public int SuctionPowerW { get; private set; }

        public VacuumCleaner(string producer, string model, string color, int powerW, int suctionPowerW)
            : base(producer, model, color, powerW)
        {
            this.SuctionPowerW = suctionPowerW;
        }

        public override void PrintSummary()
        {
            string dispalaySwitchStatus = "No";

            if (SwitchStatus == true)
            {
                dispalaySwitchStatus = "Yes";
            }

            Console.WriteLine("Class: {0}", this.GetType().Name);
            Console.WriteLine("In work: {0}", dispalaySwitchStatus);
            Console.WriteLine("Producer: {0}", Producer);
            Console.WriteLine("Model: {0}", Model);
            Console.WriteLine("Color: {0}", Color);
            Console.WriteLine("Power Consumption (W): {0}", PowerW);
            Console.WriteLine("Suction Power (W): {0}", SuctionPowerW);
            Console.WriteLine();
        }

    }

}
