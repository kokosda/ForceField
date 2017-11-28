using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.WorkflowDomain.Interfaces;
using System.IO;
using System.Diagnostics;

namespace ForceField.WorkflowInfrastructure.Presenters
{
    public class ImageStickerPresenter
    {
        public ImageStickerPresenter(IImageStickerView view, IWorkflowServiceView form)
        {
            this.view = view;
            this.form = form;
        }

        public void OnFrameSourceButtonClicked()
        {
            form.OpenFrameSourceDialog();

            if (form.ImageStickIsFrameSourceFileSelected)
            {
                form.ImageStickFrameFileSelectedStatus = NormalizeFrameFileName(form.ImageStickFrameSourceFilePath);

                view.SelectedFilePreview = form.ImageStickFrameSourceFilePath;

                form.Log("Source file selected: {0}", form.ImageStickFrameSourceFilePath);
            }
        }

        public void OnImageStickerBackgroundColorButtonClicked()
        {
            form.OpenColorPickerDialog();
        }

        public void OnImageStickButtonClicked()
        {
            if (!string.IsNullOrEmpty(form.ImageStickFrameSourceFilePath) && Path.HasExtension(form.ImageStickFrameSourceFilePath))
            {
                try
                {
                    CreateProcessEssentialStaff();
                    SourceToFrameSequence();
                    SequenctToAnimationBillboard();

                    view.GeneratedSequence = ResultFileName;

                    form.Log(string.Format("Results folder: {0}", tempDirectoryName));
                }
                catch (Exception ex)
                {
                    form.Log(string.Format("Error while composing sequence. {0} {1} {2}", Environment.NewLine, ex.Message, ex.StackTrace));
                }
            }
        }

        public void OnSaveStickResult(string destinationPath)
        {
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }

            using(var fileStream = File.OpenRead(ResultFileName))
            {
                var bytes = new byte[fileStream.Length];

                fileStream.Read(bytes, 0, (int)fileStream.Length);

                File.WriteAllBytes(destinationPath, bytes);
            }
        }

        public void OnEmptyTempDirectories()
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(Environment.CurrentDirectory))
                {
                    Directory.Delete(dir, true);
                }
            }
            catch
            {
            }
        }

        #region private

        private IImageStickerView view;

        private IWorkflowServiceView form;

        private string tempDirectoryName;

        private string shortFileName;

        private string NormalizeFrameFileName(string filePath)
        {
            var maxAllowedLength = 65;
            var subtractedLength = 10;

            if (Path.GetFileNameWithoutExtension(filePath).Length > maxAllowedLength)
            {
                filePath = string.Concat(Path.GetFileNameWithoutExtension(filePath).Substring(0, maxAllowedLength - subtractedLength), "...", Path.GetExtension(filePath));
            }

            return Path.GetFileName(filePath);
        }

        private void CreateProcessEssentialStaff()
        {
            shortFileName = Path.GetFileName(form.ImageStickFrameSourceFilePath);
            tempDirectoryName = string.Format("{0}_{1}", shortFileName, new Random().Next());

            if (Directory.Exists(tempDirectoryName))
            {
                Directory.Delete(tempDirectoryName, true);
            }

            Directory.CreateDirectory(tempDirectoryName);

            form.Log("Directory created: {0}", tempDirectoryName);
        }

        private void SourceToFrameSequence()
        {
            var logComment = string.Format("Successfully converted from source to {0}-sequence. Thanks to ffmpeg utility", form.ImageStickExtension);

            LaunchExternalProcess("ffmpeg.exe", FFMPEGArguments, logComment);
        }

        private void SequenctToAnimationBillboard()
        {
            var logComment = string.Format("Aggregated to animation billboard of {0} items. Thanks to ImageMagick montage utility", Directory.GetFiles(tempDirectoryName).Count());

            LaunchExternalProcess("montage.exe", MontageArguments, logComment);
        }

        private string FFMPEGArguments
        {
            get
            {
                var argBody = new StringBuilder();
                argBody.Append(string.Format("-i {0} ", form.ImageStickFrameSourceFilePath));

                if (!form.ImageStickFrameSize.Contains(null))
                {
                    argBody.Append(string.Format("-s {0}x{1} ", form.ImageStickFrameSize[0], form.ImageStickFrameSize[1]));
                }

                argBody.Append(string.Format("{0}/{1}_%03d{2} ", 
                                                tempDirectoryName,
                                                Path.GetFileNameWithoutExtension(shortFileName), 
                                                form.ImageStickExtension));

                return argBody.ToString();
            }
        }

        private string MontageArguments
        {
            get
            {
                var argBody = new StringBuilder();
                argBody.Append(string.Format("-quality {0} ", form.ImageStickQuality));
                
                argBody.Append(string.Format("-background {0} ", form.ImageStickBackgroundColor.IsNamedColor 
                                                                                    ? form.ImageStickBackgroundColor.Name
                                                                                    : string.Format("\"rgba({0},{1},{2},{3})\"", form.ImageStickBackgroundColor.R,
                                                                                                                                 form.ImageStickBackgroundColor.G,
                                                                                                                                 form.ImageStickBackgroundColor.B,
                                                                                                                                 form.ImageStickBackgroundColor.A)));
                
                argBody.Append("-geometry \"1x1+0+0<\" ");

                argBody.Append(SequenceFileNames);
                argBody.Append(ResultFileName);

                return argBody.ToString();
            }
        }

        private string SequenceFileNames
        {
            get
            {
                var fileNames = Directory.GetFiles(tempDirectoryName)
                                            .OrderBy(f => f)
                                            .ToList();

                var suitableFileArgForm = string.Empty;
                fileNames.ForEach(f => suitableFileArgForm = string.Concat(suitableFileArgForm, string.Format("{0}/{1} ", tempDirectoryName, Path.GetFileName(f))));

                return suitableFileArgForm;
            }
        }

        private string ResultFileName
        {
            get
            {
                return string.Format("{0}/result{1}", tempDirectoryName, form.ImageStickExtension);
            }
        }

        private void LaunchExternalProcess(string fileName, string arguments, string logComment = "", bool waitForExit = false, bool useShellExecute = false, bool redirectStandardOuput = true)
        {
            Process p = new Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = arguments;
            p.StartInfo.UseShellExecute = useShellExecute;
            p.StartInfo.RedirectStandardOutput = redirectStandardOuput;
            p.StartInfo.RedirectStandardError = true;

            p.Start();

            p.WaitForExit();

            var errorMsg = p.StandardError.ReadToEnd();
            p.WaitForExit();

            form.Log(errorMsg);

            var obj = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            form.Log(obj);

            form.Log(logComment);
        }

        #endregion
    }
}
