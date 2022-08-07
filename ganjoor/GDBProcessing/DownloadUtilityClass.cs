using System;
using System.ComponentModel;
using System.IO;
using System.Net;

//Based On: http://www.devtoolshed.com/content/c-download-file-progress-bar
namespace ganjoor
{
    class DownloadUtilityClass
    {

        public static string DownloadFileIgnoreFail(string sUrlToReadFileFrom, string TargetDir, BackgroundWorker backgroundWorker, out string expString)
        {
            expString = "";
            try
            {
                return DownloadFile(sUrlToReadFileFrom, TargetDir, backgroundWorker);
            }
            catch (Exception exp)
            {
                expString = exp.ToString();
                return string.Empty;
            }
        }

        public static string DownloadFile(string sUrlToReadFileFrom, string TargetDir, BackgroundWorker backgroundWorker)
        {
            // first, we need to get the exact size (in bytes) of the file we are downloading

            Uri url = new Uri(sUrlToReadFileFrom);

            WebRequest req = WebRequest.Create(url);
            if (req is HttpWebRequest request)
            {
                HttpWebResponse response;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException)
                {
                    sUrlToReadFileFrom = sUrlToReadFileFrom.Replace("https", "http"); // this is a workaround for https://i.ganjoor.net recent problems
                    url = new Uri(sUrlToReadFileFrom);
                    req = WebRequest.Create(url);
                    request = (HttpWebRequest)req;
                    response = (HttpWebResponse)request.GetResponse();
                }

                response.Close();

                string sFilePathToWriteFileTo = Path.Combine(TargetDir, Path.GetFileName(response.ResponseUri.LocalPath));

                // gets the size of the file in bytes

                Int64 iSize = response.ContentLength;



                // keeps track of the total bytes downloaded so we can update the progress bar

                Int64 iRunningByteTotal = 0;



                // use the webclient object to download the file

                using WebClient client = new WebClient();
                // open the file at the remote URL for reading

                using Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom));
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

                        double dIndex = iRunningByteTotal;

                        double dTotal = byteBuffer.Length;


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

                return sFilePathToWriteFileTo;
            }

            if (req is FileWebRequest)
            {
                string sFilePathToWriteFileTo = Path.Combine(TargetDir, Path.GetFileName(sUrlToReadFileFrom));
                File.Copy(sUrlToReadFileFrom, sFilePathToWriteFileTo, true);
                return sFilePathToWriteFileTo;
            }

            return string.Empty;

        }
    }
}
