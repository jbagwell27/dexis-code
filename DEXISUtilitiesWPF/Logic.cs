using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace DEXISUtilitiesWPF
{
    class DEXIS9Logic
    {
        //Patient ID
        public string Id { get; set; }
        public string DataPath { get; set; }
        public List<string> ImageFiles { get; set; }

        public DEXIS9Logic()
        {
            ImageFiles = new List<string>();
            Id = "";

        }
        public DEXIS9Logic(string path)
        {
            DataPath = path;
        }

        /**
         * Checks to see if the system is 64 or 32 bit
         */
        public bool Is64bit()
        {
            return Environment.Is64BitOperatingSystem;
            //return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"));
        }

        public bool IsServer()
        {
            try
            {
                GetLocalPath();
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }

        /**
         * local path for DEXIS 9
         * Checks for 64 or 32 bit
         * checks the proper registery location based on system Architecture (wow6432node for 64bit)
         * returns the directory from the registry: default is C:\dexis
         */
        public string GetLocalPath()
        {
            string result;

            if (Is64bit())
            {
                //Object o = RegistryView.Registry32;
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432NODE\\DEXIS\\DEXIS");

                result = key.GetValue("Directory").ToString();
            }
            else
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\DEXIS\\DEXIS");
                result = key.GetValue("Directory").ToString();
            }

            return result;
        }

        /**
         * Read the file
         * search through the file for anything starting with xdata
         * grabs xdata=C:\path\to\data
         * removes xdata=
         * returns C:\path\to\data
         */
        public string GetDataPathFromIni()
        {
            if (IsServer())
            {
                DataPath = "";
            }
            else
            {
                string result = "";

                string[] lines = File.ReadAllLines(GetLocalPath() + "\\dexis.ini");
                foreach (string s in lines)
                {
                    if (s.StartsWith("xdata"))
                    {
                        result = s;
                    }
                }
                DataPath = result.Replace("xdata=", "");
            }

            return DataPath;
        }

        public void SetDataPath(string path)
        {
            if (Directory.Exists(path))
            {
                if (File.Exists(path + "\\xpat.dat"))
                {
                    DataPath = path;
                }
                else
                {
                    DataPath = "NOTADIR";
                }
            }
            else
            {
                DataPath = "NOTADIR";
            }

        }

        /**
         * parse the entered USERID for use with DEXIS 9 folder structure
         * uses the ID and adds preceeding 0s if the length is less than 5. ex: 24 = 00024
         * adds a '\' between each character: 0\0\0\2\4\
         * returns result: "0\0\0\2\4\"
         */
        public string ParseID(string id)
        {
            string temp = id;
            string result;

            while (temp.Length < 5)
            {
                temp = "0" + temp;
            }
            result = String.Join("\\", temp.ToCharArray());

            return result;
        }

        /**get path to the patient directory
         * 
         * returns datapath with the ParsedID at the end: C:\path\to\data\0\0\0\2\4\
         */
        public string GetPatientPath(string id)
        {
            return DataPath + "\\" + ParseID(id);
        }

        /**
         * clear all birthdates from all patients
         * gets the inf file for each patient
         * removes the line starting with BD=
         * rewrites the file so the changes are active
         */
        public void ClearBirthDate()
        {
            string path = DataPath;

            string[] files = Directory.GetFiles(path, "*.inf", SearchOption.AllDirectories);

            foreach (string s in files)
            {
                string[] lines = File.ReadAllLines(s);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("BD"))
                    {
                        lines[i] = "";
                    }
                }
                File.WriteAllLines(s, lines);
            }
        }

        /**
         * clear birthdate for a single patient
         * checks the patient folder: c:\path\to\data\0\0\0\2\4\
         * reads the inf file and removes the line starting with BD=
         * rewrites the file so the changes are active
         */
        public void ClearBirthDate(string id)
        {
            string path = GetPatientPath(id);
            string[] files = Directory.GetFiles(path, "*.inf");

            foreach (string s in files)
            {
                string[] lines = File.ReadAllLines(s);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("BD"))
                    {
                        lines[i] = "";
                    }
                }
                File.WriteAllLines(s, lines);
            }
        }

        /**
         * changes the provider for all patients
         * reads through all inf files
         * scans for line starting with DT=provider1
         * changes DT=provider1 to DT=provider2
         */
        public void ChangeProvider(string provider1, string provider2)
        {
            string path = DataPath;

            string[] files = Directory.GetFiles(path, "*.inf", SearchOption.AllDirectories);

            foreach (string s in files)
            {
                string[] lines = File.ReadAllLines(s);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Equals("DT=" + provider1))
                    {
                        lines[i] = "DT=" + provider2;
                    }
                }
                File.WriteAllLines(s, lines);
            }
        }

        /**
         * changes the provider for single patients
         * reads through single patient inf file
         * scans for line starting with DT=provider1
         * changes DT=provider1 to DT=provider2
         */
        public void ChangeProvider(string provider1, string provider2, string id)
        {
            string path = GetPatientPath(id);
            string[] files = Directory.GetFiles(path, "*.inf");

            foreach (string s in files)
            {
                string[] lines = File.ReadAllLines(s);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Equals("DT=" + provider1))
                    {
                        lines[i] = "DT=" + provider2;
                    }
                }
                File.WriteAllLines(s, lines);
            }
        }

        /**
         * delete lck files in the entire folder
         * scans the data path for lck files.
         * deletes them all
         */
        public void DeleteLckFile()
        {
            string[] files = Directory.GetFiles(DataPath, "*.lck", SearchOption.AllDirectories);
            foreach (string s in files)
            {
                if (File.Exists(s))
                {
                    File.Delete(s);
                }
            }
        }

        /**
         * delete lck file for a single patient
         * scan the patient directory for a lck file
         * deletes that file
         */
        public void DeleteLckFile(string id)
        {
            string path = GetPatientPath(id);
            string[] files = Directory.GetFiles(path, "*.lck");

            foreach (string s in files)
            {
                if (File.Exists(s))
                {
                    File.Delete(s);
                }
            }
        }

        /**
         * open the patient folder in file explorer
         * C:\path\to\data\0\0\0\2\4\
         */
        public int OpenPatientFolder(string id)
        {
            if (Directory.Exists(GetPatientPath(id)))
            {
                Process.Start(GetPatientPath(id));
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /**
         * returns the patient first and last name
         * scans the patient folder inf and reads lines starting with LN and FN.
         * returns "FN LN": ex: Josh Bagwell
         */
        public string GetPatientName(string id)
        {
            string firstName = "";
            string lastName = "";
            string path = GetPatientPath(id);
            string[] files = null;
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path, "*.inf");
            }
            else
            {
                return "NO1234";
            }

            foreach (string s in files)
            {
                string[] lines = File.ReadAllLines(s);
                foreach (string l in lines)
                {
                    if (l.StartsWith("FN"))
                    {
                        firstName = l.Replace("FN=", "");
                    }
                    if (l.StartsWith("LN"))
                    {
                        lastName = l.Replace("LN=", "");
                    }
                }
            }
            return firstName + " " + lastName;
        }

        /**
         * force dexis to rebuild the index
         * creates a folder called trash in the data path
         * moves files: xpat.dat, xpatid.dat, and dexis.chk into that folder.
         * whenever dexis is relaunched those files will be regenerated
         */
        public void ForceRebuild()
        {
            string path = DataPath;
            string trashPath = path + "\\Trash";
            Directory.CreateDirectory(trashPath);

            if (File.Exists(path + "\\XPATid.dat"))
            {
                if (File.Exists(trashPath + "\\XPATid.dat"))
                {
                    File.Delete(trashPath + "\\XPATid.dat");
                }
                File.Move(path + "\\XPATid.dat", trashPath + "\\XPATid.dat");
            }
            if (File.Exists(path + "\\XPAT.dat"))
            {
                if (File.Exists(trashPath + "\\XPAT.dat"))
                {
                    File.Delete(trashPath + "\\XPAT.dat");
                }
                File.Move(path + "\\XPAT.dat", trashPath + "\\XPAT.dat");
            }
            if (File.Exists(path + "\\DEXIS.CHK"))
            {
                if (File.Exists(trashPath + "\\DEXIS.CHK"))
                {
                    File.Delete(trashPath + "\\DEXIS.CHK");
                }
                File.Move(path + "\\DEXIS.CHK", trashPath + "\\DEXIS.CHK");
            }
        }

        /**
         * Backup the data folder to a new location
         * takes a source and a destination and copys the source to the dest.
         * used for 9 and 10, checks for MDF or LDF files and omits them from the copy because SQL permissions
         */
        public void Backup(string source, string dest)
        {

            foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(source, dest));
            }
            string[] filesArray = Directory.GetFiles(source, "*", SearchOption.AllDirectories);
            List<string> filesList = new List<string>(filesArray);
            for (int i = 0; i < filesArray.Length; i++)
            {
                if (filesArray[i].Contains("mdf") || filesArray[i].Contains("ldf"))
                {
                    filesList.Remove(filesArray[i]);
                }
            }

            foreach (string file in filesList)
            {
                File.Copy(file, file.Replace(source, dest), true);
            }

        }

        public void Migration(int version, string dest)
        {
            if (version == 9)
            {
                string nineIniFile = GetLocalPath() + "\\dexis.ini";
                string[] lines = File.ReadAllLines(nineIniFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("xdata"))
                    {
                        lines[i] = "xdata=" + dest;
                    }
                }
                File.WriteAllLines(nineIniFile, lines);
            }
            else if (version == 10)
            {
                string tenIniFile = "C:\\ProgramData\\Danaher_Dental\\DEXIS.ini";
                string[] lines = File.ReadAllLines(tenIniFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("DataPath"))
                    {
                        lines[i] = "DataPath=" + dest;
                    }
                }
                File.WriteAllLines(tenIniFile, lines);
            }
        }

        /**
         * Terminal run method
         * has console write prompts
         * not used in GUI
         */
        public void Run()
        {
            Console.WriteLine("Welcome to DEXIS(c) 9 Utilities \n" +
                "Please select a function: \n" +
                "1: Single Patient \n" +
                "2: All Patients \n" +
                "3: Backup");
            int option = Int32.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Please enter the Patient ID");
                    Id = Console.ReadLine();
                    Console.WriteLine("Patient: " + GetPatientName(Id) + " is selected. \n" +
                        "What would you like to do? \n" +
                        " 1: Open Patient Folder \n" +
                        " 2: Clear Birthday \n" +
                        " 3: Change Provider \n" +
                        " 4: Force Rebuild");
                    int result = Int32.Parse(Console.ReadLine());
                    switch (result)
                    {
                        case 1:
                            OpenPatientFolder(Id);
                            break;
                        case 2:
                            ClearBirthDate(Id);
                            break;
                        case 3:
                            string oldProv;
                            string newProv;
                            Console.WriteLine("Please enter the old Provider ID:");
                            oldProv = Console.ReadLine();
                            Console.WriteLine("Please enter the new Provider ID:");
                            newProv = Console.ReadLine();
                            ChangeProvider(oldProv, newProv, Id);
                            break;
                        case 4:
                            ForceRebuild();
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine("All patients selected. \n" +
                        "What would you like to do? \n" +
                        " 1: Open Patient Folder \n" +
                        " 2: Clear Birthday \n" +
                        " 3: Change Provider \n" +
                        " 4: Force Rebuild");
                    int result2 = Int32.Parse(Console.ReadLine());

                    switch (result2)
                    {
                        case 1:
                            Console.WriteLine("Please enter an ID: ");
                            string id = Console.ReadLine();
                            OpenPatientFolder(id);
                            break;
                        case 2:
                            ClearBirthDate();
                            break;
                        case 3:
                            string oldProv;
                            string newProv;
                            Console.WriteLine("Please enter the old Provider ID:");
                            oldProv = Console.ReadLine();
                            Console.WriteLine("Please enter the new Provider ID:");
                            newProv = Console.ReadLine();
                            ChangeProvider(oldProv, newProv);
                            break;
                        case 4:
                            ForceRebuild();
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine("Please enter the new location for the backup;");
                    string destination = Console.ReadLine();
                    Backup(DataPath, destination);
                    break;
                default:
                    break;
            }
        }

        public void DeleteEmptyPatients()
        {
            DirectoryInfo di = new DirectoryInfo(DataPath);
            //Console.WriteLine(di.FullName);

            DirectoryInfo[] dirs = di.GetDirectories("*", SearchOption.AllDirectories);

            List<FileInfo> filist = new List<FileInfo>();
            List<string> emptyDirs = new List<string>();

            foreach (DirectoryInfo d in dirs)
            {

                FileInfo[] fi = d.GetFiles();
                for (int i = 0; i < fi.Length; i++)
                {
                    if (fi[i].Extension == ".inf")
                    {
                        filist.Add(fi[i]);
                        emptyDirs.Add(fi[i].DirectoryName);
                        //Console.WriteLine(fi[i].FullName);
                    }

                }
            }
            foreach (FileInfo f in filist)
            {
                string currentDir = f.Directory.FullName;
                string[] files = Directory.GetFiles(currentDir);

                foreach (string s in files)
                {
                    if (s.EndsWith(".dex") || s.EndsWith(".DEX"))
                    {
                        emptyDirs.Remove(currentDir);
                    }
                }

            }

            foreach (string s in emptyDirs)
            {
                string[] files = Directory.GetFiles(s);
                foreach (string f in files)
                {

                    //Console.WriteLine(f);
                    File.Delete(f);
                }
            }
            ForceRebuild();
        }
        public void FillPatientList(string id)
        {
            ImageFiles.Clear();
            string patientPath = GetPatientPath(id);
            foreach (var file in Directory.GetFiles(patientPath))
            {
                if (file.EndsWith(".dex") || file.EndsWith(".DEX"))
                {
                    ImageFiles.Add(file);
                }
            }
        }
    }


    /**
     * Class for DEXIS 10 Specific methods
     */
    public class DEXIS10Logic
    {

        //string for the entire sql connection generated in the EstablishConnection method
        private string _connectionString { get; set; }

        public string _id { get; set; }
        //list of images that are generated when going through certain queries
        public List<string> _files { get; set; }

        /**
         * Default constructor that generates the Connection string.
         */
        public DEXIS10Logic()
        {
            _files = new List<string>();
            _connectionString = "Server=.\\DEXIS_DATA;Database=DEXIS;Integrated Security=True;";
        }

        /**
         * Read the file
         * search through the file for anything starting with DataPath
         * grabs DataPath=C:\path\to\data
         * removes DataPath=
         * returns C:\path\to\data
         */
        public string GetDataPath()
        {
            string result = "";

            string[] lines = File.ReadAllLines(@"C:\ProgramData\Danaher_Dental\DEXIS.ini");
            foreach (string s in lines)
            {
                if (s.StartsWith("DataPath"))
                {
                    result = s;
                }
            }
            result = result.Replace("DataPath=", "");

            return result;
        }

        /**
         * Creates a SQL backup of the DEXIS database.
         * stores it in the destination
         */
        public void Backup(string dest)
        {
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }
            if (File.Exists(dest + "\\dexisBackup.bak"))
            {
                File.Delete(dest + "\\dexisBackup.bak");
            }
            string query = "BACKUP DATABASE DEXIS TO DISK ='" + dest + "\\dexisBackup.bak'";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            reader.Close();

            new DEXIS9Logic().Backup(GetDataPath(), dest);

        }

        public void GetSmallFiles()
        {
            List<string> tempList = new List<string>(_files);

            foreach (string name in tempList)
            {
                FileInfo fi = new FileInfo(name);
                if (fi.Length > 10000)
                {
                    _files.Remove(name);
                }

            }
        }

        public void getMissingFiles()
        {
            List<string> tempList = new List<string>(_files);
            foreach (string s in tempList)
            {
                if (File.Exists(s))
                {
                    _files.Remove(s);
                }
            }
        }

        public string GetPatientName(string id)
        {
            string query = "SELECT GivenName, FamilyName FROM PersonName WHERE PersonID = (SELECT PersonID FROM Patient WHERE ExternalID='" + id + "')";
            string firstName = "";
            string lastName = "";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    firstName = reader["GivenName"].ToString();
                    lastName = reader["FamilyName"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Close();
            }
            return firstName + " " + lastName;
        }

        public void AllImages()
        {
            _files.Clear();
            List<string> deletedFiles = new List<string>();
            string fullListString = "SELECT Filename FROM ActivityLog WHERE (Event='XRay' or Event='FileImport');";
            string deletedString = "SELECT Filename FROM ActivityLog WHERE Event='DeleteXRay';";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(fullListString, connection);
            SqlCommand commandDel = new SqlCommand(deletedString, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (File.Exists(reader["Filename"].ToString()))
                    {
                        _files.Add(reader["Filename"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Close();
            }
            SqlDataReader delReader = commandDel.ExecuteReader();

            try
            {
                while (delReader.Read())
                {
                    deletedFiles.Add(delReader["Filename"].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                delReader.Close();
            }
            foreach (string s in deletedFiles)
            {
                if (_files.Contains(s))
                {
                    _files.Remove(s);
                }
            }
        }

        /**
         * Runs a sql query and gets all images by a single patient ID.
         * stores the found files in the global _files variable.
         * Clears the list before populating as to avoid duplicate records
         */
        public void ImagesByID(string id)
        {
            _files.Clear();
            List<string> deletedFiles = new List<string>();
            string fullListString = "SELECT Filename FROM ActivityLog WHERE ItemID='" + id + "' and (Event='XRay' or Event='FileImport' or Event='Acquire');";
            string deletedString = "SELECT Filename FROM ActivityLog WHERE ItemID='" + id + "' and Event='DeleteXRay';";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(fullListString, connection);
            SqlCommand commandDel = new SqlCommand(deletedString, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (File.Exists(reader["Filename"].ToString()))
                    {
                        _files.Add(reader["Filename"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Close();
            }
            SqlDataReader delReader = commandDel.ExecuteReader();

            try
            {
                while (delReader.Read())
                {
                    deletedFiles.Add(delReader["Filename"].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                delReader.Close();
            }
            foreach (string s in deletedFiles)
            {
                if (_files.Contains(s))
                {
                    _files.Remove(s);
                }
            }
        }

        /**
         * Runs a SQL query to get all images in a date range, regardless of patient
         */
        public void ImagesByDate(string startDate, string endDate)
        {
            _files.Clear();
            List<string> deletedFiles = new List<string>();
            string fullListString = "SELECT Filename FROM ActivityLog WHERE EventDateTime BETWEEN CONVERT(datetime, '" + startDate + "') and CONVERT(datetime, '" + endDate + "') and (Event='XRay' or Event='FileImport');";
            string deletedString = "SELECT Filename FROM ActivityLog WHERE EventDateTime BETWEEN CONVERT(datetime, '" + startDate + "') and CONVERT(datetime, '" + endDate + "') and Event = 'DeleteXRay';";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(fullListString, connection);
            SqlCommand commandDel = new SqlCommand(deletedString, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (File.Exists(reader["Filename"].ToString()))
                    {
                        _files.Add(reader["Filename"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Close();
            }
            SqlDataReader delReader = commandDel.ExecuteReader();

            try
            {
                while (delReader.Read())
                {
                    deletedFiles.Add(delReader["Filename"].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                delReader.Close();
            }
            foreach (string s in deletedFiles)
            {
                if (_files.Contains(s))
                {
                    _files.Remove(s);
                }
            }
        }

        /**
         * Runs a sql query to get all images in a date range for a specific patient
         */
        public void ImagesByDate(string startDate, string endDate, string id)
        {
            _files.Clear();
            System.Collections.Generic.List<string> deletedFiles = new List<string>();
            string fullListString = "SELECT Filename FROM ActivityLog WHERE EventDateTime BETWEEN CONVERT(datetime, '" + startDate + "') and CONVERT(datetime, '" + endDate + "') and ItemID='" + id + "' and (Event='XRay' or Event='FileImport');";
            string deletedString = "SELECT Filename FROM ActivityLog WHERE EventDateTime BETWEEN CONVERT(datetime, '" + startDate + "') and CONVERT(datetime, '" + endDate + "') and ItemID= '" + id + "' and Event='DeleteXRay';";
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(fullListString, connection);
            SqlCommand commandDel = new SqlCommand(deletedString, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (File.Exists(reader["Filename"].ToString()))
                    {
                        _files.Add(reader["Filename"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                reader.Close();
            }
            SqlDataReader delReader = commandDel.ExecuteReader();

            try
            {
                while (delReader.Read())
                {
                    deletedFiles.Add(delReader["Filename"].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                delReader.Close();
            }
            foreach (string s in deletedFiles)
            {
                if (_files.Contains(s))
                {
                    _files.Remove(s);
                }
            }
        }

        public void OpenFile(string filePath)
        {
            Process.Start(filePath);
        }

        public void OpenContainingFolder(string filePath)
        {
            Process.Start("explorer.exe", "/select," + filePath);
        }
    }
}
