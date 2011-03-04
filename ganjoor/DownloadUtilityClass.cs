using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;

//Based On: http://www.devtoolshed.com/content/c-download-file-progress-bar
namespace ganjoor
{
    class DownloadUtilityClass
    {

        public static string DownloadFileIgnoreFail(string sUrlToReadFileFrom, string TargetDir, BackgroundWorker backgroundWorker)
        {
            try
            {
                return DownloadFile(sUrlToReadFileFrom, TargetDir, backgroundWorker);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string DownloadFile(string sUrlToReadFileFrom, string TargetDir, BackgroundWorker backgroundWorker)
        {
            // first, we need to get the exact size (in bytes) of the file we are downloading

            Uri url = new Uri(sUrlToReadFileFrom);

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            response.Close();

            string sFilePathToWriteFileTo = Path.Combine(TargetDir, Path.GetFileName(response.ResponseUri.LocalPath));

            // gets the size of the file in bytes

            Int64 iSize = response.ContentLength;



            // keeps track of the total bytes downloaded so we can update the progress bar

            Int64 iRunningByteTotal = 0;



            // use the webclient object to download the file

            using (System.Net.WebClient client = new System.Net.WebClient())
            {

                // open the file at the remote URL for reading

                using (System.IO.Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom)))
                {

                    // using the FileStream object, we can write the downloaded bytes to the file system

                    using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                    {

                        // loop the stream and get the file into the byte buffer

                        int iByteSize = 0;

                        byte[] byteBuffer = new byte[iSize];

                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {

                            // write the bytes to the file system at the file path specified

                            streamLocal.Write(byteBuffer, 0, iByteSize);

                            iRunningByteTotal += iByteSize;



                            // calculate the progress out of a base "100"

                            double dIndex = (double)(iRunningByteTotal);

                            double dTotal = (double)byteBuffer.Length;


                            double dProgressPercentage = (dIndex / dTotal);

                            int iProgressPercentage = (int)(dProgressPercentage * 100);



                            // update the progress bar

                            backgroundWorker.ReportProgress(iProgressPercentage);

                        }



                        // clean up the file stream

                        streamLocal.Close();

                    }



                    // close the connection to the remote server

                    streamRemote.Close();

                }

            }
            return sFilePathToWriteFileTo;
        }
    }
}
