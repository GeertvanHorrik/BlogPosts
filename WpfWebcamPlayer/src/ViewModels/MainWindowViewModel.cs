// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="CatenaLogic">
//   Copyright (c) 2012 - 2013 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WebcamPlayer.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Media.Imaging;
    using Catel.Data;
    using Catel.MVVM;
    using CatenaLogic.Windows.Presentation.WebcamPlayer;

    public class MainWindowViewModel : ViewModelBase
    {
        #region Constructors

        public MainWindowViewModel()
        {
            CaptureImage = new Command(OnCaptureImageExecute, OnCaptureImageCanExecute);
            ClearImages = new Command(OnClearImagesExecute, OnClearImagesCanExecute);
            ClearImage = new Command<BitmapSource>(OnClearImageExecute, OnClearImageCanExecute);

            // Create default device
            SelectedWebcamMonikerString = (CapDevice.DeviceMonikers.Length > 0) ? CapDevice.DeviceMonikers[0].MonikerString : string.Empty;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the selected webcam monikerstring.
        /// </summary>
        public string SelectedWebcamMonikerString
        {
            get { return GetValue<string>(SelectedWebcamMonikerStringProperty); }
            set { SetValue(SelectedWebcamMonikerStringProperty, value); }
        }

        /// <summary>
        /// Register the SelectedWebcamMonikerString property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SelectedWebcamMonikerStringProperty = RegisterProperty("SelectedWebcamMonikerString", typeof(string), null);

        /// <summary>
        /// Gets or sets the selected webcam.
        /// </summary>
        public CapDevice SelectedWebcam
        {
            get { return GetValue<CapDevice>(SelectedWebcamProperty); }
            set { SetValue(SelectedWebcamProperty, value); }
        }

        /// <summary>
        /// Register the SelectedWebcam property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SelectedWebcamProperty = RegisterProperty("SelectedWebcam", typeof(CapDevice), null);

        /// <summary>
        /// Gets or sets the selected webcam.
        /// </summary>
        public CapPlayer SelectedWebcamPlayer
        {
            get { return GetValue<CapPlayer>(SelectedWebcamPlayerProperty); }
            set { SetValue(SelectedWebcamPlayerProperty, value); }
        }

        /// <summary>
        /// Register the SelectedWebcamPlayer property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SelectedWebcamPlayerProperty = RegisterProperty("SelectedWebcamPlayer", typeof(CapPlayer), null);

        /// <summary>
        /// Gets or sets the WebcamRotation.
        /// </summary>
        public double WebcamRotation
        {
            get { return GetValue<double>(WebcamRotationProperty); }
            set { SetValue(WebcamRotationProperty, value); }
        }

        /// <summary>
        /// Register the WebcamRotation property so it is known in the class.
        /// </summary>
        public static readonly PropertyData WebcamRotationProperty = RegisterProperty("WebcamRotation", typeof(double), 180d);

        /// <summary>
        /// Gets or sets the selected images.
        /// </summary>
        public ObservableCollection<BitmapSource> SelectedImages
        {
            get { return GetValue<ObservableCollection<BitmapSource>>(SelectedImagesProperty); }
            set { SetValue(SelectedImagesProperty, value); }
        }

        /// <summary>
        /// Register the SelectedImages property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SelectedImagesProperty = RegisterProperty("SelectedImages", typeof(ObservableCollection<BitmapSource>), 
            () => new ObservableCollection<BitmapSource>());
        #endregion

        #region Commands

        #region Properties

        /// <summary>
        /// Gets the CaptureImage command.
        /// </summary>
        public Command CaptureImage { get; private set; }

        /// <summary>
        /// Gets the ClearImage command.
        /// </summary>
        public Command<BitmapSource> ClearImage { get; private set; }

        /// <summary>
        /// Gets the ClearImages command.
        /// </summary>
        public Command ClearImages { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to check whether the CaptureImage command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCaptureImageCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the CaptureImage command is executed.
        /// </summary>
        private void OnCaptureImageExecute()
        {
            var bitmap = webcamPlayer.CurrentBitmap;
            if (bitmap != null)
            {
                SelectedImages.Add(bitmap);
            }
        }

        /// <summary>
        /// Method to check whether the ClearImage command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnClearImageCanExecute(BitmapSource image)
        {
            return (image != null);
        }

        /// <summary>
        /// Method to invoke when the ClearImage command is executed.
        /// </summary>
        private void OnClearImageExecute(BitmapSource image)
        {
            SelectedImages.Remove(image);
        }

        /// <summary>
        /// Method to check whether the ClearImages command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnClearImagesCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ClearImages command is executed.
        /// </summary>
        private void OnClearImagesExecute()
        {
            // TODO: Handle command logic here
        }

        #endregion

        #endregion

        #region Methods

        private void OnSelectedWebcamChanged()
        {
            if (SelectedWebcam == null)
            {
                SelectedWebcam = new CapDevice("");
            }

            SelectedWebcamPlayer.MonikerString = newMonikerString;
        }
        #endregion
    }
}