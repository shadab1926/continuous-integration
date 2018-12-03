using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Silent_Installation
{
    public partial class Form1 : Form
    {
        private FileSystemWatcher m_watcher;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Watch();
        }
        void Watch()
        {
            try
            {
                Form1 obj = new Form1();
                obj.Hide();
                m_watcher = new FileSystemWatcher
                {
                    Filter = "*.*",
                    Path = @"\\D:\Test\continuous_integration", //path where new .exe arrives
                    IncludeSubdirectories = true,
                    NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName
                };
                m_watcher.Created += new FileSystemEventHandler(OnChanged);
                m_watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                WriteErrorLog(strErrorText: "Error in Form1_Load: " + ex.Message);
                // Let the user know what went wrong. 
            }
        }

        public void OnChanged(object sener, FileSystemEventArgs e)
        {
            SendMail(e.Name);       //sending mail
            MessageBox.Show("New Build With Folder Name  (" + e.Name + ")  Has Arrived");        //promt a message box
            RunScript(LoadScript(filename: @"D:\Test\FinalScript.ps1"));      //path of the Powershell script
        }

        private static void SendMail(String buildNo)
        {
            SmtpClient cv = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true, 
                Credentials = new NetworkCredential(userName: "sender mail id", password: "Sender Password") //mail id and password to mail to the team intimating the exe has been arrived 
            };
            try
            {
                cv.Send(from: "sender mail id", recipients: "reciever mail id", subject: "NEW BUILD NOTIFICATION", body: "HI,\n\n\n\n\nNew Build (" + buildNo + ") has been Generated and installaion is in process.\n\n\n\n\n\n Regards,\n Your Name.");
                cv.Send(from: "sender mail id", recipients: "reciever mail id", subject: "NEW BUILD NOTIFICATION", body: "HI,\n\n\n\n\nNew Build (" + buildNo + ") has been Generated and installaion is in process.\n\n\n\n\n\n Regards,\n Your Name.");
                cv.Send(from: "sender mail id", recipients: "reciever mail id", subject: "NEW BUILD NOTIFICATION", body: "HI,\n\n\n\n\nNew Build (" +buildNo+ ") has been Generated and installaion is in process.\n\n\n\n\n\n Regards,\n Your Name.");
                // add any number of receipents u need
            }
            catch (Exception e)
            {
                WriteErrorLog(strErrorText: "Error in SendMail: " + e.Message);
                // Let the user know what went wrong. 
                Console.WriteLine("Email cant send");
                Console.WriteLine(e.Message);

            }

        }



        public static void WriteErrorLog(string strErrorText)
        {
            try
            {   
                string strFileName = "errorLog.txt";
                string strPath = Application.StartupPath;
                File.AppendAllText(strPath + "\\" + strFileName, strErrorText + " - " + DateTime.Now.ToString() + "\r\n");
            }
            catch (Exception ex)
            {
                WriteErrorLog(strErrorText: "Error in WriteErrorLog: " + ex.Message);
                // Let the user know what went wrong. 
            }
        }


        private string RunScript(string scriptText)
        {
            try
            {


                // create Powershell runspace 
                Runspace runspace = RunspaceFactory.CreateRunspace();

                // open it 
                runspace.Open();

                // create a pipeline and feed it the script text 
                Pipeline pipeline = runspace.CreatePipeline();
                pipeline.Commands.AddScript(scriptText);

                pipeline.Commands.Add("Out-String");

                // execute the script 
                Collection<PSObject> results = pipeline.Invoke();

                // close the runspace 
                runspace.Close();

                StringBuilder stringBuilder = new StringBuilder();
                foreach (PSObject obj in results)
                {
                    stringBuilder.AppendLine(obj.ToString() + "\r\n");
                }

                return stringBuilder.ToString();
            }
            catch (Exception Run)
            {
                WriteErrorLog(strErrorText: "Error in RunScript: " + Run.Message);
                // Let the user know what went wrong. 
                string RunText = "The file could not be read:";
                RunText += Run.Message + "\n";
                return RunText;
            }

            
        }


        private string LoadScript(string filename)
        {
            try
            {
                // Create an instance of StreamReader to read from our file. 
                using (StreamReader sr = new StreamReader(filename))
                {

                    // use a string builder to get all our lines from the file 
                    StringBuilder fileContents = new StringBuilder();

                    // string to hold the current line 
                    string curLine;
                

                    while ((curLine = sr.ReadLine()) != null)
                    {
                        fileContents.Append(curLine + "\n");
                    }

                    // call RunScript and pass in our file contents 
                    // converted to a string 
                    return fileContents.ToString();
                }
            }
            catch (Exception e)
            {
                WriteErrorLog(strErrorText: "Error in WriteErrorLog: " + e.Message);
                // Let the user know what went wrong. 
                string Text = "The file could not be read:";
                Text += e.Message + "\n";
                return Text;

            }

        }
        
    }

}




